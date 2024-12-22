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

namespace PlcComDlg
{
    /// <summary>
    /// 2000.SMA-PLC 통신 프로그램
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
            /// 모델 넘버 읽음
            /// </summary>
            ReadModelNumber,
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
        private ConcurrentQueue<PlcGuiEvents> _plcGuiUpdateQue = new ConcurrentQueue<PlcGuiEvents>();
        #endregion

        #region 21000.Plc.Worker
        /// <summary>
        /// 2XXX: 워커 동작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_PlcDoWork(object sender, DoWorkEventArgs e)
        {
            #region 21100.Plc.Worker.초기화
            PlcOpModes plcMode = PlcOpModes.Connecting;

            // PLC 제어 오브젝트 생성
            PlcBase pb;
            switch (_cs.PlcType)
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
                default:
                        throw new NotImplementedException();
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
                    if (!pb.ConvertOpenParameter(_cs.PlcConnectionParam, out object item))
                    {
                        InsertLog("Fail to parse the open parameter", LogMsg.Sources.PLC, 21200, pb.LastErrMsg);
                        break;
                    }

                    // 일정한 시간 간격으로 연결을 시도한다
                    if (pb.Open(_cs.PlcConnectionParam))
                    {
                        // PC 비트를 초기화 한다
                        if (ReadFlag(pb, _cs, ref _plcData.Flags))
                        {
                            _plcData.Flags.Busy = false;
                            _plcData.Flags.MeasFin = false;
                            _plcData.Flags.MeasReqResp = false;

                            if (WriteFlag(pb, _cs, _plcData.Flags))
                            {
                                plcMode = PlcOpModes.Connected;
                                InsertLog("Connected", LogMsg.Sources.PLC);
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(_cs.PlcConnRetryInterval);
                    }
                }
                // 21300.Plc.Woker.제어-측정요청 검출
                else if (plcMode == PlcOpModes.Connected)
                {
                    // 플래그 읽기
                    if (!ReadFlag(pb, _cs, ref _plcData.Flags))
                    {
                        InsertLog($"Faile to read flage", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }

                    if (_plcData.Flags.MeasReq == true && _tcpModeRef == TcpOpModes.Connected)
                    {
                        // 모델넘버 읽기
                        if (!pb.ReadWord(_cs.PlcAddModelNumber, out short modelNum))
                        {
                            InsertLog($"Faile to read a model number", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }
                        _plcData.PlcModelNumber = modelNum;
                        _plcGuiUpdateQue.Enqueue(PlcGuiEvents.ReadModelNumber);

                        if (!ReadProductInfor(pb, _cs, ref _plcData, out string errMsg))
                        {
                            InsertLog($"Faile to read product information", LogMsg.Sources.PLC, 21300, errMsg);
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }

                        // 플래그 설정 및 전송
                        _plcData.Flags.Ok = _plcData.Flags.Ng = _plcData.Flags.MeasFin = false;
                        _plcData.Flags.MeasReqResp = _plcData.Flags.Busy = true;

                        // TCP 측정 시작
                        _plcData.MeasStartTime = DateTime.Now;
                        _tcpToPlcMeasFin = false;
                        _plcToTcpMeasReq = true;
                        plcMode = PlcOpModes.Measruement;
                        InsertLog("Start measurement", LogMsg.Sources.PLC);
                    }
                    else
                    {                    
                        // TCP 연결 시 하트비트
                        if (_tcpModeRef == TcpOpModes.Connected)
                        {
                            _plcData.Flags.HeartBeat = !_plcData.Flags.HeartBeat;
                        }

                        // 모니터 업무 처리
                        if (!PlcMonitor(pb, _cs, ref _plcMsgQue, ref _plcData, out string errMsg))
                        {
                            InsertLog("Fail to PLC monitor", LogMsg.Sources.PLC, 21300, errMsg);
                        }
                        Thread.Sleep(_cs.PlcUpdateInterval);
                    }

                    // 플래그 작성
                    if (!WriteFlag(pb, _cs, _plcData.Flags))
                    {
                        InsertLog($"Fail to write flags", LogMsg.Sources.PLC, 21300, pb.LastErrMsg);
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }
                }
                // 21400.Plc.Woker.제어-측정완료 대기               
                else if (plcMode == PlcOpModes.Measruement)
                {
                    // 플래그 읽기
                    if (!ReadFlag(pb, _cs, ref _plcData.Flags))
                    {
                        InsertLog($"Fail to read flags", LogMsg.Sources.PLC, 21400);
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }


                    TimeSpan ts = DateTime.Now - _plcData.MeasStartTime;
                    // 측정 종료 이벤트 발생
                    if (_tcpToPlcMeasFin == true)
                    {
                        _plcData.Flags.Busy = false;
                        _plcData.Flags.MeasFin = true;

                        // 측정 성공
                        if (!_tcpMeasErrFlag)
                        {
                            InsertLog("Measurement finished", LogMsg.Sources.PLC);

                            string errMsg;
                            if (!ReadLastItemFromDb(_cs, ref _plcData, out errMsg))
                            {
                                InsertLog($"Fail to read DB", LogMsg.Sources.DB, 21400, errMsg);
                            }

                            if (!WriteMesData(pb, _cs, _plcData, out errMsg))
                            {
                                InsertLog($"Fail to upload MES data", LogMsg.Sources.PLC, 21400, errMsg);
                            }

                            // OK/NG/측정완료 플래그 설정
                            _plcData.Flags.Ok = _plcData.DbPass != 0;
                            _plcData.Flags.Ng = _plcData.DbPass == 0;
                            _plcData.Flags.Alarm = false;
                          
                        }
                        // 측정 실패
                        else
                        {
                            InsertLog("Measurement failure", LogMsg.Sources.PLC, 21400, _tcpMeasErrMsg);
                            _plcData.Flags.Ok = false;
                            _plcData.Flags.Ng = false;
                            _plcData.Flags.Alarm = true;
                        }

                        _plcGuiUpdateQue.Enqueue(PlcGuiEvents.MesUpload);

                        if (!WriteFlag(pb, _cs, _plcData.Flags))
                        {
                            InsertLog("Fail to write flags", LogMsg.Sources.PLC, 21400, pb.LastErrMsg);
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }
                        plcMode = PlcOpModes.Connected;
                    }
                    // 측정 타임아웃 - 프로그래밍 오류
                    else if (ts.Seconds > _cs.PlcMeasFinWaitTimeOut)
                    {
                        _plcData.Flags.Alarm = true;
                        _plcData.Flags.Busy = false;
                        _plcData.Flags.MeasFin = true;
                        InsertLog($"Measurement timeout", LogMsg.Sources.PLC, 21400, $"{ts.Seconds:f3}/{_cs.PlcMeasFinWaitTimeOut:f3} sec");
                        plcMode = PlcOpModes.Connected;
                    }
                    // 측정 대기
                    else
                    {
                        // 옵션: 측정 대기 시 하트비트 전송
                        if (_cs.PlcCtrlHeartBeatDuringMeas)
                        {
                            _plcData.Flags.HeartBeat = !_plcData.Flags.HeartBeat;
                        }

                        if (!PlcMonitor(pb, _cs, ref _plcMsgQue, ref _plcData, out string errMsg))
                        {
                            InsertLog("Fail to PLC monitor", LogMsg.Sources.PLC, 21400, errMsg);
                        }
                        Thread.Sleep(_cs.PlcUpdateInterval);
                    }

                    // 플래그 작성
                    if (!WriteFlag(pb, _cs, _plcData.Flags))
                    {
                        InsertLog($"Fail to write flags", LogMsg.Sources.PLC, 21400, pb.LastErrMsg);
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }
                }
                // GUI 업데이트
                _workerPlc.ReportProgress((int)plcMode);
            }

            // 프로그램 종료
            pb.Close();
            while (_plcMsgQue.Count > 0)
            {
                _plcMsgQue.TryDequeue(out PlcData.ComMsg dummyMsg);
            }
            _plcModeRef = PlcOpModes.Non;
        }
        #endregion

        #region 22000.Plc.Woker.이벤트
        /// <summary>
        /// PLC 워커 시작
        /// </summary>
        private void StartPlcWorker()
        {
            // Worker 설정
            _workerPlc = new BackgroundWorker();
            _workerPlc.WorkerReportsProgress = true;
            _workerPlc.WorkerSupportsCancellation = true;
            _workerPlc.DoWork += new DoWorkEventHandler(Worker_PlcDoWork);
            _workerPlc.ProgressChanged += new ProgressChangedEventHandler(Worker_PlcProgressChanged);
            _workerPlc.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_PlcRunWorkerCompleted);

            // GUI 업데이트
            BtnPlcStart.Text = "PLC Stop";

            LblPlcAddMeasReq.Text = $"{_cs.PlcAddMeasReqBit}";
            LblPlcAddMeasReqResp.Text = $"{_cs.PlcAddMeasReqRespBit}";
            LblPlcAddHeartBeat.Text = $"{_cs.PlcAddHeartBeatBit}";
            LblPlcAddMeasFin.Text = $"{_cs.PlcAddMeasFinBit}";
            LblPlcAddOk.Text = $"{_cs.PlcAddOkBit}";
            LblPlcAddNg.Text = $"{_cs.PlcAddNgBit}";
            LblPlcAddBusy.Text = $"{_cs.PlcAddBusyBit}";
            LblPlcAddAlarm.Text = $"{_cs.PlcAddAlarmBit}";
            LblModelAdd.Text = _cs.PlcAddModelNumber;
            LblHeartBeatIntVal.Text = $"{_cs.PlcUpdateInterval} (ms)";
            LblMeasTimeVal.Text = $"{0:f1}/{_cs.PlcMeasFinWaitTimeOut:f1} (sec)";

            _workerPlc.RunWorkerAsync();
        }

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
                LblPlcAddMeasReq.BackColor = _plcData.Flags.MeasReq ? Color.Lime : DefaultBackColor;
                LblPlcAddMeasReqResp.BackColor = _plcData.Flags.MeasReqResp ? Color.Lime : DefaultBackColor;
                LblPlcAddMeasFin.BackColor = _plcData.Flags.MeasFin ? Color.Lime : DefaultBackColor;
                LblPlcAddHeartBeat.BackColor = _plcData.Flags.HeartBeat ? Color.Lime : DefaultBackColor;
                LblPlcAddOk.BackColor = _plcData.Flags.Ok ? Color.Lime : DefaultBackColor;
                LblPlcAddNg.BackColor = _plcData.Flags.Ng ? Color.Lime : DefaultBackColor;
                LblPlcAddBusy.BackColor = _plcData.Flags.Busy ? Color.Lime : DefaultBackColor;
                LblPlcAddAlarm.BackColor = _plcData.Flags.Alarm ? Color.Lime : DefaultBackColor;
            }
            else
            {
                LblPlcAddMeasReq.BackColor = LblPlcAddMeasReqResp.BackColor = LblPlcAddMeasFin.BackColor = LblPlcAddHeartBeat.BackColor = LblPlcAddOk.BackColor = LblPlcAddNg.BackColor = DefaultBackColor;
            }

            // 진행상황 시간 업데이트
            TimeSpan ts;
            if (_plcModeRef == PlcOpModes.Connecting)
            {
                LblPlcCon.Text = "Connecting";
                LblPlcCon.BackColor = Color.OrangeRed;
            }
            else if (_plcModeRef == PlcOpModes.Connected)
            {
                LblPlcCon.Text = "Ready";
                LblPlcCon.BackColor = Color.Lime;
            }
            else if (_plcModeRef == PlcOpModes.Measruement)
            {
                LblPlcCon.Text = "Measurement";
                LblPlcCon.BackColor = Color.Orange;
                ts = DateTime.Now - _plcData.MeasStartTime;
                LblMeasTimeVal.Text = $"{ts.TotalSeconds:f1}/{_cs.PlcMeasFinWaitTimeOut:f1} (sec)";
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
        /// PLC 모니터 함수
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="pq"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool PlcMonitor(PlcBase pb, ComSettings cs, ref ConcurrentQueue<PlcData.ComMsg> pq, ref PlcData pd, out string errMsg)
        {
            errMsg = "";
            if (pq.TryDequeue(out PlcData.ComMsg msg))
            {
                if (msg.MsgType == PlcData.ComMsg.MsgTypes.GetModelNumber)
                {
                    if (!pb.ReadWord(cs.PlcAddModelNumber, out short modelNum))
                    {
                        errMsg = $"모델 넘버 읽기 실패: {pb.LastErrMsg}";
                        return false;
                    }
                    pd.PlcModelNumber = modelNum;
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.ReadModelNumber);
                    InsertLog(PlcData.ComMsg.MsgTypes.GetModelNumber.ToString(), LogMsg.Sources.PLC);
                }
                else if (msg.MsgType == PlcData.ComMsg.MsgTypes.GetProductInfor)
                {
                    if (!ReadProductInfor(pb, cs, ref pd, out string errMsg1))
                    {
                        errMsg = errMsg1;
                        return false;
                    }
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.ReadProductInfor);
                    InsertLog(PlcData.ComMsg.MsgTypes.GetProductInfor.ToString(), LogMsg.Sources.PLC);
                }
                else if (msg.MsgType == PlcData.ComMsg.MsgTypes.ToggleMeasReq)
                {
                    pd.Flags.MeasReq = !pd.Flags.MeasReq;
                }
                else if (msg.MsgType == PlcData.ComMsg.MsgTypes.UploadMesData)
                {
                    string errMsg1;
                    if (!ReadLastItemFromDb(cs, ref pd, out errMsg1))
                    {
                        errMsg = $"DB 정보 읽기 실패: {errMsg1}";
                        return false;
                    }
                    InsertLog("READ DB", LogMsg.Sources.DB);

                    if (!WriteMesData(pb, cs, pd, out errMsg1))
                    {
                        errMsg = $"MES 업로드 실패: {errMsg1}";
                        return false;
                    }
                    _plcGuiUpdateQue.Enqueue(PlcGuiEvents.MesUpload);
                    InsertLog(PlcData.ComMsg.MsgTypes.UploadMesData.ToString(), LogMsg.Sources.PLC);
                }
            }
            return true;
        }

        /// <summary>
        /// Flag를 읽어온다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static bool ReadFlag(PlcBase pb, ComSettings cs, ref PlcData.CtrlFlags cf)
        {
            bool bit;

            if (!pb.ReadBit(cs.PlcAddMeasReqBit, out bit))
            {
                return false;
            }
            cf.MeasReq = bit;

            if (!pb.ReadBit(cs.PlcAddHeartBeatBit, out bit))
            {
                return false;
            }
            cf.HeartBeat = bit;
            if (!pb.ReadBit(cs.PlcAddOkBit, out bit))
            {
                return false;
            }
            cf.Ok = bit;
            if (!pb.ReadBit(cs.PlcAddNgBit, out bit))
            {
                return false;
            }
            cf.Ng = bit;
            if (!pb.ReadBit(cs.PlcAddBusyBit, out bit))
            {
                return false;
            }
            cf.Busy = bit;
            if (!pb.ReadBit(cs.PlcAddMeasFinBit, out bit))
            {
                return false;
            }
            cf.MeasFin = bit;
            if (!pb.ReadBit(cs.PlcAddAlarmBit, out bit))
            {
                return false;
            }
            cf.Alarm = bit;

            return true;
        }

        /// <summary>
        /// Flag를 PLC에 작성한다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static bool WriteFlag(PlcBase pb, ComSettings cs, PlcData.CtrlFlags cf)
        {
            if (!pb.WriteBit(cs.PlcAddMeasReqBit, cf.MeasReq))
            {
                return false;
            }

            if (!pb.WriteBit(cs.PlcAddHeartBeatBit, cf.HeartBeat))
            {
                return false;
            }
            if (!pb.WriteBit(cs.PlcAddOkBit, cf.Ok))
            {
                return false;
            }
            if (!pb.WriteBit(cs.PlcAddNgBit, cf.Ng))
            {
                return false;
            }
            if (!pb.WriteBit(cs.PlcAddBusyBit, cf.Busy))
            {
                return false;
            }
            if (!pb.WriteBit(cs.PlcAddMeasFinBit, cf.MeasFin))
            {
                return false;
            }
            if (!pb.WriteBit(cs.PlcAddAlarmBit, cf.Alarm))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// product 정보를 읽어온다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="pis"></param>
        /// <param name="plcData"></param>
        /// <returns></returns>
        public static bool ReadProductInfor(PlcBase pb, ComSettings cs,  ref PlcData plcData, out string errMsg)
        {
            errMsg = "";
            plcData.ProductInforVals.Clear();
            for (int i = 0; i < cs.ProductInfors.Count; i++)
            {
                // 문자 읽기
                if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Text)
                {
                    if (!pb.ReadBytes(cs.ProductInfors[i].DataStartAddress, cs.ProductInfors[i].DataLength, out byte[] data))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }

                    string msg = Encoding.Default.GetString(data) + Environment.NewLine;
                    plcData.ProductInforVals.Add(msg);
                }
                // 정수 값 읽기
                else if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Word)
                {
                    if (!pb.ReadWord(cs.ProductInfors[i].DataStartAddress, out short data))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                    plcData.ProductInforVals.Add(data);
                }
                else if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.DWord)
                {
                    if (!pb.ReadDWord(cs.ProductInfors[i].DataStartAddress, out int data))
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                    plcData.ProductInforVals.Add(data);
                }
                // 코딩 에러
                else
                {
                    errMsg = "Undefined product data type";
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// DB에서 마지막 아이템을 읽어온다
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool ReadLastItemFromDb(ComSettings cs, ref PlcData pd, out string errMsg)
        {
            string strConn = $@"Data Source={cs.DbPath};Read Only=True;";
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
                    for (int i = 0; i < cs.DbColumns.Count; i++)
                    {
                        if (columns.Find(n => n == cs.DbColumns[i]) == null)
                        {
                            throw new Exception($"DB 테이블에 {cs.DbColumns[i]} 필드가 존재하지 않습니다");
                        }
                    }

                    while (rdr.Read())
                    {
                        pd.DbPass = Convert.ToInt64(rdr["pass"]);
                        pd.DbFailedId = Convert.ToInt64(rdr["failed_id"]);

                        pd.DbMesVals.Clear();
                        foreach (string name in cs.DbColumns)
                        {
                            PlcData.MesDatum md = new PlcData.MesDatum();
                            md.Name = name;
                            if (DBNull.Value != rdr[name])
                            {
                                md.MeasValue = Convert.ToDouble(rdr[name]);
                            }
                            else
                            {
                                md.MeasValue = 0;
                            }
                            md.MesValue = (long)(cs.PlcMesDataScale * md.MeasValue);
                            pd.DbMesVals.Add(md);
                        }
                        break;
                    }
                    rdr.Close();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = $"PLC: DB 데이터 로드 오류로 인한 중단: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// MES 데이터 업로드
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="cs"></param>
        /// <param name="plcData"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool WriteMesData(PlcBase pb, ComSettings cs, PlcData plcData, out string errMsg)
        {
            errMsg = "";
            try
            {
                // 2302: MES Data 업로드
                int mesLen = plcData.DbMesVals.Count;
                int[] data = new int[mesLen];
                for (int i = 0; i < mesLen; i++)
                {
                    data[i] = (int)(plcData.DbMesVals[i].MeasValue * cs.PlcMesDataScale);
                }

                if (!pb.WriteDWords(cs.PlcMesStartAddress, data))
                {
                    errMsg = pb.LastErrMsg;
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }
        #endregion
    }
}
