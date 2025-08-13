using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaPlc
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlcFlags
    {
        #region 타입 정의
        #endregion

        #region 변수 선언
        /// <summary>
        /// 하트비트 주소
        /// </summary>
        [Category("Plc")]
        [DisplayName("01_Heart beat")]
        [Description("Heartbeat bit address")]
        public string HeartBeat { get; set; } = "D5100.1";

        /// <summary>
        /// 측정요청 비트 주소
        /// </summary>
        [Category("Plc")]
        [DisplayName("02_Measurement request")]
        [Description("Measurement request bit address")]
        public string MeasRequest { get; set; } = "D5000.0";


        /// <summary>
        /// 측정 완료 주소
        /// </summary>
        [Category("App")]
        [DisplayName("11_Measurement finihsed")]
        [Description("Measurement finish bit address")]
        public string MeasFinBit { get; set; } = "D5100.2";

        /// <summary>
        /// OK 비트
        /// </summary>
        [Category("App")]
        [DisplayName("21_Ok")]
        [Description("Ok bit address")]
        public string OkBit { get; set; } = "D5100.3";

        /// <summary>
        /// NG 비트
        /// </summary>
        [Category("App")]
        [DisplayName("22_Ng")]
        [Description("NG bit address")]
        public string NgBit { get; set; } = "D5100.4";

        /// <summary>
        /// 알람 비트
        /// </summary>
        [Category("App")]
        [DisplayName("31_Alarm")]
        [Description("Program alarm")]
        public string AlarmBit { get; set; } = "D10300.6";
        #endregion
    }
}
