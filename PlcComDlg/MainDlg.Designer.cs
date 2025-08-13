namespace PlcComDlg
{
    partial class MainDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDlg));
            this.SspMain = new System.Windows.Forms.StatusStrip();
            this.TslLoadLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslSmaApp = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnPlcStart = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnReload = new System.Windows.Forms.Button();
            this.PpgSettings = new System.Windows.Forms.PropertyGrid();
            this.TmrSmaCheck = new System.Windows.Forms.Timer(this.components);
            this.LblPlcCon = new System.Windows.Forms.Label();
            this.LblTcpServer = new System.Windows.Forms.Label();
            this.LblHeartBeat = new System.Windows.Forms.Label();
            this.LblPlcAddHeartBeat = new System.Windows.Forms.Label();
            this.LblHeartBeatInterval = new System.Windows.Forms.Label();
            this.LblModelNumber = new System.Windows.Forms.Label();
            this.LblMeasTime = new System.Windows.Forms.Label();
            this.LblMeasTimeVal = new System.Windows.Forms.Label();
            this.LblModelNumVal = new System.Windows.Forms.Label();
            this.LblHeartBeatIntVal = new System.Windows.Forms.Label();
            this.BtnMesDataUpload = new System.Windows.Forms.Button();
            this.BtnAlarm = new System.Windows.Forms.Button();
            this.BtnNg = new System.Windows.Forms.Button();
            this.BtnBusy = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnMeasFin = new System.Windows.Forms.Button();
            this.BtnMeasReqResp = new System.Windows.Forms.Button();
            this.BtnPlcMeasReq = new System.Windows.Forms.Button();
            this.LblPlcAddAlarm = new System.Windows.Forms.Label();
            this.LblPlcAddNg = new System.Windows.Forms.Label();
            this.LblPlcAddBusy = new System.Windows.Forms.Label();
            this.LblPlcAddMeasFin = new System.Windows.Forms.Label();
            this.LblPlcAddOk = new System.Windows.Forms.Label();
            this.LblPlcAddMeasReqResp = new System.Windows.Forms.Label();
            this.LblPlcAddMeasReq = new System.Windows.Forms.Label();
            this.LblPlcAlmCode = new System.Windows.Forms.Label();
            this.BtnTcpAbort = new System.Windows.Forms.Button();
            this.BtnTcpReady = new System.Windows.Forms.Button();
            this.BtnTcpCls = new System.Windows.Forms.Button();
            this.BtnTcpFluxStart = new System.Windows.Forms.Button();
            this.LblTcpMeasTime = new System.Windows.Forms.Label();
            this.LblTcpMeasTimeVal = new System.Windows.Forms.Label();
            this.LblTcpServerIp = new System.Windows.Forms.Label();
            this.LblTcpIdleTimeVal = new System.Windows.Forms.Label();
            this.LblTcpIdleTime = new System.Windows.Forms.Label();
            this.LblDbId = new System.Windows.Forms.Label();
            this.LblDbIdVal = new System.Windows.Forms.Label();
            this.LblMesOkVal = new System.Windows.Forms.Label();
            this.LblFailedIdVal = new System.Windows.Forms.Label();
            this.LblFailedId = new System.Windows.Forms.Label();
            this.LblMesOk = new System.Windows.Forms.Label();
            this.BtnFolderLog = new System.Windows.Forms.Button();
            this.BtnFolderDb = new System.Windows.Forms.Button();
            this.BtnFolderSettings = new System.Windows.Forms.Button();
            this.BtnTcpStart = new System.Windows.Forms.Button();
            this.TbxLog = new System.Windows.Forms.TextBox();
            this.BtnClearLog = new System.Windows.Forms.Button();
            this.LblTcpAlmCode = new System.Windows.Forms.Label();
            this.DtpMain = new C1.Win.C1Command.C1DockingTab();
            this.DptMes = new C1.Win.C1Command.C1DockingTabPage();
            this.SpcData = new System.Windows.Forms.SplitContainer();
            this.FgdProdInf = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.BtnLblReadProInf = new System.Windows.Forms.Button();
            this.FgdMesData = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.BtnMesClear = new System.Windows.Forms.Button();
            this.DtpSettings = new C1.Win.C1Command.C1DockingTabPage();
            this.DtpLog = new C1.Win.C1Command.C1DockingTabPage();
            this.FgdLog = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.LblDateTime = new System.Windows.Forms.Label();
            this.LblResult = new System.Windows.Forms.Label();
            this.LblModelNumberAdd = new System.Windows.Forms.Label();
            this.BtnMeasManual = new System.Windows.Forms.Button();
            this.SspMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtpMain)).BeginInit();
            this.DtpMain.SuspendLayout();
            this.DptMes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpcData)).BeginInit();
            this.SpcData.Panel1.SuspendLayout();
            this.SpcData.Panel2.SuspendLayout();
            this.SpcData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdProdInf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMesData)).BeginInit();
            this.DtpSettings.SuspendLayout();
            this.DtpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FgdLog)).BeginInit();
            this.SuspendLayout();
            // 
            // SspMain
            // 
            this.SspMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.SspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TslLoadLog,
            this.TslSmaApp});
            this.SspMain.Location = new System.Drawing.Point(0, 737);
            this.SspMain.Name = "SspMain";
            this.SspMain.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.SspMain.Size = new System.Drawing.Size(1264, 24);
            this.SspMain.TabIndex = 8;
            this.SspMain.Text = "statusStrip1";
            // 
            // TslLoadLog
            // 
            this.TslLoadLog.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TslLoadLog.Name = "TslLoadLog";
            this.TslLoadLog.Size = new System.Drawing.Size(34, 19);
            this.TslLoadLog.Text = "LOG";
            // 
            // TslSmaApp
            // 
            this.TslSmaApp.Name = "TslSmaApp";
            this.TslSmaApp.Size = new System.Drawing.Size(59, 19);
            this.TslSmaApp.Text = "SMA APP";
            // 
            // BtnPlcStart
            // 
            this.BtnPlcStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcStart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPlcStart.Location = new System.Drawing.Point(1128, 696);
            this.BtnPlcStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnPlcStart.Name = "BtnPlcStart";
            this.BtnPlcStart.Size = new System.Drawing.Size(128, 32);
            this.BtnPlcStart.TabIndex = 9;
            this.BtnPlcStart.Text = "PLC Start";
            this.BtnPlcStart.UseVisualStyleBackColor = true;
            this.BtnPlcStart.Click += new System.EventHandler(this.BtnWorkerStart_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(824, 527);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(128, 32);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnReload
            // 
            this.BtnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReload.Location = new System.Drawing.Point(688, 527);
            this.BtnReload.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnReload.Name = "BtnReload";
            this.BtnReload.Size = new System.Drawing.Size(128, 32);
            this.BtnReload.TabIndex = 9;
            this.BtnReload.Text = "Re-load";
            this.BtnReload.UseVisualStyleBackColor = true;
            this.BtnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // PpgSettings
            // 
            this.PpgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PpgSettings.Location = new System.Drawing.Point(8, 9);
            this.PpgSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PpgSettings.Name = "PpgSettings";
            this.PpgSettings.Size = new System.Drawing.Size(954, 502);
            this.PpgSettings.TabIndex = 14;
            // 
            // TmrSmaCheck
            // 
            this.TmrSmaCheck.Tick += new System.EventHandler(this.TmrSmaCheck_Tick);
            // 
            // LblPlcCon
            // 
            this.LblPlcCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcCon.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPlcCon.Location = new System.Drawing.Point(1000, 120);
            this.LblPlcCon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcCon.Name = "LblPlcCon";
            this.LblPlcCon.Size = new System.Drawing.Size(256, 32);
            this.LblPlcCon.TabIndex = 16;
            this.LblPlcCon.Text = "PLC Connection";
            this.LblPlcCon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpServer
            // 
            this.LblTcpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpServer.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpServer.Location = new System.Drawing.Point(1000, 512);
            this.LblTcpServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpServer.Name = "LblTcpServer";
            this.LblTcpServer.Size = new System.Drawing.Size(256, 32);
            this.LblTcpServer.TabIndex = 16;
            this.LblTcpServer.Text = "SMA TCP Server";
            this.LblTcpServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeat
            // 
            this.LblHeartBeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeat.Location = new System.Drawing.Point(1000, 272);
            this.LblHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeat.Name = "LblHeartBeat";
            this.LblHeartBeat.Size = new System.Drawing.Size(128, 32);
            this.LblHeartBeat.TabIndex = 16;
            this.LblHeartBeat.Text = "HEARTBEAT";
            this.LblHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddHeartBeat
            // 
            this.LblPlcAddHeartBeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddHeartBeat.Location = new System.Drawing.Point(1000, 248);
            this.LblPlcAddHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddHeartBeat.Name = "LblPlcAddHeartBeat";
            this.LblPlcAddHeartBeat.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddHeartBeat.TabIndex = 16;
            this.LblPlcAddHeartBeat.Text = "-";
            this.LblPlcAddHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeatInterval
            // 
            this.LblHeartBeatInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHeartBeatInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeatInterval.Location = new System.Drawing.Point(1128, 208);
            this.LblHeartBeatInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeatInterval.Name = "LblHeartBeatInterval";
            this.LblHeartBeatInterval.Size = new System.Drawing.Size(128, 32);
            this.LblHeartBeatInterval.TabIndex = 20;
            this.LblHeartBeatInterval.Text = "Update interval";
            this.LblHeartBeatInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblModelNumber
            // 
            this.LblModelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblModelNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumber.Location = new System.Drawing.Point(1000, 208);
            this.LblModelNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumber.Name = "LblModelNumber";
            this.LblModelNumber.Size = new System.Drawing.Size(128, 32);
            this.LblModelNumber.TabIndex = 20;
            this.LblModelNumber.Text = "Model number";
            this.LblModelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMeasTime
            // 
            this.LblMeasTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMeasTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMeasTime.Location = new System.Drawing.Point(1000, 48);
            this.LblMeasTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasTime.Name = "LblMeasTime";
            this.LblMeasTime.Size = new System.Drawing.Size(128, 32);
            this.LblMeasTime.TabIndex = 20;
            this.LblMeasTime.Text = "Measurement time";
            this.LblMeasTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMeasTimeVal
            // 
            this.LblMeasTimeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMeasTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMeasTimeVal.Location = new System.Drawing.Point(1128, 48);
            this.LblMeasTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasTimeVal.Name = "LblMeasTimeVal";
            this.LblMeasTimeVal.Size = new System.Drawing.Size(128, 32);
            this.LblMeasTimeVal.TabIndex = 19;
            this.LblMeasTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblModelNumVal
            // 
            this.LblModelNumVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblModelNumVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblModelNumVal.Location = new System.Drawing.Point(1000, 152);
            this.LblModelNumVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumVal.Name = "LblModelNumVal";
            this.LblModelNumVal.Size = new System.Drawing.Size(256, 32);
            this.LblModelNumVal.TabIndex = 16;
            this.LblModelNumVal.Text = "0";
            this.LblModelNumVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeatIntVal
            // 
            this.LblHeartBeatIntVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblHeartBeatIntVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeatIntVal.Location = new System.Drawing.Point(1128, 184);
            this.LblHeartBeatIntVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeatIntVal.Name = "LblHeartBeatIntVal";
            this.LblHeartBeatIntVal.Size = new System.Drawing.Size(128, 24);
            this.LblHeartBeatIntVal.TabIndex = 16;
            this.LblHeartBeatIntVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnMesDataUpload
            // 
            this.BtnMesDataUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMesDataUpload.Location = new System.Drawing.Point(824, 245);
            this.BtnMesDataUpload.Name = "BtnMesDataUpload";
            this.BtnMesDataUpload.Size = new System.Drawing.Size(128, 32);
            this.BtnMesDataUpload.TabIndex = 21;
            this.BtnMesDataUpload.Text = "MES Upload";
            this.BtnMesDataUpload.UseVisualStyleBackColor = true;
            this.BtnMesDataUpload.Click += new System.EventHandler(this.BtnMesDataUpload_Click);
            // 
            // BtnAlarm
            // 
            this.BtnAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAlarm.Location = new System.Drawing.Point(1128, 384);
            this.BtnAlarm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnAlarm.Name = "BtnAlarm";
            this.BtnAlarm.Size = new System.Drawing.Size(128, 32);
            this.BtnAlarm.TabIndex = 17;
            this.BtnAlarm.Text = "ALARM";
            this.BtnAlarm.UseVisualStyleBackColor = true;
            this.BtnAlarm.Click += new System.EventHandler(this.BtnAlarm_Click);
            // 
            // BtnNg
            // 
            this.BtnNg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNg.Location = new System.Drawing.Point(1128, 440);
            this.BtnNg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnNg.Name = "BtnNg";
            this.BtnNg.Size = new System.Drawing.Size(128, 32);
            this.BtnNg.TabIndex = 17;
            this.BtnNg.Text = "NG";
            this.BtnNg.UseVisualStyleBackColor = true;
            this.BtnNg.Click += new System.EventHandler(this.BtnNg_Click);
            // 
            // BtnBusy
            // 
            this.BtnBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBusy.Location = new System.Drawing.Point(1000, 384);
            this.BtnBusy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnBusy.Name = "BtnBusy";
            this.BtnBusy.Size = new System.Drawing.Size(128, 32);
            this.BtnBusy.TabIndex = 17;
            this.BtnBusy.Text = "BUSY";
            this.BtnBusy.UseVisualStyleBackColor = true;
            this.BtnBusy.Click += new System.EventHandler(this.BtnBusy_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(1000, 440);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(128, 32);
            this.BtnOk.TabIndex = 17;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnMeasFin
            // 
            this.BtnMeasFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasFin.Location = new System.Drawing.Point(1128, 328);
            this.BtnMeasFin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnMeasFin.Name = "BtnMeasFin";
            this.BtnMeasFin.Size = new System.Drawing.Size(128, 32);
            this.BtnMeasFin.TabIndex = 17;
            this.BtnMeasFin.Text = "MEAS FIN.";
            this.BtnMeasFin.UseVisualStyleBackColor = true;
            this.BtnMeasFin.Click += new System.EventHandler(this.BtnMeasFin_Click);
            // 
            // BtnMeasReqResp
            // 
            this.BtnMeasReqResp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasReqResp.Location = new System.Drawing.Point(1000, 328);
            this.BtnMeasReqResp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnMeasReqResp.Name = "BtnMeasReqResp";
            this.BtnMeasReqResp.Size = new System.Drawing.Size(128, 32);
            this.BtnMeasReqResp.TabIndex = 17;
            this.BtnMeasReqResp.Text = "MEAS RESP.";
            this.BtnMeasReqResp.UseVisualStyleBackColor = true;
            this.BtnMeasReqResp.Click += new System.EventHandler(this.BtnMeasReqResp_Click);
            // 
            // BtnPlcMeasReq
            // 
            this.BtnPlcMeasReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcMeasReq.Location = new System.Drawing.Point(1128, 272);
            this.BtnPlcMeasReq.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnPlcMeasReq.Name = "BtnPlcMeasReq";
            this.BtnPlcMeasReq.Size = new System.Drawing.Size(128, 32);
            this.BtnPlcMeasReq.TabIndex = 17;
            this.BtnPlcMeasReq.Text = "MEAS REQ.";
            this.BtnPlcMeasReq.UseVisualStyleBackColor = true;
            this.BtnPlcMeasReq.Click += new System.EventHandler(this.BtnPlcMeasReq_Click);
            // 
            // LblPlcAddAlarm
            // 
            this.LblPlcAddAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddAlarm.Location = new System.Drawing.Point(1128, 360);
            this.LblPlcAddAlarm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddAlarm.Name = "LblPlcAddAlarm";
            this.LblPlcAddAlarm.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddAlarm.TabIndex = 16;
            this.LblPlcAddAlarm.Text = "-";
            this.LblPlcAddAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddNg
            // 
            this.LblPlcAddNg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddNg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddNg.Location = new System.Drawing.Point(1128, 416);
            this.LblPlcAddNg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddNg.Name = "LblPlcAddNg";
            this.LblPlcAddNg.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddNg.TabIndex = 16;
            this.LblPlcAddNg.Text = "-";
            this.LblPlcAddNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddBusy
            // 
            this.LblPlcAddBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddBusy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddBusy.Location = new System.Drawing.Point(1000, 360);
            this.LblPlcAddBusy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddBusy.Name = "LblPlcAddBusy";
            this.LblPlcAddBusy.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddBusy.TabIndex = 16;
            this.LblPlcAddBusy.Text = "-";
            this.LblPlcAddBusy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasFin
            // 
            this.LblPlcAddMeasFin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddMeasFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasFin.Location = new System.Drawing.Point(1128, 304);
            this.LblPlcAddMeasFin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasFin.Name = "LblPlcAddMeasFin";
            this.LblPlcAddMeasFin.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddMeasFin.TabIndex = 16;
            this.LblPlcAddMeasFin.Text = "-";
            this.LblPlcAddMeasFin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddOk
            // 
            this.LblPlcAddOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddOk.Location = new System.Drawing.Point(1000, 416);
            this.LblPlcAddOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddOk.Name = "LblPlcAddOk";
            this.LblPlcAddOk.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddOk.TabIndex = 16;
            this.LblPlcAddOk.Text = "-";
            this.LblPlcAddOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasReqResp
            // 
            this.LblPlcAddMeasReqResp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddMeasReqResp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasReqResp.Location = new System.Drawing.Point(1000, 304);
            this.LblPlcAddMeasReqResp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasReqResp.Name = "LblPlcAddMeasReqResp";
            this.LblPlcAddMeasReqResp.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddMeasReqResp.TabIndex = 16;
            this.LblPlcAddMeasReqResp.Text = "-";
            this.LblPlcAddMeasReqResp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasReq
            // 
            this.LblPlcAddMeasReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAddMeasReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasReq.Location = new System.Drawing.Point(1128, 248);
            this.LblPlcAddMeasReq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasReq.Name = "LblPlcAddMeasReq";
            this.LblPlcAddMeasReq.Size = new System.Drawing.Size(128, 24);
            this.LblPlcAddMeasReq.TabIndex = 16;
            this.LblPlcAddMeasReq.Text = "-";
            this.LblPlcAddMeasReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAlmCode
            // 
            this.LblPlcAlmCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblPlcAlmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAlmCode.Location = new System.Drawing.Point(216, 527);
            this.LblPlcAlmCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAlmCode.Name = "LblPlcAlmCode";
            this.LblPlcAlmCode.Size = new System.Drawing.Size(192, 32);
            this.LblPlcAlmCode.TabIndex = 16;
            this.LblPlcAlmCode.Text = "PLC ALARM";
            this.LblPlcAlmCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblPlcAlmCode.Click += new System.EventHandler(this.LblPlcAlmCode_Click);
            // 
            // BtnTcpAbort
            // 
            this.BtnTcpAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTcpAbort.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpAbort.Location = new System.Drawing.Point(1128, 648);
            this.BtnTcpAbort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTcpAbort.Name = "BtnTcpAbort";
            this.BtnTcpAbort.Size = new System.Drawing.Size(128, 32);
            this.BtnTcpAbort.TabIndex = 17;
            this.BtnTcpAbort.Text = "Abort";
            this.BtnTcpAbort.UseVisualStyleBackColor = true;
            this.BtnTcpAbort.Click += new System.EventHandler(this.BtnTcpAbort_Click);
            // 
            // BtnTcpReady
            // 
            this.BtnTcpReady.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTcpReady.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpReady.Location = new System.Drawing.Point(1000, 648);
            this.BtnTcpReady.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTcpReady.Name = "BtnTcpReady";
            this.BtnTcpReady.Size = new System.Drawing.Size(128, 32);
            this.BtnTcpReady.TabIndex = 17;
            this.BtnTcpReady.Text = "Ready?";
            this.BtnTcpReady.UseVisualStyleBackColor = true;
            this.BtnTcpReady.Click += new System.EventHandler(this.BtnTcpReady_Click);
            // 
            // BtnTcpCls
            // 
            this.BtnTcpCls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTcpCls.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpCls.Location = new System.Drawing.Point(1128, 616);
            this.BtnTcpCls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTcpCls.Name = "BtnTcpCls";
            this.BtnTcpCls.Size = new System.Drawing.Size(128, 32);
            this.BtnTcpCls.TabIndex = 17;
            this.BtnTcpCls.Text = "*CLS";
            this.BtnTcpCls.UseVisualStyleBackColor = true;
            this.BtnTcpCls.Click += new System.EventHandler(this.BtnTcpCls_Click);
            // 
            // BtnTcpFluxStart
            // 
            this.BtnTcpFluxStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTcpFluxStart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpFluxStart.Location = new System.Drawing.Point(1000, 616);
            this.BtnTcpFluxStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTcpFluxStart.Name = "BtnTcpFluxStart";
            this.BtnTcpFluxStart.Size = new System.Drawing.Size(128, 32);
            this.BtnTcpFluxStart.TabIndex = 17;
            this.BtnTcpFluxStart.Text = "FLUX";
            this.BtnTcpFluxStart.UseVisualStyleBackColor = true;
            this.BtnTcpFluxStart.Click += new System.EventHandler(this.BtnTcpFluxStart_Click);
            // 
            // LblTcpMeasTime
            // 
            this.LblTcpMeasTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpMeasTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpMeasTime.Location = new System.Drawing.Point(1000, 592);
            this.LblTcpMeasTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpMeasTime.Name = "LblTcpMeasTime";
            this.LblTcpMeasTime.Size = new System.Drawing.Size(128, 24);
            this.LblTcpMeasTime.TabIndex = 20;
            this.LblTcpMeasTime.Text = "MEAS. TIME";
            this.LblTcpMeasTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpMeasTimeVal
            // 
            this.LblTcpMeasTimeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpMeasTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpMeasTimeVal.Location = new System.Drawing.Point(1128, 592);
            this.LblTcpMeasTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpMeasTimeVal.Name = "LblTcpMeasTimeVal";
            this.LblTcpMeasTimeVal.Size = new System.Drawing.Size(128, 24);
            this.LblTcpMeasTimeVal.TabIndex = 19;
            this.LblTcpMeasTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTcpServerIp
            // 
            this.LblTcpServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpServerIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpServerIp.Location = new System.Drawing.Point(1000, 544);
            this.LblTcpServerIp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpServerIp.Name = "LblTcpServerIp";
            this.LblTcpServerIp.Size = new System.Drawing.Size(256, 24);
            this.LblTcpServerIp.TabIndex = 16;
            this.LblTcpServerIp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpIdleTimeVal
            // 
            this.LblTcpIdleTimeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpIdleTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpIdleTimeVal.Location = new System.Drawing.Point(1128, 568);
            this.LblTcpIdleTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpIdleTimeVal.Name = "LblTcpIdleTimeVal";
            this.LblTcpIdleTimeVal.Size = new System.Drawing.Size(128, 24);
            this.LblTcpIdleTimeVal.TabIndex = 16;
            this.LblTcpIdleTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTcpIdleTime
            // 
            this.LblTcpIdleTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpIdleTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpIdleTime.Location = new System.Drawing.Point(1000, 568);
            this.LblTcpIdleTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpIdleTime.Name = "LblTcpIdleTime";
            this.LblTcpIdleTime.Size = new System.Drawing.Size(128, 24);
            this.LblTcpIdleTime.TabIndex = 16;
            this.LblTcpIdleTime.Text = "TCP IDLE TIME";
            this.LblTcpIdleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDbId
            // 
            this.LblDbId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblDbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDbId.Location = new System.Drawing.Point(17, 243);
            this.LblDbId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDbId.Name = "LblDbId";
            this.LblDbId.Size = new System.Drawing.Size(96, 32);
            this.LblDbId.TabIndex = 16;
            this.LblDbId.Text = "Database ID";
            this.LblDbId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDbIdVal
            // 
            this.LblDbIdVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblDbIdVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDbIdVal.Location = new System.Drawing.Point(112, 243);
            this.LblDbIdVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDbIdVal.Name = "LblDbIdVal";
            this.LblDbIdVal.Size = new System.Drawing.Size(96, 32);
            this.LblDbIdVal.TabIndex = 16;
            this.LblDbIdVal.Text = "-";
            this.LblDbIdVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMesOkVal
            // 
            this.LblMesOkVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblMesOkVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMesOkVal.Location = new System.Drawing.Point(496, 243);
            this.LblMesOkVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMesOkVal.Name = "LblMesOkVal";
            this.LblMesOkVal.Size = new System.Drawing.Size(96, 32);
            this.LblMesOkVal.TabIndex = 16;
            this.LblMesOkVal.Text = "-";
            this.LblMesOkVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblFailedIdVal
            // 
            this.LblFailedIdVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblFailedIdVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblFailedIdVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblFailedIdVal.Location = new System.Drawing.Point(304, 243);
            this.LblFailedIdVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblFailedIdVal.Name = "LblFailedIdVal";
            this.LblFailedIdVal.Size = new System.Drawing.Size(96, 32);
            this.LblFailedIdVal.TabIndex = 16;
            this.LblFailedIdVal.Text = "-";
            this.LblFailedIdVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblFailedId
            // 
            this.LblFailedId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblFailedId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblFailedId.Location = new System.Drawing.Point(208, 243);
            this.LblFailedId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblFailedId.Name = "LblFailedId";
            this.LblFailedId.Size = new System.Drawing.Size(96, 32);
            this.LblFailedId.TabIndex = 16;
            this.LblFailedId.Text = "Error code";
            this.LblFailedId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMesOk
            // 
            this.LblMesOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblMesOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMesOk.Location = new System.Drawing.Point(400, 243);
            this.LblMesOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMesOk.Name = "LblMesOk";
            this.LblMesOk.Size = new System.Drawing.Size(96, 32);
            this.LblMesOk.TabIndex = 16;
            this.LblMesOk.Text = "Result";
            this.LblMesOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnFolderLog
            // 
            this.BtnFolderLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFolderLog.Location = new System.Drawing.Point(824, 527);
            this.BtnFolderLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnFolderLog.Name = "BtnFolderLog";
            this.BtnFolderLog.Size = new System.Drawing.Size(128, 32);
            this.BtnFolderLog.TabIndex = 17;
            this.BtnFolderLog.Text = "Log folder";
            this.BtnFolderLog.UseVisualStyleBackColor = true;
            this.BtnFolderLog.Click += new System.EventHandler(this.BtnFolderLog_Click);
            // 
            // BtnFolderDb
            // 
            this.BtnFolderDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFolderDb.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnFolderDb.Location = new System.Drawing.Point(649, 287);
            this.BtnFolderDb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnFolderDb.Name = "BtnFolderDb";
            this.BtnFolderDb.Size = new System.Drawing.Size(104, 24);
            this.BtnFolderDb.TabIndex = 17;
            this.BtnFolderDb.Text = "DB folder";
            this.BtnFolderDb.UseVisualStyleBackColor = true;
            this.BtnFolderDb.Click += new System.EventHandler(this.BtnFolderDb_Click);
            // 
            // BtnFolderSettings
            // 
            this.BtnFolderSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFolderSettings.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnFolderSettings.Location = new System.Drawing.Point(545, 287);
            this.BtnFolderSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnFolderSettings.Name = "BtnFolderSettings";
            this.BtnFolderSettings.Size = new System.Drawing.Size(104, 24);
            this.BtnFolderSettings.TabIndex = 17;
            this.BtnFolderSettings.Text = "Settings folder";
            this.BtnFolderSettings.UseVisualStyleBackColor = true;
            this.BtnFolderSettings.Click += new System.EventHandler(this.BtnFolderSettings_Click);
            // 
            // BtnTcpStart
            // 
            this.BtnTcpStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTcpStart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpStart.Location = new System.Drawing.Point(1000, 696);
            this.BtnTcpStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnTcpStart.Name = "BtnTcpStart";
            this.BtnTcpStart.Size = new System.Drawing.Size(128, 32);
            this.BtnTcpStart.TabIndex = 9;
            this.BtnTcpStart.Text = "TCP Start";
            this.BtnTcpStart.UseVisualStyleBackColor = true;
            this.BtnTcpStart.Click += new System.EventHandler(this.BtnTcpStart_Click);
            // 
            // TbxLog
            // 
            this.TbxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxLog.Location = new System.Drawing.Point(16, 632);
            this.TbxLog.Multiline = true;
            this.TbxLog.Name = "TbxLog";
            this.TbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbxLog.Size = new System.Drawing.Size(972, 96);
            this.TbxLog.TabIndex = 22;
            // 
            // BtnClearLog
            // 
            this.BtnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnClearLog.Location = new System.Drawing.Point(688, 527);
            this.BtnClearLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnClearLog.Name = "BtnClearLog";
            this.BtnClearLog.Size = new System.Drawing.Size(128, 32);
            this.BtnClearLog.TabIndex = 9;
            this.BtnClearLog.Text = "LOG CLR";
            this.BtnClearLog.UseVisualStyleBackColor = true;
            this.BtnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // LblTcpAlmCode
            // 
            this.LblTcpAlmCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblTcpAlmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpAlmCode.Location = new System.Drawing.Point(16, 527);
            this.LblTcpAlmCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpAlmCode.Name = "LblTcpAlmCode";
            this.LblTcpAlmCode.Size = new System.Drawing.Size(192, 32);
            this.LblTcpAlmCode.TabIndex = 16;
            this.LblTcpAlmCode.Text = "TCP ALARM";
            this.LblTcpAlmCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblTcpAlmCode.Click += new System.EventHandler(this.LblTcpAlmCode_Click);
            // 
            // DtpMain
            // 
            this.DtpMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.DtpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DtpMain.Controls.Add(this.DptMes);
            this.DtpMain.Controls.Add(this.DtpSettings);
            this.DtpMain.Controls.Add(this.DtpLog);
            this.DtpMain.ItemSize = new System.Drawing.Size(0, 32);
            this.DtpMain.Location = new System.Drawing.Point(16, 16);
            this.DtpMain.Name = "DtpMain";
            this.DtpMain.Size = new System.Drawing.Size(970, 606);
            this.DtpMain.TabIndex = 24;
            this.DtpMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.FillToEnd;
            this.DtpMain.TabsSpacing = 5;
            // 
            // DptMes
            // 
            this.DptMes.Controls.Add(this.SpcData);
            this.DptMes.Image = global::PlcComDlg.Properties.Resources.Home;
            this.DptMes.Location = new System.Drawing.Point(1, 1);
            this.DptMes.Name = "DptMes";
            this.DptMes.Size = new System.Drawing.Size(968, 570);
            this.DptMes.TabIndex = 1;
            this.DptMes.Text = "Home";
            // 
            // SpcData
            // 
            this.SpcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpcData.Location = new System.Drawing.Point(0, 0);
            this.SpcData.Name = "SpcData";
            this.SpcData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SpcData.Panel1
            // 
            this.SpcData.Panel1.Controls.Add(this.FgdProdInf);
            this.SpcData.Panel1.Controls.Add(this.BtnLblReadProInf);
            // 
            // SpcData.Panel2
            // 
            this.SpcData.Panel2.Controls.Add(this.FgdMesData);
            this.SpcData.Panel2.Controls.Add(this.LblMesOkVal);
            this.SpcData.Panel2.Controls.Add(this.BtnMesClear);
            this.SpcData.Panel2.Controls.Add(this.BtnMesDataUpload);
            this.SpcData.Panel2.Controls.Add(this.LblFailedIdVal);
            this.SpcData.Panel2.Controls.Add(this.LblDbId);
            this.SpcData.Panel2.Controls.Add(this.LblMesOk);
            this.SpcData.Panel2.Controls.Add(this.LblDbIdVal);
            this.SpcData.Panel2.Controls.Add(this.LblFailedId);
            this.SpcData.Size = new System.Drawing.Size(968, 570);
            this.SpcData.SplitterDistance = 271;
            this.SpcData.TabIndex = 22;
            // 
            // FgdProdInf
            // 
            this.FgdProdInf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdProdInf.ColumnInfo = "10,1,0,0,0,-1,Columns:1{Caption:\"Name\";}\t2{Caption:\"Type\";}\t3{Caption:\"Address\";}" +
    "\t4{Caption:\"DB\";}\t5{Caption:\"Data\";}\t";
            this.FgdProdInf.ExtendLastCol = true;
            this.FgdProdInf.Location = new System.Drawing.Point(16, 24);
            this.FgdProdInf.Name = "FgdProdInf";
            this.FgdProdInf.Size = new System.Drawing.Size(935, 199);
            this.FgdProdInf.TabIndex = 18;
            this.FgdProdInf.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // BtnLblReadProInf
            // 
            this.BtnLblReadProInf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLblReadProInf.Location = new System.Drawing.Point(824, 229);
            this.BtnLblReadProInf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnLblReadProInf.Name = "BtnLblReadProInf";
            this.BtnLblReadProInf.Size = new System.Drawing.Size(128, 32);
            this.BtnLblReadProInf.TabIndex = 17;
            this.BtnLblReadProInf.Text = "Read product infor";
            this.BtnLblReadProInf.UseVisualStyleBackColor = true;
            this.BtnLblReadProInf.Click += new System.EventHandler(this.BtnLblReadProdInfor_Click);
            // 
            // FgdMesData
            // 
            this.FgdMesData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdMesData.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdMesData.ExtendLastCol = true;
            this.FgdMesData.Location = new System.Drawing.Point(16, 16);
            this.FgdMesData.Name = "FgdMesData";
            this.FgdMesData.Size = new System.Drawing.Size(935, 219);
            this.FgdMesData.TabIndex = 22;
            this.FgdMesData.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // BtnMesClear
            // 
            this.BtnMesClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMesClear.Location = new System.Drawing.Point(688, 245);
            this.BtnMesClear.Name = "BtnMesClear";
            this.BtnMesClear.Size = new System.Drawing.Size(128, 32);
            this.BtnMesClear.TabIndex = 21;
            this.BtnMesClear.Text = "MES Clear";
            this.BtnMesClear.UseVisualStyleBackColor = true;
            this.BtnMesClear.Click += new System.EventHandler(this.BtnMesClear_Click);
            // 
            // DtpSettings
            // 
            this.DtpSettings.Controls.Add(this.PpgSettings);
            this.DtpSettings.Controls.Add(this.BtnReload);
            this.DtpSettings.Controls.Add(this.BtnSave);
            this.DtpSettings.Controls.Add(this.BtnFolderDb);
            this.DtpSettings.Controls.Add(this.BtnFolderSettings);
            this.DtpSettings.Image = global::PlcComDlg.Properties.Resources.Settings;
            this.DtpSettings.Location = new System.Drawing.Point(1, 1);
            this.DtpSettings.Name = "DtpSettings";
            this.DtpSettings.Size = new System.Drawing.Size(968, 570);
            this.DtpSettings.TabIndex = 0;
            this.DtpSettings.Text = "Settings";
            // 
            // DtpLog
            // 
            this.DtpLog.Controls.Add(this.FgdLog);
            this.DtpLog.Controls.Add(this.BtnFolderLog);
            this.DtpLog.Controls.Add(this.BtnClearLog);
            this.DtpLog.Controls.Add(this.LblTcpAlmCode);
            this.DtpLog.Controls.Add(this.LblPlcAlmCode);
            this.DtpLog.Image = global::PlcComDlg.Properties.Resources.Log;
            this.DtpLog.Location = new System.Drawing.Point(1, 1);
            this.DtpLog.Name = "DtpLog";
            this.DtpLog.Size = new System.Drawing.Size(968, 570);
            this.DtpLog.TabIndex = 2;
            this.DtpLog.Text = "Log";
            // 
            // FgdLog
            // 
            this.FgdLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FgdLog.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.FgdLog.ExtendLastCol = true;
            this.FgdLog.Location = new System.Drawing.Point(15, 15);
            this.FgdLog.Name = "FgdLog";
            this.FgdLog.Size = new System.Drawing.Size(934, 504);
            this.FgdLog.TabIndex = 18;
            this.FgdLog.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            // 
            // LblDateTime
            // 
            this.LblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDateTime.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblDateTime.Location = new System.Drawing.Point(1000, 16);
            this.LblDateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDateTime.Name = "LblDateTime";
            this.LblDateTime.Size = new System.Drawing.Size(256, 32);
            this.LblDateTime.TabIndex = 16;
            this.LblDateTime.Text = "0000-00-00 00:00:00";
            this.LblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblResult
            // 
            this.LblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblResult.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblResult.Location = new System.Drawing.Point(1000, 80);
            this.LblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(256, 32);
            this.LblResult.TabIndex = 20;
            this.LblResult.Text = "-";
            this.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblModelNumberAdd
            // 
            this.LblModelNumberAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblModelNumberAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumberAdd.Location = new System.Drawing.Point(1000, 184);
            this.LblModelNumberAdd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumberAdd.Name = "LblModelNumberAdd";
            this.LblModelNumberAdd.Size = new System.Drawing.Size(128, 24);
            this.LblModelNumberAdd.TabIndex = 25;
            this.LblModelNumberAdd.Text = "-";
            this.LblModelNumberAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnMeasManual
            // 
            this.BtnMeasManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMeasManual.Location = new System.Drawing.Point(1001, 475);
            this.BtnMeasManual.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnMeasManual.Name = "BtnMeasManual";
            this.BtnMeasManual.Size = new System.Drawing.Size(128, 32);
            this.BtnMeasManual.TabIndex = 26;
            this.BtnMeasManual.Text = "Measurement";
            this.BtnMeasManual.UseVisualStyleBackColor = true;
            this.BtnMeasManual.Click += new System.EventHandler(this.BtnMeasManual_Click);
            // 
            // MainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.BtnMeasManual);
            this.Controls.Add(this.LblModelNumberAdd);
            this.Controls.Add(this.BtnTcpAbort);
            this.Controls.Add(this.LblPlcAddHeartBeat);
            this.Controls.Add(this.BtnPlcMeasReq);
            this.Controls.Add(this.BtnTcpReady);
            this.Controls.Add(this.LblHeartBeat);
            this.Controls.Add(this.BtnTcpCls);
            this.Controls.Add(this.DtpMain);
            this.Controls.Add(this.BtnTcpFluxStart);
            this.Controls.Add(this.LblPlcAddMeasReq);
            this.Controls.Add(this.LblPlcCon);
            this.Controls.Add(this.LblHeartBeatInterval);
            this.Controls.Add(this.BtnMeasReqResp);
            this.Controls.Add(this.LblResult);
            this.Controls.Add(this.LblMeasTime);
            this.Controls.Add(this.LblPlcAddMeasReqResp);
            this.Controls.Add(this.LblMeasTimeVal);
            this.Controls.Add(this.LblDateTime);
            this.Controls.Add(this.LblTcpServer);
            this.Controls.Add(this.LblTcpMeasTime);
            this.Controls.Add(this.LblModelNumber);
            this.Controls.Add(this.LblTcpMeasTimeVal);
            this.Controls.Add(this.LblHeartBeatIntVal);
            this.Controls.Add(this.LblTcpServerIp);
            this.Controls.Add(this.BtnMeasFin);
            this.Controls.Add(this.LblTcpIdleTimeVal);
            this.Controls.Add(this.TbxLog);
            this.Controls.Add(this.LblTcpIdleTime);
            this.Controls.Add(this.BtnAlarm);
            this.Controls.Add(this.LblModelNumVal);
            this.Controls.Add(this.LblPlcAddMeasFin);
            this.Controls.Add(this.BtnNg);
            this.Controls.Add(this.BtnBusy);
            this.Controls.Add(this.LblPlcAddBusy);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.LblPlcAddAlarm);
            this.Controls.Add(this.LblPlcAddOk);
            this.Controls.Add(this.BtnTcpStart);
            this.Controls.Add(this.LblPlcAddNg);
            this.Controls.Add(this.BtnPlcStart);
            this.Controls.Add(this.SspMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(1276, 700);
            this.Name = "MainDlg";
            this.Text = "SMA-4K COMMUNICATION APP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainDlg_FormClosing);
            this.Load += new System.EventHandler(this.MainDlg_Load);
            this.SspMain.ResumeLayout(false);
            this.SspMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtpMain)).EndInit();
            this.DtpMain.ResumeLayout(false);
            this.DptMes.ResumeLayout(false);
            this.SpcData.Panel1.ResumeLayout(false);
            this.SpcData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpcData)).EndInit();
            this.SpcData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgdProdInf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FgdMesData)).EndInit();
            this.DtpSettings.ResumeLayout(false);
            this.DtpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FgdLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip SspMain;
        private System.Windows.Forms.Button BtnPlcStart;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnReload;
        private System.Windows.Forms.PropertyGrid PpgSettings;
        private System.Windows.Forms.ToolStripStatusLabel TslLoadLog;
        private System.Windows.Forms.Timer TmrSmaCheck;
        private System.Windows.Forms.Label LblPlcCon;
        private System.Windows.Forms.Label LblTcpServer;
        private System.Windows.Forms.Label LblHeartBeat;
        private System.Windows.Forms.Label LblModelNumVal;
        private System.Windows.Forms.Label LblDbIdVal;
        private System.Windows.Forms.Label LblDbId;
        private System.Windows.Forms.Label LblMeasTimeVal;
        private System.Windows.Forms.Label LblMeasTime;
        private System.Windows.Forms.Label LblTcpIdleTimeVal;
        private System.Windows.Forms.Label LblTcpIdleTime;
        private System.Windows.Forms.Label LblFailedIdVal;
        private System.Windows.Forms.Label LblMesOk;
        private System.Windows.Forms.Label LblTcpServerIp;
        private System.Windows.Forms.Label LblPlcAddHeartBeat;
        private System.Windows.Forms.Label LblPlcAddMeasReqResp;
        private System.Windows.Forms.Label LblPlcAddMeasReq;
        private System.Windows.Forms.Label LblPlcAddMeasFin;
        private System.Windows.Forms.Button BtnMeasFin;
        private System.Windows.Forms.Button BtnMeasReqResp;
        private System.Windows.Forms.Button BtnPlcMeasReq;
        private System.Windows.Forms.Button BtnTcpReady;
        private System.Windows.Forms.Button BtnTcpCls;
        private System.Windows.Forms.Button BtnTcpFluxStart;
        private System.Windows.Forms.Label LblHeartBeatIntVal;
        private System.Windows.Forms.Button BtnFolderLog;
        private System.Windows.Forms.Button BtnFolderDb;
        private System.Windows.Forms.Button BtnFolderSettings;
        private System.Windows.Forms.Label LblPlcAddNg;
        private System.Windows.Forms.Label LblPlcAddOk;
        private System.Windows.Forms.Button BtnNg;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label LblMesOkVal;
        private System.Windows.Forms.Label LblFailedId;
        private System.Windows.Forms.Button BtnTcpStart;
        private System.Windows.Forms.Label LblPlcAddBusy;
        private System.Windows.Forms.Button BtnBusy;
        private System.Windows.Forms.Label LblPlcAlmCode;
        private System.Windows.Forms.Button BtnTcpAbort;
        private System.Windows.Forms.Label LblHeartBeatInterval;
        private System.Windows.Forms.Button BtnAlarm;
        private System.Windows.Forms.Label LblPlcAddAlarm;
        private System.Windows.Forms.TextBox TbxLog;
        private System.Windows.Forms.Label LblTcpMeasTime;
        private System.Windows.Forms.Label LblTcpMeasTimeVal;
        private System.Windows.Forms.Button BtnClearLog;
        private System.Windows.Forms.Label LblModelNumber;
        private System.Windows.Forms.Button BtnMesDataUpload;
        private System.Windows.Forms.Label LblTcpAlmCode;
        private C1.Win.C1Command.C1DockingTab DtpMain;
        private C1.Win.C1Command.C1DockingTabPage DtpSettings;
        private C1.Win.C1Command.C1DockingTabPage DptMes;
        private C1.Win.C1Command.C1DockingTabPage DtpLog;
        private System.Windows.Forms.ToolStripStatusLabel TslSmaApp;
        private System.Windows.Forms.Button BtnLblReadProInf;
        private System.Windows.Forms.SplitContainer SpcData;
        private System.Windows.Forms.Label LblDateTime;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdLog;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdMesData;
        private C1.Win.C1FlexGrid.C1FlexGrid FgdProdInf;
        private System.Windows.Forms.Label LblResult;
        private System.Windows.Forms.Label LblModelNumberAdd;
        private System.Windows.Forms.Button BtnMesClear;
        private System.Windows.Forms.Button BtnMeasManual;
    }
}

