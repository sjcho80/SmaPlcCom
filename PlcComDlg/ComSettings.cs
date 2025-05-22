using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;

using SmaPlc;

namespace PlcComDlg
{
    /// <summary>
    /// 통신설정 클래스
    /// </summary>
    [Serializable]
    public class ComSettings
    {
        #region 타입정의
        /// <summary>
        /// 설정 DB descripter
        /// </summary>
        public class DbInforSettingsConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is DbInforSettings)
                {
                    DbInforSettings sdi = (DbInforSettings)value;

                    return $"{sdi.CondItems.Count} items, {sdi.Path}";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }

        /// <summary>
        /// 설정 DB 정보
        /// </summary>
        [Serializable]
        [TypeConverter(typeof(DbInforSettingsConverter))]
        public class DbInforSettings
        {
            /// <summary>
            /// 설정 DB MES 업로드 아이템
            /// </summary>
            [Serializable]
            public class CondItemInf
            {
                /// <summary>
                /// 텍스트로부터 1개의 정수를 읽어온다
                /// </summary>
                /// <param name="buffer"></param>
                /// <param name="format"></param>
                /// <param name="arg0"></param>
                /// <returns></returns>
                [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
                public static extern int sscanf(string buffer, string format, __arglist);

                /// <summary>
                /// 이름
                /// </summary>
                [Category("Information")]
                [Description("Name")]
                [DefaultValue("")]
                public string Name { get; set; } = "";

                /// <summary>
                /// 스캔하고자 하는 DB Id
                /// </summary>
                [Category("Information")]
                [Description("DB Id")]
                [DefaultValue(0)]
                public long Id { get; set; } = 0;

                /// <summary>
                /// Err
                /// </summary>
                [Category("Information")]
                [Description("Model number")]
                [DefaultValue(0)]
                public long ModelNumber { get; set; } = 0;

                /// <summary>
                /// 활성화 여부
                /// </summary>
                [Category("Control")]
                [Description("활성화 여부")]
                [DefaultValue(false)]
                public bool Enable { get; set; } = false;

                /// <summary>
                /// 파라미터 순서 (쉼표로 구분됨)
                /// </summary>
                [Category("Control")]
                [Description("Parameter format")]
                [DefaultValue("SPM A MAX, %f")]
                public string ParameterFormat { get; set; } = "SPM A MAX, %lf";

                /// <summary>
                /// 파라미터 order
                /// </summary>
                [Category("Control")]
                [Description("Parameter order")]
                [DefaultValue(1)]
                public int ParameterOrder { get; set; } = 1;


                /// <summary>
                /// 데이터 스케일
                /// </summary>
                [Category("Control")]
                [Description("Data scale")]
                [DefaultValue(1000)]
                public int Scale { get; set; } = 1000;

                /// <summary>
                /// 업로드 데이터 주소
                /// </summary>
                [Category("Control")]
                [Description("Address")]
                public string Address { get; set; } = "";


                /// <summary>
                /// Parse parameter
                /// </summary>
                /// <param name="paramText"></param>
                /// <param name="paramVal"></param>
                /// <param name="errMsg"></param>
                /// <returns></returns>
                public bool ParseParam(string paramText, out double paramVal, out string errMsg)
                {
                    try
                    {
                        double arg0 = 0, arg1 = 0, arg2 = 0;
                        double[] args = new double[256];
                        if (ParameterOrder < 1 || ParameterOrder > 2)
                        {
                            throw new Exception($"파라미터 order 제한 {ParameterOrder}");
                        }
                        int res = sscanf(paramText, ParameterFormat, __arglist(ref arg0, ref arg1, ref arg2));

                        paramVal = -1;
                        if (ParameterOrder == 1)
                        {
                            paramVal = arg0;
                        }
                        else if (ParameterOrder == 2)
                        {
                            paramVal = arg1;
                        }
                        else if (ParameterOrder == 3)
                        {
                            paramVal = arg2;
                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg = $"Condition 텍스트 추출 실패 ({paramText}, {ParameterFormat}): {ex.Message}";
                        paramVal = 0;
                        return false;
                    }
                    errMsg = "";
                    return true;
                }
            }

            /// <summary>
            /// Db 경로
            /// </summary>
            [DisplayName("DB Path")]
            [Description("Settings database path")]
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            public string Path { get; set; } = "";

            /// <summary>
            /// MES 업로드 아이템
            /// </summary>
            [DisplayName("Upload items")]
            [Description("Settings database path")]
            public List<CondItemInf> CondItems { get; set; } = new List<CondItemInf>();
        }

        /// <summary>
        /// 측정 DB descripter
        /// </summary>
        public class DbInforMeasConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is DbInforMeas)
                {
                    DbInforMeas sdi = (DbInforMeas)value;

                    return $"{sdi.MeasCols.Count} items, {sdi.Path}";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }

        /// <summary>
        /// 측정 DB 정보
        /// </summary>
        [Serializable]
        [TypeConverter(typeof(DbInforMeasConverter))]
        public class DbInforMeas
        {
            /// <summary>
            /// 설정 DB MES 업로드 아이템
            /// </summary>
            [Serializable]
            public class MeasColInf
            {
                /// <summary>
                /// 이름
                /// </summary>
                [Category("Information")]
                [Description("Column name")]
                [DefaultValue("")]
                public string Name { get; set; } = "";

                /// <summary>
                /// 활성화 여부
                /// </summary>
                [Category("Control")]
                [Description("활성화 여부")]
                [DefaultValue(false)]
                public bool Enable { get; set; } = false;

                /// <summary>
                /// 데이터 스케일
                /// </summary>
                [Category("Control")]
                [Description("Data scale")]
                [DefaultValue(1000)]
                public int Scale { get; set; } = 1000;

                /// <summary>
                /// 업로드 데이터 주소
                /// </summary>
                [Category("Control")]
                [Description("Address")]
                [DefaultValue("")]
                public string Address { get; set; } = "";
            }

            /// <summary>
            /// Db 경로
            /// </summary>
            [DisplayName("DB Path")]
            [Description("Settings database path")]
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            public string Path { get; set; } = "";

            /// <summary>
            /// MES 업로드 아이템
            /// </summary>
            [DisplayName("Upload items")]
            [Description("Settings database path")]
            public List<MeasColInf> MeasCols { get; set; } = new List<MeasColInf>();
        }
        #endregion

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
        /// Database settings 정보
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("DB - Settings")]
        public DbInforSettings DbSettings { get; set; } = new DbInforSettings();

        /// <summary>
        /// Database settings 정보
        /// </summary>
        [Category("PLC.MES")]
        [DisplayName("DB - Measurement")]
        [Description("Path: settings file path\r\n" +
            "Id: condition item ID\r\n" +
            "Model number: model number\r\n" +
            "Parameter format: parameter format - B 0 Zp 0, %lf")]
        public DbInforMeas DbMeas { get; set; } = new DbInforMeas();
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
        /// 측정 전 측정완료 클리어
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-MeasFin")]
        [Description("Clear the measurement finish flag Before a measurement")]
        public bool PlcCtrlAutoClearMeasFin { get; set; } = false;

        /// <summary>
        /// 측정 전 측정완료 클리어
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-MeasFin")]
        [Description("Clear the meas. req. resp flag before set the meas. fin. flag")]
        public bool PlcCtrlAutoClearMeasReqResp { get; set; } = false;

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
        /// 최대 로그 라인 수
        /// </summary>
        [Category("Control")]
        [DisplayName("Maximum log lines")]
        [Description("The maximum number of lines in the log window")]
        public int LogMaximumLine { get; set; } = 100;


        /// <summary>
        /// 모든 이벤트 로그를 저장한다
        /// </summary>
        [Category("Control")]
        [DisplayName("Save all log to db")]
        [Description("True: 모든 이벤트 로그 저장/false: error 로그만 저장")]
        [DefaultValue(false)]
        public bool SaveAllLogToDb { get; set; } = false;
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
