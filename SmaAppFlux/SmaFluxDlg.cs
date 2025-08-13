using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SmaCtrl;
using SmaPlc;
using C1.Win.C1FlexGrid;

using C1.Win.Chart;

namespace SmaFlux
{
    /// <summary>
    /// SMA FLUX Form
    /// </summary>
    public partial class SmaFluxDlg : Form
    {
        #region 타입정의
        /// <summary>
        /// 동작 모드
        /// </summary>
        public enum OpModes
        {
            /// <summary>
            /// 아무런 동작도 하지 않음
            /// </summary>
            NOP,
            /// <summary>
            /// 연결 중
            /// </summary>
            Connecting,
            /// <summary>
            /// 대기 중
            /// </summary>
            Ready,
        }

        /// <summary>
        /// GUI에서 발생한 사용자 제어 이벤트
        /// </summary>
        public enum PlcReqTypes
        {
            /// <summary>
            /// 제품 정보를 읽어온다
            /// </summary>
            ReadProdInf,
            /// <summary>
            /// 측정 값을 MES에 업로드 한다
            /// </summary>
            MesUpload,
            /// <summary>
            /// MES를 클리어 한다
            /// </summary>
            MesClear,
            /// <summary>
            /// 특정 플래그를 셋 한다
            /// </summary>
            SetFlag,
            /// <summary>
            /// 특정 플래그를 클리어 한다
            /// </summary>
            ClearFlag
        }

        /// <summary>
        /// PLC에 전송하는 이벤트
        /// </summary>
        public class PlcReq
        {
            /// <summary>
            /// PLC EVENT 타입
            /// </summary>
            public PlcReqTypes PlcReqType { get; set; }

            /// <summary>
            /// PLC EVENT 데이터
            /// </summary>
            public object PlcReqData { get; set; }
        }

        /// <summary>
        /// GUI에 측정 결과를 업데이트 한다
        /// </summary>
        public enum TimerEvents
        {
            FluxMeasurement,
            ReadProdInfor,
            UploadMesData,
            ClearMesData,
            InsertDb,
        }
        #endregion

        #region 변수선언
        /// <summary>
        /// 설정 파일
        /// </summary>
        private FluxSettings _fs { get; set; } = new FluxSettings();

        /// <summary>
        /// 동작 모드
        /// </summary>
        private OpModes _omRef { get; set; } = OpModes.NOP;

        /// <summary>
        /// 로그 메시지를 저장하는 큐
        /// </summary>
        private ConcurrentQueue<LogMsg> _qLog { get; set; } = new ConcurrentQueue<LogMsg>();

        /// <summary>
        /// PLC에 제어 동작을 전달하는 큐
        /// </summary>
        private ConcurrentQueue<PlcReq> _qPlcReq { get; set; } = new ConcurrentQueue<PlcReq>();

        /// <summary>
        /// 타이머 이벤트
        /// </summary>
        private ConcurrentQueue<TimerEvents> _qTmr { get; set; } = new ConcurrentQueue<TimerEvents>();

        /// <summary>
        /// 프로그램 제어 백그라운드 워커
        /// </summary>
        private BackgroundWorker _bw { get; set; } = new BackgroundWorker();

        /// <summary>
        /// PLC 데이터
        /// </summary>
        private FluxPlcData _pd { get; set; } = new FluxPlcData();

        /// <summary>
        /// 프로그램 종료알림 플래그
        /// </summary>
        private bool _closeApp { get; set; } = false;

        /// <summary>
        /// 측정 DB
        /// </summary>
        private FluxDbMeas _mdb { get; set; } = new FluxDbMeas();

        /// <summary>
        /// 마지막 측정 값
        /// </summary>
        private FluxData _fd { get; set; } = new FluxData();

        /// <summary>
        /// 플럭스미터 제어 함수
        /// </summary>
        private Fluxmeter _fm { get; set; } = new Fluxmeter();

        /// <summary>
        /// Profile 데이터 테이블
        /// </summary>
        private DataTable _dtProf { get; set; }

        /// <summary>
        /// MES 데이터 결과
        /// </summary>
        private DataTable _dtMes { get; set; }

        /// <summary>
        /// Application 폴더 이름
        /// </summary>
        public string AppFolderName
        {
            get
            {
                return "SMA FLUX";
            }
        }

        /// <summary>
        /// Application 폴더 이름
        /// </summary>
        public string AppSettingsName
        {
            get
            {
                return "sma flux settings";
            }
        }
        #endregion

        #region 11100 이벤트 프로그램 시작/종료
        /// <summary>
        /// Constructor
        /// </summary>
        public SmaFluxDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmaFlux_Load(object sender, EventArgs e)
        {
            string errMsg = "";

            // 중복실행 체크
            string currProcess = Process.GetCurrentProcess().ProcessName.ToUpper();
            Process[] pcs = Process.GetProcessesByName(currProcess);
            if (pcs.Length > 1)
            {
                AutoClosingMessageBox.Show("프로그램이 이미 실행중입니다", "Warning", 11100);
                Application.Exit();
            }

            var vr = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            Text = Text + $" {vr.Major}.{vr.MajorRevision}.{vr.Minor}.{vr.MinorRevision}";

            string logPath = LogMsg.DefaultFilePath(AppFolderName);
            // 로그 DB 파일 생성
            if (!File.Exists(logPath))
            {
                if (LogMsg.CreateLogDb(logPath, out errMsg))
                {
                    InsertLog($"Create log DB", LogMsg.Sources.APP);
                }
                else
                {
                    TslLog.Text = $"Fail to create log DB={errMsg}";
                    TslLog.BackColor = Color.Orange;
                }
            }
            else
            {
                TslLog.Text = $"LOG";
                TslLog.BackColor = Color.Lime;
            }

            // 프로그램 설정 로드
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (File.Exists(settingsPath))
            {
                if (FluxSettings.Load(settingsPath, out FluxSettings fs, out errMsg))
                {
                    _fs = fs;
                }
                else
                {
                    InsertLog($"Fail to load settings={settingsPath}", LogMsg.Sources.APP, 11100, errMsg);
                    _fs = new FluxSettings
                    {
                        DbPath = FluxDbMeas.FilePath
                    };
                }
            }
            else
            {
                _fs = new FluxSettings
                {
                    DbPath = FluxDbMeas.FilePath
                };
            }

            PpgSettings.SelectedObject = _fs;
            LblSiteInfor.Text = _fs.SiteInformation;
            for (int i = 0; i <= _fs.DbMaxModels; i++)
            {
                MstMeasModel.Items.Add(i);
                MstDashModel.Items.Add(i);
            }

            // 대쉬보드 설정 로드
            if (!FluxDbMeas.DashConds.Load(out FluxDbMeas.DashConds dc, out errMsg))
            {
                dc = new FluxDbMeas.DashConds();
                InsertLog($"Fail to load track condition", LogMsg.Sources.APP, 11100, errMsg);
            }
            CbxDashGroup.DataSource = Enum.GetValues(typeof(FluxDbMeas.DashConds.GroupConds));
            CbxDashGroup.SelectedIndex = (int)dc.GroupCond;
            CkbDashStartDate.Checked = dc.StartEnable;
            DteDashStart.Value = dc.StartDate;
            CkbDashEndDate.Checked = dc.EndEnable;
            DteDashEnd.Value = dc.EndDate;
            MstDashPeak.Items[0].Selected = dc.NegativePeak;
            MstDashPeak.Items[1].Selected = dc.TotalPeak;
            MstDashPeak.Items[2].Selected = dc.PositivePeak;
            foreach (var item in MstDashModel.Items)
            {
                item.Selected = Array.IndexOf(dc.ModelNumbers, item.Value) != -1;
            }

            // 검색 설정 로드
            if (!FluxDbMeas.MeasConds.Load(out FluxDbMeas.MeasConds mc, out errMsg))
            {
                mc = new FluxDbMeas.MeasConds();
                InsertLog($"Fail to load search condition", LogMsg.Sources.APP, 11100, errMsg);
            }
            CkbMeasToday.Checked = mc.TodayOnly;
            CkbMeasStartDate.Checked = mc.EnableStartDate;
            DteMeasStartDate.Value = mc.StartDate;
            CkbMeasEndDate.Checked = mc.EnableEndDate;
            DteMeasEndDate.Value = mc.EndDate;
            CbxMeasItems.Value = mc.MaxItems;
            NudDbPageNum.Value = mc.PageNum;
            foreach (var item in MstMeasModel.Items)
            {
                item.Selected = Array.IndexOf(mc.ModelNumbers, item.Value) != -1;
            }

            // 측정 DB 파일 체크 및 생성
            if (!File.Exists(FluxDbMeas.FilePath))
            {
                if (FluxDbMeas.CreateDb(FluxDbMeas.FilePath, out errMsg))
                {
                    InsertLog($"측정 DB 생성 성공: {FluxDbMeas.FilePath}", LogMsg.Sources.APP);
                }
                else
                {
                    InsertLog($"측정 DB 생성 실패: {FluxDbMeas.FilePath}", LogMsg.Sources.APP, 11100, errMsg);
                }
            }
            // 측정 DB 로드
            LoadMeasDb(mc);
            LoadHistogram(dc);
            LoadTrackingChart(dc);

            // profile DB 파일 체크 및 생성
            if (!File.Exists(FluxDbProfile.DefaultDbPath))
            {
                if (FluxDbProfile.CreateDb(FluxDbProfile.DefaultDbPath, out errMsg))
                {
                    InsertLog($"Profile DB 생성 성공: {FluxDbProfile.DefaultDbPath}", LogMsg.Sources.APP);
                }
                else
                {
                    InsertLog($"Profile DB 생성 실패: {FluxDbProfile.DefaultDbPath}", LogMsg.Sources.APP, 11100, errMsg);
                }
            }

            // 프로파일 DB 로드
            if (FluxDbProfile.Load(FluxDbProfile.DefaultDbPath, out DataTable dt, out errMsg))
            {
                _dtProf = dt;
                FluxDbProfile.SetFlexGridStyle(ref FgdProfile, _dtProf);
            }
            else
            {
                InsertLog($"Profile DB 로드 실패", LogMsg.Sources.APP, 11100, errMsg);
            }

            // Worker 초기화
            _bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw.DoWork += new DoWorkEventHandler(Worker_DoWork);
            _bw.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);

            var ls = Properties.Settings.Default.TabList.Split(',');

            if (ls.Length == TabMain.TabPages.Count)
            {
                for (int i = 0; i < ls.Length; i++)
                {
                    if (-1 < TabMain.TabPages.IndexOfKey(ls[i]))
                    {
                        var tp = TabMain.TabPages[ls[i]];
                        TabMain.TabPages.Remove(tp);
                        TabMain.TabPages.Insert(i, tp);
                    }
                }
            }
            TabMain.SelectedIndex = Properties.Settings.Default.TabMainLastSelection;
            this.Size = Properties.Settings.Default.MainFormSize;
            this.Location = Properties.Settings.Default.MainFormLocation;
            this.WindowState = (FormWindowState)Properties.Settings.Default.MainFormMaximze;

            InsertLog("Application start", LogMsg.Sources.APP);

            if (_fs.AutoStart)
            {
                InsertLog("Start PLC control automatically", LogMsg.Sources.APP);
                StartWorker();
            }
        }

        /// <summary>
        /// 폼 종료 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmaFlux_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.MainFormMaximze = (int)WindowState;

            // 측정 DB 검색 설정 저장
            FluxDbMeas.MeasConds mc = LoadMeasCond();

            if (!mc.Save(out string errMsg))
            {
                InsertLog("측정 검색 저장 실패", LogMsg.Sources.APP, 11100, errMsg);
            }

            // 대쉬보드 검색 설정 저장
            FluxDbMeas.DashConds dc = LoadDashCond();

            if (!dc.Save(out errMsg))
            {
                InsertLog("트랙 기능 저장 실패", LogMsg.Sources.APP, 11100, errMsg);
            }
            
            // 대쉬보드 검색 설정 저장
            if (int.TryParse(CbxMeasItems.Value.ToString(), out int num))
            {
                Properties.Settings.Default.SearchItems = num;
            }
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MainFormLocation = this.Location;
                Properties.Settings.Default.MainFormSize = this.Size;
            }

            // 탭패이지 저장
            List<string> ls = new List<string>();
            foreach (C1.Win.C1Command.C1DockingTabPage tp in TabMain.TabPages)
            {
                 ls.Add(tp.Name);
            }
            Properties.Settings.Default.TabList = string.Join(",", ls);
            Properties.Settings.Default.TabMainLastSelection = TabMain.SelectedIndex;

            Properties.Settings.Default.Save();

           

            if (_bw.IsBusy)
            {
                _bw.CancelAsync();
            }

            while (_bw.IsBusy)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }
        #endregion

        #region 11200 이벤트
        /// <summary>
        /// PLC 연결
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcConn_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.NOP)
            {
                StartWorker();
                InsertLog("USER.PLC start", LogMsg.Sources.APP);
            }
            else
            {
                _bw.CancelAsync();
                InsertLog("USER.PLC stop", LogMsg.Sources.APP);
            }
        }

        /// <summary>
        /// 설정 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (SmaSettings.SaveXml(_fs, settingsPath, out string errMsg))
            {
                InsertLog("Save settings", LogMsg.Sources.APP);
            }
            else
            {
                InsertLog("Fail to save settings", LogMsg.Sources.APP, 12000, errMsg);
            }
        }

        /// <summary>
        /// 설정을 롤백한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRollbackSettings_Click(object sender, EventArgs e)
        {
            string settingsPath = SmaSettings.DefaultFilePath(AppFolderName, AppSettingsName);
            if (FluxSettings.Load(settingsPath, out FluxSettings fs, out string errMsg))
            {
                InsertLog($"Roll back settings={settingsPath}", LogMsg.Sources.APP);
                _fs = fs;
                PpgSettings.SelectedObject = _fs;
            }
            else
            {
                InsertLog($"Fail to load settings={settingsPath}", LogMsg.Sources.APP, 12000, errMsg);
                fs = new FluxSettings();
            }
        }

        /// <summary>
        /// Home 페이지로 이동한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbPageHome_Click(object sender, EventArgs e)
        {
            NudDbPageNum.Value = 0;
            LoadMeasDb(LoadMeasCond());
        }

        /// <summary>
        /// 페이지를 앞으로 이동한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbPageFwd_Click(object sender, EventArgs e)
        {
            if (NudDbPageNum.Value > 0)
            {
                NudDbPageNum.Value = NudDbPageNum.Value - 1;
            }
            LoadMeasDb(LoadMeasCond());
        }

        /// <summary>
        /// 페이지를 뒤로 이동한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbPageBwd_Click(object sender, EventArgs e)
        {
            NudDbPageNum.Value = NudDbPageNum.Value + 1;
            LoadMeasDb(LoadMeasCond());
        }

        /// <summary>
        /// DB를 로드한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbPageReload_Click(object sender, EventArgs e)
        {
            LoadMeasDb(LoadMeasCond());
        }

        /// <summary>
        /// 대쉬보드를 로드한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadDashboard_Click(object sender, EventArgs e)
        {
            FluxDbMeas.DashConds dc = LoadDashCond();
            LoadHistogram(dc);
            LoadTrackingChart(dc);
        }

        /// <summary>
        /// 바코드 등 제품 정보를 읽어온다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLblReadProInf_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Read product information", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = PlcReqTypes.ReadProdInf
                });
            }
        }

        private void BtnMeasClear_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Clear MES data", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = PlcReqTypes.MesClear
                });
            }
        }

        /// <summary>
        /// 플럭스 정보를 읽어오고 OK/MG 판정을 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadFlux_Click(object sender, EventArgs e)
        {
            InsertLog("USER.Read fluxmeter", LogMsg.Sources.APP);
            DataRow[] sr = _dtProf.Select($"modelNumber={_pd.Model.Number}");
            if (sr.Length < 1)
            {
                InsertLog("Fluxmetuer read failure", LogMsg.Sources.APP, 12000, $"No comport for model number={_pd.Model.Number}");
                return;
            }

            if (!ReadFlux(sr[0], _fd, _pd.Model.Number, _fm, out string errMsg))
            {
                InsertLog("Fail to read fluxmeter", LogMsg.Sources.APP, 12000, errMsg);
                return;
            }
            _qTmr.Enqueue(TimerEvents.FluxMeasurement);
        }

        /// <summary>
        /// 플럭스미터를 리셋한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResetFlux_Click(object sender, EventArgs e)
        {
            InsertLog("USER.Reset fluxmeter", LogMsg.Sources.APP);
            DataRow[] sr = _dtProf.Select($"modelNumber={_pd.Model.Number}");
            if (sr.Length < 1)
            {
                InsertLog("Fluxmetuer reset failure", LogMsg.Sources.APP, 12000, $"No comport for model number={_pd.Model.Number}");
                return;
            }
            string comPort = sr[0]["portName"].ToString();

            if (!ResetFlux(comPort, _fm, out string errMsg))
            {
                InsertLog("Fluxmetuer reset failure", LogMsg.Sources.APP, 12000, errMsg);
            }
        }

        /// <summary>
        /// DB에 측정 아이템을 삽입한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasInsert_Click(object sender, EventArgs e)
        {
            InsertLog("USER.Insert measurement and product information to DB", LogMsg.Sources.APP);
            _qTmr.Enqueue(TimerEvents.InsertDb);
        }

        /// <summary>
        /// 측정 값을 MES에 업로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasUpload_Click(object sender, EventArgs e)
        {



            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Upload MES data", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = PlcReqTypes.MesUpload
                });
            }
        }

        /// <summary>
        /// OK toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle OK bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.OkBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.OkBit.Address,
                });
            }
        }
    
        /// <summary>
        /// NG 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNg_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle NG bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.NgBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.NgBit.Address,
                });
            }
        }

        /// <summary>
        /// ALARM 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlarm_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle alarm bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.AlarmBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.AlarmBit.Address,
                });
            }
        }

        /// <summary>
        /// 측정 요청 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasReqResp_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle measurement request bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.MeasReqBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.MeasReqBit.Address,
                });
            }
        }

        /// <summary>
        /// 측정 완료 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMeasFin_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle measurement finished bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.MeasFinBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.MeasFinBit.Address,
                });
            }

        }

        /// <summary>
        /// 측정 요청 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcRstReq_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle reset request bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.ResetReqBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.ResetReqBit.Address,
                });
            }
        }

        /// <summary>
        /// 측정 완료 비트 토글
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlcRstFin_Click(object sender, EventArgs e)
        {
            if (_omRef == OpModes.Ready)
            {
                InsertLog("USER.Toggle reset finished bit", LogMsg.Sources.APP);
                _qPlcReq.Enqueue(new PlcReq()
                {
                    PlcReqType = _pd.ResetFinBit.Bit ? PlcReqTypes.ClearFlag : PlcReqTypes.SetFlag,
                    PlcReqData = _pd.ResetFinBit.Address,
                });
            }
        }

        /// <summary>
        /// 에러상태 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TslErrStatus_Click(object sender, EventArgs e)
        {
            TslErrStatus.Text = "-";
            TslErrStatus.BackColor = DefaultBackColor;

        }

        /// <summary>
        /// 에러코드 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TslErrCode_Click(object sender, EventArgs e)
        {
            TslErrCode.Text = "-";
            TslErrCode.BackColor = DefaultBackColor;
        }
        #endregion

        #region 11300 이벤트.DB
        /// <summary>
        /// 프로파일 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbProfSave_Click(object sender, EventArgs e)
        {
            Focus();

            if (FluxDbProfile.SaveDatabase(_dtProf, out string errMsg, out int errCode))
            {
                InsertLog("Save DB profiles", LogMsg.Sources.APP);
                FgdProfile.DataSource = _dtProf;
            }
            else
            {
                InsertLog("프로파일 저장 실패", LogMsg.Sources.APP, errCode, errMsg);
            }
        }

        /// <summary>
        /// 프로파일 수정 내용을 원복한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDbProfReload_Click(object sender, EventArgs e)
        {
            _dtProf.RejectChanges();
            InsertLog("Reload DB profiles", LogMsg.Sources.APP);
        }

        /// <summary>
        /// 데이터를 엑셀로 익스포트 한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Export",
                Filter = "Excel file (*.xls)|*.xls",
                FileName = $"sma flux {DateTime.Now.ToString("yyyyMMdd_HHddss")}"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FgdMeasRes.SaveExcel(sfd.FileName, FileFlags.IncludeFixedCells);
                InsertLog($"Export grid to {sfd.FileName}", LogMsg.Sources.APP);
            }
        }

        /// <summary>
        /// 데이터를 클립보드로 카피한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmCopyDataToClipboard_Click(object sender, EventArgs e)
        {
            FgdMeasRes.Copy();
        }

        /// <summary>
        /// DB 폴더를 오픈한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmOpenDbFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(_fs.DbPath));
        }

        /// <summary>
        /// 선택한 아이템을 삭제한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmDbDeleteItems_Click(object sender, EventArgs e)
        {
            List<int> ls = new List<int>();
            var cr = FgdMeasRes.Selection;

            for (int i = cr.r1; i <= cr.r2; i++)
            {
                ls.Add(Convert.ToInt32(FgdMeasRes.Rows[i]["id"]));
            }

            if (ls.Count == 0)
            {
                MessageBox.Show("No items selected");
                return;
            }

            string msg = "Delete items";
            if (ls.Count < 10)
            {
                msg += "=" + string.Join(",", ls);
            }
            else
            {
                msg += $" from {ls[0]} to {ls.Last()}";
            }

            if (MessageBox.Show(msg, "Delete items", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DeleteMeasDb(ls, LoadMeasCond());
            }
        }

        /// <summary>
        /// 모두 선택한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmDbSelectAll_Click(object sender, EventArgs e)
        {
            FgdMeasRes.Select(0, 0, FgdMeasRes.Rows.Count - 1, FgdMeasRes.Cols.Count - 1);
        }
        #endregion

        #region 11400 이벤트.타이머
        /// <summary>
        /// 타이머 이벤트: GUI 업데이트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrApp_Tick(object sender, EventArgs e)
        {
            string errMsg;

            // 로그 출력
            int logCnt = 0;
            while (_qLog.TryDequeue(out LogMsg log) && logCnt < 100)
            {
                TbxLog.AppendText(LogMsg.GetLogTxt(log));
                if (log.Code > 0)
                {
                    TbxLog.AppendText(LogMsg.GetErrTxt(log));
                    
                    TslErrStatus.Text = $"ERR CODE={log.Code} | DATE={log.EventTime.ToString("yyyy-MM-dd mm:HH:ss")}";
                    TslErrStatus.BackColor = Color.Orange;
                }

                if (TbxLog.Lines.Length > _fs.LogMaximumLine)
                {
                    // 로그 줄 수 제한 기능
                    TbxLog.Lines = TbxLog.Lines.Skip(TbxLog.Lines.Length - _fs.LogMaximumLine).ToArray();
                }

                if (_fs.SaveAllLogToDb || log.Code > 0)
                {
                    // DB 삽입
                    if (LogMsg.SaveLogDb(log, LogMsg.DefaultFilePath(AppFolderName), out errMsg))
                    {
                        TslLogStatus.Text = "LOG";
                        TslLogStatus.BackColor = Color.Lime;
                    }
                    else
                    {
                        TslLogStatus.Text = errMsg;
                        TslLogStatus.BackColor = Color.Orange;
                    }
                }
                logCnt++;
            }

            if (logCnt > 0)
            {
                LoadLog(_fs.LogMaximumDbItem);

                if (_fs.LogHoldDays > 0)
                {
                    if (!LogMsg.DeleteOldLog(_fs.LogHoldDays, LogMsg.DefaultFilePath(AppFolderName), out errMsg))
                    {
                        TslLog.Text = $"Fail to delete old log={errMsg}";
                        TslLog.BackColor = Color.Orange;
                    }
                }
            }

            // 타이머 이벤트 처리
            if (_qTmr.TryDequeue(out TimerEvents tmr))
            {
                if (tmr == TimerEvents.FluxMeasurement)
                {
                    // 플럭스 측정 데이터를 GUI에 load 한다
                    if (_fd.Values.Count > 0)
                    {
                        LblNpMeas.Text = $"{_fd.NegPeak:f2}[mWb]";
                        LblPpMeas.Text = $"{_fd.PosPeak:f2}[mWb]";
                        LblTpMeas.Text = $"{_fd.TotalPeak:f2}[mWb]";

                        LblNpLLim.Text = $"{_fd.NegPeakLoLim:f2}[mWb]";
                        LblNpULim.Text = $"{_fd.NegPeakUpLim:f2}[mWb]";
                        LblPpLLim.Text = $"{_fd.PosPeakLoLim:f2}[mWb]";
                        LblPpULim.Text = $"{_fd.PosPeakUpLim:f2}[mWb]";
                        LblTpLLim.Text = $"{_fd.TotalPeakLoLim:f2}[mWb]";
                        LblTpULim.Text = $"{_fd.TotalPeakUpLim:f2}[mWb]";
                        LblOk.Text = _fd.Ok ? "OK" : "NG";
                        LblOk.BackColor = _fd.Ok ? Color.Lime : Color.Orange;
                        LblNpTitle.BackColor = _fd.NegOk ? Color.Lime : Color.Orange;
                        LblTpTitle.BackColor = _fd.TotalOk ? Color.Lime : Color.Orange;
                        LblPpTitle.BackColor = _fd.PosOk ? Color.Lime : Color.Orange;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Name", typeof(string));
                        dt.Columns.Add("Value", typeof(object));
                        for (int i = 0; i < _fd.Values.Count; i++)
                        {
                            dt.Rows.Add(_fd.Values.ToArray()[i].Key, _fd.Values.ToArray()[i].Value);
                        }
                        FgdMeasData.BeginUpdate();
                        FgdMeasData.DataSource = dt;
                        FgdMeasData.AutoSizeCols();
                        FgdMeasData.EndUpdate();
                    }
                }
                else if (tmr == TimerEvents.InsertDb)
                {
                    if (_pd.ProdData.Rows.Count == 0)
                    {
                        InsertLog("Warning: no product data", LogMsg.Sources.DB);
                    }

                    if (_fd.Values.Count == 0)
                    {
                        InsertLog("Warning: no measurement data", LogMsg.Sources.DB);
                    }

                    // 데이터를 DB에 삽입한다.
                    if (_mdb.InsertDb(_fd, _pd.ProdData, _fs.DbPath, out errMsg))
                    {
                        FluxDbMeas.DashConds dc = LoadDashCond();
                        FluxDbMeas.MeasConds mc = LoadMeasCond();

                        LoadMeasDb(mc);
                        if (_fs.LoadDashboardAfterInsert)
                        {
                            LoadHistogram(dc);
                            LoadTrackingChart(dc);
                        }
                        InsertLog("Insert result to DB", LogMsg.Sources.DB);
                    }
                    else
                    {
                        InsertLog("Fail to insert DB", LogMsg.Sources.DB, 11400, errMsg);
                    }
                }
                else if (tmr == TimerEvents.ReadProdInfor)
                {
                    FgdProdInf.BeginUpdate();
                    FgdProdInf.DataSource = _pd.ProdData;
                    FgdProdInf.Cols["name"].Caption = "Name";
                    FgdProdInf.Cols["dataType"].Caption = "Type";
                    FgdProdInf.Cols["startAddress"].Caption = "Address";
                    FgdProdInf.Cols["dbColumnName"].Caption = "DB Col.";
                    FgdProdInf.Cols["data"].Caption = "Data";
                    FgdProdInf.AutoSizeCols();
                    FgdProdInf.EndUpdate();
                }
                else if (tmr == TimerEvents.UploadMesData)
                {
                    // MES upload 이벤트
                    FgdMesData.BeginUpdate();
                    FgdMesData.DataSource = _dtMes;
                    FgdMesData.AutoSizeCols();
                    FgdMesData.EndUpdate();
                }
                else if (tmr == TimerEvents.ClearMesData)
                {
                    // MES clear 이벤트
                    FgdMesData.BeginUpdate();
                    FgdMesData.DataSource = _dtMes;
                    FgdMesData.AutoSizeCols();
                    FgdMesData.EndUpdate();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            // 프로그램 종료 대기
            if (_closeApp)
            {
                if (_bw.IsBusy)
                {
                    _bw.CancelAsync();
                }

                while (_bw.IsBusy)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }

                Close();
            }

            LblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            LblOpMode.Text = $"{_omRef.ToString()}";
        }
        #endregion

        #region 12000 메소드
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
            _qLog.Enqueue(msg);
            return true;
        }

        /// <summary>
        /// Ok 판정 GUI를 설정한다
        /// </summary>
        /// <param name="ok"></param>
        private void SetOkGui(FluxData md)
        {
            LblNpTitle.BackColor = md.NegOk ? Color.Lime : Color.Orange;
            LblPpTitle.BackColor = md.PosOk ? Color.Lime : Color.Orange;
            LblTpTitle.BackColor = md.TotalOk ? Color.Lime : Color.Orange;
            LblOk.Text = md.Ok ? "OK" : "NG";
            LblOk.BackColor = md.Ok ? Color.Lime : Color.Orange;
        }

        /// <summary>
        /// 플럭스미터를 리셋한다
        /// </summary>
        private static bool ResetFlux(string comPort, Fluxmeter fm, out string errMsg)
        {
            if (!fm.WriteWithLock(comPort, "PKRST", out errMsg))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 플럭스미터 측정 값
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fd"></param>
        /// <param name="modelNum"></param>
        /// <param name="fm"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool ReadFlux(DataRow dr, FluxData fd, int modelNum, Fluxmeter fm, out string errMsg)
        {
            string comPort = dr["portName"].ToString();
            if (!fm.ReadWithLock(comPort, "PKPOS?", out string pmsg))
            {
                errMsg = pmsg;
                return false;
            }
            if (!double.TryParse(pmsg, out double posPk))
            {
                errMsg = "Fluxmetuer -peak conversion";
                return false;
            }
            if (!fm.ReadWithLock(comPort, "PKNEG?", out string nmsg))
            {
                errMsg = nmsg;
                return false;
            }
            if (!double.TryParse(nmsg, out double negPk))
            {
                errMsg = "Fluxmetuer -peak conversion";
                return false;
            }

            double ppk = posPk * 1000 * Convert.ToDouble(dr["scale"]);
            double npk = Math.Abs(negPk) * 1000 * Convert.ToDouble(dr["scale"]);
            double tpk = ppk + npk;
            double npl = Convert.ToDouble(dr["negPeakLoLim"]);
            double npu = Convert.ToDouble(dr["negPeakUpLim"]);
            double tpl = Convert.ToDouble(dr["totalPeakLoLim"]);
            double tpu = Convert.ToDouble(dr["totalPeakUpLim"]);
            double ppl = Convert.ToDouble(dr["posPeakLoLim"]);
            double ppu = Convert.ToDouble(dr["posPeakUpLim"]);
            bool nok = npl <= npk & npk < npu;
            bool tok = tpl <= tpk & tpk < tpu;
            bool pok = ppl <= ppk & ppk < ppu;

            fd.Values.Clear();
            fd.Values.Add("labdate", DateTime.Now);
            fd.Values.Add("modelNumber", dr["modelNumber"]);
            fd.Values.Add("modelName", dr["modelName"]);
            fd.Values.Add("posPeak", ppk);
            fd.Values.Add("negPeak", npk);
            fd.Values.Add("totalPeak", ppk + npk);
            fd.Values.Add("negPeakLoLim", npl);
            fd.Values.Add("negPeakUpLim", npu);
            fd.Values.Add("totalPeakLoLim", tpl);
            fd.Values.Add("totalPeakUpLim", tpu);
            fd.Values.Add("posPeakLoLim", ppl);
            fd.Values.Add("posPeakUpLim", ppu);
            fd.Values.Add("negOk", nok);
            fd.Values.Add("totalOk", tok);
            fd.Values.Add("posOk", pok);
            fd.Values.Add("ok", nok & pok & tok);
            errMsg = "";
            return true;
        }

        #endregion

        #region 12100 메소드.DB
        /// <summary>
        /// 검색 설정을 생성한다
        /// </summary>
        /// <returns></returns>
        private FluxDbMeas.MeasConds LoadMeasCond()
        {
            FluxDbMeas.MeasConds sc = new FluxDbMeas.MeasConds
            {
                TodayOnly = CkbMeasToday.Checked,
                EnableStartDate = CkbMeasStartDate.Checked,
                StartDate = (DateTime)DteMeasStartDate.Value,
                EnableEndDate = CkbMeasEndDate.Checked,
                EndDate = (DateTime)DteMeasEndDate.Value,
                ModelNumbers = MstMeasModel.SelectedItems.Select(x => (int)x.Value).ToArray(),
                MaxItems = (int)CbxMeasItems.Value,
                PageNum = (int)NudDbPageNum.Value,
                PageSize = (int)CbxMeasItems.Value,
            };

            return sc;
        }

        /// <summary>
        /// 대쉬보드 설정을 로드한다
        /// </summary>
        /// <returns></returns>
        private FluxDbMeas.DashConds LoadDashCond()
        {
            FluxDbMeas.DashConds tc = new FluxDbMeas.DashConds
            {
                GroupCond = (FluxDbMeas.DashConds.GroupConds)CbxDashGroup.SelectedIndex,
                StartEnable = CkbDashStartDate.Checked,
                StartDate = (DateTime)DteDashStart.Value,
                EndEnable = CkbDashEndDate.Checked,
                EndDate = (DateTime)DteDashEnd.Value,
                NegativePeak = MstDashPeak.Items[0].Selected,
                TotalPeak = MstDashPeak.Items[1].Selected,
                PositivePeak = MstDashPeak.Items[2].Selected,
                ModelNumbers = MstDashModel.SelectedItems.Select(x => (int)x.Value).ToArray()
            };
            return tc;
        }

        /// <summary>
        /// 측정 DB를 로드한다
        /// </summary>
        private async void LoadMeasDb(FluxDbMeas.MeasConds sc)
        {
            DataTable dt = null;
            string errMsg = "";
            var t = Task.Run(() => FluxDbMeas.Load(_fs.DbPath, sc, out dt, out errMsg));
            await t;

            if (t.Result)
            {
                FluxDbMeas.SetFlexGridStyle(ref FgdMeasRes, dt);
            }
            else
            {
                InsertLog($"측정 DB 로드 실패", LogMsg.Sources.APP, 12100, errMsg);
            }
        }

        /// <summary>
        /// item을 삭제하고 DB를 다시 로드한다
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="sc"></param>
        private async void DeleteMeasDb(List<int> ids, FluxDbMeas.MeasConds sc)
        {
            string errMsg = "";
            var t1 = Task.Run(() => FluxDbMeas.Delete(_fs.DbPath, ids, out errMsg));
            await t1;

            if (t1.Result)
            {
                InsertLog($"Delete item(s) where id={string.Join(",", ids)}", LogMsg.Sources.APP);
            }
            else
            {
                InsertLog($"측정 DB 삭제 실패", LogMsg.Sources.APP, 12100, errMsg);
            }

            DataTable dt = null;
            var t2 = Task.Run(() => FluxDbMeas.Load(_fs.DbPath, sc, out dt, out errMsg));
            await t2;

            if (t2.Result)
            {
                FluxDbMeas.SetFlexGridStyle(ref FgdMeasRes, dt);
            }
            else
            {
                InsertLog($"측정 DB 로드 실패", LogMsg.Sources.APP, 12100, errMsg);
            }
        }

        /// <summary>
        /// 히스토그램 차트를 로드한다
        /// </summary>
        private async void LoadHistogram(FluxDbMeas.DashConds dc)
        {
            DataTable dt = null;
            string errMsg = "";
            var t = Task.Run(() => FluxDbMeas.DashConds.LoadHistogramData(FluxDbMeas.FilePath, dc, out dt, out string sql, out errMsg));
            await t;

            if (t.Result)
            {
                string[] bindings = new string[] { "negPeak", "totalPeak", "posPeak" };
                string[] names = new string[] { "Negative", "Total", "Positive" };
                FctPeaksHisto.BeginUpdate();
                FctPeaksHisto.Series.Clear();
                FctPeaksHisto.ChartType = C1.Chart.ChartType.Histogram;
                FctPeaksHisto.DataSource = dt;

                bool noPeakSelect = !dc.NegativePeak & !dc.TotalPeak & !dc.PositivePeak;
                if (dc.NegativePeak || noPeakSelect)
                {
                    Histogram hs = new Histogram()
                    {
                        Binding = "negPeak",
                        BindingX = "negPeak",
                        Name = "Negative Peak"
                    };
                    FctPeaksHisto.Series.Add(hs);
                }
                if (dc.TotalPeak || noPeakSelect)
                {
                    Histogram hs = new Histogram()
                    {
                        Binding = "totalPeak",
                        BindingX = "totalPeak",
                        Name = "Total Peak"
                    };
                    FctPeaksHisto.Series.Add(hs);
                }
                if (dc.PositivePeak || noPeakSelect)
                {
                    Histogram hs = new Histogram()
                    {
                        Binding = "posPeak",
                        BindingX = "posPeak",
                        Name = "Positive Peak"
                    };
                    FctPeaksHisto.Series.Add(hs);
                }
                FctPeaksHisto.AxisX.Format = "f2";
                FctPeaksHisto.DataLabel.Content = "{value}";
                FctPeaksHisto.DataLabel.Position = C1.Chart.LabelPosition.Top;
                FctPeaksHisto.EndUpdate();
            }
            else
            {
                InsertLog($"대쉬보드 - 히스토그램 로드 실패", LogMsg.Sources.APP, 12100, errMsg);
            }
        }

        /// <summary>
        /// 트래킹 차트를 로드한다
        /// </summary>
        /// <param name="dc"></param>
        private async void LoadTrackingChart(FluxDbMeas.DashConds dc)
        {
            DataTable dt = null;
            string errMsg = "";
            var t = Task.Run(() => FluxDbMeas.DashConds.LoadTrackingData(FluxDbMeas.FilePath, dc, out dt, out string sql, out errMsg));
            await t;

            if (t.Result)
            {
                FctPeaksLine.BeginUpdate();
                FctPeaksLine.Series.Clear();
                FctPeaksLine.BindingX = "labdate";
                FctPeaksLine.DataSource = dt;
                FctPeaksLine.ChartType = C1.Chart.ChartType.LineSymbols;

                bool noPeakSelect = !dc.NegativePeak & !dc.TotalPeak & !dc.PositivePeak;
                if (dc.NegativePeak || noPeakSelect)
                {
                    FctPeaksLine.Series.Add(new Series()
                    {
                        Binding = "npavg",
                        BindingX = "labdate",
                        Name = "Negative Peak"
                    });
                }
                if (dc.TotalPeak || noPeakSelect)
                {
                    FctPeaksLine.Series.Add(new Series()
                    {
                        Binding = "tpavg",
                        BindingX = "labdate",
                        Name = "Total Peak"
                    });
                }
                if (dc.PositivePeak || noPeakSelect)
                {
                    FctPeaksLine.Series.Add(new Series()
                    {
                        Binding = "ppavg",
                        BindingX = "labdate",
                        Name = "Positive Peak"
                    });
                }
                FctPeaksLine.EndUpdate();
            }
            else
            {
                InsertLog($"대쉬보드 - 트래킹 로드 실패", LogMsg.Sources.APP, 12100, errMsg);
            }
        }

        /// <summary>
        /// 로그를 로드한다
        /// </summary>
        private async void LoadLog(int maxLine)
        {
            DataTable dt = null;
            string errMsg = "";
            var t = Task.Run(() => LogMsg.LoadFromDb(LogMsg.DefaultFilePath(AppFolderName), maxLine, out dt, out errMsg));
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
                InsertLog($"로그 로드 실패", LogMsg.Sources.APP, 12100, errMsg);
            }
        }

        #endregion

 
    }
}
