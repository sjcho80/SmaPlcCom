using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

using SmaPlc;

namespace PlcComDlg
{
    /// <summary>
    /// 통신설정 클래스
    /// </summary>
    [Serializable]
    public class ComSettings
    {
        #region PLC.Connection
        /// <summary>
        /// PLC 타입
        /// </summary>
        [Category("PLC.Connection")]
        [DisplayName("\tPlc type")]
        [Description("Plc type")]
        public PlcBase.PlcTypes PlcType { get; set; } = PlcBase.PlcTypes.SimensS7;

        /// <summary>
        /// PLC 연결 파라미터
        /// </summary>
        [Category("PLC.Connection")]
        [DisplayName("\tPlc connection parameters")]
        [Description("Plc connection parameters\r\n" +
            "Type 'help' or clear to print helps corresponding plc type")]
        public string PlcConnectionParam { get; set; } = "100.100.101.42, 12042, 0, 0";
        #endregion

        #region PLC.Address.Flag
        /// <summary>
        /// 측정요청 비트 주소
        /// </summary>
        [Category("PLC.Address.Flag.PlcToPc")]
        [DisplayName("Meas. request bit")]
        [Description("Measurement request bit address")]
        public string PlcAddMeasReqBit { get; set; } = "D10200.0";

        /// <summary>
        /// 측정요청 응답 비트
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Meas. request resp.")]
        [Description("Measurement request response bit address")]
        public string PlcAddMeasReqRespBit { get; set; } = "D10300.0";

        /// <summary>
        /// 하트비트 주소
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Heart beat")]
        [Description("Heartbeat bit address")]
        public string PlcAddHeartBeatBit { get; set; } = "D10300.1";

        /// <summary>
        /// 측정 완료 주소
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Meas. finihsed")]
        [Description("Measurement finish bit address")]
        public string PlcAddMeasFinBit { get; set; } = "D10300.2";

        /// <summary>
        /// OK 비트
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Ok")]
        [Description("Ok bit address")]
        public string PlcAddOkBit { get; set; } = "D10300.3";

        /// <summary>
        /// NG 비트
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Ng")]
        [Description("NG bit address")]
        public string PlcAddNgBit { get; set; } = "D10300.4";

        /// <summary>
        /// BUSY 비트
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Busy")]
        [Description("Set during measurement")]
        public string PlcAddBusyBit { get; set; } = "D10300.5";

        /// <summary>
        /// 알람 비트
        /// </summary>
        [Category("PLC.Address.Flag.PcToPlc")]
        [DisplayName("Alarm")]
        [Description("Program alarm")]
        public string PlcAddAlarmBit { get; set; } = "D10300.6";
        #endregion

        #region PLC.Address.Infor
        /// <summary>
        /// 제품 정보
        /// </summary>
        [Serializable]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ProductInfor
        {
            /// <summary>
            /// 제품 데이터 종류
            /// </summary>
            public enum DataTypes
            {
                /// <summary>
                /// Text 데이터
                /// </summary>
                Text,
                /// <summary>
                /// Word 정수 데이터
                /// </summary>
                Word,
                /// <summary>
                /// Double word 정수 데이터 타입
                /// </summary>
                DWord,
            }

            /// <summary>
            /// 이름
            /// </summary>
            [Category("Product information")]
            [DisplayName("Name")]
            [Description("Name")]
            public string Name { get; set; }

            /// <summary>
            /// 시작 주소
            /// </summary>
            [Category("Product information")]
            [DisplayName("Data Start address")]
            [Description("Data start address\r\n" +
                "For text, use byte address\r\n" +
                "For integer, use word address\r\n")]
            public string DataStartAddress { get; set; } = "DB220.DW123";

            /// <summary>
            /// 시작 주소
            /// </summary>
            [Category("Product information")]
            [DisplayName("Data length")]
            [Description("Data length")]
            public int DataLength { get; set; } = 20;

            [Category("Product information")]
            [DisplayName("Custom field name")]
            [Description("Custom field name in data base")]
            public string CustomFieldName { get; set; } = "custom 0";

            [Category("Product information")]
            [DisplayName("Data type")]
            public DataTypes DataType { get; set; } = DataTypes.Text;
        }

        /// <summary>
        /// PLC 데이터 목록
        /// </summary>
        [Category("PLC.Address.Infor")]
        [DisplayName("Product infor.")]
        [Description("Product information list")]
        public List<ProductInfor> ProductInfors { get; set; } = new List<ProductInfor>();

        /// <summary>
        /// 모델넘버 주소
        /// </summary>
        [Category("PLC.Address.Infor")]
        [DisplayName("ModelNumber")]
        [Description("Model number address (word)")]
        public string PlcAddModelNumber { get; set; } = "D10202";
        #endregion

        #region PLC.MES
        /// <summary>
        /// MES 시작 주소 헤더
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("MES start address")]
        [Description("MES start address")]
        public string PlcMesStartAddress { get; set; } = "";

        /// <summary>
        /// MES 데이터 스케일
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("MES Data scale")]
        [Description("MES data scale e.g) 1.234 * scale = 1234, where scale = 1000")]
        public double PlcMesDataScale { get; set; } = 1000;

        /// <summary>
        /// Database path
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("DB path")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string DbPath { get; set; }

        /// <summary>
        /// Database columns
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("DB columns")]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor," + "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public List<string> DbColumns { get; set; } = new List<string>();
        #endregion

        #region PLC.Control
        /// <summary>
        /// 측정 타임아웃
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Meas. timeout (sec)")]
        [Description("Maximum measurement time (sec)")]
        public double PlcMeasFinWaitTimeOut { get; set; } = 50;

        /// <summary>
        /// PLC Heart beat 간격
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Update interval (ms)")]
        [Description("Update interval (ms)")]
        public int PlcUpdateInterval { get; set; } = 500;

        /// <summary>
        /// 연결 시도 대기 간격
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Conn. retry interval (ms)")]
        [Description("Connection retry interval (ms)")]
        public int PlcConnRetryInterval { get; set; } = 3000;

        /// <summary>
        /// 측정 중 하트비트 동작
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Heart beat during measurement")]
        [Description("Toggle heart beat bit during measurement")]
        public bool PlcCtrlHeartBeatDuringMeas { get; set; } = false;

        /// <summary>
        /// 측정 전 OK/NG clear
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-OK/NG")]
        [Description("Clear the OK and NG flags Before a measurement")]
        public bool PlcCtrlAutoClearOkNG { get; set; } = false;

        /// <summary>
        /// 측정 전 OK/NG clear
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-MeasFin")]
        [Description("Clear the measurement finish flag Before a measurement")]
        public bool PlcCtrlAutoClearMeasFin { get; set; } = false;

        #endregion

        #region TCP.Connection
        /// <summary>
        /// SMA PC IP Address
        /// </summary>
        [Category("TCP.Connection")]
        [DisplayName("\tIP")]
        [Description("SMA Application IP address")]
        public string TcpIpAdd { get; set; } = "100.100.3.85";

        /// <summary>
        /// SMA PC Port
        /// </summary>
        [Category("TCP.Connection")]
        [DisplayName("\tPort")]
        [Description("SMA Application port number")]
        public int TcpPort { get; set; } = 50001;
        #endregion

        #region TCP.Control
        /// <summary>
        /// TCP 재연결 시도 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Conn. retry int.")]
        [Description("Connection retry interval (ms)")]
        public int TcpConnWaitTimeMilSec { get; set; } = 3000;

        /// <summary>
        /// TCP 모니터 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Idle monitor interval (ms)")]
        [Description("TCP monitor interval (ms) during idle")]
        public int TcpMonitorTimeMilSec { get; set; } = 1000;

        /// <summary>
        /// TCP 측정 종료 체크 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Meas. monitor interval (ms)")]
        [Description("TCP monitor interval (ms) during measurement")]
        public int TcpMeasFinCheckTimeMilSec { get; set; } = 1000;

        /// <summary>
        /// TCP 측정 종료 체크 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("MAX meas. timeout (sec)")]
        [Description("MAX meas. time (second)")]
        public int TcpMaxMeasTimeSec { get; set; } = 1000;

        /// <summary>
        /// TCP Idle 타임 최대값
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("TCP idle time (min)")]
        [Description("TCP idle time limit (min). After idle time, send a communication interface clear (*cls) message prevent to sleep.")]
        public double TcpIdleTimeMinLimit { get; set; } = 30;

        /// <summary>
        /// TCP 모니터 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Meas. start delay (ms)")]
        [Description("Delay interval (ms) before start measurement")]
        public int TcpMeasStartDelay { get; set; } = 1500;

        /// <summary>
        /// TCP 연결 해제 시 자동 종료
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Auto close program")]
        [Description("Close this program if the TCP is disconnected")]
        public bool TcpAutoCloseIfDisconnected { get; set; } = false;

        /// <summary>
        /// TCP 연결 해제 시 자동 종료
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Maximum error count")]
        [Description("Maximum error count during measurement monitoring")]
        public int TcpMaxErrorCount { get; set; } = 10;
        #endregion

        #region Control
        /// <summary>
        /// SMA APP 이름
        /// </summary>
        [Category("Control")]
        [DisplayName("Application name")]
        [Description("SMA application name")]
        public string SmaAppName { get; set; } = "sma-4100r";

        /// <summary>
        /// 자동 시작 여부
        /// </summary>
        [Category("Control")]
        [DisplayName("Auto start")]
        [Description("Start communication automatically when the app is launched")]
        public bool AutoStart { get; set; } = false;

        /// <summary>
        /// 자동 시작 여부
        /// </summary>
        [Category("Control")]
        [DisplayName("Maximum log lines")]
        [Description("The maximum number of lines in the log window")]
        public int LogMaximumLine { get; set; } = 100;
        #endregion

        #region 메소드
        /// <summary>
        /// 설정 파일 경로 반환 함수
        /// </summary>
        public static string FilePath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "ComSetting.Xml");
            }
        }
        #endregion
    }
}
