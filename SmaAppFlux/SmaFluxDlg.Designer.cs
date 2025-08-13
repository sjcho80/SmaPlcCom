namespace SmaFlux
{
    partial class SmaFluxDlg
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            C1.Chart.ElementSize elementSize1 = new C1.Chart.ElementSize();
            C1.Win.Chart.Series series1 = new C1.Win.Chart.Series();
            C1.Chart.ElementSize elementSize2 = new C1.Chart.ElementSize();
            C1.Win.Chart.Series series2 = new C1.Win.Chart.Series();
            C1.Win.TreeView.C1CheckListItem c1CheckListItem1 = new C1.Win.TreeView.C1CheckListItem();
            C1.Win.TreeView.C1CheckListItem c1CheckListItem2 = new C1.Win.TreeView.C1CheckListItem();
            C1.Win.TreeView.C1CheckListItem c1CheckListItem3 = new C1.Win.TreeView.C1CheckListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmaFluxDlg));
            this.TmrApp = new System.Windows.Forms.Timer(this.components);
            this.TbxLog = new System.Windows.Forms.TextBox();
            this.TabMain = new C1.Win.C1Command.C1DockingTab();
            this.DtpMain = new C1.Win.C1Command.C1DockingTabPage();
            this.FgdMeasRes = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.PnlDbSearch = new System.Windows.Forms.Panel();
            this.CkbMeasToday = new System.Windows.Forms.CheckBox();
            this.MstMeasModel = new C1.Win.Input.C1MultiSelect();
            this.CkbMeasStartDate = new System.Windows.Forms.CheckBox();
            this.LblModels = new System.Windows.Forms.Label();
            this.DteMeasStartDate = new C1.Win.Calendar.C1DateEdit();
            this.DteMeasEndDate = new C1.Win.Calendar.C1DateEdit();
            this.CkbMeasEndDate = new System.Windows.Forms.CheckBox();
            this.TlpMeas = new System.Windows.Forms.TableLayoutPanel();
            this.TlpPps = new System.Windows.Forms.TableLayoutPanel();
            this.LblPpLLim = new System.Windows.Forms.Label();
            this.LblPpMeas = new System.Windows.Forms.Label();
            this.LblPpLLimTxt = new System.Windows.Forms.Label();
            this.LblPpMeasTxt = new System.Windows.Forms.Label();
            this.LblPpULimTxt = new System.Windows.Forms.Label();
            this.LblPpULim = new System.Windows.Forms.Label();
            this.TlpTps = new System.Windows.Forms.TableLayoutPanel();
            this.LblTpLLim = new System.Windows.Forms.Label();
            this.LblTpMeas = new System.Windows.Forms.Label();
            this.LblTpLLimTxt = new System.Windows.Forms.Label();
            this.LblTpMeasTxt = new System.Windows.Forms.Label();
            this.LblTpULimTxt = new System.Windows.Forms.Label();
            this.LblTpULim = new System.Windows.Forms.Label();
            this.LblPpTitle = new System.Windows.Forms.Label();
            this.LblTpTitle = new System.Windows.Forms.Label();
            this.LblNpTitle = new System.Windows.Forms.Label();
            this.TlpNps = new System.Windows.Forms.TableLayoutPanel();
            this.LblNpLLim = new System.Windows.Forms.Label();
            this.LblNpMeas = new System.Windows.Forms.Label();
            this.LblNpLLimTxt = new System.Windows.Forms.Label();
            this.LblNpMeasTxt = new System.Windows.Forms.Label();
            this.LblNpULimTxt = new System.Windows.Forms.Label();
            this.LblNpULim = new System.Windows.Forms.Label();
            this.PnlDbNavi = new System.Windows.Forms.Panel();
            this.BtnDbPageFwd = new System.Windows.Forms.Button();
            this.BtnDbPageReload = new System.Windows.Forms.Button();
            this.BtnDbPageHome = new System.Windows.Forms.Button();
            this.BtnDbPageBwd = new System.Windows.Forms.Button();
            this.NudDbPageNum = new System.Windows.Forms.NumericUpDown();
            this.CbxMeasItems = new C1.Win.C1Input.C1ComboBox();
            this.LblDbPageNum = new System.Windows.Forms.Label();
            this.LblMeasItems = new System.Windows.Forms.Label();
            this.DtpProfiles = new C1.Win.C1Command.C1DockingTabPage();
            this.BtnDbProfReload = new System.Windows.Forms.Button();
            this.BtnDbProfSave = new System.Windows.Forms.Button();
            this.FgdProfile = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.DtpSettings = new C1.Win.C1Command.C1DockingTabPage();
            this.PpgSettings = new System.Windows.Forms.PropertyGrid();
            this.BtnRollbackSettings = new System.Windows.Forms.Button();
            this.BtnSaveSettings = new System.Windows.Forms.Button();
            this.DtpDashBoard = new C1.Win.C1Command.C1DockingTabPage();
            this.SpcDashboard = new C1.Win.C1SplitContainer.C1SplitContainer();
            this.SppDashLineChart = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.FctPeaksLine = new C1.Win.Chart.FlexChart();
            this.SppDashHistoChart = new C1.Win.C1SplitContainer.C1SplitterPanel();
            this.FctPeaksHisto = new C1.Win.Chart.FlexChart();
            this.FlpDashboard = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnLoadDashboard = new System.Windows.Forms.Button();
            this.DteDashEnd = new C1.Win.Calendar.C1DateEdit();
            this.CkbDashEndDate = new System.Windows.Forms.CheckBox();
            this.DteDashStart = new C1.Win.Calendar.C1DateEdit();
            this.CkbDashStartDate = new System.Windows.Forms.CheckBox();
            this.MstDashModel = new C1.Win.Input.C1MultiSelect();
            this.LblDashModel = new System.Windows.Forms.Label();
            this.MstDashPeak = new C1.Win.Input.C1MultiSelect();
            this.LblDashPeak = new System.Windows.Forms.Label();
            this.CbxDashGroup = new System.Windows.Forms.ComboBox();
            this.LblDashGroup = new System.Windows.Forms.Label();
            this.DtpLog = new C1.Win.C1Command.C1DockingTabPage();
            this.FgdLog = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.LblModelNumberTitle = new System.Windows.Forms.Label();
            this.LblPlcAddModel = new System.Windows.Forms.Label();
            this.LblPlcAddHeartBeat = new System.Windows.Forms.Label();
            this.LblHeartBeat = new System.Windows.Forms.Label();
            this.BtnLblReadProInf = new System.Windows.Forms.Button();
            this.BtnPlcRstFin = new System.Windows.Forms.Button();
            this.LblPlcAddRstFin = new System.Windows.Forms.Label();
            this.BtnMeasReqResp = new System.Windows.Forms.Button();
            this.LblPlcAddMeasReqBit = new System.Windows.Forms.Label();
            this.BtnMeasFin = new System.Windows.Forms.Button();
            this.BtnAlarm = new System.Windows.Forms.Button();
            this.LblPlcAddMeasFin = new System.Windows.Forms.Label();
            this.BtnNg = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.LblPlcAddAlarm = new System.Windows.Forms.Label();
            this.LblPlcAddOk = new System.Windows.Forms.Label();
            this.LblPlcAddNg = new System.Windows.Forms.Label();
            this.LblPlcAddRstReq = new System.Windows.Forms.Label();
            this.BtnPlcRstReq = new System.Windows.Forms.Button();
            this.LblModelName = new System.Windows.Forms.Label();
            this.LblDateTime = new System.Windows.Forms.Label();
            this.LblOpMode = new System.Windows.Forms.Label();
            this.LblOk = new System.Windows.Forms.Label();
            this.BtnReadFlux = new System.Windows.Forms.Button();
            this.BtnResetFlux = new System.Windows.Forms.Button();
            this.BtnMeasInsert = new System.Windows.Forms.Button();
            this.BtnMeasUpload = new System.Windows.Forms.Button();
            this.FgdProdInf = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.FgdMesData = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.LblProdInfs = new System.Windows.Forms.Label();
            this.LblMesData = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TslLogStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslErrStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslErrCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblSiteInfor = new System.Windows.Forms.Label();
            this.FgdMeasData = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.LblMeasData = new System.Windows.Forms.Label();
            this.BtnMeasClear = new System.Windows.Forms.Button();
            this.BtnPlcConn = new System.Windows.Forms.Button();
            this.CmsDbMeas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmDbDeleteItems = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmCopyDataToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmOpenDbFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmDbSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.TssDbSeparator = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).BeginInit();
            this.TabMain.SuspendLayout();
            this.DtpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMeasRes)).BeginInit();
            this.PnlDbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DteMeasStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DteMeasEndDate)).BeginInit();
            this.TlpMeas.SuspendLayout();
            this.TlpPps.SuspendLayout();
            this.TlpTps.SuspendLayout();
            this.TlpNps.SuspendLayout();
            this.PnlDbNavi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudDbPageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CbxMeasItems)).BeginInit();
            this.DtpProfiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdProfile)).BeginInit();
            this.DtpSettings.SuspendLayout();
            this.DtpDashBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpcDashboard)).BeginInit();
            this.SpcDashboard.SuspendLayout();
            this.SppDashLineChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FctPeaksLine)).BeginInit();
            this.SppDashHistoChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FctPeaksHisto)).BeginInit();
            this.FlpDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DteDashEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DteDashStart)).BeginInit();
            this.DtpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdProdInf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMesData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMeasData)).BeginInit();
            this.CmsDbMeas.SuspendLayout();
            this.SuspendLayout();
            // 
            // TmrApp
            // 
            this.TmrApp.Enabled = true;
            this.TmrApp.Tick += new System.EventHandler(this.TmrApp_Tick);
            // 
            // TbxLog
            // 
            this.TbxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxLog.Location = new System.Drawing.Point(8, 720);
            this.TbxLog.Multiline = true;
            this.TbxLog.Name = "TbxLog";
            this.TbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbxLog.Size = new System.Drawing.Size(911, 108);
            this.TbxLog.TabIndex = 23;
            // 
            // TabMain
            // 
            this.TabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabMain.CanMoveTabs = true;
            this.TabMain.Controls.Add(this.DtpMain);
            this.TabMain.Controls.Add(this.DtpProfiles);
            this.TabMain.Controls.Add(this.DtpSettings);
            this.TabMain.Controls.Add(this.DtpDashBoard);
            this.TabMain.Controls.Add(this.DtpLog);
            this.TabMain.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TabMain.ItemSize = new System.Drawing.Size(0, 32);
            this.TabMain.Location = new System.Drawing.Point(8, 56);
            this.TabMain.Margin = new System.Windows.Forms.Padding(2);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedTabBold = true;
            this.TabMain.Size = new System.Drawing.Size(908, 652);
            this.TabMain.TabIndex = 24;
            this.TabMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.FillToEnd;
            this.TabMain.TabsSpacing = 5;
            // 
            // DtpMain
            // 
            this.DtpMain.Controls.Add(this.FgdMeasRes);
            this.DtpMain.Controls.Add(this.PnlDbSearch);
            this.DtpMain.Controls.Add(this.TlpMeas);
            this.DtpMain.Controls.Add(this.PnlDbNavi);
            this.DtpMain.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DtpMain.Image = global::SmaFlux.Properties.Resources.Home;
            this.DtpMain.Location = new System.Drawing.Point(1, 1);
            this.DtpMain.Name = "DtpMain";
            this.DtpMain.Size = new System.Drawing.Size(906, 616);
            this.DtpMain.TabIndex = 0;
            this.DtpMain.Text = "Main";
            // 
            // FgdMeasRes
            // 
            this.FgdMeasRes.AllowEditing = false;
            this.FgdMeasRes.AllowFiltering = true;
            this.FgdMeasRes.AutoClipboard = true;
            this.FgdMeasRes.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndAllHeaders;
            this.FgdMeasRes.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdMeasRes.ContextMenuStrip = this.CmsDbMeas;
            this.FgdMeasRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FgdMeasRes.ExtendLastCol = true;
            this.FgdMeasRes.Location = new System.Drawing.Point(0, 215);
            this.FgdMeasRes.Name = "FgdMeasRes";
            this.FgdMeasRes.Size = new System.Drawing.Size(906, 369);
            this.FgdMeasRes.TabIndex = 0;
            this.FgdMeasRes.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // PnlDbSearch
            // 
            this.PnlDbSearch.Controls.Add(this.CkbMeasToday);
            this.PnlDbSearch.Controls.Add(this.MstMeasModel);
            this.PnlDbSearch.Controls.Add(this.CkbMeasStartDate);
            this.PnlDbSearch.Controls.Add(this.LblModels);
            this.PnlDbSearch.Controls.Add(this.DteMeasStartDate);
            this.PnlDbSearch.Controls.Add(this.DteMeasEndDate);
            this.PnlDbSearch.Controls.Add(this.CkbMeasEndDate);
            this.PnlDbSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlDbSearch.Location = new System.Drawing.Point(0, 183);
            this.PnlDbSearch.Name = "PnlDbSearch";
            this.PnlDbSearch.Size = new System.Drawing.Size(906, 32);
            this.PnlDbSearch.TabIndex = 59;
            // 
            // CkbMeasToday
            // 
            this.CkbMeasToday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CkbMeasToday.AutoSize = true;
            this.CkbMeasToday.Location = new System.Drawing.Point(8, 8);
            this.CkbMeasToday.Margin = new System.Windows.Forms.Padding(2);
            this.CkbMeasToday.Name = "CkbMeasToday";
            this.CkbMeasToday.Size = new System.Drawing.Size(60, 16);
            this.CkbMeasToday.TabIndex = 0;
            this.CkbMeasToday.Text = "Today";
            this.CkbMeasToday.UseVisualStyleBackColor = true;
            // 
            // MstMeasModel
            // 
            this.MstMeasModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MstMeasModel.Location = new System.Drawing.Point(584, 8);
            this.MstMeasModel.Margin = new System.Windows.Forms.Padding(2);
            this.MstMeasModel.Name = "MstMeasModel";
            this.MstMeasModel.Size = new System.Drawing.Size(132, 17);
            this.MstMeasModel.TabIndex = 59;
            // 
            // CkbMeasStartDate
            // 
            this.CkbMeasStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CkbMeasStartDate.Location = new System.Drawing.Point(72, 6);
            this.CkbMeasStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.CkbMeasStartDate.Name = "CkbMeasStartDate";
            this.CkbMeasStartDate.Size = new System.Drawing.Size(78, 20);
            this.CkbMeasStartDate.TabIndex = 2;
            this.CkbMeasStartDate.Text = "Start date";
            this.CkbMeasStartDate.UseVisualStyleBackColor = true;
            // 
            // LblModels
            // 
            this.LblModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LblModels.AutoSize = true;
            this.LblModels.Location = new System.Drawing.Point(536, 10);
            this.LblModels.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModels.Name = "LblModels";
            this.LblModels.Size = new System.Drawing.Size(47, 12);
            this.LblModels.TabIndex = 58;
            this.LblModels.Text = "Models";
            this.LblModels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DteMeasStartDate
            // 
            this.DteMeasStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DteMeasStartDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.DteMeasStartDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.DteMeasStartDate.CustomFormat = "yy-MM-dd HH:mm:ss";
            this.DteMeasStartDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DteMeasStartDate.GapHeight = 0;
            this.DteMeasStartDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.DteMeasStartDate.Location = new System.Drawing.Point(152, 7);
            this.DteMeasStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.DteMeasStartDate.Name = "DteMeasStartDate";
            this.DteMeasStartDate.Size = new System.Drawing.Size(143, 19);
            this.DteMeasStartDate.TabIndex = 1;
            this.DteMeasStartDate.Tag = null;
            // 
            // DteMeasEndDate
            // 
            this.DteMeasEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DteMeasEndDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.DteMeasEndDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.DteMeasEndDate.CustomFormat = "yy-MM-dd HH:mm:ss";
            this.DteMeasEndDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DteMeasEndDate.GapHeight = 0;
            this.DteMeasEndDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.DteMeasEndDate.Location = new System.Drawing.Point(384, 7);
            this.DteMeasEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.DteMeasEndDate.Name = "DteMeasEndDate";
            this.DteMeasEndDate.Size = new System.Drawing.Size(141, 19);
            this.DteMeasEndDate.TabIndex = 11;
            this.DteMeasEndDate.Tag = null;
            // 
            // CkbMeasEndDate
            // 
            this.CkbMeasEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CkbMeasEndDate.Location = new System.Drawing.Point(304, 6);
            this.CkbMeasEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.CkbMeasEndDate.Name = "CkbMeasEndDate";
            this.CkbMeasEndDate.Size = new System.Drawing.Size(74, 20);
            this.CkbMeasEndDate.TabIndex = 4;
            this.CkbMeasEndDate.Text = "End date";
            this.CkbMeasEndDate.UseVisualStyleBackColor = true;
            // 
            // TlpMeas
            // 
            this.TlpMeas.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.TlpMeas.ColumnCount = 3;
            this.TlpMeas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.TlpMeas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpMeas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpMeas.Controls.Add(this.TlpPps, 2, 1);
            this.TlpMeas.Controls.Add(this.TlpTps, 1, 1);
            this.TlpMeas.Controls.Add(this.LblPpTitle, 2, 0);
            this.TlpMeas.Controls.Add(this.LblTpTitle, 1, 0);
            this.TlpMeas.Controls.Add(this.LblNpTitle, 0, 0);
            this.TlpMeas.Controls.Add(this.TlpNps, 0, 1);
            this.TlpMeas.Dock = System.Windows.Forms.DockStyle.Top;
            this.TlpMeas.Location = new System.Drawing.Point(0, 0);
            this.TlpMeas.Name = "TlpMeas";
            this.TlpMeas.RowCount = 2;
            this.TlpMeas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TlpMeas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.TlpMeas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TlpMeas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TlpMeas.Size = new System.Drawing.Size(906, 183);
            this.TlpMeas.TabIndex = 1;
            // 
            // TlpPps
            // 
            this.TlpPps.ColumnCount = 2;
            this.TlpPps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpPps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpPps.Controls.Add(this.LblPpLLim, 1, 2);
            this.TlpPps.Controls.Add(this.LblPpMeas, 1, 1);
            this.TlpPps.Controls.Add(this.LblPpLLimTxt, 0, 2);
            this.TlpPps.Controls.Add(this.LblPpMeasTxt, 0, 1);
            this.TlpPps.Controls.Add(this.LblPpULimTxt, 0, 0);
            this.TlpPps.Controls.Add(this.LblPpULim, 1, 0);
            this.TlpPps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TlpPps.Location = new System.Drawing.Point(606, 50);
            this.TlpPps.Name = "TlpPps";
            this.TlpPps.RowCount = 3;
            this.TlpPps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpPps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpPps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpPps.Size = new System.Drawing.Size(296, 129);
            this.TlpPps.TabIndex = 12;
            // 
            // LblPpLLim
            // 
            this.LblPpLLim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpLLim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpLLim.Location = new System.Drawing.Point(151, 86);
            this.LblPpLLim.Name = "LblPpLLim";
            this.LblPpLLim.Size = new System.Drawing.Size(142, 43);
            this.LblPpLLim.TabIndex = 11;
            this.LblPpLLim.Text = "0.00 [mWb]";
            this.LblPpLLim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPpMeas
            // 
            this.LblPpMeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpMeas.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpMeas.Location = new System.Drawing.Point(151, 43);
            this.LblPpMeas.Name = "LblPpMeas";
            this.LblPpMeas.Size = new System.Drawing.Size(142, 43);
            this.LblPpMeas.TabIndex = 11;
            this.LblPpMeas.Text = "0.00 [mWb]";
            this.LblPpMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPpLLimTxt
            // 
            this.LblPpLLimTxt.AutoSize = true;
            this.LblPpLLimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpLLimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpLLimTxt.Location = new System.Drawing.Point(3, 86);
            this.LblPpLLimTxt.Name = "LblPpLLimTxt";
            this.LblPpLLimTxt.Size = new System.Drawing.Size(142, 43);
            this.LblPpLLimTxt.TabIndex = 6;
            this.LblPpLLimTxt.Text = "Lower limit";
            this.LblPpLLimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPpMeasTxt
            // 
            this.LblPpMeasTxt.AutoSize = true;
            this.LblPpMeasTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpMeasTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpMeasTxt.Location = new System.Drawing.Point(3, 43);
            this.LblPpMeasTxt.Name = "LblPpMeasTxt";
            this.LblPpMeasTxt.Size = new System.Drawing.Size(142, 43);
            this.LblPpMeasTxt.TabIndex = 2;
            this.LblPpMeasTxt.Text = "Measurement";
            this.LblPpMeasTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPpULimTxt
            // 
            this.LblPpULimTxt.AutoSize = true;
            this.LblPpULimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpULimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpULimTxt.Location = new System.Drawing.Point(3, 0);
            this.LblPpULimTxt.Name = "LblPpULimTxt";
            this.LblPpULimTxt.Size = new System.Drawing.Size(142, 43);
            this.LblPpULimTxt.TabIndex = 5;
            this.LblPpULimTxt.Text = "Upper limit";
            this.LblPpULimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPpULim
            // 
            this.LblPpULim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpULim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpULim.Location = new System.Drawing.Point(151, 0);
            this.LblPpULim.Name = "LblPpULim";
            this.LblPpULim.Size = new System.Drawing.Size(142, 43);
            this.LblPpULim.TabIndex = 4;
            this.LblPpULim.Text = "0.00 [mWb]";
            this.LblPpULim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TlpTps
            // 
            this.TlpTps.ColumnCount = 2;
            this.TlpTps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpTps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpTps.Controls.Add(this.LblTpLLim, 1, 2);
            this.TlpTps.Controls.Add(this.LblTpMeas, 1, 1);
            this.TlpTps.Controls.Add(this.LblTpLLimTxt, 0, 2);
            this.TlpTps.Controls.Add(this.LblTpMeasTxt, 0, 1);
            this.TlpTps.Controls.Add(this.LblTpULimTxt, 0, 0);
            this.TlpTps.Controls.Add(this.LblTpULim, 1, 0);
            this.TlpTps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TlpTps.Location = new System.Drawing.Point(305, 50);
            this.TlpTps.Name = "TlpTps";
            this.TlpTps.RowCount = 3;
            this.TlpTps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpTps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpTps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpTps.Size = new System.Drawing.Size(294, 129);
            this.TlpTps.TabIndex = 11;
            // 
            // LblTpLLim
            // 
            this.LblTpLLim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpLLim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpLLim.Location = new System.Drawing.Point(150, 86);
            this.LblTpLLim.Name = "LblTpLLim";
            this.LblTpLLim.Size = new System.Drawing.Size(141, 43);
            this.LblTpLLim.TabIndex = 11;
            this.LblTpLLim.Text = "0.00 [mWb]";
            this.LblTpLLim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTpMeas
            // 
            this.LblTpMeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpMeas.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpMeas.Location = new System.Drawing.Point(150, 43);
            this.LblTpMeas.Name = "LblTpMeas";
            this.LblTpMeas.Size = new System.Drawing.Size(141, 43);
            this.LblTpMeas.TabIndex = 11;
            this.LblTpMeas.Text = "0.00 [mWb]";
            this.LblTpMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTpLLimTxt
            // 
            this.LblTpLLimTxt.AutoSize = true;
            this.LblTpLLimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpLLimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpLLimTxt.Location = new System.Drawing.Point(3, 86);
            this.LblTpLLimTxt.Name = "LblTpLLimTxt";
            this.LblTpLLimTxt.Size = new System.Drawing.Size(141, 43);
            this.LblTpLLimTxt.TabIndex = 6;
            this.LblTpLLimTxt.Text = "Lower limit";
            this.LblTpLLimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTpMeasTxt
            // 
            this.LblTpMeasTxt.AutoSize = true;
            this.LblTpMeasTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpMeasTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpMeasTxt.Location = new System.Drawing.Point(3, 43);
            this.LblTpMeasTxt.Name = "LblTpMeasTxt";
            this.LblTpMeasTxt.Size = new System.Drawing.Size(141, 43);
            this.LblTpMeasTxt.TabIndex = 2;
            this.LblTpMeasTxt.Text = "Measurement";
            this.LblTpMeasTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTpULimTxt
            // 
            this.LblTpULimTxt.AutoSize = true;
            this.LblTpULimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpULimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpULimTxt.Location = new System.Drawing.Point(3, 0);
            this.LblTpULimTxt.Name = "LblTpULimTxt";
            this.LblTpULimTxt.Size = new System.Drawing.Size(141, 43);
            this.LblTpULimTxt.TabIndex = 5;
            this.LblTpULimTxt.Text = "Upper limit";
            this.LblTpULimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTpULim
            // 
            this.LblTpULim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpULim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpULim.Location = new System.Drawing.Point(150, 0);
            this.LblTpULim.Name = "LblTpULim";
            this.LblTpULim.Size = new System.Drawing.Size(141, 43);
            this.LblTpULim.TabIndex = 4;
            this.LblTpULim.Text = "0.00 [mWb]";
            this.LblTpULim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPpTitle
            // 
            this.LblPpTitle.AutoSize = true;
            this.LblPpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPpTitle.Font = new System.Drawing.Font("굴림", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPpTitle.Location = new System.Drawing.Point(606, 1);
            this.LblPpTitle.Name = "LblPpTitle";
            this.LblPpTitle.Size = new System.Drawing.Size(296, 45);
            this.LblPpTitle.TabIndex = 10;
            this.LblPpTitle.Text = "POSITIVE PEAK";
            this.LblPpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTpTitle
            // 
            this.LblTpTitle.AutoSize = true;
            this.LblTpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTpTitle.Font = new System.Drawing.Font("굴림", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTpTitle.Location = new System.Drawing.Point(305, 1);
            this.LblTpTitle.Name = "LblTpTitle";
            this.LblTpTitle.Size = new System.Drawing.Size(294, 45);
            this.LblTpTitle.TabIndex = 9;
            this.LblTpTitle.Text = "TOTAL PEAK";
            this.LblTpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblNpTitle
            // 
            this.LblNpTitle.AutoSize = true;
            this.LblNpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpTitle.Font = new System.Drawing.Font("굴림", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpTitle.Location = new System.Drawing.Point(4, 1);
            this.LblNpTitle.Name = "LblNpTitle";
            this.LblNpTitle.Size = new System.Drawing.Size(294, 45);
            this.LblNpTitle.TabIndex = 8;
            this.LblNpTitle.Text = "NEGATIVE PEAK";
            this.LblNpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TlpNps
            // 
            this.TlpNps.ColumnCount = 2;
            this.TlpNps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpNps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpNps.Controls.Add(this.LblNpLLim, 1, 2);
            this.TlpNps.Controls.Add(this.LblNpMeas, 1, 1);
            this.TlpNps.Controls.Add(this.LblNpLLimTxt, 0, 2);
            this.TlpNps.Controls.Add(this.LblNpMeasTxt, 0, 1);
            this.TlpNps.Controls.Add(this.LblNpULimTxt, 0, 0);
            this.TlpNps.Controls.Add(this.LblNpULim, 1, 0);
            this.TlpNps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TlpNps.Location = new System.Drawing.Point(4, 50);
            this.TlpNps.Name = "TlpNps";
            this.TlpNps.RowCount = 3;
            this.TlpNps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpNps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpNps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TlpNps.Size = new System.Drawing.Size(294, 129);
            this.TlpNps.TabIndex = 7;
            // 
            // LblNpLLim
            // 
            this.LblNpLLim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpLLim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpLLim.Location = new System.Drawing.Point(150, 86);
            this.LblNpLLim.Name = "LblNpLLim";
            this.LblNpLLim.Size = new System.Drawing.Size(141, 43);
            this.LblNpLLim.TabIndex = 11;
            this.LblNpLLim.Text = "0.00 [mWb]";
            this.LblNpLLim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblNpMeas
            // 
            this.LblNpMeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpMeas.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpMeas.Location = new System.Drawing.Point(150, 43);
            this.LblNpMeas.Name = "LblNpMeas";
            this.LblNpMeas.Size = new System.Drawing.Size(141, 43);
            this.LblNpMeas.TabIndex = 11;
            this.LblNpMeas.Text = "0.00 [mWb]";
            this.LblNpMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblNpLLimTxt
            // 
            this.LblNpLLimTxt.AutoSize = true;
            this.LblNpLLimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpLLimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpLLimTxt.Location = new System.Drawing.Point(3, 86);
            this.LblNpLLimTxt.Name = "LblNpLLimTxt";
            this.LblNpLLimTxt.Size = new System.Drawing.Size(141, 43);
            this.LblNpLLimTxt.TabIndex = 6;
            this.LblNpLLimTxt.Text = "Lower limit";
            this.LblNpLLimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblNpMeasTxt
            // 
            this.LblNpMeasTxt.AutoSize = true;
            this.LblNpMeasTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpMeasTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpMeasTxt.Location = new System.Drawing.Point(3, 43);
            this.LblNpMeasTxt.Name = "LblNpMeasTxt";
            this.LblNpMeasTxt.Size = new System.Drawing.Size(141, 43);
            this.LblNpMeasTxt.TabIndex = 2;
            this.LblNpMeasTxt.Text = "Measurement";
            this.LblNpMeasTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblNpULimTxt
            // 
            this.LblNpULimTxt.AutoSize = true;
            this.LblNpULimTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpULimTxt.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpULimTxt.Location = new System.Drawing.Point(3, 0);
            this.LblNpULimTxt.Name = "LblNpULimTxt";
            this.LblNpULimTxt.Size = new System.Drawing.Size(141, 43);
            this.LblNpULimTxt.TabIndex = 5;
            this.LblNpULimTxt.Text = "Upper limit";
            this.LblNpULimTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblNpULim
            // 
            this.LblNpULim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNpULim.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblNpULim.Location = new System.Drawing.Point(150, 0);
            this.LblNpULim.Name = "LblNpULim";
            this.LblNpULim.Size = new System.Drawing.Size(141, 43);
            this.LblNpULim.TabIndex = 4;
            this.LblNpULim.Text = "0.00 [mWb]";
            this.LblNpULim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PnlDbNavi
            // 
            this.PnlDbNavi.Controls.Add(this.BtnDbPageFwd);
            this.PnlDbNavi.Controls.Add(this.BtnDbPageReload);
            this.PnlDbNavi.Controls.Add(this.BtnDbPageHome);
            this.PnlDbNavi.Controls.Add(this.BtnDbPageBwd);
            this.PnlDbNavi.Controls.Add(this.NudDbPageNum);
            this.PnlDbNavi.Controls.Add(this.CbxMeasItems);
            this.PnlDbNavi.Controls.Add(this.LblDbPageNum);
            this.PnlDbNavi.Controls.Add(this.LblMeasItems);
            this.PnlDbNavi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlDbNavi.Location = new System.Drawing.Point(0, 584);
            this.PnlDbNavi.Name = "PnlDbNavi";
            this.PnlDbNavi.Size = new System.Drawing.Size(906, 32);
            this.PnlDbNavi.TabIndex = 58;
            // 
            // BtnDbPageFwd
            // 
            this.BtnDbPageFwd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnDbPageFwd.Location = new System.Drawing.Point(8, 4);
            this.BtnDbPageFwd.Name = "BtnDbPageFwd";
            this.BtnDbPageFwd.Size = new System.Drawing.Size(64, 24);
            this.BtnDbPageFwd.TabIndex = 62;
            this.BtnDbPageFwd.Text = "<<";
            this.BtnDbPageFwd.UseVisualStyleBackColor = true;
            this.BtnDbPageFwd.Click += new System.EventHandler(this.BtnDbPageFwd_Click);
            // 
            // BtnDbPageReload
            // 
            this.BtnDbPageReload.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnDbPageReload.Location = new System.Drawing.Point(392, 4);
            this.BtnDbPageReload.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDbPageReload.Name = "BtnDbPageReload";
            this.BtnDbPageReload.Size = new System.Drawing.Size(64, 24);
            this.BtnDbPageReload.TabIndex = 56;
            this.BtnDbPageReload.Text = "Reload";
            this.BtnDbPageReload.UseVisualStyleBackColor = true;
            this.BtnDbPageReload.Click += new System.EventHandler(this.BtnDbPageReload_Click);
            // 
            // BtnDbPageHome
            // 
            this.BtnDbPageHome.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnDbPageHome.Location = new System.Drawing.Point(80, 4);
            this.BtnDbPageHome.Name = "BtnDbPageHome";
            this.BtnDbPageHome.Size = new System.Drawing.Size(64, 24);
            this.BtnDbPageHome.TabIndex = 62;
            this.BtnDbPageHome.Text = "Home";
            this.BtnDbPageHome.UseVisualStyleBackColor = true;
            this.BtnDbPageHome.Click += new System.EventHandler(this.BtnDbPageHome_Click);
            // 
            // BtnDbPageBwd
            // 
            this.BtnDbPageBwd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnDbPageBwd.Location = new System.Drawing.Point(832, 4);
            this.BtnDbPageBwd.Name = "BtnDbPageBwd";
            this.BtnDbPageBwd.Size = new System.Drawing.Size(64, 24);
            this.BtnDbPageBwd.TabIndex = 62;
            this.BtnDbPageBwd.Text = ">>";
            this.BtnDbPageBwd.UseVisualStyleBackColor = true;
            this.BtnDbPageBwd.Click += new System.EventHandler(this.BtnDbPageBwd_Click);
            // 
            // NudDbPageNum
            // 
            this.NudDbPageNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NudDbPageNum.Location = new System.Drawing.Point(192, 6);
            this.NudDbPageNum.Name = "NudDbPageNum";
            this.NudDbPageNum.Size = new System.Drawing.Size(80, 21);
            this.NudDbPageNum.TabIndex = 61;
            // 
            // CbxMeasItems
            // 
            this.CbxMeasItems.AllowSpinLoop = false;
            this.CbxMeasItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CbxMeasItems.DataType = typeof(int);
            this.CbxMeasItems.GapHeight = 0;
            this.CbxMeasItems.ImagePadding = new System.Windows.Forms.Padding(0);
            this.CbxMeasItems.Items.Add("0");
            this.CbxMeasItems.Items.Add("25");
            this.CbxMeasItems.Items.Add("50");
            this.CbxMeasItems.Items.Add("100");
            this.CbxMeasItems.Items.Add("250");
            this.CbxMeasItems.Items.Add("500");
            this.CbxMeasItems.Location = new System.Drawing.Point(320, 7);
            this.CbxMeasItems.Margin = new System.Windows.Forms.Padding(2);
            this.CbxMeasItems.Name = "CbxMeasItems";
            this.CbxMeasItems.Size = new System.Drawing.Size(65, 19);
            this.CbxMeasItems.TabIndex = 60;
            this.CbxMeasItems.Tag = null;
            // 
            // LblDbPageNum
            // 
            this.LblDbPageNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblDbPageNum.AutoSize = true;
            this.LblDbPageNum.Location = new System.Drawing.Point(152, 10);
            this.LblDbPageNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDbPageNum.Name = "LblDbPageNum";
            this.LblDbPageNum.Size = new System.Drawing.Size(34, 12);
            this.LblDbPageNum.TabIndex = 6;
            this.LblDbPageNum.Text = "Page";
            this.LblDbPageNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblMeasItems
            // 
            this.LblMeasItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblMeasItems.AutoSize = true;
            this.LblMeasItems.Location = new System.Drawing.Point(280, 10);
            this.LblMeasItems.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasItems.Name = "LblMeasItems";
            this.LblMeasItems.Size = new System.Drawing.Size(36, 12);
            this.LblMeasItems.TabIndex = 6;
            this.LblMeasItems.Text = "Items";
            this.LblMeasItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DtpProfiles
            // 
            this.DtpProfiles.Controls.Add(this.BtnDbProfReload);
            this.DtpProfiles.Controls.Add(this.BtnDbProfSave);
            this.DtpProfiles.Controls.Add(this.FgdProfile);
            this.DtpProfiles.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DtpProfiles.Image = global::SmaFlux.Properties.Resources.Profile;
            this.DtpProfiles.Location = new System.Drawing.Point(1, 1);
            this.DtpProfiles.Name = "DtpProfiles";
            this.DtpProfiles.Size = new System.Drawing.Size(906, 616);
            this.DtpProfiles.TabIndex = 1;
            this.DtpProfiles.Text = "Profiles";
            // 
            // BtnDbProfReload
            // 
            this.BtnDbProfReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDbProfReload.Location = new System.Drawing.Point(549, 576);
            this.BtnDbProfReload.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDbProfReload.Name = "BtnDbProfReload";
            this.BtnDbProfReload.Size = new System.Drawing.Size(168, 32);
            this.BtnDbProfReload.TabIndex = 1;
            this.BtnDbProfReload.Text = "Reload";
            this.BtnDbProfReload.UseVisualStyleBackColor = true;
            this.BtnDbProfReload.Click += new System.EventHandler(this.BtnDbProfReload_Click);
            // 
            // BtnDbProfSave
            // 
            this.BtnDbProfSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDbProfSave.Location = new System.Drawing.Point(725, 576);
            this.BtnDbProfSave.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDbProfSave.Name = "BtnDbProfSave";
            this.BtnDbProfSave.Size = new System.Drawing.Size(168, 32);
            this.BtnDbProfSave.TabIndex = 1;
            this.BtnDbProfSave.Text = "Save";
            this.BtnDbProfSave.UseVisualStyleBackColor = true;
            this.BtnDbProfSave.Click += new System.EventHandler(this.BtnDbProfSave_Click);
            // 
            // FgdProfile
            // 
            this.FgdProfile.AllowAddNew = true;
            this.FgdProfile.AllowDelete = true;
            this.FgdProfile.AllowFiltering = true;
            this.FgdProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdProfile.AutoClipboard = true;
            this.FgdProfile.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdProfile.ExtendLastCol = true;
            this.FgdProfile.Location = new System.Drawing.Point(19, 17);
            this.FgdProfile.Margin = new System.Windows.Forms.Padding(2);
            this.FgdProfile.Name = "FgdProfile";
            this.FgdProfile.Size = new System.Drawing.Size(873, 550);
            this.FgdProfile.TabIndex = 0;
            this.FgdProfile.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // DtpSettings
            // 
            this.DtpSettings.Controls.Add(this.PpgSettings);
            this.DtpSettings.Controls.Add(this.BtnRollbackSettings);
            this.DtpSettings.Controls.Add(this.BtnSaveSettings);
            this.DtpSettings.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DtpSettings.Image = global::SmaFlux.Properties.Resources.Settings;
            this.DtpSettings.Location = new System.Drawing.Point(1, 1);
            this.DtpSettings.Name = "DtpSettings";
            this.DtpSettings.Size = new System.Drawing.Size(906, 616);
            this.DtpSettings.TabIndex = 2;
            this.DtpSettings.Text = "Settings";
            // 
            // PpgSettings
            // 
            this.PpgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PpgSettings.Location = new System.Drawing.Point(18, 8);
            this.PpgSettings.Margin = new System.Windows.Forms.Padding(2);
            this.PpgSettings.Name = "PpgSettings";
            this.PpgSettings.Size = new System.Drawing.Size(874, 557);
            this.PpgSettings.TabIndex = 15;
            // 
            // BtnRollbackSettings
            // 
            this.BtnRollbackSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRollbackSettings.Location = new System.Drawing.Point(549, 576);
            this.BtnRollbackSettings.Margin = new System.Windows.Forms.Padding(2);
            this.BtnRollbackSettings.Name = "BtnRollbackSettings";
            this.BtnRollbackSettings.Size = new System.Drawing.Size(168, 32);
            this.BtnRollbackSettings.TabIndex = 56;
            this.BtnRollbackSettings.Text = "Rollback";
            this.BtnRollbackSettings.UseVisualStyleBackColor = true;
            this.BtnRollbackSettings.Click += new System.EventHandler(this.BtnRollbackSettings_Click);
            // 
            // BtnSaveSettings
            // 
            this.BtnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaveSettings.Location = new System.Drawing.Point(725, 576);
            this.BtnSaveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSaveSettings.Name = "BtnSaveSettings";
            this.BtnSaveSettings.Size = new System.Drawing.Size(168, 32);
            this.BtnSaveSettings.TabIndex = 56;
            this.BtnSaveSettings.Text = "Save";
            this.BtnSaveSettings.UseVisualStyleBackColor = true;
            this.BtnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);
            // 
            // DtpDashBoard
            // 
            this.DtpDashBoard.Controls.Add(this.SpcDashboard);
            this.DtpDashBoard.Controls.Add(this.FlpDashboard);
            this.DtpDashBoard.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DtpDashBoard.Image = global::SmaFlux.Properties.Resources.Dashboard;
            this.DtpDashBoard.Location = new System.Drawing.Point(1, 1);
            this.DtpDashBoard.Name = "DtpDashBoard";
            this.DtpDashBoard.Size = new System.Drawing.Size(906, 616);
            this.DtpDashBoard.TabIndex = 3;
            this.DtpDashBoard.Text = "Dashboard";
            // 
            // SpcDashboard
            // 
            this.SpcDashboard.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.SpcDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.SpcDashboard.CollapsingCueColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(133)))), ((int)(((byte)(150)))));
            this.SpcDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpcDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SpcDashboard.HeaderHeight = 15;
            this.SpcDashboard.Location = new System.Drawing.Point(0, 0);
            this.SpcDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.SpcDashboard.Name = "SpcDashboard";
            this.SpcDashboard.Panels.Add(this.SppDashLineChart);
            this.SpcDashboard.Panels.Add(this.SppDashHistoChart);
            this.SpcDashboard.Size = new System.Drawing.Size(906, 566);
            this.SpcDashboard.SplitterWidth = 3;
            this.SpcDashboard.TabIndex = 1;
            // 
            // SppDashLineChart
            // 
            this.SppDashLineChart.Controls.Add(this.FctPeaksLine);
            this.SppDashLineChart.Height = 281;
            this.SppDashLineChart.Location = new System.Drawing.Point(0, 15);
            this.SppDashLineChart.MinHeight = 27;
            this.SppDashLineChart.MinWidth = 27;
            this.SppDashLineChart.Name = "SppDashLineChart";
            this.SppDashLineChart.Size = new System.Drawing.Size(906, 266);
            this.SppDashLineChart.SizeRatio = 49.939D;
            this.SppDashLineChart.TabIndex = 0;
            this.SppDashLineChart.Text = "Tracking data";
            this.SppDashLineChart.Width = 906;
            // 
            // FctPeaksLine
            // 
            this.FctPeaksLine.AnimationLoad.Direction = C1.Chart.AnimationDirection.Y;
            this.FctPeaksLine.AnimationLoad.Duration = 400;
            this.FctPeaksLine.AnimationLoad.Easing = C1.Chart.Easing.Linear;
            this.FctPeaksLine.AnimationLoad.Type = C1.Chart.AnimationType.All;
            this.FctPeaksLine.AnimationSettings = C1.Chart.AnimationSettings.None;
            this.FctPeaksLine.AnimationUpdate.Duration = 400;
            this.FctPeaksLine.AnimationUpdate.Easing = C1.Chart.Easing.Linear;
            this.FctPeaksLine.AnimationUpdate.Type = C1.Chart.AnimationType.All;
            this.FctPeaksLine.AxisX.Chart = this.FctPeaksLine;
            this.FctPeaksLine.AxisX.DataSource = null;
            this.FctPeaksLine.AxisX.GroupProvider = null;
            this.FctPeaksLine.AxisX.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.FctPeaksLine.AxisX.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.FctPeaksLine.AxisX.GroupVisibilityLevel = 0;
            this.FctPeaksLine.AxisX.LabelMax = false;
            this.FctPeaksLine.AxisX.LabelMin = false;
            this.FctPeaksLine.AxisX.PlotAreaName = null;
            this.FctPeaksLine.AxisX.Position = C1.Chart.Position.Bottom;
            this.FctPeaksLine.AxisX.TimeUnit = C1.Chart.TimeUnits.Day;
            this.FctPeaksLine.AxisX.Title = "Date";
            this.FctPeaksLine.AxisY.AxisLine = false;
            this.FctPeaksLine.AxisY.Chart = this.FctPeaksLine;
            this.FctPeaksLine.AxisY.DataSource = null;
            this.FctPeaksLine.AxisY.GroupProvider = null;
            this.FctPeaksLine.AxisY.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.FctPeaksLine.AxisY.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.FctPeaksLine.AxisY.GroupVisibilityLevel = 0;
            this.FctPeaksLine.AxisY.LabelMax = false;
            this.FctPeaksLine.AxisY.LabelMin = false;
            this.FctPeaksLine.AxisY.MajorGrid = true;
            this.FctPeaksLine.AxisY.MajorTickMarks = C1.Chart.TickMark.None;
            this.FctPeaksLine.AxisY.PlotAreaName = null;
            this.FctPeaksLine.AxisY.Position = C1.Chart.Position.Left;
            this.FctPeaksLine.AxisY.TimeUnit = C1.Chart.TimeUnits.Day;
            this.FctPeaksLine.AxisY.Title = "Flux [mVs]";
            this.FctPeaksLine.ChartType = C1.Chart.ChartType.Line;
            this.FctPeaksLine.DataLabel.Angle = 0;
            this.FctPeaksLine.DataLabel.Border = false;
            this.FctPeaksLine.DataLabel.ConnectingLine = false;
            this.FctPeaksLine.DataLabel.Content = null;
            this.FctPeaksLine.DataLabel.ContentOptions = C1.Chart.ContentOptions.WordWrap;
            this.FctPeaksLine.DataLabel.MaxAutoLabels = 100;
            this.FctPeaksLine.DataLabel.MaxLines = 0;
            this.FctPeaksLine.DataLabel.MaxWidth = 0;
            this.FctPeaksLine.DataLabel.Offset = 0;
            this.FctPeaksLine.DataLabel.Overlapping = C1.Chart.LabelOverlapping.Hide;
            this.FctPeaksLine.DataLabel.OverlappingOptions = C1.Chart.LabelOverlappingOptions.OutsidePlotArea;
            this.FctPeaksLine.DataLabel.Position = C1.Chart.LabelPosition.None;
            this.FctPeaksLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FctPeaksLine.Legend.ItemMaxWidth = 0;
            this.FctPeaksLine.Legend.Orientation = C1.Chart.Orientation.Auto;
            this.FctPeaksLine.Legend.Position = C1.Chart.Position.Right;
            this.FctPeaksLine.Legend.Reversed = false;
            this.FctPeaksLine.Legend.ScrollBars = C1.Chart.LegendScrollBars.None;
            this.FctPeaksLine.Legend.TextWrapping = C1.Chart.TextWrapping.None;
            this.FctPeaksLine.Legend.Title = null;
            this.FctPeaksLine.LegendToggle = true;
            this.FctPeaksLine.Location = new System.Drawing.Point(0, 0);
            this.FctPeaksLine.Margin = new System.Windows.Forms.Padding(7);
            this.FctPeaksLine.Name = "FctPeaksLine";
            elementSize1.SizeType = C1.Chart.ElementSizeType.Percentage;
            elementSize1.Value = 70D;
            this.FctPeaksLine.Options.ClusterSize = elementSize1;
            this.FctPeaksLine.PlotMargin = new System.Windows.Forms.Padding(0);
            this.FctPeaksLine.SelectedSeries = null;
            this.FctPeaksLine.SelectionStyle.StrokeColor = System.Drawing.Color.Red;
            series1.DataLabel = null;
            series1.Name = "Series 1";
            series1.StackingGroup = -1;
            series1.Style.StrokeWidth = 2F;
            series1.Tooltip = null;
            this.FctPeaksLine.Series.Add(series1);
            this.FctPeaksLine.Size = new System.Drawing.Size(906, 266);
            this.FctPeaksLine.TabIndex = 0;
            this.FctPeaksLine.Text = "flexChart1";
            // 
            // 
            // 
            this.FctPeaksLine.ToolTip.Content = "";
            // 
            // SppDashHistoChart
            // 
            this.SppDashHistoChart.Controls.Add(this.FctPeaksHisto);
            this.SppDashHistoChart.Height = 282;
            this.SppDashHistoChart.Location = new System.Drawing.Point(0, 299);
            this.SppDashHistoChart.MinHeight = 27;
            this.SppDashHistoChart.MinWidth = 27;
            this.SppDashHistoChart.Name = "SppDashHistoChart";
            this.SppDashHistoChart.Size = new System.Drawing.Size(906, 267);
            this.SppDashHistoChart.TabIndex = 1;
            this.SppDashHistoChart.Text = "Histogram";
            this.SppDashHistoChart.Width = 906;
            // 
            // FctPeaksHisto
            // 
            this.FctPeaksHisto.AnimationLoad.Direction = C1.Chart.AnimationDirection.Y;
            this.FctPeaksHisto.AnimationLoad.Duration = 400;
            this.FctPeaksHisto.AnimationLoad.Easing = C1.Chart.Easing.Linear;
            this.FctPeaksHisto.AnimationLoad.Type = C1.Chart.AnimationType.All;
            this.FctPeaksHisto.AnimationSettings = C1.Chart.AnimationSettings.None;
            this.FctPeaksHisto.AnimationUpdate.Duration = 400;
            this.FctPeaksHisto.AnimationUpdate.Easing = C1.Chart.Easing.Linear;
            this.FctPeaksHisto.AnimationUpdate.Type = C1.Chart.AnimationType.All;
            this.FctPeaksHisto.AxisX.Chart = this.FctPeaksHisto;
            this.FctPeaksHisto.AxisX.DataSource = null;
            this.FctPeaksHisto.AxisX.GroupProvider = null;
            this.FctPeaksHisto.AxisX.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.FctPeaksHisto.AxisX.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.FctPeaksHisto.AxisX.GroupVisibilityLevel = 0;
            this.FctPeaksHisto.AxisX.LabelMax = false;
            this.FctPeaksHisto.AxisX.LabelMin = false;
            this.FctPeaksHisto.AxisX.PlotAreaName = null;
            this.FctPeaksHisto.AxisX.Position = C1.Chart.Position.Bottom;
            this.FctPeaksHisto.AxisX.TimeUnit = C1.Chart.TimeUnits.Day;
            this.FctPeaksHisto.AxisX.Title = "Flux [mVs]";
            this.FctPeaksHisto.AxisY.AxisLine = false;
            this.FctPeaksHisto.AxisY.Chart = this.FctPeaksHisto;
            this.FctPeaksHisto.AxisY.DataSource = null;
            this.FctPeaksHisto.AxisY.GroupProvider = null;
            this.FctPeaksHisto.AxisY.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.FctPeaksHisto.AxisY.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.FctPeaksHisto.AxisY.GroupVisibilityLevel = 0;
            this.FctPeaksHisto.AxisY.LabelMax = false;
            this.FctPeaksHisto.AxisY.LabelMin = false;
            this.FctPeaksHisto.AxisY.MajorGrid = true;
            this.FctPeaksHisto.AxisY.MajorTickMarks = C1.Chart.TickMark.None;
            this.FctPeaksHisto.AxisY.PlotAreaName = null;
            this.FctPeaksHisto.AxisY.Position = C1.Chart.Position.Left;
            this.FctPeaksHisto.AxisY.TimeUnit = C1.Chart.TimeUnits.Day;
            this.FctPeaksHisto.AxisY.Title = "Number";
            this.FctPeaksHisto.DataLabel.Angle = 0;
            this.FctPeaksHisto.DataLabel.Border = false;
            this.FctPeaksHisto.DataLabel.ConnectingLine = false;
            this.FctPeaksHisto.DataLabel.Content = null;
            this.FctPeaksHisto.DataLabel.ContentOptions = C1.Chart.ContentOptions.WordWrap;
            this.FctPeaksHisto.DataLabel.MaxAutoLabels = 100;
            this.FctPeaksHisto.DataLabel.MaxLines = 0;
            this.FctPeaksHisto.DataLabel.MaxWidth = 0;
            this.FctPeaksHisto.DataLabel.Offset = 0;
            this.FctPeaksHisto.DataLabel.Overlapping = C1.Chart.LabelOverlapping.Hide;
            this.FctPeaksHisto.DataLabel.OverlappingOptions = C1.Chart.LabelOverlappingOptions.OutsidePlotArea;
            this.FctPeaksHisto.DataLabel.Position = C1.Chart.LabelPosition.None;
            this.FctPeaksHisto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FctPeaksHisto.Legend.ItemMaxWidth = 0;
            this.FctPeaksHisto.Legend.Orientation = C1.Chart.Orientation.Auto;
            this.FctPeaksHisto.Legend.Position = C1.Chart.Position.Right;
            this.FctPeaksHisto.Legend.Reversed = false;
            this.FctPeaksHisto.Legend.ScrollBars = C1.Chart.LegendScrollBars.None;
            this.FctPeaksHisto.Legend.TextWrapping = C1.Chart.TextWrapping.None;
            this.FctPeaksHisto.Legend.Title = null;
            this.FctPeaksHisto.LegendToggle = true;
            this.FctPeaksHisto.Location = new System.Drawing.Point(0, 0);
            this.FctPeaksHisto.Margin = new System.Windows.Forms.Padding(7);
            this.FctPeaksHisto.Name = "FctPeaksHisto";
            elementSize2.SizeType = C1.Chart.ElementSizeType.Percentage;
            elementSize2.Value = 70D;
            this.FctPeaksHisto.Options.ClusterSize = elementSize2;
            this.FctPeaksHisto.PlotMargin = new System.Windows.Forms.Padding(0);
            this.FctPeaksHisto.SelectedSeries = null;
            this.FctPeaksHisto.SelectionStyle.StrokeColor = System.Drawing.Color.Red;
            series2.DataLabel = null;
            series2.Name = "Series 1";
            series2.StackingGroup = -1;
            series2.Style.StrokeWidth = 2F;
            series2.Tooltip = null;
            this.FctPeaksHisto.Series.Add(series2);
            this.FctPeaksHisto.Size = new System.Drawing.Size(906, 267);
            this.FctPeaksHisto.TabIndex = 0;
            this.FctPeaksHisto.Text = "flexChart1";
            // 
            // 
            // 
            this.FctPeaksHisto.ToolTip.Content = "";
            // 
            // FlpDashboard
            // 
            this.FlpDashboard.AutoSize = true;
            this.FlpDashboard.Controls.Add(this.BtnLoadDashboard);
            this.FlpDashboard.Controls.Add(this.DteDashEnd);
            this.FlpDashboard.Controls.Add(this.CkbDashEndDate);
            this.FlpDashboard.Controls.Add(this.DteDashStart);
            this.FlpDashboard.Controls.Add(this.CkbDashStartDate);
            this.FlpDashboard.Controls.Add(this.MstDashModel);
            this.FlpDashboard.Controls.Add(this.LblDashModel);
            this.FlpDashboard.Controls.Add(this.MstDashPeak);
            this.FlpDashboard.Controls.Add(this.LblDashPeak);
            this.FlpDashboard.Controls.Add(this.CbxDashGroup);
            this.FlpDashboard.Controls.Add(this.LblDashGroup);
            this.FlpDashboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FlpDashboard.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FlpDashboard.Location = new System.Drawing.Point(0, 566);
            this.FlpDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.FlpDashboard.Name = "FlpDashboard";
            this.FlpDashboard.Size = new System.Drawing.Size(906, 50);
            this.FlpDashboard.TabIndex = 1;
            // 
            // BtnLoadDashboard
            // 
            this.BtnLoadDashboard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnLoadDashboard.Location = new System.Drawing.Point(831, 3);
            this.BtnLoadDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.BtnLoadDashboard.Name = "BtnLoadDashboard";
            this.BtnLoadDashboard.Size = new System.Drawing.Size(73, 20);
            this.BtnLoadDashboard.TabIndex = 63;
            this.BtnLoadDashboard.Text = "Load";
            this.BtnLoadDashboard.UseVisualStyleBackColor = true;
            this.BtnLoadDashboard.Click += new System.EventHandler(this.BtnLoadDashboard_Click);
            // 
            // DteDashEnd
            // 
            this.DteDashEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DteDashEnd.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.DteDashEnd.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.DteDashEnd.CustomFormat = "yy-MM-dd HH:mm:ss";
            this.DteDashEnd.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DteDashEnd.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.DteDashEnd.GapHeight = 0;
            this.DteDashEnd.ImagePadding = new System.Windows.Forms.Padding(0);
            this.DteDashEnd.Location = new System.Drawing.Point(685, 3);
            this.DteDashEnd.Margin = new System.Windows.Forms.Padding(2);
            this.DteDashEnd.Name = "DteDashEnd";
            this.DteDashEnd.Size = new System.Drawing.Size(142, 19);
            this.DteDashEnd.TabIndex = 2;
            this.DteDashEnd.Tag = null;
            // 
            // CkbDashEndDate
            // 
            this.CkbDashEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CkbDashEndDate.AutoSize = true;
            this.CkbDashEndDate.Location = new System.Drawing.Point(635, 5);
            this.CkbDashEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.CkbDashEndDate.Name = "CkbDashEndDate";
            this.CkbDashEndDate.Size = new System.Drawing.Size(46, 16);
            this.CkbDashEndDate.TabIndex = 64;
            this.CkbDashEndDate.Text = "End";
            this.CkbDashEndDate.UseVisualStyleBackColor = true;
            // 
            // DteDashStart
            // 
            this.DteDashStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DteDashStart.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.DteDashStart.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.DteDashStart.CustomFormat = "yy-MM-dd HH:mm:ss";
            this.DteDashStart.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DteDashStart.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat | C1.Win.C1Input.FormatInfoInheritFlags.NullText) 
            | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.DteDashStart.GapHeight = 0;
            this.DteDashStart.ImagePadding = new System.Windows.Forms.Padding(0);
            this.DteDashStart.Location = new System.Drawing.Point(489, 3);
            this.DteDashStart.Margin = new System.Windows.Forms.Padding(2);
            this.DteDashStart.Name = "DteDashStart";
            this.DteDashStart.Size = new System.Drawing.Size(142, 19);
            this.DteDashStart.TabIndex = 2;
            this.DteDashStart.Tag = null;
            // 
            // CkbDashStartDate
            // 
            this.CkbDashStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CkbDashStartDate.AutoSize = true;
            this.CkbDashStartDate.Location = new System.Drawing.Point(436, 5);
            this.CkbDashStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.CkbDashStartDate.Name = "CkbDashStartDate";
            this.CkbDashStartDate.Size = new System.Drawing.Size(49, 16);
            this.CkbDashStartDate.TabIndex = 64;
            this.CkbDashStartDate.Text = "Start";
            this.CkbDashStartDate.UseVisualStyleBackColor = true;
            // 
            // MstDashModel
            // 
            this.MstDashModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MstDashModel.Location = new System.Drawing.Point(311, 4);
            this.MstDashModel.Margin = new System.Windows.Forms.Padding(2);
            this.MstDashModel.Name = "MstDashModel";
            this.MstDashModel.Size = new System.Drawing.Size(121, 17);
            this.MstDashModel.TabIndex = 60;
            // 
            // LblDashModel
            // 
            this.LblDashModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblDashModel.AutoSize = true;
            this.LblDashModel.Location = new System.Drawing.Point(267, 7);
            this.LblDashModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDashModel.Name = "LblDashModel";
            this.LblDashModel.Size = new System.Drawing.Size(40, 12);
            this.LblDashModel.TabIndex = 61;
            this.LblDashModel.Text = "Model";
            // 
            // MstDashPeak
            // 
            this.MstDashPeak.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MstDashPeak.AutoSize = true;
            c1CheckListItem1.Value = "Negative";
            c1CheckListItem2.Value = "Total";
            c1CheckListItem3.Value = "Positive";
            this.MstDashPeak.Items.Add(c1CheckListItem1);
            this.MstDashPeak.Items.Add(c1CheckListItem2);
            this.MstDashPeak.Items.Add(c1CheckListItem3);
            this.MstDashPeak.Location = new System.Drawing.Point(144, 2);
            this.MstDashPeak.Margin = new System.Windows.Forms.Padding(2);
            this.MstDashPeak.Name = "MstDashPeak";
            this.MstDashPeak.Size = new System.Drawing.Size(119, 22);
            this.MstDashPeak.TabIndex = 60;
            // 
            // LblDashPeak
            // 
            this.LblDashPeak.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblDashPeak.AutoSize = true;
            this.LblDashPeak.Location = new System.Drawing.Point(107, 7);
            this.LblDashPeak.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDashPeak.Name = "LblDashPeak";
            this.LblDashPeak.Size = new System.Drawing.Size(33, 12);
            this.LblDashPeak.TabIndex = 61;
            this.LblDashPeak.Text = "Peak";
            // 
            // CbxDashGroup
            // 
            this.CbxDashGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CbxDashGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxDashGroup.FormattingEnabled = true;
            this.CbxDashGroup.Location = new System.Drawing.Point(793, 28);
            this.CbxDashGroup.Margin = new System.Windows.Forms.Padding(2);
            this.CbxDashGroup.Name = "CbxDashGroup";
            this.CbxDashGroup.Size = new System.Drawing.Size(111, 20);
            this.CbxDashGroup.TabIndex = 62;
            // 
            // LblDashGroup
            // 
            this.LblDashGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblDashGroup.AutoSize = true;
            this.LblDashGroup.Location = new System.Drawing.Point(750, 32);
            this.LblDashGroup.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDashGroup.Name = "LblDashGroup";
            this.LblDashGroup.Size = new System.Drawing.Size(39, 12);
            this.LblDashGroup.TabIndex = 61;
            this.LblDashGroup.Text = "Group";
            // 
            // DtpLog
            // 
            this.DtpLog.Controls.Add(this.FgdLog);
            this.DtpLog.Image = global::SmaFlux.Properties.Resources.Log;
            this.DtpLog.Location = new System.Drawing.Point(1, 1);
            this.DtpLog.Name = "DtpLog";
            this.DtpLog.Size = new System.Drawing.Size(906, 616);
            this.DtpLog.TabIndex = 4;
            this.DtpLog.Text = "Log";
            // 
            // FgdLog
            // 
            this.FgdLog.AllowFiltering = true;
            this.FgdLog.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FgdLog.ExtendLastCol = true;
            this.FgdLog.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FgdLog.Location = new System.Drawing.Point(0, 0);
            this.FgdLog.Margin = new System.Windows.Forms.Padding(2);
            this.FgdLog.Name = "FgdLog";
            this.FgdLog.Size = new System.Drawing.Size(906, 616);
            this.FgdLog.TabIndex = 0;
            this.FgdLog.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // LblModelNumberTitle
            // 
            this.LblModelNumberTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblModelNumberTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumberTitle.Location = new System.Drawing.Point(924, 112);
            this.LblModelNumberTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumberTitle.Name = "LblModelNumberTitle";
            this.LblModelNumberTitle.Size = new System.Drawing.Size(176, 24);
            this.LblModelNumberTitle.TabIndex = 29;
            this.LblModelNumberTitle.Text = "Model information";
            this.LblModelNumberTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddModel
            // 
            this.LblPlcAddModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddModel.Location = new System.Drawing.Point(1100, 112);
            this.LblPlcAddModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddModel.Name = "LblPlcAddModel";
            this.LblPlcAddModel.Size = new System.Drawing.Size(176, 24);
            this.LblPlcAddModel.TabIndex = 26;
            this.LblPlcAddModel.Text = "-";
            this.LblPlcAddModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddHeartBeat
            // 
            this.LblPlcAddHeartBeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddHeartBeat.Location = new System.Drawing.Point(924, 544);
            this.LblPlcAddHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddHeartBeat.Name = "LblPlcAddHeartBeat";
            this.LblPlcAddHeartBeat.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddHeartBeat.TabIndex = 30;
            this.LblPlcAddHeartBeat.Text = "-";
            this.LblPlcAddHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeat
            // 
            this.LblHeartBeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeat.Location = new System.Drawing.Point(924, 568);
            this.LblHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeat.Name = "LblHeartBeat";
            this.LblHeartBeat.Size = new System.Drawing.Size(88, 32);
            this.LblHeartBeat.TabIndex = 31;
            this.LblHeartBeat.Text = "Heartbeat";
            this.LblHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnLblReadProInf
            // 
            this.BtnLblReadProInf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLblReadProInf.Location = new System.Drawing.Point(1100, 696);
            this.BtnLblReadProInf.Margin = new System.Windows.Forms.Padding(2);
            this.BtnLblReadProInf.Name = "BtnLblReadProInf";
            this.BtnLblReadProInf.Size = new System.Drawing.Size(176, 32);
            this.BtnLblReadProInf.TabIndex = 38;
            this.BtnLblReadProInf.Text = "Read product infor";
            this.BtnLblReadProInf.UseVisualStyleBackColor = true;
            this.BtnLblReadProInf.Click += new System.EventHandler(this.BtnLblReadProInf_Click);
            // 
            // BtnPlcRstFin
            // 
            this.BtnPlcRstFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcRstFin.Location = new System.Drawing.Point(1100, 568);
            this.BtnPlcRstFin.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPlcRstFin.Name = "BtnPlcRstFin";
            this.BtnPlcRstFin.Size = new System.Drawing.Size(88, 32);
            this.BtnPlcRstFin.TabIndex = 40;
            this.BtnPlcRstFin.Text = "Reset\r\nFinished";
            this.BtnPlcRstFin.UseVisualStyleBackColor = true;
            this.BtnPlcRstFin.Click += new System.EventHandler(this.BtnPlcRstFin_Click);
            // 
            // LblPlcAddRstFin
            // 
            this.LblPlcAddRstFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddRstFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddRstFin.Location = new System.Drawing.Point(1100, 544);
            this.LblPlcAddRstFin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddRstFin.Name = "LblPlcAddRstFin";
            this.LblPlcAddRstFin.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddRstFin.TabIndex = 39;
            this.LblPlcAddRstFin.Text = "-";
            this.LblPlcAddRstFin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnMeasReqResp
            // 
            this.BtnMeasReqResp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasReqResp.Location = new System.Drawing.Point(1188, 568);
            this.BtnMeasReqResp.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasReqResp.Name = "BtnMeasReqResp";
            this.BtnMeasReqResp.Size = new System.Drawing.Size(88, 32);
            this.BtnMeasReqResp.TabIndex = 46;
            this.BtnMeasReqResp.Text = "Meas.\r\nRequest";
            this.BtnMeasReqResp.UseVisualStyleBackColor = true;
            this.BtnMeasReqResp.Click += new System.EventHandler(this.BtnMeasReqResp_Click);
            // 
            // LblPlcAddMeasReqBit
            // 
            this.LblPlcAddMeasReqBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddMeasReqBit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasReqBit.Location = new System.Drawing.Point(1188, 544);
            this.LblPlcAddMeasReqBit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasReqBit.Name = "LblPlcAddMeasReqBit";
            this.LblPlcAddMeasReqBit.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddMeasReqBit.TabIndex = 41;
            this.LblPlcAddMeasReqBit.Text = "-";
            this.LblPlcAddMeasReqBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnMeasFin
            // 
            this.BtnMeasFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasFin.Location = new System.Drawing.Point(924, 624);
            this.BtnMeasFin.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasFin.Name = "BtnMeasFin";
            this.BtnMeasFin.Size = new System.Drawing.Size(88, 32);
            this.BtnMeasFin.TabIndex = 47;
            this.BtnMeasFin.Text = "Meas.\r\nFinished";
            this.BtnMeasFin.UseVisualStyleBackColor = true;
            this.BtnMeasFin.Click += new System.EventHandler(this.BtnMeasFin_Click);
            // 
            // BtnAlarm
            // 
            this.BtnAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAlarm.Location = new System.Drawing.Point(1188, 624);
            this.BtnAlarm.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAlarm.Name = "BtnAlarm";
            this.BtnAlarm.Size = new System.Drawing.Size(88, 32);
            this.BtnAlarm.TabIndex = 48;
            this.BtnAlarm.Text = "ALARM";
            this.BtnAlarm.UseVisualStyleBackColor = true;
            this.BtnAlarm.Click += new System.EventHandler(this.BtnAlarm_Click);
            // 
            // LblPlcAddMeasFin
            // 
            this.LblPlcAddMeasFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddMeasFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasFin.Location = new System.Drawing.Point(924, 600);
            this.LblPlcAddMeasFin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasFin.Name = "LblPlcAddMeasFin";
            this.LblPlcAddMeasFin.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddMeasFin.TabIndex = 42;
            this.LblPlcAddMeasFin.Text = "-";
            this.LblPlcAddMeasFin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnNg
            // 
            this.BtnNg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNg.Location = new System.Drawing.Point(1100, 624);
            this.BtnNg.Margin = new System.Windows.Forms.Padding(2);
            this.BtnNg.Name = "BtnNg";
            this.BtnNg.Size = new System.Drawing.Size(88, 32);
            this.BtnNg.TabIndex = 49;
            this.BtnNg.Text = "NG";
            this.BtnNg.UseVisualStyleBackColor = true;
            this.BtnNg.Click += new System.EventHandler(this.BtnNg_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(1012, 624);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(88, 32);
            this.BtnOk.TabIndex = 50;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // LblPlcAddAlarm
            // 
            this.LblPlcAddAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddAlarm.Location = new System.Drawing.Point(1188, 600);
            this.LblPlcAddAlarm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddAlarm.Name = "LblPlcAddAlarm";
            this.LblPlcAddAlarm.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddAlarm.TabIndex = 43;
            this.LblPlcAddAlarm.Text = "-";
            this.LblPlcAddAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddOk
            // 
            this.LblPlcAddOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddOk.Location = new System.Drawing.Point(1012, 600);
            this.LblPlcAddOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddOk.Name = "LblPlcAddOk";
            this.LblPlcAddOk.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddOk.TabIndex = 44;
            this.LblPlcAddOk.Text = "-";
            this.LblPlcAddOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddNg
            // 
            this.LblPlcAddNg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddNg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddNg.Location = new System.Drawing.Point(1100, 600);
            this.LblPlcAddNg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddNg.Name = "LblPlcAddNg";
            this.LblPlcAddNg.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddNg.TabIndex = 45;
            this.LblPlcAddNg.Text = "-";
            this.LblPlcAddNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddRstReq
            // 
            this.LblPlcAddRstReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddRstReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddRstReq.Location = new System.Drawing.Point(1012, 544);
            this.LblPlcAddRstReq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddRstReq.Name = "LblPlcAddRstReq";
            this.LblPlcAddRstReq.Size = new System.Drawing.Size(88, 24);
            this.LblPlcAddRstReq.TabIndex = 39;
            this.LblPlcAddRstReq.Text = "-";
            this.LblPlcAddRstReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPlcRstReq
            // 
            this.BtnPlcRstReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcRstReq.Location = new System.Drawing.Point(1012, 568);
            this.BtnPlcRstReq.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPlcRstReq.Name = "BtnPlcRstReq";
            this.BtnPlcRstReq.Size = new System.Drawing.Size(88, 32);
            this.BtnPlcRstReq.TabIndex = 40;
            this.BtnPlcRstReq.Text = "Reset\r\nRequest";
            this.BtnPlcRstReq.UseVisualStyleBackColor = true;
            this.BtnPlcRstReq.Click += new System.EventHandler(this.BtnPlcRstReq_Click);
            // 
            // LblModelName
            // 
            this.LblModelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblModelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelName.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblModelName.Location = new System.Drawing.Point(924, 136);
            this.LblModelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelName.Name = "LblModelName";
            this.LblModelName.Size = new System.Drawing.Size(352, 32);
            this.LblModelName.TabIndex = 53;
            this.LblModelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDateTime
            // 
            this.LblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDateTime.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblDateTime.Location = new System.Drawing.Point(924, 40);
            this.LblDateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDateTime.Name = "LblDateTime";
            this.LblDateTime.Size = new System.Drawing.Size(352, 32);
            this.LblDateTime.TabIndex = 54;
            this.LblDateTime.Text = "yyyy-MM-dd HH:mm:SS";
            this.LblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblOpMode
            // 
            this.LblOpMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblOpMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblOpMode.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblOpMode.Location = new System.Drawing.Point(924, 8);
            this.LblOpMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblOpMode.Name = "LblOpMode";
            this.LblOpMode.Size = new System.Drawing.Size(352, 32);
            this.LblOpMode.TabIndex = 54;
            this.LblOpMode.Text = "NOP";
            this.LblOpMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblOk
            // 
            this.LblOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblOk.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblOk.Location = new System.Drawing.Point(924, 72);
            this.LblOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblOk.Name = "LblOk";
            this.LblOk.Size = new System.Drawing.Size(352, 40);
            this.LblOk.TabIndex = 55;
            this.LblOk.Text = "-";
            this.LblOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnReadFlux
            // 
            this.BtnReadFlux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReadFlux.Location = new System.Drawing.Point(924, 696);
            this.BtnReadFlux.Margin = new System.Windows.Forms.Padding(2);
            this.BtnReadFlux.Name = "BtnReadFlux";
            this.BtnReadFlux.Size = new System.Drawing.Size(176, 32);
            this.BtnReadFlux.TabIndex = 56;
            this.BtnReadFlux.Text = "Flux read";
            this.BtnReadFlux.UseVisualStyleBackColor = true;
            this.BtnReadFlux.Click += new System.EventHandler(this.BtnReadFlux_Click);
            // 
            // BtnResetFlux
            // 
            this.BtnResetFlux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnResetFlux.Location = new System.Drawing.Point(924, 664);
            this.BtnResetFlux.Margin = new System.Windows.Forms.Padding(2);
            this.BtnResetFlux.Name = "BtnResetFlux";
            this.BtnResetFlux.Size = new System.Drawing.Size(176, 32);
            this.BtnResetFlux.TabIndex = 56;
            this.BtnResetFlux.Text = "Flux reset";
            this.BtnResetFlux.UseVisualStyleBackColor = true;
            this.BtnResetFlux.Click += new System.EventHandler(this.BtnResetFlux_Click);
            // 
            // BtnMeasInsert
            // 
            this.BtnMeasInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasInsert.Location = new System.Drawing.Point(924, 728);
            this.BtnMeasInsert.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasInsert.Name = "BtnMeasInsert";
            this.BtnMeasInsert.Size = new System.Drawing.Size(176, 32);
            this.BtnMeasInsert.TabIndex = 56;
            this.BtnMeasInsert.Text = "Insert DB";
            this.BtnMeasInsert.UseVisualStyleBackColor = true;
            this.BtnMeasInsert.Click += new System.EventHandler(this.BtnMeasInsert_Click);
            // 
            // BtnMeasUpload
            // 
            this.BtnMeasUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasUpload.Location = new System.Drawing.Point(1100, 728);
            this.BtnMeasUpload.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasUpload.Name = "BtnMeasUpload";
            this.BtnMeasUpload.Size = new System.Drawing.Size(176, 32);
            this.BtnMeasUpload.TabIndex = 56;
            this.BtnMeasUpload.Text = "MES upload";
            this.BtnMeasUpload.UseVisualStyleBackColor = true;
            this.BtnMeasUpload.Click += new System.EventHandler(this.BtnMeasUpload_Click);
            // 
            // FgdProdInf
            // 
            this.FgdProdInf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdProdInf.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdProdInf.ExtendLastCol = true;
            this.FgdProdInf.Location = new System.Drawing.Point(924, 320);
            this.FgdProdInf.Margin = new System.Windows.Forms.Padding(2);
            this.FgdProdInf.Name = "FgdProdInf";
            this.FgdProdInf.Size = new System.Drawing.Size(352, 96);
            this.FgdProdInf.TabIndex = 57;
            // 
            // FgdMesData
            // 
            this.FgdMesData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdMesData.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdMesData.ExtendLastCol = true;
            this.FgdMesData.Location = new System.Drawing.Point(924, 440);
            this.FgdMesData.Margin = new System.Windows.Forms.Padding(2);
            this.FgdMesData.Name = "FgdMesData";
            this.FgdMesData.Size = new System.Drawing.Size(352, 96);
            this.FgdMesData.TabIndex = 57;
            // 
            // LblProdInfs
            // 
            this.LblProdInfs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblProdInfs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblProdInfs.Location = new System.Drawing.Point(924, 296);
            this.LblProdInfs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblProdInfs.Name = "LblProdInfs";
            this.LblProdInfs.Size = new System.Drawing.Size(352, 24);
            this.LblProdInfs.TabIndex = 51;
            this.LblProdInfs.Text = "Product information";
            this.LblProdInfs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMesData
            // 
            this.LblMesData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMesData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMesData.Location = new System.Drawing.Point(924, 416);
            this.LblMesData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMesData.Name = "LblMesData";
            this.LblMesData.Size = new System.Drawing.Size(352, 24);
            this.LblMesData.TabIndex = 51;
            this.LblMesData.Text = "MES DATA";
            this.LblMesData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TslLogStatus,
            this.TslErrStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 839);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip1.TabIndex = 58;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TslLogStatus
            // 
            this.TslLogStatus.Name = "TslLogStatus";
            this.TslLogStatus.Size = new System.Drawing.Size(30, 17);
            this.TslLogStatus.Text = "LOG";
            // 
            // TslErrStatus
            // 
            this.TslErrStatus.Name = "TslErrStatus";
            this.TslErrStatus.Size = new System.Drawing.Size(43, 17);
            this.TslErrStatus.Text = "ERROR";
            this.TslErrStatus.Click += new System.EventHandler(this.TslErrStatus_Click);
            // 
            // TslLog
            // 
            this.TslLog.Name = "TslLog";
            this.TslLog.Size = new System.Drawing.Size(30, 17);
            this.TslLog.Text = "LOG";
            // 
            // TslErrCode
            // 
            this.TslErrCode.Name = "TslErrCode";
            this.TslErrCode.Size = new System.Drawing.Size(62, 17);
            this.TslErrCode.Text = "Error code";
            this.TslErrCode.Click += new System.EventHandler(this.TslErrCode_Click);
            // 
            // LblSiteInfor
            // 
            this.LblSiteInfor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblSiteInfor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblSiteInfor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSiteInfor.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblSiteInfor.Location = new System.Drawing.Point(8, 8);
            this.LblSiteInfor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblSiteInfor.Name = "LblSiteInfor";
            this.LblSiteInfor.Size = new System.Drawing.Size(908, 40);
            this.LblSiteInfor.TabIndex = 54;
            this.LblSiteInfor.Text = "Site information";
            this.LblSiteInfor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FgdMeasData
            // 
            this.FgdMeasData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdMeasData.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdMeasData.ExtendLastCol = true;
            this.FgdMeasData.Location = new System.Drawing.Point(924, 200);
            this.FgdMeasData.Margin = new System.Windows.Forms.Padding(2);
            this.FgdMeasData.Name = "FgdMeasData";
            this.FgdMeasData.Size = new System.Drawing.Size(352, 96);
            this.FgdMeasData.TabIndex = 60;
            // 
            // LblMeasData
            // 
            this.LblMeasData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMeasData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMeasData.Location = new System.Drawing.Point(924, 176);
            this.LblMeasData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasData.Name = "LblMeasData";
            this.LblMeasData.Size = new System.Drawing.Size(352, 24);
            this.LblMeasData.TabIndex = 59;
            this.LblMeasData.Text = "Measurement data";
            this.LblMeasData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnMeasClear
            // 
            this.BtnMeasClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasClear.Location = new System.Drawing.Point(1100, 664);
            this.BtnMeasClear.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasClear.Name = "BtnMeasClear";
            this.BtnMeasClear.Size = new System.Drawing.Size(176, 32);
            this.BtnMeasClear.TabIndex = 61;
            this.BtnMeasClear.Text = "MES clear";
            this.BtnMeasClear.UseVisualStyleBackColor = true;
            this.BtnMeasClear.Click += new System.EventHandler(this.BtnMeasClear_Click);
            // 
            // BtnPlcConn
            // 
            this.BtnPlcConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcConn.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPlcConn.Location = new System.Drawing.Point(926, 782);
            this.BtnPlcConn.Name = "BtnPlcConn";
            this.BtnPlcConn.Size = new System.Drawing.Size(352, 48);
            this.BtnPlcConn.TabIndex = 62;
            this.BtnPlcConn.Text = "Connect";
            this.BtnPlcConn.UseVisualStyleBackColor = true;
            this.BtnPlcConn.Click += new System.EventHandler(this.BtnPlcConn_Click);
            // 
            // CmsDbMeas
            // 
            this.CmsDbMeas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmDbDeleteItems,
            this.TsmExportExcel,
            this.TsmCopyDataToClipboard,
            this.TsmDbSelectAll,
            this.TssDbSeparator,
            this.TsmOpenDbFolder});
            this.CmsDbMeas.Name = "CmsDbMeas";
            this.CmsDbMeas.Size = new System.Drawing.Size(184, 120);
            // 
            // TsmDbDeleteItems
            // 
            this.TsmDbDeleteItems.Name = "TsmDbDeleteItems";
            this.TsmDbDeleteItems.Size = new System.Drawing.Size(183, 22);
            this.TsmDbDeleteItems.Text = "Delete selected item";
            this.TsmDbDeleteItems.Click += new System.EventHandler(this.TsmDbDeleteItems_Click);
            // 
            // TsmExportExcel
            // 
            this.TsmExportExcel.Name = "TsmExportExcel";
            this.TsmExportExcel.Size = new System.Drawing.Size(183, 22);
            this.TsmExportExcel.Text = "Export to excel..";
            this.TsmExportExcel.Click += new System.EventHandler(this.TsmExportExcel_Click);
            // 
            // TsmCopyDataToClipboard
            // 
            this.TsmCopyDataToClipboard.Name = "TsmCopyDataToClipboard";
            this.TsmCopyDataToClipboard.Size = new System.Drawing.Size(183, 22);
            this.TsmCopyDataToClipboard.Text = "Copy clipboard";
            this.TsmCopyDataToClipboard.Click += new System.EventHandler(this.TsmCopyDataToClipboard_Click);
            // 
            // TsmOpenDbFolder
            // 
            this.TsmOpenDbFolder.Name = "TsmOpenDbFolder";
            this.TsmOpenDbFolder.Size = new System.Drawing.Size(183, 22);
            this.TsmOpenDbFolder.Text = "Open DB file folder";
            this.TsmOpenDbFolder.Click += new System.EventHandler(this.TsmOpenDbFolder_Click);
            // 
            // TsmDbSelectAll
            // 
            this.TsmDbSelectAll.Name = "TsmDbSelectAll";
            this.TsmDbSelectAll.Size = new System.Drawing.Size(183, 22);
            this.TsmDbSelectAll.Text = "Select all";
            this.TsmDbSelectAll.Click += new System.EventHandler(this.TsmDbSelectAll_Click);
            // 
            // TssDbSeparator
            // 
            this.TssDbSeparator.Name = "TssDbSeparator";
            this.TssDbSeparator.Size = new System.Drawing.Size(180, 6);
            // 
            // SmaFluxDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 861);
            this.Controls.Add(this.BtnPlcConn);
            this.Controls.Add(this.BtnMeasClear);
            this.Controls.Add(this.FgdMeasData);
            this.Controls.Add(this.LblMeasData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.FgdMesData);
            this.Controls.Add(this.FgdProdInf);
            this.Controls.Add(this.BtnResetFlux);
            this.Controls.Add(this.BtnReadFlux);
            this.Controls.Add(this.BtnMeasInsert);
            this.Controls.Add(this.BtnMeasUpload);
            this.Controls.Add(this.LblOk);
            this.Controls.Add(this.LblSiteInfor);
            this.Controls.Add(this.LblOpMode);
            this.Controls.Add(this.LblDateTime);
            this.Controls.Add(this.LblModelName);
            this.Controls.Add(this.LblMesData);
            this.Controls.Add(this.LblProdInfs);
            this.Controls.Add(this.BtnMeasReqResp);
            this.Controls.Add(this.LblPlcAddMeasReqBit);
            this.Controls.Add(this.BtnMeasFin);
            this.Controls.Add(this.BtnAlarm);
            this.Controls.Add(this.LblPlcAddMeasFin);
            this.Controls.Add(this.BtnNg);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.LblPlcAddAlarm);
            this.Controls.Add(this.LblPlcAddOk);
            this.Controls.Add(this.LblPlcAddNg);
            this.Controls.Add(this.BtnPlcRstReq);
            this.Controls.Add(this.LblPlcAddRstReq);
            this.Controls.Add(this.BtnPlcRstFin);
            this.Controls.Add(this.LblPlcAddRstFin);
            this.Controls.Add(this.BtnLblReadProInf);
            this.Controls.Add(this.LblPlcAddHeartBeat);
            this.Controls.Add(this.LblHeartBeat);
            this.Controls.Add(this.TabMain);
            this.Controls.Add(this.TbxLog);
            this.Controls.Add(this.LblPlcAddModel);
            this.Controls.Add(this.LblModelNumberTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1300, 900);
            this.Name = "SmaFluxDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SMA FLUX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmaFlux_FormClosing);
            this.Load += new System.EventHandler(this.SmaFlux_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).EndInit();
            this.TabMain.ResumeLayout(false);
            this.DtpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgdMeasRes)).EndInit();
            this.PnlDbSearch.ResumeLayout(false);
            this.PnlDbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DteMeasStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DteMeasEndDate)).EndInit();
            this.TlpMeas.ResumeLayout(false);
            this.TlpMeas.PerformLayout();
            this.TlpPps.ResumeLayout(false);
            this.TlpPps.PerformLayout();
            this.TlpTps.ResumeLayout(false);
            this.TlpTps.PerformLayout();
            this.TlpNps.ResumeLayout(false);
            this.TlpNps.PerformLayout();
            this.PnlDbNavi.ResumeLayout(false);
            this.PnlDbNavi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudDbPageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CbxMeasItems)).EndInit();
            this.DtpProfiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgdProfile)).EndInit();
            this.DtpSettings.ResumeLayout(false);
            this.DtpDashBoard.ResumeLayout(false);
            this.DtpDashBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpcDashboard)).EndInit();
            this.SpcDashboard.ResumeLayout(false);
            this.SppDashLineChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FctPeaksLine)).EndInit();
            this.SppDashHistoChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FctPeaksHisto)).EndInit();
            this.FlpDashboard.ResumeLayout(false);
            this.FlpDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DteDashEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DteDashStart)).EndInit();
            this.DtpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgdLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdProdInf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMesData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMeasData)).EndInit();
            this.CmsDbMeas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer TmrApp;
        private System.Windows.Forms.TextBox TbxLog;
        private C1.Win.C1Command.C1DockingTab TabMain;
        private C1.Win.C1Command.C1DockingTabPage DtpMain;
        private C1.Win.C1Command.C1DockingTabPage DtpDashBoard;
        private C1.Win.C1Command.C1DockingTabPage DtpProfiles;
        private C1.Win.C1Command.C1DockingTabPage DtpSettings;
        private System.Windows.Forms.Label LblModelNumberTitle;
        private System.Windows.Forms.Label LblPlcAddModel;
        private System.Windows.Forms.Label LblPlcAddHeartBeat;
        private System.Windows.Forms.Label LblHeartBeat;
        private System.Windows.Forms.Button BtnLblReadProInf;
        private System.Windows.Forms.Button BtnPlcRstFin;
        private System.Windows.Forms.Label LblPlcAddRstFin;
        private System.Windows.Forms.Button BtnMeasReqResp;
        private System.Windows.Forms.Label LblPlcAddMeasReqBit;
        private System.Windows.Forms.Button BtnMeasFin;
        private System.Windows.Forms.Button BtnAlarm;
        private System.Windows.Forms.Label LblPlcAddMeasFin;
        private System.Windows.Forms.Button BtnNg;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label LblPlcAddAlarm;
        private System.Windows.Forms.Label LblPlcAddOk;
        private System.Windows.Forms.Label LblPlcAddNg;
        private System.Windows.Forms.Label LblPlcAddRstReq;
        private System.Windows.Forms.Button BtnPlcRstReq;
        private System.Windows.Forms.Label LblModelName;
        private System.Windows.Forms.Label LblDateTime;
        private System.Windows.Forms.Label LblOpMode;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdMeasRes;
        private System.Windows.Forms.TableLayoutPanel TlpMeas;
        private System.Windows.Forms.Label LblPpTitle;
        private System.Windows.Forms.Label LblTpTitle;
        private System.Windows.Forms.Label LblNpTitle;
        private System.Windows.Forms.TableLayoutPanel TlpNps;
        private System.Windows.Forms.Label LblNpMeasTxt;
        private System.Windows.Forms.Label LblNpULim;
        private System.Windows.Forms.Label LblNpLLim;
        private System.Windows.Forms.Label LblNpMeas;
        private System.Windows.Forms.Label LblNpLLimTxt;
        private System.Windows.Forms.Label LblNpULimTxt;
        private System.Windows.Forms.Label LblOk;
        private System.Windows.Forms.TableLayoutPanel TlpPps;
        private System.Windows.Forms.Label LblPpLLim;
        private System.Windows.Forms.Label LblPpMeas;
        private System.Windows.Forms.Label LblPpLLimTxt;
        private System.Windows.Forms.Label LblPpMeasTxt;
        private System.Windows.Forms.Label LblPpULimTxt;
        private System.Windows.Forms.Label LblPpULim;
        private System.Windows.Forms.TableLayoutPanel TlpTps;
        private System.Windows.Forms.Label LblTpLLim;
        private System.Windows.Forms.Label LblTpMeas;
        private System.Windows.Forms.Label LblTpLLimTxt;
        private System.Windows.Forms.Label LblTpMeasTxt;
        private System.Windows.Forms.Label LblTpULimTxt;
        private System.Windows.Forms.Label LblTpULim;
        private System.Windows.Forms.Button BtnReadFlux;
        private System.Windows.Forms.Button BtnResetFlux;
        private System.Windows.Forms.PropertyGrid PpgSettings;
        private System.Windows.Forms.Button BtnSaveSettings;
        private System.Windows.Forms.Button BtnRollbackSettings;
        private System.Windows.Forms.Button BtnDbPageReload;
        private System.Windows.Forms.Button BtnMeasInsert;
        private System.Windows.Forms.CheckBox CkbMeasToday;
        private System.Windows.Forms.CheckBox CkbMeasEndDate;
        private C1.Win.Calendar.C1DateEdit DteMeasStartDate;
        private System.Windows.Forms.CheckBox CkbMeasStartDate;
        private System.Windows.Forms.Label LblMeasItems;
        private System.Windows.Forms.Button BtnMeasUpload;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdProfile;
        private System.Windows.Forms.Button BtnDbProfReload;
        private System.Windows.Forms.Button BtnDbProfSave;
        private C1.Win.Chart.FlexChart FctPeaksLine;
        private C1.Win.Chart.FlexChart FctPeaksHisto;
        private C1.Win.C1SplitContainer.C1SplitContainer SpcDashboard;
        private C1.Win.C1SplitContainer.C1SplitterPanel SppDashLineChart;
        private C1.Win.C1SplitContainer.C1SplitterPanel SppDashHistoChart;
        private C1.Win.Calendar.C1DateEdit DteMeasEndDate;
        private C1.Win.Input.C1MultiSelect MstMeasModel;
        private System.Windows.Forms.Label LblModels;
        private C1.Win.C1Input.C1ComboBox CbxMeasItems;
        private System.Windows.Forms.ComboBox CbxDashGroup;
        private System.Windows.Forms.Label LblDashGroup;
        private System.Windows.Forms.Label LblDashModel;
        private C1.Win.Input.C1MultiSelect MstDashModel;
        private C1.Win.Calendar.C1DateEdit DteDashEnd;
        private C1.Win.Calendar.C1DateEdit DteDashStart;
        private System.Windows.Forms.Button BtnLoadDashboard;
        private System.Windows.Forms.CheckBox CkbDashEndDate;
        private System.Windows.Forms.CheckBox CkbDashStartDate;
        private System.Windows.Forms.Label LblDashPeak;
        private C1.Win.Input.C1MultiSelect MstDashPeak;
        private System.Windows.Forms.FlowLayoutPanel FlpDashboard;
        private C1.Win.C1Command.C1DockingTabPage DtpLog;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdLog;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdProdInf;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdMesData;
        private System.Windows.Forms.Label LblProdInfs;
        private System.Windows.Forms.Label LblMesData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TslLog;
        private System.Windows.Forms.Label LblSiteInfor;
        private System.Windows.Forms.ToolStripStatusLabel TslErrCode;
        private System.Windows.Forms.ToolStripStatusLabel TslLogStatus;
        private System.Windows.Forms.ToolStripStatusLabel TslErrStatus;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdMeasData;
        private System.Windows.Forms.Label LblMeasData;
        private System.Windows.Forms.Button BtnMeasClear;
        private System.Windows.Forms.Button BtnPlcConn;
        private System.Windows.Forms.Panel PnlDbNavi;
        private System.Windows.Forms.Button BtnDbPageFwd;
        private System.Windows.Forms.Button BtnDbPageBwd;
        private System.Windows.Forms.NumericUpDown NudDbPageNum;
        private System.Windows.Forms.Label LblDbPageNum;
        private System.Windows.Forms.Panel PnlDbSearch;
        private System.Windows.Forms.Button BtnDbPageHome;
        private System.Windows.Forms.ContextMenuStrip CmsDbMeas;
        private System.Windows.Forms.ToolStripMenuItem TsmDbDeleteItems;
        private System.Windows.Forms.ToolStripMenuItem TsmExportExcel;
        private System.Windows.Forms.ToolStripMenuItem TsmCopyDataToClipboard;
        private System.Windows.Forms.ToolStripMenuItem TsmOpenDbFolder;
        private System.Windows.Forms.ToolStripMenuItem TsmDbSelectAll;
        private System.Windows.Forms.ToolStripSeparator TssDbSeparator;
    }
}

