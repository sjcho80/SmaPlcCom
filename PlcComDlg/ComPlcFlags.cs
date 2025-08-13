using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaPlc;

namespace PlcComDlg
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ComPlcFlags : PlcFlags
    {
        /// <summary>
        /// 측정요청 비트 주소
        /// </summary>
        [Category("Plc")]
        [DisplayName("32_Busy")]
        [Description("측정 중 ON")]
        public string BusyBit { get; set; } = "";

        /// <summary>
        /// 측정요청 비트 주소
        /// </summary>
        [Category("Plc")]
        [DisplayName("12_Measurement request check")]
        [Description("측정 요청 응답")]
        public string MeasReqRespBit { get; set; } = "";
    }
}
