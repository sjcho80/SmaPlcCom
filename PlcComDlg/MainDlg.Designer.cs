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
            this.TsslLoadLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslLoadSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnPlcStart = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnReload = new System.Windows.Forms.Button();
            this.PpgSettings = new System.Windows.Forms.PropertyGrid();
            this.LvwDb = new System.Windows.Forms.ListView();
            this.clhAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClhDbVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClhMesVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TmrSmaCheck = new System.Windows.Forms.Timer(this.components);
            this.LblPlcCon = new System.Windows.Forms.Label();
            this.LblTcpServer = new System.Windows.Forms.Label();
            this.LblSmaApp = new System.Windows.Forms.Label();
            this.LblHeartBeat = new System.Windows.Forms.Label();
            this.GbxPlc = new System.Windows.Forms.GroupBox();
            this.LblPlcAddHeartBeat = new System.Windows.Forms.Label();
            this.LvwProInfor = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LblHeartBeatInterval = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblModelNumber = new System.Windows.Forms.Label();
            this.LblMeasTime = new System.Windows.Forms.Label();
            this.LblMeasTimeVal = new System.Windows.Forms.Label();
            this.BtnLblReadProInf = new System.Windows.Forms.Button();
            this.BtnReadCustomNum = new System.Windows.Forms.Button();
            this.BtnPlcReadModel = new System.Windows.Forms.Button();
            this.LblPlcAddCustomNum = new System.Windows.Forms.Label();
            this.LblModelAdd = new System.Windows.Forms.Label();
            this.LblCustomNumVal = new System.Windows.Forms.Label();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnTcpAbort = new System.Windows.Forms.Button();
            this.BtnTcpReady = new System.Windows.Forms.Button();
            this.BtnTcpCls = new System.Windows.Forms.Button();
            this.BtnTcpSetBarcode = new System.Windows.Forms.Button();
            this.BtnTcpFluxStart = new System.Windows.Forms.Button();
            this.LblTcpMeasTime = new System.Windows.Forms.Label();
            this.LblTcpMeasTimeVal = new System.Windows.Forms.Label();
            this.LblTcpServerIp = new System.Windows.Forms.Label();
            this.LblTcpIdleTimeVal = new System.Windows.Forms.Label();
            this.LblTcpIdleTime = new System.Windows.Forms.Label();
            this.LblDbId = new System.Windows.Forms.Label();
            this.LblDbIdVal = new System.Windows.Forms.Label();
            this.GbxMesData = new System.Windows.Forms.GroupBox();
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
            this.GbxPlcFlags = new System.Windows.Forms.GroupBox();
            this.BtnCustomAlm = new System.Windows.Forms.Button();
            this.LblPlcAddCustomAlm = new System.Windows.Forms.Label();
            this.BtnCustomFin = new System.Windows.Forms.Button();
            this.LblPlcAddCustomFin = new System.Windows.Forms.Label();
            this.BtnCustomReq = new System.Windows.Forms.Button();
            this.LblPlcAddCustomReq = new System.Windows.Forms.Label();
            this.SspMain.SuspendLayout();
            this.GbxPlc.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GbxMesData.SuspendLayout();
            this.GbxPlcFlags.SuspendLayout();
            this.SuspendLayout();
            // 
            // SspMain
            // 
            this.SspMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.SspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslLoadLog,
            this.TsslLoadSettings});
            this.SspMain.Location = new System.Drawing.Point(0, 737);
            this.SspMain.Name = "SspMain";
            this.SspMain.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.SspMain.Size = new System.Drawing.Size(1376, 24);
            this.SspMain.TabIndex = 8;
            this.SspMain.Text = "statusStrip1";
            // 
            // TsslLoadLog
            // 
            this.TsslLoadLog.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TsslLoadLog.Name = "TsslLoadLog";
            this.TsslLoadLog.Size = new System.Drawing.Size(31, 19);
            this.TsslLoadLog.Text = "Log";
            // 
            // TsslLoadSettings
            // 
            this.TsslLoadSettings.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TsslLoadSettings.Name = "TsslLoadSettings";
            this.TsslLoadSettings.Size = new System.Drawing.Size(73, 19);
            this.TsslLoadSettings.Text = "TsslSettings";
            // 
            // BtnPlcStart
            // 
            this.BtnPlcStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlcStart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPlcStart.Location = new System.Drawing.Point(1256, 536);
            this.BtnPlcStart.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPlcStart.Name = "BtnPlcStart";
            this.BtnPlcStart.Size = new System.Drawing.Size(104, 24);
            this.BtnPlcStart.TabIndex = 9;
            this.BtnPlcStart.Text = "PLC Start";
            this.BtnPlcStart.UseVisualStyleBackColor = true;
            this.BtnPlcStart.Click += new System.EventHandler(this.BtnWorkerStart_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnSave.Location = new System.Drawing.Point(1256, 512);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(104, 24);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnReload
            // 
            this.BtnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReload.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnReload.Location = new System.Drawing.Point(1048, 512);
            this.BtnReload.Margin = new System.Windows.Forms.Padding(2);
            this.BtnReload.Name = "BtnReload";
            this.BtnReload.Size = new System.Drawing.Size(104, 24);
            this.BtnReload.TabIndex = 9;
            this.BtnReload.Text = "Re-load";
            this.BtnReload.UseVisualStyleBackColor = true;
            this.BtnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // PpgSettings
            // 
            this.PpgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PpgSettings.Location = new System.Drawing.Point(944, 16);
            this.PpgSettings.Margin = new System.Windows.Forms.Padding(2);
            this.PpgSettings.Name = "PpgSettings";
            this.PpgSettings.Size = new System.Drawing.Size(416, 464);
            this.PpgSettings.TabIndex = 14;
            // 
            // LvwDb
            // 
            this.LvwDb.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.LvwDb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LvwDb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhAdd,
            this.ClhName,
            this.ClhDbVal,
            this.ClhMesVal});
            this.LvwDb.GridLines = true;
            this.LvwDb.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LvwDb.HideSelection = false;
            this.LvwDb.Location = new System.Drawing.Point(16, 80);
            this.LvwDb.Name = "LvwDb";
            this.LvwDb.Size = new System.Drawing.Size(336, 440);
            this.LvwDb.TabIndex = 15;
            this.LvwDb.UseCompatibleStateImageBehavior = false;
            this.LvwDb.View = System.Windows.Forms.View.Details;
            // 
            // clhAdd
            // 
            this.clhAdd.Text = "Add.";
            this.clhAdd.Width = 49;
            // 
            // ClhName
            // 
            this.ClhName.Text = "Name";
            this.ClhName.Width = 108;
            // 
            // ClhDbVal
            // 
            this.ClhDbVal.Text = "Data";
            this.ClhDbVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ClhDbVal.Width = 86;
            // 
            // ClhMesVal
            // 
            this.ClhMesVal.Text = "MES";
            this.ClhMesVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ClhMesVal.Width = 86;
            // 
            // TmrSmaCheck
            // 
            this.TmrSmaCheck.Tick += new System.EventHandler(this.TmrSmaCheck_Tick);
            // 
            // LblPlcCon
            // 
            this.LblPlcCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcCon.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPlcCon.Location = new System.Drawing.Point(16, 24);
            this.LblPlcCon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcCon.Name = "LblPlcCon";
            this.LblPlcCon.Size = new System.Drawing.Size(336, 24);
            this.LblPlcCon.TabIndex = 16;
            this.LblPlcCon.Text = "PLC Connection";
            this.LblPlcCon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpServer
            // 
            this.LblTcpServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpServer.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpServer.Location = new System.Drawing.Point(16, 24);
            this.LblTcpServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpServer.Name = "LblTcpServer";
            this.LblTcpServer.Size = new System.Drawing.Size(336, 24);
            this.LblTcpServer.TabIndex = 16;
            this.LblTcpServer.Text = "TCP Server";
            this.LblTcpServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblSmaApp
            // 
            this.LblSmaApp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblSmaApp.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblSmaApp.Location = new System.Drawing.Point(240, 152);
            this.LblSmaApp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblSmaApp.Name = "LblSmaApp";
            this.LblSmaApp.Size = new System.Drawing.Size(112, 24);
            this.LblSmaApp.TabIndex = 16;
            this.LblSmaApp.Text = "SMA APP";
            this.LblSmaApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeat
            // 
            this.LblHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeat.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblHeartBeat.Location = new System.Drawing.Point(16, 96);
            this.LblHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeat.Name = "LblHeartBeat";
            this.LblHeartBeat.Size = new System.Drawing.Size(168, 24);
            this.LblHeartBeat.TabIndex = 16;
            this.LblHeartBeat.Text = "HEARTBEAT";
            this.LblHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GbxPlc
            // 
            this.GbxPlc.Controls.Add(this.LblPlcAddHeartBeat);
            this.GbxPlc.Controls.Add(this.LblHeartBeat);
            this.GbxPlc.Controls.Add(this.LvwProInfor);
            this.GbxPlc.Controls.Add(this.LblPlcCon);
            this.GbxPlc.Controls.Add(this.LblHeartBeatInterval);
            this.GbxPlc.Controls.Add(this.label3);
            this.GbxPlc.Controls.Add(this.LblModelNumber);
            this.GbxPlc.Controls.Add(this.LblMeasTime);
            this.GbxPlc.Controls.Add(this.LblMeasTimeVal);
            this.GbxPlc.Controls.Add(this.BtnLblReadProInf);
            this.GbxPlc.Controls.Add(this.BtnReadCustomNum);
            this.GbxPlc.Controls.Add(this.BtnPlcReadModel);
            this.GbxPlc.Controls.Add(this.LblPlcAddCustomNum);
            this.GbxPlc.Controls.Add(this.LblModelAdd);
            this.GbxPlc.Controls.Add(this.LblCustomNumVal);
            this.GbxPlc.Controls.Add(this.LblModelNumVal);
            this.GbxPlc.Controls.Add(this.LblHeartBeatIntVal);
            this.GbxPlc.Location = new System.Drawing.Point(16, 192);
            this.GbxPlc.Margin = new System.Windows.Forms.Padding(2);
            this.GbxPlc.Name = "GbxPlc";
            this.GbxPlc.Padding = new System.Windows.Forms.Padding(2);
            this.GbxPlc.Size = new System.Drawing.Size(360, 376);
            this.GbxPlc.TabIndex = 18;
            this.GbxPlc.TabStop = false;
            this.GbxPlc.Text = "PLC";
            // 
            // LblPlcAddHeartBeat
            // 
            this.LblPlcAddHeartBeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddHeartBeat.Location = new System.Drawing.Point(184, 96);
            this.LblPlcAddHeartBeat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddHeartBeat.Name = "LblPlcAddHeartBeat";
            this.LblPlcAddHeartBeat.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddHeartBeat.TabIndex = 16;
            this.LblPlcAddHeartBeat.Text = "-";
            this.LblPlcAddHeartBeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LvwProInfor
            // 
            this.LvwProInfor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.LvwProInfor.HideSelection = false;
            this.LvwProInfor.Location = new System.Drawing.Point(16, 208);
            this.LvwProInfor.Name = "LvwProInfor";
            this.LvwProInfor.Size = new System.Drawing.Size(336, 104);
            this.LvwProInfor.TabIndex = 17;
            this.LvwProInfor.UseCompatibleStateImageBehavior = false;
            this.LvwProInfor.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NAME";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ADD";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "VAL";
            this.columnHeader3.Width = 183;
            // 
            // LblHeartBeatInterval
            // 
            this.LblHeartBeatInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeatInterval.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblHeartBeatInterval.Location = new System.Drawing.Point(16, 48);
            this.LblHeartBeatInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeatInterval.Name = "LblHeartBeatInterval";
            this.LblHeartBeatInterval.Size = new System.Drawing.Size(168, 24);
            this.LblHeartBeatInterval.TabIndex = 20;
            this.LblHeartBeatInterval.Text = "Update";
            this.LblHeartBeatInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(16, 344);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "Custom number";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblModelNumber
            // 
            this.LblModelNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumber.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblModelNumber.Location = new System.Drawing.Point(16, 152);
            this.LblModelNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumber.Name = "LblModelNumber";
            this.LblModelNumber.Size = new System.Drawing.Size(168, 24);
            this.LblModelNumber.TabIndex = 20;
            this.LblModelNumber.Text = "Model number";
            this.LblModelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMeasTime
            // 
            this.LblMeasTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMeasTime.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblMeasTime.Location = new System.Drawing.Point(16, 72);
            this.LblMeasTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasTime.Name = "LblMeasTime";
            this.LblMeasTime.Size = new System.Drawing.Size(168, 24);
            this.LblMeasTime.TabIndex = 20;
            this.LblMeasTime.Text = "TOTAL MEAS. TIME";
            this.LblMeasTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMeasTimeVal
            // 
            this.LblMeasTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMeasTimeVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblMeasTimeVal.Location = new System.Drawing.Point(184, 72);
            this.LblMeasTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMeasTimeVal.Name = "LblMeasTimeVal";
            this.LblMeasTimeVal.Size = new System.Drawing.Size(168, 24);
            this.LblMeasTimeVal.TabIndex = 19;
            this.LblMeasTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnLblReadProInf
            // 
            this.BtnLblReadProInf.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnLblReadProInf.Location = new System.Drawing.Point(16, 184);
            this.BtnLblReadProInf.Margin = new System.Windows.Forms.Padding(2);
            this.BtnLblReadProInf.Name = "BtnLblReadProInf";
            this.BtnLblReadProInf.Size = new System.Drawing.Size(336, 24);
            this.BtnLblReadProInf.TabIndex = 17;
            this.BtnLblReadProInf.Text = "Read product infor";
            this.BtnLblReadProInf.UseVisualStyleBackColor = true;
            this.BtnLblReadProInf.Click += new System.EventHandler(this.BtnLblReadProdInfor_Click);
            // 
            // BtnReadCustomNum
            // 
            this.BtnReadCustomNum.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnReadCustomNum.Location = new System.Drawing.Point(16, 320);
            this.BtnReadCustomNum.Margin = new System.Windows.Forms.Padding(2);
            this.BtnReadCustomNum.Name = "BtnReadCustomNum";
            this.BtnReadCustomNum.Size = new System.Drawing.Size(168, 24);
            this.BtnReadCustomNum.TabIndex = 17;
            this.BtnReadCustomNum.Text = "Read custom";
            this.BtnReadCustomNum.UseVisualStyleBackColor = true;
            this.BtnReadCustomNum.Click += new System.EventHandler(this.BtnReadCustomNum_Click);
            // 
            // BtnPlcReadModel
            // 
            this.BtnPlcReadModel.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPlcReadModel.Location = new System.Drawing.Point(16, 128);
            this.BtnPlcReadModel.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPlcReadModel.Name = "BtnPlcReadModel";
            this.BtnPlcReadModel.Size = new System.Drawing.Size(168, 24);
            this.BtnPlcReadModel.TabIndex = 17;
            this.BtnPlcReadModel.Text = "Read model";
            this.BtnPlcReadModel.UseVisualStyleBackColor = true;
            this.BtnPlcReadModel.Click += new System.EventHandler(this.BtnPlcReadModel_Click);
            // 
            // LblPlcAddCustomNum
            // 
            this.LblPlcAddCustomNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddCustomNum.Location = new System.Drawing.Point(184, 320);
            this.LblPlcAddCustomNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddCustomNum.Name = "LblPlcAddCustomNum";
            this.LblPlcAddCustomNum.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddCustomNum.TabIndex = 16;
            this.LblPlcAddCustomNum.Text = "-";
            this.LblPlcAddCustomNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblModelAdd
            // 
            this.LblModelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelAdd.Location = new System.Drawing.Point(184, 128);
            this.LblModelAdd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelAdd.Name = "LblModelAdd";
            this.LblModelAdd.Size = new System.Drawing.Size(168, 24);
            this.LblModelAdd.TabIndex = 16;
            this.LblModelAdd.Text = "-";
            this.LblModelAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblCustomNumVal
            // 
            this.LblCustomNumVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblCustomNumVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblCustomNumVal.Location = new System.Drawing.Point(184, 344);
            this.LblCustomNumVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblCustomNumVal.Name = "LblCustomNumVal";
            this.LblCustomNumVal.Size = new System.Drawing.Size(168, 24);
            this.LblCustomNumVal.TabIndex = 16;
            this.LblCustomNumVal.Text = "0";
            this.LblCustomNumVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblModelNumVal
            // 
            this.LblModelNumVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblModelNumVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblModelNumVal.Location = new System.Drawing.Point(184, 152);
            this.LblModelNumVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblModelNumVal.Name = "LblModelNumVal";
            this.LblModelNumVal.Size = new System.Drawing.Size(168, 24);
            this.LblModelNumVal.TabIndex = 16;
            this.LblModelNumVal.Text = "0";
            this.LblModelNumVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblHeartBeatIntVal
            // 
            this.LblHeartBeatIntVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblHeartBeatIntVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblHeartBeatIntVal.Location = new System.Drawing.Point(184, 48);
            this.LblHeartBeatIntVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblHeartBeatIntVal.Name = "LblHeartBeatIntVal";
            this.LblHeartBeatIntVal.Size = new System.Drawing.Size(168, 24);
            this.LblHeartBeatIntVal.TabIndex = 16;
            this.LblHeartBeatIntVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnMesDataUpload
            // 
            this.BtnMesDataUpload.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnMesDataUpload.Location = new System.Drawing.Point(16, 528);
            this.BtnMesDataUpload.Name = "BtnMesDataUpload";
            this.BtnMesDataUpload.Size = new System.Drawing.Size(336, 24);
            this.BtnMesDataUpload.TabIndex = 21;
            this.BtnMesDataUpload.Text = "MES Upload";
            this.BtnMesDataUpload.UseVisualStyleBackColor = true;
            this.BtnMesDataUpload.Click += new System.EventHandler(this.BtnMesDataUpload_Click);
            // 
            // BtnAlarm
            // 
            this.BtnAlarm.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnAlarm.Location = new System.Drawing.Point(8, 288);
            this.BtnAlarm.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAlarm.Name = "BtnAlarm";
            this.BtnAlarm.Size = new System.Drawing.Size(168, 24);
            this.BtnAlarm.TabIndex = 17;
            this.BtnAlarm.Text = "ALARM";
            this.BtnAlarm.UseVisualStyleBackColor = true;
            this.BtnAlarm.Click += new System.EventHandler(this.BtnAlarm_Click);
            // 
            // BtnNg
            // 
            this.BtnNg.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnNg.Location = new System.Drawing.Point(8, 240);
            this.BtnNg.Margin = new System.Windows.Forms.Padding(2);
            this.BtnNg.Name = "BtnNg";
            this.BtnNg.Size = new System.Drawing.Size(168, 24);
            this.BtnNg.TabIndex = 17;
            this.BtnNg.Text = "NG";
            this.BtnNg.UseVisualStyleBackColor = true;
            this.BtnNg.Click += new System.EventHandler(this.BtnNg_Click);
            // 
            // BtnBusy
            // 
            this.BtnBusy.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnBusy.Location = new System.Drawing.Point(8, 24);
            this.BtnBusy.Margin = new System.Windows.Forms.Padding(2);
            this.BtnBusy.Name = "BtnBusy";
            this.BtnBusy.Size = new System.Drawing.Size(168, 24);
            this.BtnBusy.TabIndex = 17;
            this.BtnBusy.Text = "BUSY";
            this.BtnBusy.UseVisualStyleBackColor = true;
            this.BtnBusy.Click += new System.EventHandler(this.BtnBusy_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnOk.Location = new System.Drawing.Point(8, 192);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(2);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(168, 24);
            this.BtnOk.TabIndex = 17;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnMeasFin
            // 
            this.BtnMeasFin.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnMeasFin.Location = new System.Drawing.Point(8, 336);
            this.BtnMeasFin.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasFin.Name = "BtnMeasFin";
            this.BtnMeasFin.Size = new System.Drawing.Size(168, 24);
            this.BtnMeasFin.TabIndex = 17;
            this.BtnMeasFin.Text = "MEAS FIN.";
            this.BtnMeasFin.UseVisualStyleBackColor = true;
            this.BtnMeasFin.Click += new System.EventHandler(this.BtnMeasFin_Click);
            // 
            // BtnMeasReqResp
            // 
            this.BtnMeasReqResp.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnMeasReqResp.Location = new System.Drawing.Point(8, 144);
            this.BtnMeasReqResp.Margin = new System.Windows.Forms.Padding(2);
            this.BtnMeasReqResp.Name = "BtnMeasReqResp";
            this.BtnMeasReqResp.Size = new System.Drawing.Size(168, 24);
            this.BtnMeasReqResp.TabIndex = 17;
            this.BtnMeasReqResp.Text = "MEAS RESP.";
            this.BtnMeasReqResp.UseVisualStyleBackColor = true;
            this.BtnMeasReqResp.Click += new System.EventHandler(this.BtnMeasReqResp_Click);
            // 
            // BtnPlcMeasReq
            // 
            this.BtnPlcMeasReq.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPlcMeasReq.Location = new System.Drawing.Point(8, 96);
            this.BtnPlcMeasReq.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPlcMeasReq.Name = "BtnPlcMeasReq";
            this.BtnPlcMeasReq.Size = new System.Drawing.Size(168, 24);
            this.BtnPlcMeasReq.TabIndex = 17;
            this.BtnPlcMeasReq.Text = "MEAS REQ.";
            this.BtnPlcMeasReq.UseVisualStyleBackColor = true;
            this.BtnPlcMeasReq.Click += new System.EventHandler(this.BtnPlcMeasReq_Click);
            // 
            // LblPlcAddAlarm
            // 
            this.LblPlcAddAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddAlarm.Location = new System.Drawing.Point(8, 312);
            this.LblPlcAddAlarm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddAlarm.Name = "LblPlcAddAlarm";
            this.LblPlcAddAlarm.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddAlarm.TabIndex = 16;
            this.LblPlcAddAlarm.Text = "-";
            this.LblPlcAddAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddNg
            // 
            this.LblPlcAddNg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddNg.Location = new System.Drawing.Point(8, 264);
            this.LblPlcAddNg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddNg.Name = "LblPlcAddNg";
            this.LblPlcAddNg.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddNg.TabIndex = 16;
            this.LblPlcAddNg.Text = "-";
            this.LblPlcAddNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddBusy
            // 
            this.LblPlcAddBusy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddBusy.Location = new System.Drawing.Point(8, 48);
            this.LblPlcAddBusy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddBusy.Name = "LblPlcAddBusy";
            this.LblPlcAddBusy.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddBusy.TabIndex = 16;
            this.LblPlcAddBusy.Text = "-";
            this.LblPlcAddBusy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasFin
            // 
            this.LblPlcAddMeasFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasFin.Location = new System.Drawing.Point(8, 360);
            this.LblPlcAddMeasFin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasFin.Name = "LblPlcAddMeasFin";
            this.LblPlcAddMeasFin.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddMeasFin.TabIndex = 16;
            this.LblPlcAddMeasFin.Text = "-";
            this.LblPlcAddMeasFin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddOk
            // 
            this.LblPlcAddOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddOk.Location = new System.Drawing.Point(8, 216);
            this.LblPlcAddOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddOk.Name = "LblPlcAddOk";
            this.LblPlcAddOk.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddOk.TabIndex = 16;
            this.LblPlcAddOk.Text = "-";
            this.LblPlcAddOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasReqResp
            // 
            this.LblPlcAddMeasReqResp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasReqResp.Location = new System.Drawing.Point(8, 168);
            this.LblPlcAddMeasReqResp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasReqResp.Name = "LblPlcAddMeasReqResp";
            this.LblPlcAddMeasReqResp.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddMeasReqResp.TabIndex = 16;
            this.LblPlcAddMeasReqResp.Text = "-";
            this.LblPlcAddMeasReqResp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAddMeasReq
            // 
            this.LblPlcAddMeasReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddMeasReq.Location = new System.Drawing.Point(8, 120);
            this.LblPlcAddMeasReq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddMeasReq.Name = "LblPlcAddMeasReq";
            this.LblPlcAddMeasReq.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddMeasReq.TabIndex = 16;
            this.LblPlcAddMeasReq.Text = "-";
            this.LblPlcAddMeasReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblPlcAlmCode
            // 
            this.LblPlcAlmCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPlcAlmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAlmCode.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblPlcAlmCode.Location = new System.Drawing.Point(944, 488);
            this.LblPlcAlmCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAlmCode.Name = "LblPlcAlmCode";
            this.LblPlcAlmCode.Size = new System.Drawing.Size(208, 24);
            this.LblPlcAlmCode.TabIndex = 16;
            this.LblPlcAlmCode.Text = "PLC ALARM";
            this.LblPlcAlmCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblPlcAlmCode.Click += new System.EventHandler(this.LblPlcAlmCode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnTcpAbort);
            this.groupBox2.Controls.Add(this.LblSmaApp);
            this.groupBox2.Controls.Add(this.BtnTcpReady);
            this.groupBox2.Controls.Add(this.BtnTcpCls);
            this.groupBox2.Controls.Add(this.BtnTcpSetBarcode);
            this.groupBox2.Controls.Add(this.BtnTcpFluxStart);
            this.groupBox2.Controls.Add(this.LblTcpServer);
            this.groupBox2.Controls.Add(this.LblTcpMeasTime);
            this.groupBox2.Controls.Add(this.LblTcpMeasTimeVal);
            this.groupBox2.Controls.Add(this.LblTcpServerIp);
            this.groupBox2.Controls.Add(this.LblTcpIdleTimeVal);
            this.groupBox2.Controls.Add(this.LblTcpIdleTime);
            this.groupBox2.Location = new System.Drawing.Point(16, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(360, 184);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SMA TCP COMM";
            // 
            // BtnTcpAbort
            // 
            this.BtnTcpAbort.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpAbort.Location = new System.Drawing.Point(128, 152);
            this.BtnTcpAbort.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpAbort.Name = "BtnTcpAbort";
            this.BtnTcpAbort.Size = new System.Drawing.Size(112, 24);
            this.BtnTcpAbort.TabIndex = 17;
            this.BtnTcpAbort.Text = "Abort";
            this.BtnTcpAbort.UseVisualStyleBackColor = true;
            this.BtnTcpAbort.Click += new System.EventHandler(this.BtnTcpAbort_Click);
            // 
            // BtnTcpReady
            // 
            this.BtnTcpReady.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpReady.Location = new System.Drawing.Point(240, 128);
            this.BtnTcpReady.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpReady.Name = "BtnTcpReady";
            this.BtnTcpReady.Size = new System.Drawing.Size(112, 24);
            this.BtnTcpReady.TabIndex = 17;
            this.BtnTcpReady.Text = "Ready?";
            this.BtnTcpReady.UseVisualStyleBackColor = true;
            this.BtnTcpReady.Click += new System.EventHandler(this.BtnTcpReady_Click);
            // 
            // BtnTcpCls
            // 
            this.BtnTcpCls.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpCls.Location = new System.Drawing.Point(128, 128);
            this.BtnTcpCls.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpCls.Name = "BtnTcpCls";
            this.BtnTcpCls.Size = new System.Drawing.Size(112, 24);
            this.BtnTcpCls.TabIndex = 17;
            this.BtnTcpCls.Text = "*CLS";
            this.BtnTcpCls.UseVisualStyleBackColor = true;
            this.BtnTcpCls.Click += new System.EventHandler(this.BtnTcpCls_Click);
            // 
            // BtnTcpSetBarcode
            // 
            this.BtnTcpSetBarcode.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpSetBarcode.Location = new System.Drawing.Point(16, 152);
            this.BtnTcpSetBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpSetBarcode.Name = "BtnTcpSetBarcode";
            this.BtnTcpSetBarcode.Size = new System.Drawing.Size(112, 24);
            this.BtnTcpSetBarcode.TabIndex = 17;
            this.BtnTcpSetBarcode.Text = "SEND BC";
            this.BtnTcpSetBarcode.UseVisualStyleBackColor = true;
            this.BtnTcpSetBarcode.Click += new System.EventHandler(this.BtnTcpSetBarcode_Click);
            // 
            // BtnTcpFluxStart
            // 
            this.BtnTcpFluxStart.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnTcpFluxStart.Location = new System.Drawing.Point(16, 128);
            this.BtnTcpFluxStart.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpFluxStart.Name = "BtnTcpFluxStart";
            this.BtnTcpFluxStart.Size = new System.Drawing.Size(112, 24);
            this.BtnTcpFluxStart.TabIndex = 17;
            this.BtnTcpFluxStart.Text = "FLUX";
            this.BtnTcpFluxStart.UseVisualStyleBackColor = true;
            this.BtnTcpFluxStart.Click += new System.EventHandler(this.BtnTcpFluxStart_Click);
            // 
            // LblTcpMeasTime
            // 
            this.LblTcpMeasTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpMeasTime.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpMeasTime.Location = new System.Drawing.Point(16, 96);
            this.LblTcpMeasTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpMeasTime.Name = "LblTcpMeasTime";
            this.LblTcpMeasTime.Size = new System.Drawing.Size(168, 24);
            this.LblTcpMeasTime.TabIndex = 20;
            this.LblTcpMeasTime.Text = "SMA MEAS. TIME";
            this.LblTcpMeasTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpMeasTimeVal
            // 
            this.LblTcpMeasTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpMeasTimeVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpMeasTimeVal.Location = new System.Drawing.Point(184, 96);
            this.LblTcpMeasTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpMeasTimeVal.Name = "LblTcpMeasTimeVal";
            this.LblTcpMeasTimeVal.Size = new System.Drawing.Size(168, 24);
            this.LblTcpMeasTimeVal.TabIndex = 19;
            this.LblTcpMeasTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTcpServerIp
            // 
            this.LblTcpServerIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpServerIp.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpServerIp.Location = new System.Drawing.Point(16, 48);
            this.LblTcpServerIp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpServerIp.Name = "LblTcpServerIp";
            this.LblTcpServerIp.Size = new System.Drawing.Size(336, 24);
            this.LblTcpServerIp.TabIndex = 16;
            this.LblTcpServerIp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTcpIdleTimeVal
            // 
            this.LblTcpIdleTimeVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpIdleTimeVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpIdleTimeVal.Location = new System.Drawing.Point(184, 72);
            this.LblTcpIdleTimeVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpIdleTimeVal.Name = "LblTcpIdleTimeVal";
            this.LblTcpIdleTimeVal.Size = new System.Drawing.Size(168, 24);
            this.LblTcpIdleTimeVal.TabIndex = 16;
            this.LblTcpIdleTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblTcpIdleTime
            // 
            this.LblTcpIdleTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpIdleTime.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpIdleTime.Location = new System.Drawing.Point(16, 72);
            this.LblTcpIdleTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpIdleTime.Name = "LblTcpIdleTime";
            this.LblTcpIdleTime.Size = new System.Drawing.Size(168, 24);
            this.LblTcpIdleTime.TabIndex = 16;
            this.LblTcpIdleTime.Text = "TCP IDLE TIME";
            this.LblTcpIdleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDbId
            // 
            this.LblDbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDbId.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblDbId.Location = new System.Drawing.Point(240, 24);
            this.LblDbId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDbId.Name = "LblDbId";
            this.LblDbId.Size = new System.Drawing.Size(112, 24);
            this.LblDbId.TabIndex = 16;
            this.LblDbId.Text = "DB ID";
            this.LblDbId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDbIdVal
            // 
            this.LblDbIdVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDbIdVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblDbIdVal.Location = new System.Drawing.Point(240, 48);
            this.LblDbIdVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblDbIdVal.Name = "LblDbIdVal";
            this.LblDbIdVal.Size = new System.Drawing.Size(112, 24);
            this.LblDbIdVal.TabIndex = 16;
            this.LblDbIdVal.Text = "-";
            this.LblDbIdVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GbxMesData
            // 
            this.GbxMesData.Controls.Add(this.BtnMesDataUpload);
            this.GbxMesData.Controls.Add(this.LblMesOkVal);
            this.GbxMesData.Controls.Add(this.LblFailedIdVal);
            this.GbxMesData.Controls.Add(this.LblFailedId);
            this.GbxMesData.Controls.Add(this.LblMesOk);
            this.GbxMesData.Controls.Add(this.LblDbId);
            this.GbxMesData.Controls.Add(this.LblDbIdVal);
            this.GbxMesData.Controls.Add(this.LvwDb);
            this.GbxMesData.Location = new System.Drawing.Point(384, 8);
            this.GbxMesData.Margin = new System.Windows.Forms.Padding(2);
            this.GbxMesData.Name = "GbxMesData";
            this.GbxMesData.Padding = new System.Windows.Forms.Padding(2);
            this.GbxMesData.Size = new System.Drawing.Size(360, 560);
            this.GbxMesData.TabIndex = 19;
            this.GbxMesData.TabStop = false;
            this.GbxMesData.Text = "MES";
            // 
            // LblMesOkVal
            // 
            this.LblMesOkVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMesOkVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblMesOkVal.Location = new System.Drawing.Point(16, 48);
            this.LblMesOkVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMesOkVal.Name = "LblMesOkVal";
            this.LblMesOkVal.Size = new System.Drawing.Size(112, 24);
            this.LblMesOkVal.TabIndex = 16;
            this.LblMesOkVal.Text = "-";
            this.LblMesOkVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblFailedIdVal
            // 
            this.LblFailedIdVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblFailedIdVal.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblFailedIdVal.Location = new System.Drawing.Point(128, 48);
            this.LblFailedIdVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblFailedIdVal.Name = "LblFailedIdVal";
            this.LblFailedIdVal.Size = new System.Drawing.Size(112, 24);
            this.LblFailedIdVal.TabIndex = 16;
            this.LblFailedIdVal.Text = "-";
            this.LblFailedIdVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblFailedId
            // 
            this.LblFailedId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblFailedId.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblFailedId.Location = new System.Drawing.Point(128, 24);
            this.LblFailedId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblFailedId.Name = "LblFailedId";
            this.LblFailedId.Size = new System.Drawing.Size(112, 24);
            this.LblFailedId.TabIndex = 16;
            this.LblFailedId.Text = "ERR. CODE";
            this.LblFailedId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMesOk
            // 
            this.LblMesOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMesOk.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblMesOk.Location = new System.Drawing.Point(16, 24);
            this.LblMesOk.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblMesOk.Name = "LblMesOk";
            this.LblMesOk.Size = new System.Drawing.Size(112, 24);
            this.LblMesOk.TabIndex = 16;
            this.LblMesOk.Text = "OK";
            this.LblMesOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnFolderLog
            // 
            this.BtnFolderLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFolderLog.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnFolderLog.Location = new System.Drawing.Point(944, 512);
            this.BtnFolderLog.Margin = new System.Windows.Forms.Padding(2);
            this.BtnFolderLog.Name = "BtnFolderLog";
            this.BtnFolderLog.Size = new System.Drawing.Size(104, 24);
            this.BtnFolderLog.TabIndex = 17;
            this.BtnFolderLog.Text = "Log folder";
            this.BtnFolderLog.UseVisualStyleBackColor = true;
            this.BtnFolderLog.Click += new System.EventHandler(this.BtnFolderLog_Click);
            // 
            // BtnFolderDb
            // 
            this.BtnFolderDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFolderDb.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnFolderDb.Location = new System.Drawing.Point(1152, 512);
            this.BtnFolderDb.Margin = new System.Windows.Forms.Padding(2);
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
            this.BtnFolderSettings.Location = new System.Drawing.Point(944, 536);
            this.BtnFolderSettings.Margin = new System.Windows.Forms.Padding(2);
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
            this.BtnTcpStart.Location = new System.Drawing.Point(1152, 536);
            this.BtnTcpStart.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTcpStart.Name = "BtnTcpStart";
            this.BtnTcpStart.Size = new System.Drawing.Size(104, 24);
            this.BtnTcpStart.TabIndex = 9;
            this.BtnTcpStart.Text = "TCP Start";
            this.BtnTcpStart.UseVisualStyleBackColor = true;
            this.BtnTcpStart.Click += new System.EventHandler(this.BtnTcpStart_Click);
            // 
            // TbxLog
            // 
            this.TbxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxLog.Location = new System.Drawing.Point(16, 576);
            this.TbxLog.Multiline = true;
            this.TbxLog.Name = "TbxLog";
            this.TbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbxLog.Size = new System.Drawing.Size(1344, 153);
            this.TbxLog.TabIndex = 22;
            // 
            // BtnClearLog
            // 
            this.BtnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnClearLog.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnClearLog.Location = new System.Drawing.Point(1048, 536);
            this.BtnClearLog.Margin = new System.Windows.Forms.Padding(2);
            this.BtnClearLog.Name = "BtnClearLog";
            this.BtnClearLog.Size = new System.Drawing.Size(104, 24);
            this.BtnClearLog.TabIndex = 9;
            this.BtnClearLog.Text = "LOG CLR";
            this.BtnClearLog.UseVisualStyleBackColor = true;
            this.BtnClearLog.Click += new System.EventHandler(this.BtnClearLog_Click);
            // 
            // LblTcpAlmCode
            // 
            this.LblTcpAlmCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTcpAlmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTcpAlmCode.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LblTcpAlmCode.Location = new System.Drawing.Point(1152, 488);
            this.LblTcpAlmCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTcpAlmCode.Name = "LblTcpAlmCode";
            this.LblTcpAlmCode.Size = new System.Drawing.Size(208, 24);
            this.LblTcpAlmCode.TabIndex = 16;
            this.LblTcpAlmCode.Text = "TCP ALARM";
            this.LblTcpAlmCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblTcpAlmCode.Click += new System.EventHandler(this.LblTcpAlmCode_Click);
            // 
            // GbxPlcFlags
            // 
            this.GbxPlcFlags.Controls.Add(this.BtnCustomAlm);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddCustomAlm);
            this.GbxPlcFlags.Controls.Add(this.BtnPlcMeasReq);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddMeasReq);
            this.GbxPlcFlags.Controls.Add(this.BtnMeasReqResp);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddMeasReqResp);
            this.GbxPlcFlags.Controls.Add(this.BtnCustomFin);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddCustomFin);
            this.GbxPlcFlags.Controls.Add(this.BtnMeasFin);
            this.GbxPlcFlags.Controls.Add(this.BtnAlarm);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddMeasFin);
            this.GbxPlcFlags.Controls.Add(this.BtnNg);
            this.GbxPlcFlags.Controls.Add(this.BtnBusy);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddBusy);
            this.GbxPlcFlags.Controls.Add(this.BtnOk);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddAlarm);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddOk);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddNg);
            this.GbxPlcFlags.Controls.Add(this.BtnCustomReq);
            this.GbxPlcFlags.Controls.Add(this.LblPlcAddCustomReq);
            this.GbxPlcFlags.Location = new System.Drawing.Point(752, 8);
            this.GbxPlcFlags.Name = "GbxPlcFlags";
            this.GbxPlcFlags.Size = new System.Drawing.Size(184, 560);
            this.GbxPlcFlags.TabIndex = 23;
            this.GbxPlcFlags.TabStop = false;
            this.GbxPlcFlags.Text = "Flags";
            // 
            // BtnCustomAlm
            // 
            this.BtnCustomAlm.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnCustomAlm.Location = new System.Drawing.Point(8, 504);
            this.BtnCustomAlm.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCustomAlm.Name = "BtnCustomAlm";
            this.BtnCustomAlm.Size = new System.Drawing.Size(168, 24);
            this.BtnCustomAlm.TabIndex = 19;
            this.BtnCustomAlm.Text = "CUSTOM ALARM";
            this.BtnCustomAlm.UseVisualStyleBackColor = true;
            this.BtnCustomAlm.Click += new System.EventHandler(this.BtnCustomAlm_Click);
            // 
            // LblPlcAddCustomAlm
            // 
            this.LblPlcAddCustomAlm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddCustomAlm.Location = new System.Drawing.Point(8, 528);
            this.LblPlcAddCustomAlm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddCustomAlm.Name = "LblPlcAddCustomAlm";
            this.LblPlcAddCustomAlm.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddCustomAlm.TabIndex = 18;
            this.LblPlcAddCustomAlm.Text = "-";
            this.LblPlcAddCustomAlm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnCustomFin
            // 
            this.BtnCustomFin.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnCustomFin.Location = new System.Drawing.Point(8, 456);
            this.BtnCustomFin.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCustomFin.Name = "BtnCustomFin";
            this.BtnCustomFin.Size = new System.Drawing.Size(168, 24);
            this.BtnCustomFin.TabIndex = 19;
            this.BtnCustomFin.Text = "CUSTOM FIN";
            this.BtnCustomFin.UseVisualStyleBackColor = true;
            this.BtnCustomFin.Click += new System.EventHandler(this.BtnCustomFin_Click);
            // 
            // LblPlcAddCustomFin
            // 
            this.LblPlcAddCustomFin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddCustomFin.Location = new System.Drawing.Point(8, 480);
            this.LblPlcAddCustomFin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddCustomFin.Name = "LblPlcAddCustomFin";
            this.LblPlcAddCustomFin.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddCustomFin.TabIndex = 18;
            this.LblPlcAddCustomFin.Text = "-";
            this.LblPlcAddCustomFin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnCustomReq
            // 
            this.BtnCustomReq.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnCustomReq.Location = new System.Drawing.Point(8, 408);
            this.BtnCustomReq.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCustomReq.Name = "BtnCustomReq";
            this.BtnCustomReq.Size = new System.Drawing.Size(168, 24);
            this.BtnCustomReq.TabIndex = 17;
            this.BtnCustomReq.Text = "CUSTOM REQ.";
            this.BtnCustomReq.UseVisualStyleBackColor = true;
            this.BtnCustomReq.Click += new System.EventHandler(this.BtnCustomReq_Click);
            // 
            // LblPlcAddCustomReq
            // 
            this.LblPlcAddCustomReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblPlcAddCustomReq.Location = new System.Drawing.Point(8, 432);
            this.LblPlcAddCustomReq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblPlcAddCustomReq.Name = "LblPlcAddCustomReq";
            this.LblPlcAddCustomReq.Size = new System.Drawing.Size(168, 24);
            this.LblPlcAddCustomReq.TabIndex = 16;
            this.LblPlcAddCustomReq.Text = "-";
            this.LblPlcAddCustomReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 761);
            this.Controls.Add(this.GbxPlcFlags);
            this.Controls.Add(this.LblTcpAlmCode);
            this.Controls.Add(this.LblPlcAlmCode);
            this.Controls.Add(this.TbxLog);
            this.Controls.Add(this.BtnFolderDb);
            this.Controls.Add(this.GbxPlc);
            this.Controls.Add(this.BtnFolderLog);
            this.Controls.Add(this.BtnFolderSettings);
            this.Controls.Add(this.GbxMesData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.PpgSettings);
            this.Controls.Add(this.BtnClearLog);
            this.Controls.Add(this.BtnReload);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnTcpStart);
            this.Controls.Add(this.BtnPlcStart);
            this.Controls.Add(this.SspMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainDlg";
            this.Text = "SMA-4K COMMUNICATION APP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainDlg_FormClosing);
            this.Load += new System.EventHandler(this.MainDlg_Load);
            this.SspMain.ResumeLayout(false);
            this.SspMain.PerformLayout();
            this.GbxPlc.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.GbxMesData.ResumeLayout(false);
            this.GbxPlcFlags.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip SspMain;
        private System.Windows.Forms.Button BtnPlcStart;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnReload;
        private System.Windows.Forms.PropertyGrid PpgSettings;
        private System.Windows.Forms.ListView LvwDb;
        private System.Windows.Forms.ColumnHeader ClhName;
        private System.Windows.Forms.ColumnHeader ClhDbVal;
        private System.Windows.Forms.ColumnHeader ClhMesVal;
        private System.Windows.Forms.ToolStripStatusLabel TsslLoadLog;
        private System.Windows.Forms.ToolStripStatusLabel TsslLoadSettings;
        private System.Windows.Forms.Timer TmrSmaCheck;
        private System.Windows.Forms.Label LblPlcCon;
        private System.Windows.Forms.Label LblTcpServer;
        private System.Windows.Forms.Label LblSmaApp;
        private System.Windows.Forms.Label LblHeartBeat;
        private System.Windows.Forms.GroupBox GbxPlc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LblModelNumVal;
        private System.Windows.Forms.Label LblDbIdVal;
        private System.Windows.Forms.Label LblDbId;
        private System.Windows.Forms.Label LblMeasTimeVal;
        private System.Windows.Forms.Label LblMeasTime;
        private System.Windows.Forms.Label LblTcpIdleTimeVal;
        private System.Windows.Forms.Label LblTcpIdleTime;
        private System.Windows.Forms.GroupBox GbxMesData;
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
        private System.Windows.Forms.Button BtnLblReadProInf;
        private System.Windows.Forms.Button BtnPlcReadModel;
        private System.Windows.Forms.Label LblModelAdd;
        private System.Windows.Forms.Button BtnTcpStart;
        private System.Windows.Forms.Label LblPlcAddBusy;
        private System.Windows.Forms.Button BtnTcpSetBarcode;
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
        private System.Windows.Forms.ListView LvwProInfor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label LblModelNumber;
        private System.Windows.Forms.Button BtnMesDataUpload;
        private System.Windows.Forms.ColumnHeader clhAdd;
        private System.Windows.Forms.Label LblTcpAlmCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnReadCustomNum;
        private System.Windows.Forms.Label LblPlcAddCustomNum;
        private System.Windows.Forms.Label LblCustomNumVal;
        private System.Windows.Forms.GroupBox GbxPlcFlags;
        private System.Windows.Forms.Button BtnCustomAlm;
        private System.Windows.Forms.Label LblPlcAddCustomAlm;
        private System.Windows.Forms.Button BtnCustomFin;
        private System.Windows.Forms.Label LblPlcAddCustomFin;
        private System.Windows.Forms.Button BtnCustomReq;
        private System.Windows.Forms.Label LblPlcAddCustomReq;
    }
}

