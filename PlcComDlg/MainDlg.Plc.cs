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

using SmaPlc;
using SmaCtrl;

namespace PlcComDlg
{
    /// <summary>
    /// 2. SMA-PLC 통신 프로그램
    /// </summary>
    public partial class MainDlg : Form
    {
        #region 타입정의
        /// <summary>
        /// GUI 업데이트 이벤트
        /// </summary>
        private enum PlcGuiEvents
        {
            /// <summary>
            /// MES 업로드
            /// </summary>
            MesUpload,
            /// <summary>
            /// MES 클리어
            /// </summary>
            MesClear,
            /// <summary>
            /// 제품 정보 읽음
            /// </summary>
            ReadProductInfor,
        }

        #endregion

        #region 변수선언
        /// <summary>
        /// Plc 업데이트 이벤트
        /// </summary>
        private ConcurrentQueue<PlcGuiEvents> _plcGuiUpdateQue { get; set; } = new ConcurrentQueue<PlcGuiEvents>();

        /// <summary>
        /// 마지막 측정 id
        /// </summary>
        private long _lastDbId { get; set; } = -1;

        /// <summary>
        /// 수동 측정 시작
        /// </summary>
        private bool _manualMeasTrigger { get; set; } = false;
        #endregion

        #region 21000.Plc.Worker
        /// <summary>
        /// 2XXX: 워커 동작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_PlcDoWork(object sender, DoWorkEventArgs e)
        {
            string errMsg;

            #region 21100.Plc.Worker.초기화
            PlcOpModes plcMode = PlcOpModes.Connecting;
            long lastDbId = -1;

            // PLC 제어 오브젝트 생성
            PlcBase pb;
            switch (_cs.PlcSettings.PlcType)
            {
                case PlcBase.PlcTypes.MelsecMxComponent:
                    pb = new MelsecMxComponent();
                    break;
                case PlcBase.PlcTypes.SimensS7:
                    pb = new SiemensS7();
                    break;
                case PlcBase.PlcTypes.MelsecMcProtocol:
                    pb = new MelsecMcProtocol();
                    break;
                case PlcBase.PlcTypes.LsXgComm:
                    pb = new LsXGComm();
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (!pb.ConvertOpenParameter(_cs.PlcSettings.ConnectionParam, out object item))
            {
                InsertLog("Fail to parse the open parameter", LogMsg.Sources.PLC, 21200, pb.LastErrMsg);
                return;
            }
            #endregion

            while (plcMode != PlcOpModes.Non)
            {
                // 프로그램 동작 종료 체크
                if (_workerPlc.CancellationPending)
                {
                    break;
                }
                _plcModeRef = plcMode;

                // 21200.Plc.Woker.제어-연결
                if (plcMode == PlcOpModes.Connecting)
                {
                    // 일정한 시간 간격으로 연결을 시도한다
                    if (pb.Open(_cs.PlcSettings.ConnectionParam))
                    {
                        bool res = pb.WriteBit(_pd.BusyBit.Address, false)
                            & pb.WriteBit(_pd.MeasFinBit.Address, false)
                            & pb.WriteBit(_pd.OkBit.Address, false)
                            & pb.WriteBit(_pd.NgBit.Address, false)
                            & pb.WriteBit(_pd.MeasReqRespBit.Address, false);
                        if (res)
                        {
                            plcMode = PlcOpModes.Connected;
                            _pd.InitFlags();
                            InsertLog("Connected", LogMsg.Sources.PLC);
                        }
                    }
                    else
                    {
                        Thread.Sleep(_cs.PlcSettings.ConnRetryInterval);
                    }
                }
                // 21300.Plc.Woker.제어-측정요청 검출
                else if (plcMode == PlcOpModes.Connected)
                {
                    // 플래그/모델넘버 읽기
                    if (!ReadFlag(pb, _pd, out errMsg))
                    {
                        InsertLog($"Faile to read flage", LogMsg.Sources.PLC, 21300, errMsg);
                        if (_cs.GoToPlcConnectionDuringReady)
                        {
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }
                        else
                        {
                            plcMode = PlcOpModes.Non;
                            break;
                        }
                    }

                    if ((_pd.MeasReqBit.Event == _cs.PlcSettings.FlagEventType || _manualMeasTrigger) && _tcpModeRef == TcpOpModes.Connected)
                    {
                        if (_manualMeasTrigger)
                        {
                            _manualMeasTrigger = false;
                        }
                        // 마지막 DB ID를 읽어온다
                        if (!ReadLastIdFromDb(_cs, out lastDbId, out errMsg))
                        {
                            InsertLog($"Faile to read last DB Id", LogMsg.Sources.DB, 21300, errMsg);
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }
                        _lastDbId = lastDbId;

                        // 제품정보 읽기
                        if (!PlcData.ReadProdInfor(pb, _cs.ProductInfors, _pd, out errMsg))
                        {
                            InsertLog($"Faile to read product information", LogMsg.Sources.PLC, 21300, errMsg);
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }
                        _plcGuiUpdateQue.Enqueue(PlcGuiEvents.ReadProductInfor);

                        if (_cs.ClearMesBeforeMeasurement)
                        {
                            if (!ClearMesData(_cs.DbMeas, _cs.DbSettings, pb, _pd, out errMsg))
                            {
                                InsertLog($"Fail to clear MES DATA", LogMsg.Sources.DB, 21400, errMsg);
                                break;
                            }
                        }

                        // PLC flag 쓰기
                        if (_cs.PlcSettings.CtrlAutoClearOkNG)
                        {
                            pb.WriteBit(_pd.OkBit.Address, false);
                            pb.WriteBit(_pd.NgBit.Address, false);
                        }
                        if (_cs.PlcSettings.CtrlAutoClearMeasFin)
                        {
                            pb.WriteBit(_pd.MeasFinBit.Address, false);
                        }
                        pb.WriteBit(_pd.MeasReqRespBit.Address, true);
                        pb.WriteBit(_pd.BusyBit.Address, true);

                        // TCP 측정 시작
                        _pd.MeasStartTime = DateTime.Now;
                        _tcpToPlcMeasFin = false;
                        _plcToTcpMeasReq = true;
                        plcMode = PlcOpModes.Measruement;
                        InsertLog("Start measurement", LogMsg.Sources.PLC);
                        InsertLog($"\tLast DB ID={lastDbId}", LogMsg.Sources.DB);
                    }
                    else
                    {                    
                        // TCP 연결 시 하트비트
                        if (_tcpModeRef == TcpOpModes.Connected)
                        {
                            if (!pb.WriteBit(_pd.HeartBeat.Address, !_pd.HeartBeat.Bit))
                            {
                                InsertLog($"Heartbeat failure", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                                if (_cs.GoToPlcConnectionDuringReady)
                                {
                                    plcMode = PlcOpModes.Connecting;
                                    continue;
                                }
                                else
                                {
                                    plcMode = PlcOpModes.Non;
                                    break;
                                }
                            }
                        }

                        // 모니터 업무 처리
                        if (_plcMsgQue.TryDequeue(out ComPlcData.ComMsg msg))
                        {
                            if (!PlcMonitor(pb, _cs, msg, _pd, out errMsg))
                            {
                                InsertLog("Plc monitor failure", LogMsg.Sources.PLC, 21300, errMsg);
                                break;
                            }
                        }
                        Thread.Sleep(_cs.PlcSettings.UpdateInterval);
                    }
                }
                // 21400.Plc.Woker.제어-측정완료 대기
                else if (plcMode == PlcOpModes.Measruement)
                {
                    // 플래그 읽기
                    if (!ReadFlag(pb, _pd, out errMsg))
                    {
                        InsertLog($"Fail to read flags", LogMsg.Sources.PLC, 21400, errMsg);
                        break;
                    }

                    TimeSpan ts = DateTime.Now - _pd.MeasStartTime;
                    // 측정 종료 이벤트 발생
                    if (_tcpToPlcMeasFin == true)
                    {
                        bool tcpAlarm, tcpOk, tcpNg;
                        // 측정 성공
                        if (!_tcpMeasErrFlag)
                        {
                            InsertLog("Measurement finished", LogMsg.Sources.PLC);

                            if (!UploadMesData(_cs.DbMeas, _cs.DbSettings, pb, _pd, out errMsg))
                            {
                                InsertLog($"Fail to upload MES DATA", LogMsg.Sources.DB, 21400, errMsg);
                                break;
                            }

                            // OK/NG/측정완료 플래그 설정
                            tcpOk = _pd.DbPass != 0;
                            tcpNg = _pd.DbPass == 0;
                            tcpAlarm = false;
                        }
                        // 측정 실패
                        else
                        {
                            InsertLog("Measurement failure", LogMsg.Sources.PLC, 21400, _tcpMeasErrMsg);
                            tcpOk = tcpNg = false;
                            tcpAlarm = true;
                        }

                        _plcGuiUpdateQue.Enqueue(PlcGuiEvents.MesUpload);

                        if (_cs.PlcSettings.CtrlAutoClearMeasReqResp)
                        {
                            pb.WriteBit(_pd.MeasReqRespBit.Address, false);
                        }
                        pb.WriteBit(_pd.AlarmBit.Address, tcpAlarm);
                        pb.WriteBit(_pd.OkBit.Address, tcpOk);
                        pb.WriteBit(_pd.NgBit.Address, tcpNg);
                        pb.WriteBit(_pd.BusyBit.Address, false);
                        pb.WriteBit(_pd.MeasFinBit.Address, true);
                        plcMode = PlcOpModes.Connected;
                    }
                    // 측정 타임아웃 - 프로그래밍 오류
                    else if (ts.Seconds > _cs.PlcSettings.MeasFinWaitTimeOut)
                    {
                        InsertLog($"Measurement timeout", LogMsg.Sources.PLC, 21400, $"{ts.Seconds:f3}/{_cs.PlcSettings.MeasFinWaitTimeOut:f3} sec");
                        if (_cs.PlcSettings.CtrlAutoClearMeasReqResp)
                        {
                            pb.WriteBit(_pd.MeasReqRespBit.Address, false);
                        }
                        pb.WriteBit(_pd.AlarmBit.Address, true);
                        pb.WriteBit(_pd.OkBit.Address, false);
                        pb.WriteBit(_pd.NgBit.Address, false);
                        pb.WriteBit(_pd.BusyBit.Address, false);
                        pb.WriteBit(_pd.MeasFinBit.Address, true);
                        plcMode = PlcOpModes.Connected;
                    }
                    // 측정 대기
                    else
                    {
                        pb.WriteBit(_cs.PlcAddress.HeartBeat, !_pd.HeartBeat.Bit);
                        if (_plcMsgQue.TryDequeue(out ComPlcData.ComMsg msg))
                        {
                            if (!PlcMonitor(pb, _cs, msg, _pd, out errMsg))
                            {
                                InsertLog("Plc monitor failure", LogMsg.Sources.PLC, 21400, errMsg);
                                break;
                            }
                        }
                        Thread.Sleep(_cs.PlcSettings.UpdateInterval);
                    }
                }
                else
                {
                    InsertLog($"정의되지 않은 PLC 모드", LogMsg.Sources.PLC, 21500, plcMode.ToString());
                }

                // GUI 업데이트
                _workerPlc.ReportProgress((int)plcMode);
            }

            // 프로그램 종료
            pb.Close();
            _plcModeRef = PlcOpModes.Non;
        }
        #endregion

        #region 22000.Plc.Woker.이벤트
        /// <summary>
        /// 진행상황 업데이트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_PlcProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 통신 연결상태 체크
            bool plcConnected = _plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement;
            LblPlcCon.BackColor = plcConnected ? Color.Lime : Color.OrangeRed;

            if (plcConnected)
            {
                LblPlcAddMeasReq.BackColor = _pd.MeasReqBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddMeasReqResp.BackColor = _pd.MeasReqRespBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddMeasFin.BackColor = _pd.MeasFinBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddHeartBeat.BackColor = _pd.HeartBeat.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddOk.BackColor = _pd.OkBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddNg.BackColor = _pd.NgBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddBusy.BackColor = _pd.BusyBit.Bit ? Color.Lime : DefaultBackColor;
                LblPlcAddAlarm.BackColor = _pd.AlarmBit.Bit ? Color.Lime : DefaultBackColor;
                LblModelNumVal.Text = $"{_pd.Model.Number}";
            }
            else
            {
                LblPlcAddMeasReq.BackColor = LblPlcAddMeasReqResp.BackColor = LblPlcAddMeasFin.BackColor = LblPlcAddHeartBeat.BackColor = LblPlcAddOk.BackColor = LblPlcAddNg.BackColor = DefaultBackColor;
            }

            // 진행상황 시간 업데이트
            TimeSpan ts;
            if (_plcModeRef == PlcOpModes.Connecting)
            {
                LblPlcCon.Text = "Connecting to PLC";
                LblPlcCon.BackColor = Color.OrangeRed;
            }
            else if (_plcModeRef == PlcOpModes.Connected)
            {
                LblPlcCon.Text = "PLC Ready";
                LblPlcCon.BackColor = Color.Lime;
            }
            else if (_plcModeRef == PlcOpModes.Measruement)
            {
                LblPlcCon.Text = "Measurement";
                LblPlcCon.BackColor = Color.Orange;
                ts = DateTime.Now - _pd.MeasStartTime;
                LblMeasTimeVal.Text = $"{ts.TotalSeconds:f1}/{_cs.PlcSettings.MeasFinWaitTimeOut:f1} (sec)";
            }
        }

        /// <summary>
        /// PLC worker 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_PlcRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnPlcStart.Text = "PLC Start";

            LblPlcCon.BackColor = DefaultBackColor;
            LblPlcAddMeasReq.BackColor = LblPlcAddMeasReqResp.BackColor = LblPlcAddMeasFin.BackColor = LblPlcAddHeartBeat.BackColor = LblPlcAddOk.BackColor = LblPlcAddNg.BackColor = DefaultBackColor;

            _plcModeRef = PlcOpModes.Non;

            InsertLog("The process is terminated", LogMsg.Sources.PLC);
        }
        #endregion

        #region 23000.Plc.Woker.메소드
        /// <summary>
        /// PLC 워커 시작
        /// </summary>
        private void StartPlcWorker()
        {
            _plcMsgQue = new ConcurrentQueue<ComPlcData.ComMsg>();

            // Worker 설정
            _workerPlc = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _workerPlc.DoWork += new DoWorkEventHandler(Worker_PlcDoWork);
            _workerPlc.ProgressChanged += new ProgressChangedEventHandler(Worker_PlcProgressChanged);
            _workerPlc.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_PlcRunWorkerCompleted);

            // GUI 업데이트
            BtnPlcStart.Text = "PLC Stop";

            LblPlcAddMeasReq.Text = _pd.MeasReqBit.Address = _cs.PlcAddress.MeasRequest;
            LblPlcAddMeasReqResp.Text = _pd.MeasReqRespBit.Address = _cs.PlcAddress.MeasReqRespBit;
            LblPlcAddHeartBeat.Text = _pd.HeartBeat.Address = _cs.PlcAddress.HeartBeat;
            LblPlcAddMeasFin.Text = _pd.MeasFinBit.Address = _cs.PlcAddress.MeasFinBit;
            LblPlcAddOk.Text = _pd.OkBit.Address = _cs.PlcAddress.OkBit;
            LblPlcAddNg.Text = _pd.NgBit.Address = _cs.PlcAddress.NgBit;
            LblPlcAddBusy.Text = _pd.BusyBit.Address = _cs.PlcAddress.BusyBit;
            LblPlcAddAlarm.Text = _pd.AlarmBit.Address = _cs.PlcAddress.AlarmBit;
            LblModelNumberAdd.Text = _pd.Model.Address = _cs.PlcAddModelNumber;
            LblHeartBeatIntVal.Text = $"{_cs.PlcSettings.UpdateInterval} (ms)";
            LblMeasTimeVal.Text = $"{0:f1}/{_cs.PlcSettings.MeasFinWaitTimeOut:f1} (sec)";

            _workerPlc.RunWorkerAsync();
        }

        /// <summary>
        /// PLC 모니터 함수
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="pq"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool PlcMonitor(PlcBase pb, ComSettings cs, ComPlcData.ComMsg msg, ComPlcData pd, out string errMsg)
        {
            if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.GetProductInfor)
            {
                if (PlcData.ReadProdInfor(pb, cs.ProductInfors, pd, out errMsg))
                {
                    InsertLog("Read product information", LogMsg.Sources.PLC);
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.ReadProductInfor);
                    return true;
                }
                else
                {
                    InsertLog("Fail to read product information", LogMsg.Sources.PLC, 21300, errMsg);
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.UploadMesData)
            {
                if (UploadMesData(cs.DbMeas, cs.DbSettings, pb, pd, out errMsg))
                {
                    InsertLog("Upload MES data", LogMsg.Sources.PLC);
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.MesUpload);
                    return true;
                }
                else
                {
                    InsertLog("Fail to upload MES data", LogMsg.Sources.PLC, 21300, errMsg);
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ClearMesData)
            {
                if (ClearMesData(cs.DbMeas, cs.DbSettings, pb, pd, out errMsg))
                {
                    InsertLog("Clear MES data", LogMsg.Sources.PLC);
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.MesClear);
                    return true;
                }
                else
                {
                    InsertLog("Fail to clear MES data", LogMsg.Sources.PLC, 21300, errMsg);
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleMeasReq)
            {
                if (pb.WriteBit(pd.MeasReqBit.Address, !pd.MeasReqBit.Bit))
                {
                    InsertLog("Toggle MeasReq bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle MeasReq bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleMeasReqResp)
            {
                if (pb.WriteBit(pd.MeasReqRespBit.Address, !pd.MeasReqRespBit.Bit))
                {
                    InsertLog("Toggle MeasReqResp bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle MeasReqResp bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleMeasFin)
            {
                if (pb.WriteBit(pd.MeasFinBit.Address, !pd.MeasFinBit.Bit))
                {
                    InsertLog("Toggle MeasFin bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle MeasFin bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleOk)
            {
                if (pb.WriteBit(pd.OkBit.Address, !pd.OkBit.Bit))
                {
                    InsertLog("Toggle Ok bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle Ok bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleNg)
            {
                if (pb.WriteBit(pd.NgBit.Address, !pd.NgBit.Bit))
                {
                    InsertLog("Toggle Ng bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle Ng bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleBusy)
            {
                if (pb.WriteBit(pd.BusyBit.Address, !pd.BusyBit.Bit))
                {
                    InsertLog("Toggle Busy bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle Busy Ng bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else if (msg.MsgType == ComPlcData.ComMsg.MsgTypes.ToggleAlarm)
            {
                if (pb.WriteBit(pd.AlarmBit.Address, !pd.AlarmBit.Bit))
                {
                    InsertLog("Toggle Alarm bit", LogMsg.Sources.PLC);
                    errMsg = "";
                    return true;
                }
                else
                {
                    InsertLog("Fail to toggle Alarm Ng bit", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                    errMsg = pb.LastErrMsg;
                    return false;
                }
            }
            else
            {
                errMsg = "정의되지 않은 PLC 메시지";
                return false;
            }
        }

        /// <summary>
        /// Flag를 읽어온다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static bool ReadFlag(PlcBase pb, ComPlcData pd, out string errMsg)
        {
            if (pb.ReadBit(pd.HeartBeat.Address, out bool heartBeat))
            {
                pd.HeartBeat.SetBit(heartBeat);
            }
            else
            {
                errMsg = $"하트비트 비트 읽기 실패={pd.MeasReqBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.MeasReqBit.Address, out bool measReq))
            {
                pd.MeasReqBit.SetBit(measReq);
            }
            else
            {
                errMsg = $"측정요청 비트 읽기 실패={pd.MeasReqBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.MeasFinBit.Address, out bool measFin))
            {
                pd.MeasFinBit.SetBit(measFin);
            }
            else
            {
                errMsg = $"측정완료 비트 읽기 실패={pd.MeasFinBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.OkBit.Address, out bool ok))
            {
                pd.OkBit.SetBit(ok);
            }
            else
            {
                errMsg = $"OK 비트 읽기 실패={pd.OkBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.NgBit.Address, out bool ng))
            {
                pd.NgBit.SetBit(ng);
            }
            else
            {
                errMsg = $"NG 비트 읽기 실패={pd.NgBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.AlarmBit.Address, out bool alarm))
            {
                pd.AlarmBit.SetBit(alarm);
            }
            else
            {
                errMsg = $"알람 비트 읽기 실패={pd.AlarmBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.MeasReqRespBit.Address, out bool measReqResp))
            {
                pd.MeasReqRespBit.SetBit(measReqResp);
            }
            else
            {
                errMsg = $"측정요청응답 비트 읽기 실패={pd.MeasReqRespBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.BusyBit.Address, out bool busy))
            {
                pd.BusyBit.SetBit(busy);
            }
            else
            {
                errMsg = $"BUSY 비트 읽기 실패={pd.BusyBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadWord(pd.Model.Address, out short mn))
            {
                pd.Model.Number = mn;
            }
            else
            {
                errMsg = $"MODEL 넘버 읽기 실패={pd.Model.Address}, {pb.LastErrMsg}";
                return false;
            }
            errMsg = "";
            return true;
        }
        
        /// <summary>
        /// DB에서 마지막 ID를 읽어온다
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool ReadLastIdFromDb(ComSettings cs, out long id, out string errMsg)
        {
            string strConn = $@"Data Source={cs.DbMeas.Path};Read Only=True;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    cmd = new SQLiteCommand(@"select MAX(Id) from sma_measurement", conn);
                    id = (long)cmd.ExecuteScalar();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                id = -1;
                errMsg = $"PLC: DB 데이터 로드 오류로 인한 중단: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// MES 데이터 업로드
        /// </summary>
        /// <param name="di"></param>
        /// <param name="si"></param>
        /// <param name="pb"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool UploadMesData(ComSettings.DbInforMeas di, ComSettings.DbInforSettings si, PlcBase pb, ComPlcData pd, out string errMsg)
        {
            string strConn;
            pd.MesData.Rows.Clear();

            // 측정 아이템 정보를 읽어온다
            strConn = $@"Data Source={di.Path};Read Only=True;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    cmd = new SQLiteCommand(@"select MAX(Id) from sma_measurement", conn);
                    pd.DbId = (long)cmd.ExecuteScalar();

                    string sql = $"SELECT * FROM sma_measurement WHERE Id = {pd.DbId}";
                    cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    var columns = Enumerable.Range(0, rdr.FieldCount).Select(rdr.GetName).ToList();

                    // Column 존재여부 검증
                    foreach (var d in di.MeasCols.Where(x => x.Enable))
                    {
                        if (columns.Find(n => n == d.DataName) == null)
                        {
                            throw new Exception($"DB 테이블에 {d.DataName} 필드가 존재하지 않습니다");
                        }
                    }

                    while (rdr.Read())
                    {
                        pd.DbPass = Convert.ToInt64(rdr["pass"]);
                        pd.DbFailedId = Convert.ToInt64(rdr["failed_id"]);
                        foreach (var d in di.MeasCols.Where(x => x.Enable))
                        {
                            double rdata;
                            if (DBNull.Value != rdr[d.DataName])
                            {
                                rdata = Convert.ToDouble(rdr[d.DataName]);
                            }
                            else
                            {
                                rdata = 0;
                            }
                            pd.MesData.Rows.Add(d.Name, d.DataName, rdata, rdata * d.Scale, d.Address, d.DataType);
                        }
                        break;
                    }
                    rdr.Close();
                }
                errMsg = "";
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            // 설정 DB로 부터 업로드 아이템을 읽어온다.
            strConn = $@"Data Source={si.Path};Read Only=True;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    conn.Open();
                    SQLiteCommand cmd;

                    foreach (var s in si.CondItems.FindAll(x =>x.Enable))
                    {
                        if (pd.Model.Number != s.ModelNumber)
                        {
                            continue;
                        }
                        string sql = $"SELECT * FROM sma_conditions WHERE Id = {s.Id} AND sample_id = {s.ModelNumber}";

                        cmd = new SQLiteCommand(sql, conn);
                        SQLiteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            if (!s.ParseParam(rdr["parameters"].ToString(), out double paramVal, out errMsg))
                            {
                                throw new Exception($"{errMsg}");
                            }
                            pd.MesData.Rows.Add($"{s.Name} ({s.ModelNumber}.{s.Id})", s.DataName, paramVal, paramVal * s.Scale, s.Address, s.DataType);
                        }
                        rdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            // MES Data 업로드
            foreach (DataRow r in pd.MesData.Rows)
            {
                PlcMesInfors.MesDataTypes t = (PlcMesInfors.MesDataTypes)Convert.ToInt32(r["dataType"]);
                if (t == PlcMesInfors.MesDataTypes.Word)
                {
                    if (!pb.WriteWord(r["address"].ToString(), Convert.ToInt16(r["mesData"])))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (t == PlcMesInfors.MesDataTypes.DWord)
                {
                    if (!pb.WriteDWord(r["address"].ToString(), Convert.ToInt32(r["mesData"])))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else
                {
                    errMsg = $"지원하지 않는 데이터 타입: {t.ToString()}";
                    return false;
                }
            }
            errMsg = "";
            return true;
        }

        /// <summary>
        /// MES 데이터 업로드
        /// </summary>
        /// <param name="di"></param>
        /// <param name="si"></param>
        /// <param name="pb"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool ClearMesData(ComSettings.DbInforMeas di, ComSettings.DbInforSettings si, PlcBase pb, ComPlcData pd, out string errMsg)
        {
            pd.MesData.Rows.Clear();
            foreach (var d in di.MeasCols.Where(x => x.Enable))
            {
                pd.MesData.Rows.Add(d.Name, d.DataName, 0, 0, d.Address, d.DataType);
            }

            foreach (var s in si.CondItems.FindAll(x => x.Enable))
            {
                if (pd.Model.Number != s.ModelNumber)
                {
                    continue;
                }
                pd.MesData.Rows.Add($"{s.Name} ({s.ModelNumber}.{s.Id})", s.DataName, 0, 0, s.Address, s.DataType);
            }

            // MES Data 업로드
            foreach (DataRow r in pd.MesData.Rows)
            {
                PlcMesInfors.MesDataTypes t = (PlcMesInfors.MesDataTypes)Convert.ToInt32(r["dataType"]);
                if (t == PlcMesInfors.MesDataTypes.Word)
                {
                    if (!pb.WriteWord(r["address"].ToString(), Convert.ToInt16(r["mesData"])))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (t == PlcMesInfors.MesDataTypes.DWord)
                {
                    if (!pb.WriteDWord(r["address"].ToString(), Convert.ToInt32(r["mesData"])))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else
                {
                    errMsg = $"지원하지 않는 데이터 타입: {t.ToString()}";
                    return false;
                }
            }
            errMsg = "";
            return true;
        }
        #endregion
    }
}
