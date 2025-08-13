using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaPlc;

namespace SmaFlux
{
    public class FluxPlcData : PlcData
    {
        /// <summary>
        /// 리셋 요청
        /// </summary>
        public Flag ResetReqBit { get; set; } = new Flag();

        /// <summary>
        /// 리셋 완료
        /// </summary>
        public Flag ResetFinBit { get; set; } = new Flag();

        /// <summary>
        /// flag 초기화
        /// </summary>
        public void InitFlags()
        {
            InitBaseFlags();
            ResetFinBit.Initialized = true;
            ResetReqBit.Initialized = true;
        }
    }
}
