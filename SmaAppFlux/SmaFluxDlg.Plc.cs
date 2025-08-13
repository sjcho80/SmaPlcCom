using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using SmaCtrl;
using SmaPlc;

namespace SmaFlux
{
    partial class SmaFluxDlg : Form
    {
        #region 변수 선언
        /// <summary>
        /// 플럭스미터 통신 파라미터
        /// </summary>
        public class FluxCom
        {
            /// <summary>
            /// 포트 이름
            /// </summary>
            public string PortName { get; set; }

            /// <summary>
            /// 타임아웃
            /// </summary>
            public int Timeout { get; set; } = 1000;
        }
        #endregion

        #region 21100 워커 DoWork
        /// <summary>
        /// 2100 프로그램 제어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            OpModes om = OpModes.Connecting;
            string errMsg = "";
            PlcData.Flag.Events flagEvent = _fs.PlcSettings.FlagEventType;

            // PLC 제어 오브젝트 생성
            #region PLC 초기화
            PlcBase pb;
            switch (_fs.PlcSettings.PlcType)
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
                    InsertLog("정의되지 않은 PLC 타입", LogMsg.Sources.PLC, 21100, $"{_fs.PlcSettings.PlcType.ToString()}");
                    return;
            }

            if (!pb.ConvertOpenParameter(_fs.PlcSettings.ConnectionParam, out object item))
            {
                InsertLog($"Fail to parse PLC parameter={_fs.PlcSettings.ConnectionParam}", LogMsg.Sources.PLC, 21100, pb.LastErrMsg);
                return;
            }
            #endregion

            // PLC 메시지 QUEUE를 비워준다
            int maxQueClrTry = 0;
            while (!_qPlcReq.IsEmpty || maxQueClrTry < 100)
            {
                _qPlcReq.TryDequeue(out PlcReq dummy);
                ++maxQueClrTry;
            }

            // 제어 알고리즘 시작
            while (om != OpModes.NOP)
            {
                if (om == OpModes.Connecting)
                {
                    #region 연결시도
                    // 일정한 시간 간격으로 연결을 시도한다
                    if (pb.Open(_fs.PlcSettings.ConnectionParam))
                    {
                        _pd.InitFlags();
                        InsertLog("Connected", LogMsg.Sources.PLC);
                        om = OpModes.Ready;
                    }
                    else
                    {
                        Thread.Sleep(_fs.PlcSettings.ConnRetryInterval);
                    }
                    #endregion
                }
                else if (om == OpModes.Ready)
                {
                    #region 대기상태
                    // Flags read
                    if (!ReadFlags(pb, _pd, out errMsg))
                    {
                        InsertLog("플래그 읽기 실패", LogMsg.Sources.PLC, 21100, errMsg);
                        if (_fs.GoToPlcConnectionDuringReady)
                        {
                            om = OpModes.Connecting;
                            continue;
                        }
                        else
                        {
                            om = OpModes.NOP;
                            break;
                        }
                    }

                    // 하트 비트
                    if (!pb.WriteBit(_pd.HeartBeat.Address, !_pd.HeartBeat.Bit))
                    {
                        InsertLog("하트비트 쓰기 실패", LogMsg.Sources.PLC, 21100, pb.LastErrMsg);
                        if (_fs.GoToPlcConnectionDuringReady)
                        {
                            om = OpModes.Connecting;
                            continue;
                        }
                        else
                        {
                            om = OpModes.NOP;
                            break;
                        }
                    }

                    // 플럭스미터 리셋 요청
                    if (_pd.ResetReqBit.Event == _fs.PlcSettings.FlagEventType && _pd.MeasReqBit.Event != _fs.PlcSettings.FlagEventType)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        DataRow[] dr = _dtProf.Select($"modelNumber={_pd.Model.Number}");
                        if (dr.Length != 1)
                        {
                            InsertLog("model information failure", LogMsg.Sources.PLC, 21100, $"modelNumber={_pd.Model.Number}");
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        string comPort = dr[0]["portName"].ToString();
                        int sleepTime = Convert.ToInt32(dr[0]["resetInt"]);
                        if (sleepTime < 0)
                        {
                            InsertLog("Reset failure", LogMsg.Sources.PLC, 21100, $"sleep time error: {sleepTime}");
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        if (!ResetFlux(comPort, _fm, out errMsg))
                        {
                            InsertLog("Reset failure", LogMsg.Sources.PLC, 21100, $"Fail to commute with fluxmeter");
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        Thread.Sleep(sleepTime);
                        if (!ResetFlux(comPort, _fm, out errMsg))
                        {
                            InsertLog("Reset failure", LogMsg.Sources.PLC, 21100, $"Fail to commute with fluxmeter");
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }

                        if (_fs.PlcSettings.ClearMeasFlagsAfterReset)
                        {
                            pb.WriteBit(_pd.MeasFinBit.Address, false);
                            pb.WriteBit(_pd.OkBit.Address, false);
                            pb.WriteBit(_pd.NgBit.Address, false);
                        }
                        pb.WriteBit(_pd.ResetFinBit.Address, true);
                        sw.Stop();
                        InsertLog($"Reset finished, model={_pd.Model.Number}, elapsed time={sw.ElapsedMilliseconds} ms", LogMsg.Sources.PLC);
                    }
                    // 플럭스미터 측정 요청
                    else if (_pd.ResetReqBit.Event != _fs.PlcSettings.FlagEventType && _pd.MeasReqBit.Event == _fs.PlcSettings.FlagEventType)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        // MES Clear
                        if (_fs.ClearMesBeforeMeasurement)
                        {
                            if (!MesClear(pb, _fs.PlcMesInfs, out DataTable dtClr, out errMsg))
                            {
                                InsertLog("MES Clear failure", LogMsg.Sources.PLC, 21100, errMsg);
                                pb.WriteBit(_pd.AlarmBit.Address, true);
                                break;
                            }
                            InsertLog("MES Clear", LogMsg.Sources.PLC);
                            _dtMes = dtClr;
                        }

                        // 제품 정보 읽어오기
                        if (!PlcData.ReadProdInfor(pb, _fs.PlcProdInfs, _pd, out errMsg))
                        {
                            InsertLog("Fail to read product information", LogMsg.Sources.PLC, 21100, errMsg);
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        _qTmr.Enqueue(TimerEvents.ReadProdInfor);
                        InsertLog($"Read product information (model={_pd.Model.Number})", LogMsg.Sources.PLC);

                        // 플럭스미터 측정
                        DataRow[] dr = _dtProf.Select($"modelNumber={_pd.Model.Number}");
                        if (dr.Length != 1)
                        {
                            InsertLog("model information failure", LogMsg.Sources.PLC, 21100, $"modelNumber={_pd.Model.Number}");
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        if (!ReadFlux(dr[0], _fd, _pd.Model.Number, _fm, out errMsg))
                        {
                            InsertLog("Peak read failure", LogMsg.Sources.PLC, 21100, errMsg);
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        InsertLog($"Read flux from fluxmeter (model={_pd.Model.Number})", LogMsg.Sources.PLC);
                        // 플럭스미터 측정 GUI 업로드
                        _qTmr.Enqueue(TimerEvents.FluxMeasurement);

                        // DB 측정 데이터 삽입
                        _qTmr.Enqueue(TimerEvents.InsertDb);

                        // MES 측정 정보 업로드
                        if (!MesUpload(pb, _fs.PlcMesInfs, _fd, out DataTable dtMes, out errMsg))
                        {
                            InsertLog("MES Upload failure", LogMsg.Sources.PLC, 21100, errMsg);
                            pb.WriteBit(_pd.AlarmBit.Address, true);
                            break;
                        }
                        _dtMes = dtMes;
                        _qTmr.Enqueue(TimerEvents.UploadMesData);
                        InsertLog($"MES upload (model={_pd.Model.Number})", LogMsg.Sources.PLC);

                        // 완료 비트 설정
                        if (_fs.PlcSettings.ClearResetFlagsAfterMeas)
                        {
                            pb.WriteBit(_pd.ResetFinBit.Address, false);
                        }
                        pb.WriteBit(_pd.OkBit.Address, _fd.Ok);
                        pb.WriteBit(_pd.NgBit.Address, !_fd.Ok);
                        pb.WriteBit(_pd.MeasFinBit.Address, true);
                        sw.Stop();
                        InsertLog($"Measurement finished, model={_pd.Model.Number}, elapsed time={sw.ElapsedMilliseconds} ms", LogMsg.Sources.PLC);
                    }
                    else
                    {
                        // GUI 컨트롤 요청 처리
                        if (_qPlcReq.TryDequeue(out PlcReq pe))
                        {
                            if (pe.PlcReqType == PlcReqTypes.ReadProdInf)
                            {
                                if (PlcData.ReadProdInfor(pb, _fs.PlcProdInfs, _pd, out errMsg))
                                {
                                    _qTmr.Enqueue(TimerEvents.ReadProdInfor);
                                }
                                else
                                {
                                    InsertLog("Fail to read product information", LogMsg.Sources.PLC, 21100, errMsg);
                                }
                            }
                            else if (pe.PlcReqType == PlcReqTypes.SetFlag)
                            {
                                if (!pb.WriteBit(pe.PlcReqData.ToString(), true))
                                {
                                    InsertLog("Fail to set flag", LogMsg.Sources.PLC, 21100, pb.LastErrMsg);
                                }
                            }
                            else if (pe.PlcReqType == PlcReqTypes.ClearFlag)
                            {
                                if (!pb.WriteBit(pe.PlcReqData.ToString(), false))
                                {
                                    InsertLog("Fail to clear flag", LogMsg.Sources.PLC, 21100, pb.LastErrMsg);
                                }
                            }
                            else if (pe.PlcReqType == PlcReqTypes.MesUpload)
                            {
                                if (MesUpload(pb, _fs.PlcMesInfs, _fd, out DataTable dt, out errMsg))
                                {
                                    InsertLog($"MES data upload (model={_pd.Model.Number})", LogMsg.Sources.PLC);
                                    _dtMes = dt;
                                    _qTmr.Enqueue(TimerEvents.UploadMesData);
                                }
                                else
                                {
                                    InsertLog("Fail to upload MES data", LogMsg.Sources.PLC, 21100, errMsg);
                                }
                            }
                            else if (pe.PlcReqType == PlcReqTypes.MesClear)
                            {
                                if (MesClear(pb, _fs.PlcMesInfs, out DataTable dt, out errMsg))
                                {
                                    InsertLog($"MES clear (model={_pd.Model.Number})", LogMsg.Sources.PLC);
                                    _dtMes = dt;
                                    _qTmr.Enqueue(TimerEvents.ClearMesData);
                                }
                                else
                                {
                                    InsertLog("Fail to clear MES data", LogMsg.Sources.PLC, 21100, errMsg);
                                }
                            }
                            else
                            {
                                InsertLog($"정의되지 않은 PLC 이벤트={pe.PlcReqType}", LogMsg.Sources.PLC, 21100, errMsg);
                                break;
                            }
                        }
                    }

                    // 대기 상태
                    Thread.Sleep(_fs.PlcSettings.UpdateInterval);
                    #endregion
                }
                else
                {
                    InsertLog($"정의되지 않은 동작 모드 요청", LogMsg.Sources.PLC, 21100, $"동작 모드={om.ToString()}");
                    break;
                }

                _omRef = om;
                if (_bw.CancellationPending)
                {
                    InsertLog("Disconnected", LogMsg.Sources.PLC);
                    break;
                }
                else
                {
                    // GUI 업데이트
                    _bw.ReportProgress((int)om);
                }
            }

            // 프로그램 종료
            pb.Close();
            _omRef = OpModes.NOP;
        }
        #endregion

        #region 21200 워커 이벤트
        /// <summary>
        /// 2200 Worker 업데이트 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LblPlcAddHeartBeat.BackColor = _pd.HeartBeat.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddMeasReqBit.BackColor = _pd.MeasReqBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddMeasFin.BackColor = _pd.MeasFinBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddRstReq.BackColor = _pd.ResetReqBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddRstFin.BackColor = _pd.ResetFinBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddOk.BackColor = _pd.OkBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddNg.BackColor = _pd.NgBit.Bit ? Color.Lime : SystemColors.Control;
            LblPlcAddAlarm.BackColor = _pd.AlarmBit.Bit ? Color.Lime : SystemColors.Control;
            LblHeartBeat.Text = $"Heartbit\r\n{_fs.PlcSettings.UpdateInterval} ms";

            DataRow[] sr = _dtProf.Select($"modelNumber={_pd.Model.Number}");
            if (sr.Length > 0)
            {
                LblModelName.Text = $"{sr[0]["modelName"].ToString()} ({_pd.Model.Number})";
            }
            else
            {
                LblModelName.Text = $"Unknown model=({_pd.Model.Number})";
            }
            if (_omRef == OpModes.Connecting)
            {
                LblOpMode.BackColor = Color.Orange;
            }
            else if (_omRef == OpModes.Ready)
            {
                LblOpMode.BackColor = Color.Lime;
            }
            else
            {
                LblOpMode.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// 2300 Worker 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnPlcConn.Text = "Connect";
            LblHeartBeat.BackColor = SystemColors.Control;
            LblPlcAddMeasReqBit.BackColor = SystemColors.Control;
            LblPlcAddMeasFin.BackColor = SystemColors.Control;
            LblPlcAddRstReq.BackColor = SystemColors.Control;
            LblPlcAddRstFin.BackColor = SystemColors.Control;
            LblPlcAddOk.BackColor = SystemColors.Control;
            LblPlcAddNg.BackColor = SystemColors.Control;
            LblPlcAddAlarm.BackColor = SystemColors.Control;
            LblOpMode.BackColor = SystemColors.Control;
            _omRef = OpModes.NOP;
        }
        #endregion

        #region 22000 methods
        /// <summary>
        /// Background worker를 시작한다
        /// </summary>
        private void StartWorker()
        {
            BtnPlcConn.Text = "Disconnect";

            LblPlcAddHeartBeat.Text = _pd.HeartBeat.Address = _fs.PlcAddress.HeartBeat;
            LblPlcAddMeasReqBit.Text = _pd.MeasReqBit.Address = _fs.PlcAddress.MeasRequest;
            LblPlcAddMeasFin.Text = _pd.MeasFinBit.Address = _fs.PlcAddress.MeasFinBit;
            LblPlcAddRstReq.Text = _pd.ResetReqBit.Address = _fs.PlcAddress.ResetReqBit;
            LblPlcAddRstFin.Text = _pd.ResetFinBit.Address = _fs.PlcAddress.ResetFinBit;
            LblPlcAddOk.Text = _pd.OkBit.Address = _fs.PlcAddress.OkBit;
            LblPlcAddNg.Text = _pd.NgBit.Address = _fs.PlcAddress.NgBit;
            LblPlcAddAlarm.Text = _pd.AlarmBit.Address = _fs.PlcAddress.AlarmBit;
            LblPlcAddModel.Text = _pd.Model.Address = _fs.PlcModelNumberAddress;
            _bw.RunWorkerAsync();
        }

        /// <summary>
        /// Flag를 읽어온다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="pd"></param>
        /// <returns></returns>
        private static bool ReadFlags(PlcBase pb, FluxPlcData pd, out string errMsg)
        {
            if (pb.ReadBit(pd.HeartBeat.Address, out bool heartBeat))
            {
                pd.HeartBeat.SetBit(heartBeat);
            }
            else
            {
                errMsg = $"Heartbit 비트 읽기 실패={pd.HeartBeat.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.MeasReqBit.Address, out bool measReqBit))
            {
                pd.MeasReqBit.SetBit(measReqBit);
            }
            else
            {
                errMsg = $"측정요청 비트 읽기 실패={pd.MeasReqBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.MeasFinBit.Address, out bool measFinBit))
            {
                pd.MeasFinBit.SetBit(measFinBit);
            }
            else
            {
                errMsg = $"측정완료 비트 읽기 실패={pd.MeasFinBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.ResetReqBit.Address, out bool resetReq))
            {
                pd.ResetReqBit.SetBit(resetReq);
            }
            else
            {
                errMsg = $"리셋요청 비트 읽기 실패={pd.ResetReqBit.Address}, {pb.LastErrMsg}";
                return false;
            }

            if (pb.ReadBit(pd.ResetFinBit.Address, out bool resetFin))
            {
                pd.ResetFinBit.SetBit(resetFin);
            }
            else
            {
                errMsg = $"리셋완료 비트 읽기 실패={pd.ResetFinBit.Address}, {pb.LastErrMsg}";
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

            if (pb.ReadBit(pd.AlarmBit.Address, out bool alaram))
            {
                pd.AlarmBit.SetBit(alaram);
            }
            else
            {
                errMsg = $"알람 비트 읽기 실패={pd.AlarmBit.Address}, {pb.LastErrMsg}";
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
        /// MES 데이터를 업로드 한다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="pa"></param>
        /// <param name="fd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool MesUpload(PlcBase pb, List<PlcMesInfors> pm, FluxData fd, out DataTable dt, out string errMsg)
        {
            dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Value");

            if (fd.Values.Count == 0)
            {
                errMsg = $"No flux data";
                return false;
            }

            foreach (PlcMesInfors pi in pm.Where(x => x.Enable))
            {
                if (!fd.Values.ContainsKey(pi.DataName))
                {
                    errMsg = $"{pi.DataName} ({pi.Name}) does not exist in Flux data set";
                    return false;
                }

                double val = Convert.ToDouble(fd.Values[pi.DataName]) * pi.Scale;
                dt.Rows.Add(pi.Name, pi.DataName, pi.Address, val);
                if (pi.DataType == PlcMesInfors.MesDataTypes.DWord)
                {
                    if (!pb.WriteDWord(pi.Address, (int)val))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (pi.DataType == PlcMesInfors.MesDataTypes.Word)
                {
                    if (!pb.WriteWord(pi.Address, (short)val))
                    {
                        errMsg = pb.LastErrMsg;
                    }
                }
                else
                {
                    errMsg = $"정의되지 않은 PLC 데이터 타입={pi.DataType.ToString()}";
                    return false;
                }
            }

            errMsg = "";
            return true;
        }

        /// <summary>
        /// MES 데이터를 클리어 한다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="pa"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool MesClear(PlcBase pb, List<PlcMesInfors> pm, out DataTable dt, out string errMsg)
        {
            dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Value");
            foreach (PlcMesInfors pi in pm.Where(x => x.Enable))
            {
                if (pi.DataType == PlcMesInfors.MesDataTypes.DWord)
                {
                    if (!pb.WriteDWord(pi.Address, 0))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (pi.DataType == PlcMesInfors.MesDataTypes.Word)
                {
                    if (!pb.WriteWord(pi.Address, 0))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else
                {
                    errMsg = $"정의되지 않은 PLC 데이터 타입={pi.DataType.ToString()}";
                    return false;
                }
                dt.Rows.Add(pi.Name, pi.DataName, pi.Address, 0);
            }

            errMsg = "";
            return true;
        }
        #endregion
    }
}
