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
    /// 1. 주 프로그램
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
            /// User operation
            /// </summary>
            WaitUserOperation,
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
        private BackgroundWorker _workerPlc { get; set; } = new BackgroundWorker();

        /// <summary>
        /// PLC 데이터
        /// </summary>
        private ComPlcData _pd { get; set; } = new ComPlcData();

        /// <summary>
        /// PLC 동작 모드
        /// </summary>
        private PlcOpModes _plcModeRef { get; set; } = PlcOpModes.Non;

        /// <summary>
        /// TCP 백그라운드 워커
        /// </summary>
        private BackgroundWorker _workerTcp { get; set; } = new BackgroundWorker();

        /// <summary>
        /// TCP 데이터
        /// </summary>
        private TcpData _tcpData { get; set; } = new TcpData();

        /// <summary>
        /// TCP 동작 모드
        /// </summary>
        private TcpOpModes _tcpModeRef { get; set; } = TcpOpModes.Non;

        /// <summary>
        /// 통신 설정
        /// </summary>
        private ComSettings _cs { get; set; }

        /// <summary>
        /// PLC message queue
        /// </summary>
        private ConcurrentQueue<ComPlcData.ComMsg> _plcMsgQue { get; set; } = new ConcurrentQueue<ComPlcData.ComMsg>();

        /// <summary>
        /// TCP message queue
        /// </summary>
        private ConcurrentQueue<TcpData.ComMsg> _tcpMsgQue { get; set; } = new ConcurrentQueue<TcpData.ComMsg>();

        /// <summary>
        /// 로그 메시지를 저장하는 que
        /// </summary>
        private ConcurrentQueue<LogMsg> _logQue { get; set; } = new ConcurrentQueue<LogMsg>();

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

        /// <summary>
        /// APP 폴더 이름
        /// </summary>
        public string AppFolderName
        {
            get
            {
                return "PlcComDlg";
            }
        }

        /// <summary>
        /// Application 폴더 이름
        /// </summary>
        public string AppSettingsName
        {
            get
            {
                return "ComSettings";
            }
        }
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
        /// 폼 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainDlg_Load(object sender, EventArgs e)
        {
            string errMsg = "";

            Text = Text + $" {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

            // 중복실행 방지
            string currProcess = Process.GetCurrentProcess().ProcessName.ToUpper();
            Process[] pcs = Process.GetProcessesByName(currProcess);
            if (pcs.Length > 1)
            {
                AutoClosingMessageBox.Show("프로그램이 이미 실행중입니다", "Warning", 1000);
                Application.Exit();
            }

            // 로그용 DB 파일 생성
            string logPath = LogMsg.DefaultFilePath(AppFolderName);
            if (!File.Exists(logPath))
            {
                if (LogMsg.CreateLogDb(logPath, out errMsg))
                {
                    InsertLog($"Create log db={logPath}", LogMsg.Sources.APP);
                }
                else
                {
                    TslLoadLog.Text = $"Fail to create db={errMsg}";
                    TslLoadLog.BackColor = Color.Orange;
                }
            }
            else
            {
                TslLoadLog.Text = "LOG";
                TslLoadLog.BackColor = Color.Lime;
            }


            // 설정 로드
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (!ComSettings.Load(settingsPath, out ComSettings cs, out errMsg))
            {
                InsertLog($"Fail to load settings={settingsPath}", LogMsg.Sources.APP, 11000, errMsg);
                cs = new ComSettings();
            }
            _cs = cs;
            PpgSettings.SelectedObject = _cs;

            TmrSmaCheck.Enabled = true;

            InsertLog("Application start", LogMsg.Sources.APP);

            DtpMain.SelectedIndex = Properties.Settings.Default.TabIndex;
            Size = Properties.Settings.Default.WindowSize;
            //Location = Properties.Settings.Default.DlgPosition;

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
            Properties.Settings.Default.WindowSize = Size;
            Properties.Settings.Default.DlgPosition = Location;
            Properties.Settings.Default.TabIndex = DtpMain.SelectedIndex;
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
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (ComSettings.Load(settingsPath, out ComSettings cs, out string errMsg))
            {
                InsertLog($"Roll back settings={settingsPath}", LogMsg.Sources.APP);
                _cs = cs;
                PpgSettings.SelectedObject = _cs;
            }
            else
            {
                InsertLog($"Fail to load settings={settingsPath}", LogMsg.Sources.APP, 12000, errMsg);
                cs = new ComSettings();
            }
        }

        /// <summary>
        /// 설정 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (SaveComSettings(_cs, settingsPath, out string errMsg))
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
            Process.Start(Path.GetDirectoryName(_cs.DbMeas.Path));
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
            string errMsg;

            LblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Application 상태
            if (Process.GetProcessesByName(_cs.SmaAppName).Length > 0)
            {
                TslSmaApp.BackColor = Color.Lime;
            }
            else
            {
                TslSmaApp.BackColor = DefaultBackColor;
            }

            // PLC 업데이트
            if (_plcGuiUpdateQue.TryDequeue(out PlcGuiEvents res))
            {
                if (res == PlcGuiEvents.MesUpload)
                {
                    FgdMesData.BeginUpdate();
                    FgdMesData.DataSource = _pd.MesData;
                    FgdMesData.Cols["name"].Caption = "Name";
                    FgdMesData.Cols["dataName"].Caption = "DB Column";
                    FgdMesData.Cols["dbData"].Caption = "Raw Data";
                    FgdMesData.Cols["dbData"].Format = "f3";
                    FgdMesData.Cols["mesData"].Caption = "MES Data";
                    FgdMesData.Cols["mesData"].Format = "n0";
                    FgdMesData.Cols["address"].Caption = "Address";
                    FgdMesData.Cols["dataType"].Caption = "Type";
                    FgdMesData.AutoSizeCols();
                    FgdMesData.EndUpdate();
                    LblResult.Text = LblMesOkVal.Text = _pd.DbPass > 0 ? "OK" : "NG";
                    LblResult.BackColor = LblMesOkVal.BackColor = _pd.DbPass > 0 ? Color.Lime : DefaultBackColor;
                    LblDbIdVal.Text = $"{_pd.DbId}";
                    LblFailedIdVal.Text = $"{ _pd.DbFailedId}";
                }
                else if (res == PlcGuiEvents.MesClear)
                {
                    FgdMesData.BeginUpdate();
                    FgdMesData.DataSource = _pd.MesData;
                    FgdMesData.Cols["name"].Caption = "Name";
                    FgdMesData.Cols["dataName"].Caption = "DB Column";
                    FgdMesData.Cols["dbData"].Caption = "Raw Data";
                    FgdMesData.Cols["dbData"].Format = "f3";
                    FgdMesData.Cols["mesData"].Caption = "MES Data";
                    FgdMesData.Cols["mesData"].Format = "n0";
                    FgdMesData.Cols["address"].Caption = "Address";
                    FgdMesData.Cols["dataType"].Caption = "Type";
                    FgdMesData.AutoSizeCols();
                    FgdMesData.EndUpdate();
                    LblResult.Text = LblMesOkVal.Text = "-" ;
                    LblResult.BackColor = LblMesOkVal.BackColor = DefaultBackColor;
                    LblDbIdVal.Text = $"-";
                    LblFailedIdVal.Text = $"-";
                }
                else if (res == PlcGuiEvents.ReadProductInfor)
                {
                    FgdProdInf.BeginUpdate();
                    FgdProdInf.DataSource = _pd.ProdData;
                    FgdProdInf.Cols["name"].Caption = "Name";
                    FgdProdInf.Cols["dataType"].Caption = "Type";
                    FgdProdInf.Cols["startAddress"].Caption = "Address";
                    FgdProdInf.Cols["dbColumnName"].Caption = "Target DB Column";
                    FgdProdInf.Cols["data"].Caption = "Data";
                    FgdProdInf.AutoSizeCols();
                    FgdProdInf.EndUpdate();
                }
            }

            // 로그 출력
            int logCnt = 0;
            while (_logQue.TryDequeue(out LogMsg log) && logCnt < 100)
            {
                TbxLog.AppendText(LogMsg.GetLogTxt(log));
                if (log.Code > 0)
                {
                    TbxLog.AppendText(LogMsg.GetErrTxt(log));
                    if (log.LogSource == LogMsg.Sources.PLC)
                    {
                        LblPlcAlmCode.Text = $"{log.Code}";
                        LblPlcAlmCode.BackColor = Color.OrangeRed;
                    }
                    else if (log.LogSource == LogMsg.Sources.TCP)
                    {
                        LblTcpAlmCode.Text = $"{log.Code}";
                        LblTcpAlmCode.BackColor = Color.OrangeRed;
                    }
                }
                // 로그 줄 수 제한
                if (TbxLog.Lines.Length > _cs.LogMaximumLine)
                {
                    TbxLog.Lines = TbxLog.Lines.Skip(TbxLog.Lines.Length - _cs.LogMaximumLine).ToArray();
                }

                // DB 삽입
                if (_cs.SaveAllLogToDb || log.Code > 0)
                {
                    if (LogMsg.SaveLogDb(log, LogMsg.DefaultFilePath(AppFolderName), out errMsg))
                    {
                        TslLoadLog.Text = "LOG";
                        TslLoadLog.BackColor = Color.Lime;
                    }
                    else
                    {
                        TslLoadLog.Text = errMsg;
                        TslLoadLog.BackColor = Color.Orange;
                    }
                }
                logCnt++;
            }
            if (logCnt > 0)
            {
                LoadLog(_cs.LogMaximumDbItem);

                if (_cs.LogHoldDays > 0)
                {
                    if (!LogMsg.DeleteOldLog(_cs.LogHoldDays, LogMsg.DefaultFilePath(AppFolderName), out errMsg))
                    {
                        TslLoadLog.Text = $"Fail to delete old log={errMsg}";
                        TslLoadLog.BackColor = Color.Orange;
                    }
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
                TcpData.ComMsg msg = new TcpData.ComMsg
                {
                    Message = "FLUX"
                };
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
                TcpData.ComMsg msg = new TcpData.ComMsg
                {
                    Message = "ABORT"
                };
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
                TcpData.ComMsg msg = new TcpData.ComMsg
                {
                    Message = "*CLS"
                };
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
                TcpData.ComMsg msg = new TcpData.ComMsg
                {
                    Message = "READY?"
                };
                _tcpMsgQue.Enqueue(msg);
            }
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
        /// 측정 요청을 toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcMeasReq_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected || _plcModeRef == PlcOpModes.Measruement)
            {
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleMeasReq
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleMeasReqResp
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleMeasFin
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleOk
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleNg
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ToggleBusy
                };
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
                    ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                    {
                        MsgType = ComPlcData.ComMsg.MsgTypes.ToggleAlarm
                    };
                    _plcMsgQue.Enqueue(msg);
                }
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.GetProductInfor
                };
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
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.UploadMesData
                };
                _plcMsgQue.Enqueue(msg);
            }
        }

        /// <summary>
        /// MES 데이터를 클리어 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMesClear_Click(object sender, EventArgs e)
        {
            if (_workerPlc.IsBusy)
            {
                ComPlcData.ComMsg msg = new ComPlcData.ComMsg
                {
                    MsgType = ComPlcData.ComMsg.MsgTypes.ClearMesData
                };
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

        /// <summary>
        /// 수동으로 측정을 시작한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasManual_Click(object sender, EventArgs e)
        {
            if (_plcModeRef == PlcOpModes.Connected && _tcpModeRef == TcpOpModes.Connected)
            {
                _manualMeasTrigger = true;
                InsertLog("Start measurement by user", LogMsg.Sources.APP);
            }
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
        /// 로그를 삽입한다
        /// </summary>
        /// <param name="message"></param>
        /// <param name="lsource"></param>
        /// <param name="code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool InsertLog(string message, LogMsg.Sources lsource, int code = 0, string errMsg = "")
        {
            LogMsg msg = new LogMsg
            {
                Message = message,
                Code = code,
                Comment = errMsg,
                EventTime = DateTime.Now,
                LogSource = lsource
            };
            _logQue.Enqueue(msg);
            return true;
        }

        /// <summary>
        /// 로그를 로드한다
        /// </summary>
        private async void LoadLog(int maxItem)
        {
            DataTable dt = null;
            string errMsg = "";
            string logPath = LogMsg.DefaultFilePath(AppFolderName);
            var t = Task.Run(() => LogMsg.LoadFromDb(logPath, maxItem, out dt, out errMsg));
            await t;

            var dict = Enum.GetValues(typeof(LogMsg.Sources))
               .Cast<LogMsg.Sources>().ToDictionary(x => (long)x, x => x.ToString());

            if (t.Result && !FgdLog.IsDisposed)
            {
                FgdLog.BeginUpdate();
                FgdLog.DataSource = dt;
                FgdLog.AutoSizeCols();
                FgdLog.Cols["source"].DataMap = dict;
                FgdLog.EndUpdate();
            }
            else
            {
                InsertLog($"로그 로드 실패", LogMsg.Sources.APP, 16000, errMsg);
            }
        }

        #endregion


    }
}
