using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaPlc
{
    [Serializable]
    [TypeConverter(typeof(PlcSettingsConverter))]
    public class PlcSettings
    {
        /// <summary>
        /// Plc 설정 descripter
        /// </summary>
        public class PlcSettingsConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is PlcSettings)
                {
                    PlcSettings sdi = (PlcSettings)value;

                    return $"{sdi.PlcType.ToString()} ({sdi.ConnectionParam})";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }

        /// <summary>
        /// PLC 타입
        /// </summary>
        [Category("Connection")]
        [DisplayName("\tPlc type")]
        [Description("Plc type")]
        [DefaultValue(PlcBase.PlcTypes.SimensS7)]
        public PlcBase.PlcTypes PlcType { get; set; } = PlcBase.PlcTypes.SimensS7;

        /// <summary>
        /// PLC 연결 파라미터
        /// </summary>
        [Category("Connection")]
        [DisplayName("\tPlc connection parameters")]
        [Description("Plc connection parameters\r\n" +
            "Type 'help' or clear to print helps corresponding plc type")]
        [DefaultValue("100.100.101.42, 12042, 0, 0")]
        public string ConnectionParam { get; set; } = "100.100.101.42, 12042, 0, 0";

        /// <summary>
        /// 연결 시도 대기 간격
        /// </summary>
        [Category("Control")]
        [DisplayName("Connection retry")]
        [Description("연결 재시도 간격 (ms)")]
        [DefaultValue(3000)]
        public int ConnRetryInterval { get; set; } = 3000;

        /// <summary>
        /// PLC Heart beat 간격
        /// </summary>
        [Category("Control")]
        [DisplayName("Update interval")]
        [Description("PLC 모니터 업데이트 간격 (ms)")]
        [DefaultValue(500)]
        public int UpdateInterval { get; set; } = 500;

        /// <summary>
        /// 리셋 시 측정 flag 초기화
        /// </summary>
        [Category("Control")]
        [DisplayName("Clear meas. finish flags")]
        [Description("리셋 후 측정 완료 플래그를 자동으로 클리어 한다")]
        [DefaultValue(true)]
        public bool ClearMeasFlagsAfterReset { get; set; } = true;

        /// <summary>
        /// 측정 시 리셋 flag 초기화
        /// </summary>
        [Category("Control")]
        [DisplayName("Clear reset finish flags")]
        [Description("측정 후 리셋 완료 플래그를 자동으로 클리어 한다")]
        [DefaultValue(true)]
        public bool ClearResetFlagsAfterMeas { get; set; } = true;

        /// <summary>
        /// 비트 이벤트 검출 방식
        /// </summary>
        [Category("Control")]
        [DisplayName("Bit event type")]
        [Description("비트 이벤트 검출 방식: 하강 (falling)/상승 (rising)/ 켜짐 (on)/ 꺼짐 (off)")]
        [DefaultValue(PlcData.Flag.Events.Rising)]
        public PlcData.Flag.Events FlagEventType { get; set; } = PlcData.Flag.Events.Rising;
    }
}
