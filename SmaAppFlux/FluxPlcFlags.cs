using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaPlc;

namespace SmaFlux
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FluxPlcFlags : PlcFlags
    {
        /// <summary>
        /// 측정요청 비트 주소
        /// </summary>
        [Category("Plc")]
        [DisplayName("03_Reset request")]
        [Description("Reset request bit address")]
        public string ResetReqBit { get; set; } = "D5000.2";

        /// <summary>
        /// 측정 완료 주소
        /// </summary>
        [Category("App")]
        [DisplayName("12_Reset finihsed")]
        [Description("Reset finish bit address")]
        public string ResetFinBit { get; set; } = "D5100.6";
    }
}
