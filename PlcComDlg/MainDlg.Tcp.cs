using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace PlcComDlg
{
    /// <summary>
    /// 3000.SMA-PLC 통신 프로그램
    /// </summary>
    public partial class MainDlg : Form
    {
        #region 31000.Tcp.Worker
        /// <summary>
        /// TCP 제어 워커
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_TcpDoWork(object sender, DoWorkEventArgs e)
        {
            TcpOpModes tcpMode = TcpOpModes.Connecting;
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int errCnt = 0;

            while (tcpMode != TcpOpModes.Non)
            {
                // 프로그램 동작 종료 체크
                if (_workerTcp.CancellationPending)
                {
                    tcpMode = TcpOpModes.Non;
                    break;
                }
                _tcpModeRef = tcpMode;

                // 31100: TCP 연결 시도
                if (tcpMode == TcpOpModes.Connecting)
                {
                    if (!sock.Connected)
                    {
                        try
                        {
                            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_cs.TcpIpAdd), _cs.TcpPort);
                            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                            var waiter = sock.BeginConnect(ep, null, null);

                            // WaitOne()의 첫 번째 인수는 대기할 시간을 ms(밀리초) 단위로 넣는다. (1초 = 1000ms)
                            if (waiter.AsyncWaitHandle.WaitOne(_cs.TcpConnWaitTimeMilSec, true))
                            {
                                sock.EndConnect(waiter);
                            }
                        }
                        catch
                        {
                            sock.Close();
                        }
                    }

                    // 연결 성공 시 모드 전환
                    if (sock.Connected)
                    {
                        tcpMode = TcpOpModes.Connected;
                        InsertLog("Connected", LogMsg.Sources.TCP);
                    }
                    else
                    {
                        Thread.Sleep(_cs.TcpConnWaitTimeMilSec);
                    }
                }
                // 31200: TCP 모니터
                else if (tcpMode == TcpOpModes.Connected)
                {
                    if (!IsSocketConnected(sock))
                    {
                        sock.Close();
                        tcpMode = TcpOpModes.Connecting;
                        InsertLog("Sever is disconnected", LogMsg.Sources.TCP);

                        if (_cs.TcpAutoCloseIfDisconnected)
                        {
                            _closeApp = true;
                        }
                        continue;
                    }

                    if (_plcModeRef == PlcOpModes.Measruement && _plcToTcpMeasReq == true)
                    {
                        _plcToTcpMeasReq = false;

                        if (!GetProductInforMsg(_plcData, _cs, out string[] tcpMsgs, out string errMsg))
                        {
                            InsertLog("Fail to get product information from PLC", LogMsg.Sources.TCP, 31200, errMsg);
                            sock.Close();
                            tcpMode = TcpOpModes.Connecting;
                        }

                        for (int i = 0; i < tcpMsgs.Length; i++)
                        {
                            if (!SendTcpMessage(sock, tcpMsgs[i], ref _tcpData))
                            {
                                InsertLog("Fail to send product information to PC", LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                                sock.Close();
                                tcpMode = TcpOpModes.Connecting;
                            }
                            InsertLog(tcpMsgs[i], LogMsg.Sources.TCP);
                        }

                        Thread.Sleep(500); // 데이터 전송 속도 조절

                        if (!SendTcpMessage(sock, $"FLUX {_plcData.PlcModelNumber}", ref _tcpData))
                        {
                            InsertLog("Fail to start measurement", LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                            sock.Close();
                            tcpMode = TcpOpModes.Connecting;
                        }
                        InsertLog(_tcpData.LastComMsg, LogMsg.Sources.TCP);

                        Thread.Sleep(_cs.TcpMeasStartDelay); // 데이터 전송 속도 조절

                        errCnt = 0;
                        tcpMode = TcpOpModes.Measurement;
                        _tcpData.MeasStartTime = DateTime.Now;
                        
                    }
                    // 31300: IDLE 동작
                    else
                    {
                        // 제어 메시지 처리
                        if (_tcpMsgQue.TryDequeue(out TcpData.ComMsg msg))
                        {
                            if (!SendTcpMessage(sock, msg.Message, ref _tcpData))
                            {
                                InsertLog("Fail to send a control message to PC", LogMsg.Sources.TCP, 31300, _tcpData.LastErrMsg);
                                sock.Close();
                                tcpMode = TcpOpModes.Connecting;
                            }
                        }

                        // 3213: 하트비트 타임 체크
                        TimeSpan ts = DateTime.Now - _tcpData.LastCommTime;
                        if (ts.TotalMinutes > _cs.TcpIdleTimeMinLimit)
                        {
                            TcpData.ComMsg idleMsg = new TcpData.ComMsg();
                            idleMsg.Message = $"*CLS";
                            _tcpMsgQue.Enqueue(idleMsg);
                        }

                        Thread.Sleep(_cs.TcpMonitorTimeMilSec);
                    }
                }
                // 31300: SMA 측정 완료 대기
                else if (tcpMode == TcpOpModes.Measurement)
                {
                    // SMA 측정 완료 체크
                    if (SendTcpMessage(sock, $"READY?", ref _tcpData))
                    {
                        if (_tcpData.ReadBuf[0] == '0')
                        {
                            string errMsg;
                            ReadLastIdFromDb(_cs, out long newDbId, out errMsg);

                            // 아날라이저 측정이 정상 종료
                            if (_lastDbId < newDbId)
                            {
                                tcpMode = TcpOpModes.Connected;
                                _tcpToPlcMeasFin = true;
                                _tcpMeasErrFlag = false;
                                _tcpMeasErrMsg = "";
                                InsertLog($"Flux finished", LogMsg.Sources.TCP);
                            }
                            // 아날라이저 측정이 비정상 종료 - 통신 다시 시도
                            else
                            {
                                if (!GetProductInforMsg(_plcData, _cs, out string[] tcpMsgs, out errMsg))
                                {
                                    errCnt++;
                                }
                                for (int i = 0; i < tcpMsgs.Length; i++)
                                {
                                    if (!SendTcpMessage(sock, tcpMsgs[i], ref _tcpData))
                                    {
                                        errCnt++;
                                    }
                                    InsertLog(tcpMsgs[i], LogMsg.Sources.TCP);
                                }
                                Thread.Sleep(500); // 데이터 전송 속도 조절
                                if (!SendTcpMessage(sock, $"FLUX {_plcData.PlcModelNumber}", ref _tcpData))
                                {
                                    errCnt++;
                                }
                                InsertLog(_tcpData.LastComMsg, LogMsg.Sources.TCP);
                                Thread.Sleep(_cs.TcpMeasStartDelay); // 데이터 전송 속도 조절
                            }
                        }
                    }
                    // 에러 발생 횟수 체크
                    else
                    {
                        errCnt++;
                        if (errCnt > _cs.TcpMaxErrorCount)
                        {
                            _tcpMeasErrFlag = true;
                            _tcpToPlcMeasFin = true;
                            _tcpMeasErrMsg = "COM ERR";
                            sock.Close();
                            tcpMode = TcpOpModes.Connecting;
                            InsertLog("Communication error", LogMsg.Sources.TCP, 31300, "The number of com. error is greater than 10");
                        }
                    }

                    // 타임아웃 체크
                    TimeSpan ts = DateTime.Now - _tcpData.MeasStartTime;
                    if (ts.TotalSeconds > _cs.TcpMaxMeasTimeSec)
                    {
                        tcpMode = TcpOpModes.Connected;
                        _tcpMeasErrFlag = true;
                        _tcpToPlcMeasFin = true;
                        _tcpMeasErrMsg = "Time out";
                        SendTcpMessage(sock, $"ABORT", ref _tcpData);
                        InsertLog("Timeout", LogMsg.Sources.TCP, 31300, $"{ts.TotalSeconds:f3}/{_cs.TcpMaxMeasTimeSec:f3} sec");
                    }

                    Thread.Sleep(_cs.TcpMeasFinCheckTimeMilSec);
                }

                _workerTcp.ReportProgress((int)tcpMode);
            }

            while (_tcpMsgQue.Count > 0)
            {
                _tcpMsgQue.TryDequeue(out TcpData.ComMsg dummyMsg);
            }
            sock.Close();
        }
        #endregion

        #region 32000.Tcp.Worker.이벤트
        /// <summary>
        /// 워커 시작
        /// </summary>
        private void StartTcpWorker()
        {
            // Worker 설정
            _workerTcp = new BackgroundWorker();
            _workerTcp.WorkerReportsProgress = true;
            _workerTcp.WorkerSupportsCancellation = true;
            _workerTcp.DoWork += new DoWorkEventHandler(Worker_TcpDoWork);
            _workerTcp.ProgressChanged += new ProgressChangedEventHandler(Worker_TcpProgressChanged);
            _workerTcp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_TcpRunWorkerCompleted);

            // GUI 업데이트
            BtnTcpStart.Text = "TCP Stop";

            LblTcpServerIp.Text = $"{_cs.TcpIpAdd} ({_cs.TcpPort})";
            LblTcpServer.BackColor = Color.OrangeRed;
            LblTcpMeasTimeVal.Text = $"{0:f1}/{_cs.TcpMaxMeasTimeSec:f1} (sec)";

            _workerTcp.RunWorkerAsync();
        }

        /// <summary>
        /// 진행상황 업데이트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_TcpProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 통신 연결상태 체크
            TcpOpModes mode = (TcpOpModes)e.ProgressPercentage;

            // 진행상황 시간 업데이트
            TimeSpan ts;
            if (mode == TcpOpModes.Connecting)
            {
                LblTcpServer.Text = "Connecting";
                LblTcpServer.BackColor = Color.OrangeRed;
            }
            else if (mode == TcpOpModes.Connected)
            {
                LblTcpServer.Text = "Ready";
                LblTcpServer.BackColor = Color.Lime;
                ts = DateTime.Now - _tcpData.LastCommTime;
                LblTcpIdleTimeVal.Text = $"{ts.TotalMinutes:f1}/{_cs.TcpIdleTimeMinLimit:f1} (min)";
            }
            else if (mode == TcpOpModes.Measurement)
            {
                LblTcpServer.Text = "Measurement";
                LblTcpServer.BackColor = Color.Orange;
                ts = DateTime.Now - _tcpData.LastCommTime;
                LblTcpIdleTimeVal.Text = $"{ts.TotalMinutes:f1}/{_cs.TcpIdleTimeMinLimit:f1} (min)";
                TimeSpan tsMeas = DateTime.Now - _tcpData.MeasStartTime;
                LblTcpMeasTimeVal.Text = $"{tsMeas.TotalSeconds:f1}/{_cs.TcpMaxMeasTimeSec:f1} (sec)";
            }
        }

        /// <summary>
        /// Tcp worker 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_TcpRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnTcpStart.Text = "TCP Start";
            InsertLog("The process is terminated", LogMsg.Sources.TCP);
            LblTcpServer.BackColor = DefaultBackColor;

            _tcpModeRef = TcpOpModes.Non;
        }
        #endregion

        #region 33000.Tcp.메소드
        /// <summary>
        /// 소켓 연결 여부 체크
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool IsSocketConnected(Socket s)
        {
            // 연결 상태 체크 TcpClient형의 객체명 : tcpClient 
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnections = null;

            try
            {
                tcpConnections = ipProperties.GetActiveTcpConnections().Where(x => x.LocalEndPoint.Equals(s.LocalEndPoint) &&
                x.RemoteEndPoint.Equals(s.RemoteEndPoint)).ToArray();
            }
            catch
            {
                // Exception 처리 -> 여기서는 Disconnected된 것으로 보면 된다.
                return false;
            }

            if (tcpConnections != null && tcpConnections.Length > 0)
            {
                TcpState stateOfConnection = tcpConnections.First().State;
                return stateOfConnection == TcpState.Established;
            }
            return false;
        }

        /// <summary>
        /// TCP 통신
        /// </summary>
        /// <param name="s"></param>
        /// <param name="msg"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        private static bool SendTcpMessage(Socket s, string msg, ref TcpData cd)
        {
            cd.LastComMsg = msg;
            cd.LastErrMsg = "";
            try
            {
                cd.WriteBuf = Encoding.UTF8.GetBytes(msg);
                s.Send(cd.WriteBuf, SocketFlags.None);
                cd.ReadBufLen = s.Receive(cd.ReadBuf);
                cd.LastCommTime = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                cd.LastErrMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// TCP 데이터를 생성한다
        /// </summary>
        /// <param name="plcData"></param>
        /// <param name="cs"></param>
        /// <param name="tcpMsgs"></param>
        /// <returns></returns>
        private static bool GetProductInforMsg(PlcData plcData, ComSettings cs, out string[] tcpMsgs, out string errMsg)
        {
            
            if (cs.ProductInfors.Count != plcData.ProductInforVals.Count)
            {
                tcpMsgs = null;
                errMsg = "PLC에서 충분한 제품 정보를 넘겨주지 않음";
                return false;
            }

            tcpMsgs = new string[cs.ProductInfors.Count];

            for (int i = 0; i < cs.ProductInfors.Count; i++)
            {
                string msg = $"custom \"{cs.ProductInfors[i].CustomFieldName}\" ";
                if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Text)
                {
                    string nMsg = plcData.ProductInforVals[i].ToString();
                    int nZeroInd = nMsg.IndexOf('\0');
                    if (nZeroInd > 0)
                    {
                        nMsg = nMsg.Substring(0, nZeroInd);
                    }
                    else
                    {
                        nMsg = "NO DATA";
                    }
                    msg += $" \"{nMsg}\"";
                }
                else if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Word)
                {
                    msg += $"\"{Convert.ToInt32(plcData.ProductInforVals[i])}\"";
                }
                else
                {
                    errMsg = "정의할 수 없는 제품 데이터 타입";
                    return false;
                }
                tcpMsgs[i] = msg;
            }
            errMsg = "";
            return true;
        }
        #endregion
    }
}
