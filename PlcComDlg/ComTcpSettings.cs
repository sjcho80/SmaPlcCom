using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcComDlg
{
    /// <summary>
    /// TCP 연결 설정
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(TcpSettingsConverter))]
    public class ComTcpSettings
    {
        /// <summary>
        /// TCP descripter
        /// </summary>
        public class TcpSettingsConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is ComTcpSettings)
                {
                    ComTcpSettings sdi = (ComTcpSettings)value;

                    return $"{sdi.IpAdd} ({sdi.Port})";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }

        /// <summary>
        /// SMA PC IP Address
        /// </summary>
        [Category("TCP.Connection")]
        [DisplayName("\tIP")]
        [Description("SMA Application의 IP address")]
        public string IpAdd { get; set; } = "100.100.3.85";

        /// <summary>
        /// SMA PC Port
        /// </summary>
        [Category("TCP.Connection")]
        [DisplayName("\tPort")]
        [Description("SMA Application의 port number")]
        public int Port { get; set; } = 50001;

        /// <summary>
        /// TCP 재연결 시도 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Conn. retry int.")]
        [Description("연결 시도 간격 (ms)")]
        public int ConnWaitTimeMilSec { get; set; } = 3000;

        /// <summary>
        /// TCP 모니터 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("TCP monitor interval (ms)")]
        [Description("TCP thread의 시스템 체크 간격 (ms)")]
        public int MonitorTimeMilSec { get; set; } = 1000;

        /// <summary>
        /// TCP 측정 종료 체크 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Meas. monitor interval (ms)")]
        [Description("측정 중 SMA APP에 측정 완료 query를 날리는 시간 간격 (ms)")]
        public int MeasFinCheckTimeMilSec { get; set; } = 1000;

        /// <summary>
        /// TCP 측정 종료 체크 시간
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("MAX meas. timeout (sec)")]
        [Description("최대 측정 시간 (second)")]
        public int MaxMeasTimeSec { get; set; } = 1000;

        /// <summary>
        /// TCP Idle 타임 최대값
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("TCP idle time (min)")]
        [Description("TCP 통신의 최대 유휴 시간 (min)\r\n" +
            "이 시간이 지나면 communication interface clear (*cls) 메시지를 자동으로 전송한다.")]
        public double IdleTimeMinLimit { get; set; } = 30;

        /// <summary>
        /// 측정 시작 딜레이
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Meas. start delay (ms)")]
        [Description("측정 시작 메시지 전송 딜레이 (ms)")]
        public int MeasStartDelay { get; set; } = 1500;

        /// <summary>
        /// TCP 연결 해제 시 자동 종료
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Auto close program")]
        [Description("TCP 연결이 해제되면 프로그램을 자동으로 종료한다")]
        public bool AutoCloseIfDisconnected { get; set; } = false;

        /// <summary>
        /// TCP 연결 해제 시 자동 종료
        /// </summary>
        [Category("TCP.Control")]
        [DisplayName("Maximum error count")]
        [Description("측정 시 TCP 연결을 해제하는 최대 에러 수")]
        public int MaxErrorCount { get; set; } = 10;
    }
}
