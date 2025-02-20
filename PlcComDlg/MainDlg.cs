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
    /// 1000.주 프로그램
    /// </summary>
    public partial class MainDlg : Form
    {
        #region 타입 정의
        /// <summary>
        /// PLC 동작 모드
        /// </summary>
        public enum PlcOpModes
        {
            /// <summary>
            /// 아무것도 하지 않음
            /// </summary>
            Non,
            /// <summary>
            /// PLC와 연결
            /// </summary>
            Connecting,
            /// <summary>
            /// TCP와 연결
            /// </summary>
            Connected,
            /// <summary>
            /// 측정 완료 대기
            /// </summary>
            Measruement,
        }

        /// <summary>
        /// TCP 동작 모드
        /// </summary>
        public enum TcpOpModes
        {
            /// <summary>
            /// 아무것도 하지 않음
            /// </summary>
            Non,
            /// <summary>
            /// 연결
            /// </summary>
            Connecting,
            /// <summary>
            /// TCP와 연결
            /// </summary>
            Connected,
            /// <summary>
            /// 측정 완료 대기
            /// </summary>
            Measurement,
        }
        #endregion

        #region 변수 선언
        /// <summary>
        /// PLC 백그라운드 워커
        /// </summary>
        private BackgroundWorker _workerPlc = new BackgroundWorker();

        /// <summary>
        /// PLC 데이터
        /// </summary>
        private PlcData _plcData = new PlcData();

        /// <summary>
        /// PLC 동작 모드
        /// </summary>
        private PlcOpModes _plcModeRef = PlcOpModes.Non;

        /// <summary>
        /// TCP 백그라운드 워커
        /// </summary>
        private BackgroundWorker _workerTcp = new BackgroundWorker();

        /// <summary>
        /// TCP 데이터
        /// </summary>
        private TcpData _tcpData = new TcpData();

        /// <summary>
        /// TCP 동작 모드
        /// </summary>
        private TcpOpModes _tcpModeRef { get; set; } = TcpOpModes.Non;

        /// <summary>
        /// 통신 설정
        /// </summary>
        private ComSettings _cs;

        /// <summary>
        /// 로그 DB 소스
        /// </summary>
        private string _logSource;

        /// <summary>
        /// PLC message queue
        /// </summary>
        private ConcurrentQueue<PlcData.ComMsg> _plcMsgQue = new ConcurrentQueue<PlcData.ComMsg>();

        /// <summary>
        /// TCP message queue
        /// </summary>
        private ConcurrentQueue<TcpData.ComMsg> _tcpMsgQue = new ConcurrentQueue<TcpData.ComMsg>();

        /// <summary>
        /// 로그 메시지를 저장하는 que
        /// </summary>
        private ConcurrentQueue<LogMsg> _logQue = new ConcurrentQueue<LogMsg>();

        /// <summary>
        /// TCP에 측정을 요청한다
        /// </summary>
        private bool _plcToTcpMeasReq { get; set; } = false;

        /// <summary>
        /// PLC에 측정을 완료한다
        /// </summary>
        private bool _tcpToPlcMeasFin { get; set; } = false;

        /// <summary>
        /// TCP 측정 에러 플래그
        /// </summary>
        private bool _tcpMeasErrFlag { get; set; } = false;

        /// <summary>
        /// TCP 측정 에러 메시지
        /// </summary>
        private string _tcpMeasErrMsg { get; set; } = "";

        /// <summary>
        /// 프로그램을 종료하는 플래그
        /// </summary>
        private bool _closeApp { get; set; } = false;
        #endregion

        #region 11000.이벤트.초기화
        /// <summary>
        /// 폼 초기화
        /// </summary>
        public MainDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 1000: 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainDlg_Load(object sender, EventArgs e)
        {
            // 중복실행 방지
            string currProcess = Process.GetCurrentProcess().ProcessName.ToUpper();
            Process[] pcs = Process.GetProcessesByName(currProcess);
            if (pcs.Length > 1)
            {
                AutoClosingMessageBox.Show("프로그램이 이미 실행중입니다", "Warning", 3000);
                Application.Exit();
            }

            // 1002: 로그용 DB 파일 생성
            string logPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "log.db");
            _logSource = @"Data Source=" + logPath;
            if (!File.Exists(logPath))
            {
                try
                {
                    SQLiteConnection.CreateFile(logPath);
                    using (SQLiteConnection conn = new SQLiteConnection(_logSource))
                    {
                        conn.Open();
                        string sql = @"CREATE TABLE ""log"" (
                        ""id""    INTEGER NOT NULL UNIQUE,
                        ""labdate""   NUMERIC,
	                    ""code""  INTEGER,
	                    ""message""   TEXT,
	                    PRIMARY KEY(""id"" AUTOINCREMENT)
                        );";
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    TsslLoadLog.BackColor = Color.Lime;
                }
                catch (Exception)
                {
                    TsslLoadLog.BackColor = Color.Orange;
                }
            }
            else
            {
                TsslLoadLog.BackColor = Color.Lime;
            }

            // 설정 로드
            if (File.Exists(ComSettings.FilePath))
            {
                if (LoadSettings(out string errMsg))
                {
                    TsslLoadSettings.BackColor = Color.Lime;
                }
                else
                {
                    TsslLoadSettings.BackColor = Color.OrangeRed;
                }
            }
            else
            {
                _cs = new ComSettings();
                _cs.DbColumns.AddRange(new string[] { "SPM A MAX", "SPM A MIN", "SPM A AVR", "SPM A RNG", "SPM A STD", "SPM Zd MAX", "SPM Zd MIN", "SPM Zd AVR", "SPM Zd STD", "SPM Zd RNG", "pass", "failed_id" });
                if (!SaveComSettings(_cs, ComSettings.FilePath, out string errMsg))
                {
                    InsertLog("Fail to create settings file", LogMsg.Sources.APP, 11000, errMsg);
                }
                
                TsslLoadSettings.BackColor = Color.Yellow;
            }
            PpgSettings.SelectedObject = _cs;

            TmrSmaCheck.Enabled = true;

            InsertLog("Application start", LogMsg.Sources.APP);

            // 옵션: 프로그램 자동 시작
            if (_cs.AutoStart)
            {
                InsertLog("Option-Auto start", LogMsg.Sources.APP);
                StartPlcWorker();
                StartTcpWorker();
            }
        }

        /// <summary>
        /// 폼 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            //            Properties.Settings.Default.DlgPosition = Location;
            Properties.Settings.Default.Save();

            if (_workerPlc.IsBusy)
            {
                _workerPlc.CancelAsync();
            }
            if (_workerTcp.IsBusy)
            {
                _workerTcp.CancelAsync();
            }

            while (_workerPlc.IsBusy || _workerTcp.IsBusy)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }

        }
        #endregion

        #region 12000.이벤트.설정
        /// <summary>
        /// 설정 다시 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReload_Click(object sender, EventArgs e)
        {
            if (LoadSettings(out string errMsg))
            {
                TsslLoadSettings.BackColor = Color.Lime;
            }
            else
            {
                InsertLog("Fail to load settings", LogMsg.Sources.APP, 12000, errMsg);
                TsslLoadSettings.BackColor = Color.Orange;
            }
            PpgSettings.SelectedObject = _cs;
        }

        /// <summary>
        /// 설정 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveComSettings(_cs, ComSettings.FilePath, out string errMsg))
            {
                InsertLog("Save settings", LogMsg.Sources.APP);
            }
            else
            {
                InsertLog("Fail to save settings", LogMsg.Sources.APP, 12000, errMsg);
            }
        }

        /// <summary>
        /// Log 폴더 오픈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFolderLog_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
        }

        /// <summary>
        /// 설정 폴더 오픈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFolderSettings_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
        }

        /// <summary>
        /// DB 폴더 오픈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFolderDb_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(_cs.DbPath));
        }

        /// <summary>
        /// 로그 클리어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            TbxLog.Text = "";
        }
        #endregion

        #region 13000.이벤트.타이머
        /// <summary>
        /// 타이머 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrSmaCheck_Tick(object sender, EventArgs e)
        {
            // Application 상태
            if (Process.GetProcessesByName(_cs.SmaAppName).Length > 0)
            {
                LblSmaApp.BackColor = Color.Lime;
            }
            else
            {
                LblSmaApp.BackColor = DefaultBackColor;
            }

            // PLC 업데이트
            if (_plcGuiUpdateQue.TryDequeue(out PlcGuiEvents res))
            {
                if (res == PlcGuiEvents.MesUpload)
                {
                    LvwDb.Items.Clear();
                    for (int i = 0; i < _plcData.DbMesVals.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem(new string[]
                        {
                            $"{i * 2}",
                            _plcData.DbMesVals[i].Name,
                            _plcData.DbMesVals[i].MeasValue.ToString("f3"),
                            _plcData.DbMesVals[i].MesValue.ToString("n0")
                        });
                        LvwDb.Items.Add(lvi);
                    }
                    LblMesOkVal.Text = _plcData.DbPass > 0 ? "OK" : "NG";
                    LblMesOkVal.BackColor = _plcData.DbPass > 0 ? Color.Lime : DefaultBackColor;
                    LblDbIdVal.Text = $"{_plcData.DbId}";
                    LblFailedIdVal.Text = $"{ _plcData.DbFailedId}";
                }
                else if (res == PlcGuiEvents.ReadModelNumber)
                {
                    LblModelNumVal.Text = $"{_plcData.PlcModelNumber}";
                }
                else if (res == PlcGuiEvents.ReadProductInfor)
                {
                    List<ListViewItem> pis = new List<ListViewItem>();
                    if (_plcData.ProductInforVals.Count != _cs.ProductInfors.Count)
                    {
                        return;
                    }

                    for (int i = 0; i < _cs.ProductInfors.Count; i++)
                    {
                        ListViewItem lvItem = new ListViewItem(_cs.ProductInfors[i].Name);

                        // 문자 읽기
                        if (_cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Text)
                        {
                            lvItem.SubItems.Add($"{_cs.ProductInfors[i].DataStartAddress}({_cs.ProductInfors[i].DataLength})");
                        }
                        // 정수 값 읽기
                        else if (_cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.Word)
                        {
                            lvItem.SubItems.Add($"{_cs.ProductInfors[i].DataStartAddress}");
                        }
                        // 더블워드 정수값
                        else if (_cs.ProductInfors[i].DataType == ComSettings.ProductInfor.DataTypes.DWord)
                        {
                            lvItem.SubItems.Add($"{_cs.ProductInfors[i].DataStartAddress}");
                        }
                        else
                        {
                            continue;
                        }
                        lvItem.SubItems.Add(_plcData.ProductInforVals[i].ToString());
                        pis.Add(lvItem);
                    }
                    LvwProInfor.Items.Clear();
                    LvwProInfor.Items.AddRange(pis.ToArray());
                }
            }

            // 로그 업데이트
            if (_logQue.TryDequeue(out LogMsg log))
            {
                // 로그 줄 수 제한 기능
                if (TbxLog.Lines.Length > _cs.LogMaximumLine)
                {
                    TbxLog.Lines = TbxLog.Lines.Skip(TbxLog.Lines.Length - _cs.LogMaximumLine).ToArray();
                }

                // 로그 출력
                string logMessage = $"{log.EventTime.ToString("yy/MM/dd HH:mm:ss")} {log.LogSource.ToString()}) {log.Message}\r\n";
                TbxLog.AppendText(logMessage);
                if (log.ErrCode > 0)
                {
                    logMessage = $"\t({log.ErrCode}) {log.ErrMsg}\r\n";
                    TbxLog.AppendText(logMessage);
                    if (log.LogSource == LogMsg.Sources.PLC)
                    {
                        LblPlcAlmCode.Text = $"{log.ErrCode}";
                        LblPlcAlmCode.BackColor = Color.OrangeRed;

                    }
                    else if (log.LogSource == LogMsg.Sources.TCP)
                    {
                        LblTcpAlmCode.Text = $"{log.ErrCode}";
                        LblTcpAlmCode.BackColor = Color.OrangeRed;
                    }
                }

                // DB 삽입
                try
                {
                    using (SQLiteConnection conn = new SQLiteConnection(_logSource))
                    {
                        conn.Open();

                        string sql = "INSERT INTO log (labdate, code, message) values (@param1, @param2, @param3)";
                        SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                        cmd.Parameters.Add(new SQLiteParameter("@param1", log.EventTime));
                        cmd.Parameters.Add(new SQLiteParameter("@param2", log.ErrCode));
                        cmd.Parameters.Add(new SQLiteParameter("@param3", log.Message));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TsslLoadLog.BackColor = Color.Orange;
                }
            }

            // 프로그램 종료
            if (_closeApp)
            {
                if (_workerPlc.IsBusy)
                {
                    _workerPlc.CancelAsync();
                }
                if (_workerTcp.IsBusy)
                {
                    _workerTcp.CancelAsync();
                }

                while (_workerPlc.IsBusy || _workerTcp.IsBusy)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }

                Close();
            }
        }
        #endregion

        #region 14000.이벤트.TCP
        /// <summary>
        /// TCP Worker 시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpStart_Click(object sender, EventArgs e)
        {
            if (_tcpModeRef == TcpOpModes.Non)
            {
                StartTcpWorker();
                InsertLog("TCP manual start", LogMsg.Sources.APP);
            }
            else
            {
                _workerTcp.CancelAsync();
                InsertLog("TCP manual stop", LogMsg.Sources.APP);
            }
        }
        /// <summary>
        /// FLUX 측정 명령 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpFluxStart_Click(object sender, EventArgs e)
        {
            if (_tcpModeRef == TcpOpModes.Connected)
            {
                TcpData.ComMsg msg = new TcpData.ComMsg();
                msg.Message = "FLUX";
                _tcpMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 측정을 중단한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpAbort_Click(object sender, EventArgs e)
        {
            if (_tcpModeRef == TcpOpModes.Connected)
            {
                TcpData.ComMsg msg = new TcpData.ComMsg();
                msg.Message = "ABORT";
                _tcpMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// CLS 명령 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpCls_Click(object sender, EventArgs e)
        {
            if (_tcpModeRef == TcpOpModes.Connected)
            {
                TcpData.ComMsg msg = new TcpData.ComMsg();
                msg.Message = "*CLS";
                _tcpMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// Ready 명령 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpReady_Click(object sender, EventArgs e)
        {
            if (_tcpModeRef == TcpOpModes.Connected)
            {
                TcpData.ComMsg msg = new TcpData.ComMsg();
                msg.Message = "READY?";
                _tcpMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 바코드를 전송한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTcpSetBarcode_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 알람 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblTcpAlmCode_Click(object sender, EventArgs e)
        {
            Label s = (Label)sender;
            s.BackColor = DefaultBackColor;
            s.Text = "TCP ALARM";
        }
        #endregion

        #region 15000.이벤트.PLC
        /// <summary>
        /// PLC worker 시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWorkerStart_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Non)
            {
                StartPlcWorker();
                InsertLog("PLC manual start", LogMsg.Sources.APP);
            }
            else
            {
                _workerPlc.CancelAsync();
                InsertLog("PLC manual stop", LogMsg.Sources.APP);
            }
        }

        /// <summary>
        /// 바코드를 복사한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCpyBarcode_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 측정 요청을 toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcMeasReq_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleMeasReq;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 측정 요청 응답을 toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasReqResp_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleMeasReqResp;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 측정 완료 응답을 toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasFin_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleMeasFin;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// OK 값을 toggle 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleOk;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// NG 값을 toggle 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNg_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleNg;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// BUSY 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBusy_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleBusy;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// Alarm 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlarm_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
                {
                    PlcData.ComMsg msg = new PlcData.ComMsg();
                    msg.MsgType = PlcData.ComMsg.MsgTypes.ToggleAlarm;
                    _plcMsgQue.Enqueue(msg);
                }
            }
        }

        /// <summary>
        /// Model number를 읽어온다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcReadModel_Click(object sender, EventArgs e)
        {
            if (_workerPlc.IsBusy)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.GetModelNumber;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// Barcode를 읽어온다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLblReadProdInfor_Click(object sender, EventArgs e)
        {
            if (_workerPlc.IsBusy)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.GetProductInfor;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// MES 데이터를 업로드 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMesDataUpload_Click(object sender, EventArgs e)
        {
            if (_workerPlc.IsBusy)
            {
                PlcData.ComMsg msg = new PlcData.ComMsg();
                msg.MsgType = PlcData.ComMsg.MsgTypes.UploadMesData;
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 알람 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblPlcAlmCode_Click(object sender, EventArgs e)
        {
            Label s = (Label)sender;
            s.BackColor = DefaultBackColor;
            s.Text = "PLC ALARM";
        }
        #endregion

        #region 16000.메소드
        /// <summary>
        /// 설정을 저장한다
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool SaveComSettings(ComSettings cs, string path, out string errMsg)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(cs.GetType());
                using (TextWriter writer = new StreamWriter(path))
                {
                    ser.Serialize(writer, cs);
                    writer.Close();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 설정을 로드한다
        /// </summary>
        private bool LoadSettings(out string errMsg)
        {
            bool res;
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ComSettings));
                fs = new FileStream(ComSettings.FilePath, FileMode.Open);
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create(fs);
                _cs = (ComSettings)serializer.Deserialize(reader);
                errMsg = "";
                res = true;
            }
            catch (Exception ex)
            {
                _cs = new ComSettings();
                _cs.DbColumns.AddRange(new string[] { "SPM A MAX", "SPM A MIN", "SPM A AVR", "SPM A RNG", "SPM A STD", "SPM Zd MAX", "SPM Zd MIN", "SPM Zd AVR", "SPM Zd STD", "SPM Zd RNG", "pass", "failed_id" });
                errMsg = ex.Message;
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return res;
        }

        /// <summary>
        /// 로그를 삽입한다
        /// </summary>
        /// <param name="message"></param>
        /// <param name="lsource"></param>
        /// <param name="code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool InsertLog(string message, LogMsg.Sources lsource, int code = 0, string errMsg = "")
        {
            LogMsg msg = new LogMsg();

            msg.Message = message;
            msg.ErrCode = code;
            msg.ErrMsg = errMsg;
            msg.EventTime = DateTime.Now;
            msg.LogSource = lsource;
            _logQue.Enqueue(msg);
            return true;
        }
        #endregion


    }
}
