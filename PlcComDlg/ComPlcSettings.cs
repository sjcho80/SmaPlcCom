using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaPlc;

namespace PlcComDlg
{
    public class ComPlcSettings : PlcSettings
    {
        /// <summary>
        /// 측정 타임아웃
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Meas. timeout (sec)")]
        [Description("Maximum measurement time (sec)")]
        public double MeasFinWaitTimeOut { get; set; } = 50;

        /// <summary>
        /// 측정 전 OK/NG clear
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-OK/NG")]
        [Description("Clear the OK and NG flags Before a measurement")]
        public bool CtrlAutoClearOkNG { get; set; } = false;

        /// <summary>
        /// 측정 전 측정완료 클리어
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-MeasFin")]
        [Description("Clear the measurement finish flag Before a measurement")]
        public bool CtrlAutoClearMeasFin { get; set; } = false;

        /// <summary>
        /// 측정 전 측정완료 클리어
        /// </summary>
        [Category("PLC.Control")]
        [DisplayName("Auto clear-MeasReqResp")]
        [Description("Clear the meas. req. resp flag before set the meas. fin. flag")]
        public bool CtrlAutoClearMeasReqResp { get; set; } = false;
    }
}
