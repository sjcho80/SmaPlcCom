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

using SmaCtrl;
using SmaPlc;

namespace PlcComDlg
{
    /// <summary>
    /// 3. TCP 통신 프로그램
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
            string errMsg;
            int errCnt = 0;

            while (tcpMode != TcpOpModes.Non)
            {
                // 프로그램 동작 종료 체크
                if (_workerTcp.CancellationPending)
                {
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
                            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_cs.TcpSettings.IpAdd), _cs.TcpSettings.Port);
                            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                            var waiter = sock.BeginConnect(ep, null, null);

                            // WaitOne()의 첫 번째 인수는 대기할 시간을 ms(밀리초) 단위로 넣는다. (1초 = 1000ms)
                            if (waiter.AsyncWaitHandle.WaitOne(_cs.TcpSettings.ConnWaitTimeMilSec, true))
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
                        InsertLog("TCP connected", LogMsg.Sources.TCP);
                    }
                    else
                    {
                        Thread.Sleep(_cs.TcpSettings.ConnWaitTimeMilSec);
                    }
                }
                // 31200: TCP 모니터
                else if (tcpMode == TcpOpModes.Connected)
                {
                    // 연결 상태 체크
                    if (!IsSocketConnected(sock))
                    {
                        if (_cs.TcpSettings.AutoCloseIfDisconnected)
                        {
                            _closeApp = true;
                            InsertLog("TCP server is closed", LogMsg.Sources.TCP);
                        }
                        break;
                    }

                    // 측정요청 체크
                    if (_plcModeRef == PlcOpModes.Measruement && _plcToTcpMeasReq == true)
                    {
                        _plcToTcpMeasReq = false;

                        if (!GetProductInforMsg(_pd, _cs, out string[] tcpMsgs, out errMsg))
                        {
                            InsertLog($"Fail to get product information ({_pd.Model.Number})", LogMsg.Sources.TCP, 31200, errMsg);
                            break;
                        }

                        for (int i = 0; i < tcpMsgs.Length; i++)
                        {
                            if (!SendTcpMessage(sock, tcpMsgs[i], _tcpData))
                            {
                                InsertLog($"Fail to send product information ({_pd.Model.Number})", LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                                break;
                            }
                            InsertLog(tcpMsgs[i], LogMsg.Sources.TCP);
                        }

                        Thread.Sleep(500); // 데이터 전송 속도 조절

                        if (!SendTcpMessage(sock, $"FLUX {_pd.Model.Number}", _tcpData))
                        {
                            InsertLog($"Fail to start measurment ({_pd.Model.Number})", LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                            break;
                        }
                        InsertLog(_tcpData.LastComMsg, LogMsg.Sources.TCP);

                        Thread.Sleep(_cs.TcpSettings.MeasStartDelay); // 데이터 전송 속도 조절

                        errCnt = 0;
                        tcpMode = TcpOpModes.Measurement;
                        _tcpData.MeasStartTime = DateTime.Now;
                    }
                    // IDLE 동작
                    else
                    {
                        // 제어 메시지 처리
                        if (_tcpMsgQue.TryDequeue(out TcpData.ComMsg msg))
                        {
                            if (!SendTcpMessage(sock, msg.Message, _tcpData))
                            {
                                InsertLog($"Fail to send a control message to PC: {_tcpData.LastErrMsg}", LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                                break;
                            }
                        }

                        // 하트비트 타임 체크
                        TimeSpan ts = DateTime.Now - _tcpData.LastCommTime;
                        if (ts.TotalMinutes > _cs.TcpSettings.IdleTimeMinLimit)
                        {
                            TcpData.ComMsg idleMsg = new TcpData.ComMsg
                            {
                                Message = $"*CLS"
                            };
                            _tcpMsgQue.Enqueue(idleMsg);
                        }

                        Thread.Sleep(_cs.TcpSettings.MonitorTimeMilSec);
                    }

                    try
                    {
                        
                    }
                    catch (Exception ex)
                    {
                        // 예외 발생 시 connecting mode로 돌아간다
                        InsertLog(ex.Message, LogMsg.Sources.TCP, 31200, _tcpData.LastErrMsg);
                        
                        tcpMode = TcpOpModes.Connecting;
                    }
                }
                // 31300: SMA 측정 완료 대기
                else if (tcpMode == TcpOpModes.Measurement)
                {
                    // SMA 측정 완료 체크
                    if (SendTcpMessage(sock, $"READY?", _tcpData))
                    {
                        if (_tcpData.ReadBuf[0] == '0')
                        {
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
                                if (!GetProductInforMsg(_pd, _cs, out string[] tcpMsgs, out errMsg))
                                {
                                    errCnt++;
                                }
                                for (int i = 0; i < tcpMsgs.Length; i++)
                                {
                                    if (!SendTcpMessage(sock, tcpMsgs[i], _tcpData))
                                    {
                                        errCnt++;
                                    }
                                    InsertLog(tcpMsgs[i], LogMsg.Sources.TCP);
                                }
                                Thread.Sleep(500); // 데이터 전송 속도 조절
                                if (!SendTcpMessage(sock, $"FLUX {_pd.Model.Number}", _tcpData))
                                {
                                    errCnt++;
                                }
                                InsertLog(_tcpData.LastComMsg, LogMsg.Sources.TCP);
                                Thread.Sleep(_cs.TcpSettings.MeasStartDelay); // 데이터 전송 속도 조절
                            }
                        }
                    }
                    // 에러 발생 횟수 체크
                    else
                    {
                        errCnt++;
                        if (errCnt > _cs.TcpSettings.MaxErrorCount)
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
                    if (ts.TotalSeconds > _cs.TcpSettings.MaxMeasTimeSec)
                    {
                        tcpMode = TcpOpModes.Connected;
                        _tcpMeasErrFlag = true;
                        _tcpToPlcMeasFin = true;
                        _tcpMeasErrMsg = "Time out";
                        SendTcpMessage(sock, $"ABORT", _tcpData);
                        InsertLog("Timeout", LogMsg.Sources.TCP, 31300, $"{ts.TotalSeconds:f3}/{_cs.TcpSettings.MaxMeasTimeSec:f3} sec");
                    }

                    Thread.Sleep(_cs.TcpSettings.MeasFinCheckTimeMilSec);
                }

                _workerTcp.ReportProgress((int)tcpMode);
            }
            sock.Close();
            _tcpModeRef = TcpOpModes.Non;
        }
        #endregion

        #region 32000.Tcp.Worker.이벤트
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
                LblTcpServer.Text = "Connecting to TCP server";
                LblTcpServer.BackColor = Color.OrangeRed;
            }
            else if (mode == TcpOpModes.Connected)
            {
                LblTcpServer.Text = "TCP Ready";
                LblTcpServer.BackColor = Color.Lime;
                ts = DateTime.Now - _tcpData.LastCommTime;
                LblTcpIdleTimeVal.Text = $"{ts.TotalMinutes:f1}/{_cs.TcpSettings.IdleTimeMinLimit:f1} (min)";
            }
            else if (mode == TcpOpModes.Measurement)
            {
                LblTcpServer.Text = "Measurement";
                LblTcpServer.BackColor = Color.Orange;
                ts = DateTime.Now - _tcpData.LastCommTime;
                LblTcpIdleTimeVal.Text = $"{ts.TotalMinutes:f1}/{_cs.TcpSettings.IdleTimeMinLimit:f1} (min)";
                TimeSpan tsMeas = DateTime.Now - _tcpData.MeasStartTime;
                LblTcpMeasTimeVal.Text = $"{tsMeas.TotalSeconds:f1}/{_cs.TcpSettings.MaxMeasTimeSec:f1} (sec)";
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
        /// 워커 시작
        /// </summary>
        private void StartTcpWorker()
        {
            _tcpMsgQue = new ConcurrentQueue<TcpData.ComMsg>();
            // Worker 설정
            _workerTcp = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _workerTcp.DoWork += new DoWorkEventHandler(Worker_TcpDoWork);
            _workerTcp.ProgressChanged += new ProgressChangedEventHandler(Worker_TcpProgressChanged);
            _workerTcp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_TcpRunWorkerCompleted);

            // GUI 업데이트
            BtnTcpStart.Text = "TCP Stop";

            LblTcpServerIp.Text = $"{_cs.TcpSettings.IpAdd} ({_cs.TcpSettings.Port})";
            LblTcpServer.BackColor = Color.OrangeRed;
            LblTcpMeasTimeVal.Text = $"{0:f1}/{_cs.TcpSettings.MaxMeasTimeSec:f1} (sec)";

            _workerTcp.RunWorkerAsync();
        }

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
        private static bool SendTcpMessage(Socket s, string msg, TcpData cd)
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
        /// <param name="pd"></param>
        /// <param name="cs"></param>
        /// <param name="tcpMsgs"></param>
        /// <returns></returns>
        private static bool GetProductInforMsg(ComPlcData pd, ComSettings cs, out string[] tcpMsgs, out string errMsg)
        {
            tcpMsgs = new string[pd.ProdData.Rows.Count];
            for (int i = 0; i < tcpMsgs.Length; i++)
            {
                DataRow r = pd.ProdData.Rows[i];
                tcpMsgs[i] = $"custom \"{r["dbColumnName"]}\" \"{r["data"]}\"";
            }
            errMsg = "";
            return true;
        }
        #endregion
    }
}
