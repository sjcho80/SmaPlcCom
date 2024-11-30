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

namespace PlcComDlg
{
    /// <summary>
    /// SMA-PLC 통신 프로그램
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
 

        /// <summary>
        /// 통신 컨트롤
        /// </summary>
        public class ComCtrl
        {
            /// <summary>
            /// PLC 통신
            /// </summary>
            public ActUtlType Aut = null;

            /// <summary>
            /// TCP client 소켓
            /// </summary>
            public Socket Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
        private TcpOpModes _tcpModeRef = TcpOpModes.Non;

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

        private ConcurrentQueue<LogMsg> _logQue = new ConcurrentQueue<LogMsg>();

        /// <summary>
        /// TCP에 측정을 요청한다
        /// </summary>
        private bool _plcToTcpMeasReq = false;

        /// <summary>
        /// PLC에 측정을 완료한다
        /// </summary>
        private bool _tcpToPlcMeasFin = false;

        /// <summary>
        /// TCP 측정 에러 플래그
        /// </summary>
        private bool _tcpMeasErrFlag = false;

        /// <summary>
        /// TCP 측정 에러 메시지
        /// </summary>
        private string _tcpMeasErrMsg = "";

        /// <summary>
        /// 프로그램을 종료하는 플래그
        /// </summary>
        private bool _closeApp { get; set; } = false;
        #endregion

        #region 이벤트.초기화
        /// <summary>
        /// 폼 초기화
        /// </summary>
        public MainDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 1XXX: 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainDlg_Load(object sender, EventArgs e)
        {
            // 1001: 중복실행 방지
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

            // 1003: 설정 로드
            if (File.Exists(ComSettings.FilePath))
            {
                if (LoadSettings())
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
                SaveComSettings(_cs, ComSettings.FilePath);
                InsertLog("Create settings file", 1003);
                TsslLoadSettings.BackColor = Color.Yellow;
            }
            PpgSettings.SelectedObject = _cs;

            TmrSmaCheck.Enabled = true;
             
            InsertLog("** PLC COM Application start **");

            // 1004: 프로그램 자동 시작
            if (_cs.AutoStart)
            {
                InsertLog("Auto start");
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

        #region 이벤트.설정

        /// <summary>
        /// 설정 다시 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReload_Click(object sender, EventArgs e)
        {
            if (LoadSettings())
            {
                TsslLoadSettings.BackColor = Color.Lime;
            }
            else
            {
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
            SaveComSettings(_cs, ComSettings.FilePath);
            MessageBox.Show("설정이 저장되었습니다");
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

        #region 이벤트.타이머
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

            if (_logQue.TryDequeue(out LogMsg log))
            {
                // 로그 출력
                if (TbxLog.Lines.Length > _cs.LogMaximumLine)
                {
                    TbxLog.Lines = TbxLog.Lines.Skip(TbxLog.Lines.Length - _cs.LogMaximumLine).ToArray();
                }

                string logMessage;
                logMessage = log.EventTime.ToString("yy/MM/dd HH:mm:ss) ");
                if (log.ErrCode > 0)
                {
                    logMessage += $"({log.ErrCode})";
                }
                logMessage += log.Message + Environment.NewLine;

                TbxLog.AppendText(logMessage);

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

        #region 이벤트.TCP
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
                InsertLog("TCP manual start");
            }
            else
            {
                _workerTcp.CancelAsync();
                InsertLog("TCP manual stop");
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
            if (_tcpModeRef == TcpOpModes.Connected)
            {
                if (!GetProductInforMsg(_plcData, _cs, out string[] tcpMsgs))
                {
                    for (int i = 0; i < tcpMsgs.Length; i++)
                    {
                        TcpData.ComMsg msg = new TcpData.ComMsg();
                        msg.Message = tcpMsgs[i];
                        _tcpMsgQue.Enqueue(msg);
                    }
                }
            }
        }

        #endregion

        #region 이벤트.PLC
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
                InsertLog("PLC manual start");
            }
            else
            {
                _workerPlc.CancelAsync();
                InsertLog("PLC manual stop");
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
                _plcData.Flags.MeasReqResp = !_plcData.Flags.MeasReqResp;
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
                _plcData.Flags.MeasFin = !_plcData.Flags.MeasFin;
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
                _plcData.Flags.Ok = !_plcData.Flags.Ok;
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
                _plcData.Flags.Ng = !_plcData.Flags.Ng;
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
                _plcData.Flags.Busy = !_plcData.Flags.Busy;
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
                _plcData.Flags.Alarm = !_plcData.Flags.Alarm;
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
        #endregion

        #region 메소드
        /// <summary>
        /// 설정을 저장한다
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool SaveComSettings(ComSettings cs, string path)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(cs.GetType());
                using (TextWriter writer = new StreamWriter(path))
                {
                    ser.Serialize(writer, cs);
                    writer.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 설정을 로드한다
        /// </summary>
        private bool LoadSettings()
        {
            bool res;
            FileStream fs = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ComSettings));
                fs = new FileStream(ComSettings.FilePath, FileMode.Open);
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create(fs);
                _cs = (ComSettings)serializer.Deserialize(reader);
                res = true;
            }
            catch (Exception ex)
            {
                _cs = new ComSettings();
                _cs.DbColumns.AddRange(new string[] { "SPM A MAX", "SPM A MIN", "SPM A AVR", "SPM A RNG", "SPM A STD", "SPM Zd MAX", "SPM Zd MIN", "SPM Zd AVR", "SPM Zd STD", "SPM Zd RNG", "pass", "failed_id" });
                InsertLog("Fail to load settings: " + ex.Message, 1001);
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
        /// <param name="code"></param>
        private bool InsertLog(string message, int code = 0)
        {
            LogMsg msg = new LogMsg();

            msg.Message = message;
            msg.ErrCode = code;
            msg.EventTime = DateTime.Now;
            _logQue.Enqueue(msg);
            return true;
        }
        #endregion

    }
}
