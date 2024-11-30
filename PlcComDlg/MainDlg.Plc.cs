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

using ActUtlTypeLib;
using McProtocol.Mitsubishi;
using McProtocol;

using PlcCom;

namespace PlcComDlg
{
    /// <summary>
    /// SMA-PLC 통신 프로그램
    /// </summary>
    public partial class MainDlg : Form
    {
        #region PLC Worker 제어
        /// <summary>
        /// 2XXX: 워커 동작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_PlcDoWork(object sender, DoWorkEventArgs e)
        {
            PlcOpModes plcMode = PlcOpModes.Connecting;

            // PLC control 인스턴스 생성
            PlcCtrl pct = new PlcCtrl();

            while (plcMode != PlcOpModes.Non)
            {
                // 프로그램 동작 종료 체크
                if (_workerPlc.CancellationPending)
                {
                    break;
                }
                _plcModeRef = plcMode;

                // 2101: PLC 연결 시도
                if (plcMode == PlcOpModes.Connecting)
                {
                    // 일정한 시간 간격으로 연결을 시도한다
                    if (pct.Open(_cs.PlcIp))
                    {
                        // PC 비트를 초기화 한다
                        if (ReadFlag(pct, _cs, ref _plcData.Flags))
                        {
                            _plcData.Flags.Busy = false;
                            _plcData.Flags.MeasFin = false;
                            _plcData.Flags.MeasReqResp = false;

                            if (WriteFlag(pct, _cs, _plcData.Flags))
                            {
                                plcMode = PlcOpModes.Connected;
                                InsertLog("PLC: Connected");
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(_cs.PlcConnRetryInterval);
                    }
                }
                // 2201: PLC로 부터 측정 요청 검출
                else if (plcMode == PlcOpModes.Connected)
                {
                    // 플래그를 읽는다
                    if (!ReadFlag(pct, _cs, ref _plcData.Flags))
                    {
                        InsertLog("PLC: fail to read flag");
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }

                    // TCP 연결 시 하트비트 전송
                    if (_tcpModeRef == TcpOpModes.Connected)
                    {
                        _plcData.Flags.HeartBeat = !_plcData.Flags.HeartBeat;
                    }

                    if (_plcData.Flags.MeasReq == true && _tcpModeRef == TcpOpModes.Connected)
                    {
                        // 모델넘버 읽기
                        if (!pct.ReadWord(_cs.PlcAddModelNumber, out ushort modelNum))
                        {
                            plcMode = PlcOpModes.Connecting;
                            InsertLog("PLC: fail to read model number");
                            continue;
                        }
                        _plcData.PlcModelNumber = modelNum;

                        if (!ReadProductInfor(pct, _cs, out List<ListViewItem> pis, ref _plcData))
                        {
                            InsertLog("PLC: fail to read product infor");
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }

                        LvwProInfor.Invoke((MethodInvoker)delegate ()
                        {
                            LvwProInfor.Items.Clear();
                            LvwProInfor.Items.AddRange(pis.ToArray());
                        });

                        // 플래그 설정 및 전송
                        _plcData.Flags.Ok = _plcData.Flags.Ng = _plcData.Flags.MeasFin = false;
                        _plcData.Flags.MeasReqResp = _plcData.Flags.Busy = true;

                        // TCP 측정 시작
                        _plcData.MeasStartTime = DateTime.Now;
                        _tcpToPlcMeasFin = false;
                        _plcToTcpMeasReq = true;
                        plcMode = PlcOpModes.Measruement;
                        InsertLog("PLC: Start measurement");
                    }
                    else
                    {
                        PlcMonitor(pct, _cs, ref _plcMsgQue, ref _plcData);
                        Thread.Sleep(_cs.PlcUpdateInterval);
                    }

                    // 플래그를 작성한다
                    if (!WriteFlag(pct, _cs, _plcData.Flags))
                    {
                        InsertLog("PLC: fail to write flag");
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }
                }
                // 23XX: SMA 측정 완료 대기
                else if (plcMode == PlcOpModes.Measruement)
                {
                    if (!ReadFlag(pct, _cs, ref _plcData.Flags))
                    {
                        InsertLog("PLC: fail to read flag");
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }

                    // 측정 대기 시 하트비트 전송
                    if (_cs.PlcCtrlHeartBeatDuringMeas)
                    {
                        _plcData.Flags.HeartBeat = !_plcData.Flags.HeartBeat;
                    }

                    TimeSpan ts = DateTime.Now - _plcData.MeasStartTime;

                    // 측정 종료 이벤트 발생
                    if (_tcpToPlcMeasFin == true)
                    {
                        // 측정 성공
                        if (!_tcpMeasErrFlag)
                        {
                            // 2301: 마지막 DB 아이템 로드
                            string strConn = $@"Data Source={_cs.DbPath};Read Only=True;";
                            try
                            {
                                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                                {
                                    conn.Open();
                                    SQLiteCommand cmd;

                                    cmd = new SQLiteCommand(@"select MAX(Id) from sma_measurement", conn);
                                    _plcData.DbId = (long)cmd.ExecuteScalar();

                                    string sql = $"SELECT * FROM sma_measurement WHERE Id = {_plcData.DbId}";
                                    cmd = new SQLiteCommand(sql, conn);
                                    SQLiteDataReader rdr = cmd.ExecuteReader();

                                    var columns = Enumerable.Range(0, rdr.FieldCount).Select(rdr.GetName).ToList();
                                    for (int i = 0; i < _cs.DbColumns.Count; i++)
                                    {
                                        if (columns.Find(n => n == _cs.DbColumns[i]) == null)
                                        {
                                            throw new Exception($"DB 테이블에 {_cs.DbColumns[i]} 필드가 존재하지 않습니다");
                                        }
                                    }

                                    while (rdr.Read())
                                    {
                                        _plcData.DbPass = Convert.ToInt64(rdr["pass"]);
                                        _plcData.DbFailedId = Convert.ToInt64(rdr["failed_id"]);

                                        _plcData.DbMesVals.Clear();
                                        foreach (string name in _cs.DbColumns)
                                        {
                                            PlcData.MesDatum md = new PlcData.MesDatum();
                                            md.Name = name;
                                            md.MeasValue = Convert.ToDouble(rdr[name]);
                                            md.MesValue = (long)(_cs.PlcMesDataScale * md.MeasValue);
                                            _plcData.DbMesVals.Add(md);
                                        }
                                        break;
                                    }
                                    rdr.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                string errMsg = $"PLC: DB 데이터 로드 오류로 인한 중단: {ex.Message}";
                                MessageBox.Show(errMsg);
                                LblSmaError.Invoke((MethodInvoker)delegate ()
                                {
                                    LblSmaError.Text = "DB ERR";
                                    LblSmaError.BackColor = Color.OrangeRed;
                                });
                                InsertLog(errMsg, 2300);
                            }

                            // 2302: MES Data 업로드
                            int mesLen = _plcData.DbMesVals.Count;
                            int[] data = new int [mesLen];
                            for (int i = 0; i < mesLen; i++)
                            {
                                data[i] = (int)(_plcData.DbMesVals[i].MeasValue * _cs.PlcMesDataScale);
                            }
                            pct.WriteWords(_cs.PlcMesStartAddress, data);

                            // OK/NG/측정완료 플래그 설정
                            _plcData.Flags.Ok = _plcData.DbPass != 0;
                            _plcData.Flags.Ng = _plcData.DbPass == 0;
                            _plcData.Flags.Alarm = false;

                            LblMesOk.Invoke((MethodInvoker)delegate ()
                            {
                                LblMesOk.Text = _plcData.DbPass > 0 ? "OK" : "NG";
                            });
                            InsertLog("PLC: Measurement success");
                        }
                        // 측정 실패
                        else
                        {
                            _plcData.Flags.Ok = false;
                            _plcData.Flags.Ng = false;
                            _plcData.Flags.Alarm = false;
                            InsertLog($"PLC: Measurement failure - {_tcpMeasErrMsg}");
                        }

                        _plcData.Flags.Busy = false;
                        _plcData.Flags.MeasFin = true;

                        if (!WriteFlag(pct, _cs, _plcData.Flags))
                        {
                            plcMode = PlcOpModes.Connecting;
                            continue;
                        }

                        // 라벨 출력
                        LblMesOk.Invoke((MethodInvoker)delegate ()
                        {
                            if (!_tcpMeasErrFlag)
                            {
                                LblMesOkVal.Text = _plcData.DbPass > 0 ? "OK" : "NG";
                                LblMesOkVal.BackColor = _plcData.DbPass > 0 ? Color.Lime : DefaultBackColor;
                            }
                            else
                            {
                                LblMesOkVal.Text = "-";
                                LblMesOkVal.BackColor = DefaultBackColor;
                            }
                        });
                        LblFailedIdVal.Invoke((MethodInvoker)delegate ()
                        {
                            if (!_tcpMeasErrFlag)
                            {
                                LblFailedIdVal.Text = $"{ _plcData.DbFailedId}";
                            }
                            else
                            {
                                LblFailedIdVal.Text = $"-";
                            }
                                
                        });
                        LvwDb.Invoke((MethodInvoker)delegate ()
                        {
                            LvwDb.Items.Clear();
                            if (!_tcpMeasErrFlag)
                            {
                                for (int i = 0; i < _plcData.DbMesVals.Count; i++)
                                {
                                    ListViewItem lvi = new ListViewItem(new string[]
                                    {
                                    _plcData.DbMesVals[i].Name,
                                    _plcData.DbMesVals[i].MeasValue.ToString("f3"),
                                    _plcData.DbMesVals[i].MesValue.ToString("n0")
                                    });
                                    LvwDb.Items.Add(lvi);
                                }
                            }
                        });
                        LblSmaError.Invoke((MethodInvoker)delegate ()
                        {
                            if (!_tcpMeasErrFlag)
                            {
                                LblSmaError.Text = "No error";
                                LblSmaError.BackColor = DefaultBackColor;
                            }
                            else
                            {
                                LblSmaError.Text = _tcpMeasErrMsg;
                                LblSmaError.BackColor = Color.OrangeRed;
                            }
                        });
                        plcMode = PlcOpModes.Connected;
                    }
                    // 측정 타임아웃 - 프로그래밍 오류
                    else if (ts.Seconds > _cs.PlcMeasFinWaitTimeOut)
                    {
                        _plcData.Flags.Alarm = true;
                        _plcData.Flags.Busy = false;
                        _plcData.Flags.MeasFin = true;

                        LblSmaError.Invoke((MethodInvoker)delegate ()
                        {
                            LblMesOkVal.Text = "ALM:PLC";
                            LblSmaError.BackColor = Color.OrangeRed;
                        });
                        InsertLog($"PLC: Measurement failure - timeout");
                        plcMode = PlcOpModes.Connected;
                    }
                    // 기타
                    else
                    {
                        PlcMonitor(pct, _cs, ref _plcMsgQue, ref _plcData);
                        Thread.Sleep(_cs.PlcUpdateInterval);
                    }

                    if (!WriteFlag(pct, _cs, _plcData.Flags))
                    {
                        InsertLog("PLC: fail to write flag");
                        plcMode = PlcOpModes.Connecting;
                        continue;
                    }
                }

                // GUI 업데이트
                _workerPlc.ReportProgress((int)plcMode);
            }

            // 프로그램 종료
            pct.Close();
            while (_plcMsgQue.Count > 0)
            {
                _plcMsgQue.TryDequeue(out PlcData.ComMsg dummyMsg);
            }
            _plcModeRef = PlcOpModes.Non;
        }
        #endregion

        #region PLC Worker 이벤트
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

            // 업데이트
            LblModelNumVal.Text = $"{_plcData.PlcModelNumber}";
            LblDbIdVal.Text = $"{_plcData.DbId}";

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

            InsertLog("PLC: the process is terminated");
        }
        #endregion

        #region PLC Woker 메소드
        /// <summary>
        /// PLC 모니터 함수
        /// </summary>
        /// <param name="pct"></param>
        /// <param name="cs"></param>
        /// <param name="istLog"></param>
        /// <param name="pq"></param>
        /// <param name="pd"></param>
        /// <returns></returns>
        private bool PlcMonitor(PlcCtrl pct, ComSettings cs, ref ConcurrentQueue<PlcData.ComMsg> pq, ref PlcData pd)
        {
            if (pq.TryDequeue(out PlcData.ComMsg msg))
            {

                if (msg.MsgType == PlcData.ComMsg.MsgTypes.GetModelNumber)
                {
                    if (!pct.ReadWord(cs.PlcAddModelNumber, out ushort modelNum))
                    {
                        throw new Exception($"모델 넘버 읽기 실패");
                    }
                    pd.PlcModelNumber = modelNum;
                }
                else if (msg.MsgType == PlcData.ComMsg.MsgTypes.GetProductInfor)
                {
                    if (!ReadProductInfor(pct, cs, out List<ListViewItem> pis, ref pd))
                    {
                        return false;
                    }

                    LvwProInfor.Invoke((MethodInvoker)delegate ()
                    {
                        LvwProInfor.Items.Clear();
                        LvwProInfor.Items.AddRange(pis.ToArray());
                    });
                }
                else if (msg.MsgType == PlcData.ComMsg.MsgTypes.ToggleMeasReq)
                {
                    pd.Flags.MeasReq = !pd.Flags.MeasReq;
                }
            }
            return true;
        }

        /// <summary>
        /// Flag를 읽어온다
        /// </summary>
        /// <param name="pct"></param>
        /// <param name="cs"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static bool ReadFlag(PlcCtrl pct, ComSettings cs, ref PlcData.CtrlFlags cf)
        {
            bool bit;

            if (!pct.ReadBit(cs.PlcAddMeasReqBit, out bit))
            {
                return false;
            }
            cf.MeasReq = bit;

            if (!pct.ReadBit(cs.PlcAddHeartBeatBit, out bit))
            {
                return false;
            }
            cf.HeartBeat = bit;
            if (!pct.ReadBit(cs.PlcAddOkBit, out bit))
            {
                return false;
            }
            cf.Ok = bit;
            if (!pct.ReadBit(cs.PlcAddNgBit, out bit))
            {
                return false;
            }
            cf.Ng = bit;
            if (!pct.ReadBit(cs.PlcAddBusyBit, out bit))
            {
                return false;
            }
            cf.Busy = bit;
            if (!pct.ReadBit(cs.PlcAddMeasFinBit, out bit))
            {
                return false;
            }
            cf.MeasFin = bit;
            if (!pct.ReadBit(cs.PlcAddAlarmBit, out bit))
            {
                return false;
            }
            cf.Alarm = bit;

            return true;
        }


        /// <summary>
        /// Flag를 PLC에 작성한다
        /// </summary>
        /// <param name="pct"></param>
        /// <param name="cs"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static bool WriteFlag(PlcCtrl pct, ComSettings cs, PlcData.CtrlFlags cf)
        {
            if (!pct.WriteBit(cs.PlcAddMeasReqBit, cf.MeasReq))
            {
                return false;
            }

            if (!pct.WriteBit(cs.PlcAddHeartBeatBit, cf.HeartBeat))
            {
                return false;
            }
            if (!pct.WriteBit(cs.PlcAddOkBit, cf.Ok))
            {
                return false;
            }
            if (!pct.WriteBit(cs.PlcAddNgBit, cf.Ng))
            {
                return false;
            }
            if (!pct.WriteBit(cs.PlcAddBusyBit, cf.Busy))
            {
                return false;
            }
            if (!pct.WriteBit(cs.PlcAddMeasFinBit, cf.MeasFin))
            {
                return false;
            }
            if (!pct.WriteBit(cs.PlcAddAlarmBit, cf.Alarm))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// product 정보를 읽어온다
        /// </summary>
        /// <param name="pct"></param>
        /// <param name="cs"></param>
        /// <param name="pis"></param>
        /// <param name="plcData"></param>
        /// <returns></returns>
        public static bool ReadProductInfor(PlcCtrl pct, ComSettings cs, out List<ListViewItem> pis, ref PlcData plcData)
        {
            plcData.ProductInforVals.Clear();
            pis = new List<ListViewItem>();
            for (int i = 0; i < cs.ProductInfors.Count; i++)
            {
                ListViewItem lvItem = new ListViewItem(cs.ProductInfors[i].Name);
                
                // 문자 읽기
                if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Text)
                {
                    if (!pct.ReadBytes(cs.ProductInfors[i].DataStartAddress, cs.ProductInfors[i].DataLength, out byte[] data))
                    {
                        return false;
                    }

                    string msg = Encoding.Default.GetString(data) + Environment.NewLine;
                    plcData.ProductInforVals.Add(msg);
                    lvItem.SubItems.Add($"{cs.ProductInfors[i].DataStartAddress}({cs.ProductInfors[i].DataLength})");
                    lvItem.SubItems.Add(msg);
                }
                // 정수 값 읽기
                else if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Word)
                {
                    if (!pct.ReadWord(cs.ProductInfors[i].DataStartAddress, out ushort data))
                    {
                        return false;
                    }
                    plcData.ProductInforVals.Add(data);
                    lvItem.SubItems.Add($"{cs.ProductInfors[i].DataStartAddress}");
                    lvItem.SubItems.Add(data.ToString());
                }
                else if (cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.DWord)
                {
                    if (!pct.ReadDWord(cs.ProductInfors[i].DataStartAddress, out uint data))
                    {
                        return false;
                    }
                    plcData.ProductInforVals.Add(data);
                    lvItem.SubItems.Add($"{cs.ProductInfors[i].DataStartAddress}");
                    lvItem.SubItems.Add(data.ToString());
                }
                // 코딩 에러
                else
                {
                    return false;
                }
                pis.Add(lvItem);
            }
            return true;
        }
        #endregion
    }
}
