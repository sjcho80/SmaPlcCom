using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaFlux
{
    /// <summary>
    /// 측정 데이터
    /// </summary>
    public class FluxData
    {
        /// <summary>
        /// 측정 데이터 셋
        /// </summary>
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 측정 시간
        /// </summary>
        public DateTime LabDate
        {
            get
            {
                return Convert.ToDateTime(Values["labdate"]);
            }
        }

        /// <summary>
        /// 모델 넘버
        /// </summary>
        public int ModelNumber
        {
            get
            {
                return Convert.ToInt32(Values["modelNumber"]);
            }
        }

        /// <summary>
        /// Model 이름
        /// </summary>
        public string ModelName
        {
            get
            {
                return Values["modelName"].ToString();
            }
        }

        /// <summary>
        /// 양의 peak 값
        /// </summary>
        public double NegPeak
        {
            get
            {
                return Convert.ToDouble(Values["negPeak"]);
            }
        }

        /// <summary>
        /// 총 peak 값
        /// </summary>
        public double TotalPeak
        {
            get
            {
                return Convert.ToDouble(Values["totalPeak"]);
            }
        }

        /// <summary>
        /// 양의 peak 값
        /// </summary>
        public double PosPeak
        {
            get
            {
                return Convert.ToDouble(Values["posPeak"]);
            }
        }

        /// <summary>
        /// 음의 값 하한
        /// </summary>
        public double NegPeakLoLim
        {
            get
            {
                return Convert.ToDouble(Values["negPeakLoLim"]);
            }
        }

        /// <summary>
        /// 음의 값 상한
        /// </summary>
        public double NegPeakUpLim
        {
            get
            {
                return Convert.ToDouble(Values["negPeakUpLim"]);
            }
        }

        /// <summary>
        /// 총합 하한
        /// </summary>
        public double TotalPeakLoLim
        {
            get
            {
                return Convert.ToDouble(Values["totalPeakLoLim"]);
            }
        }

        /// <summary>
        /// 총합 상한
        /// </summary>
        public double TotalPeakUpLim
        {
            get
            {
                return Convert.ToDouble(Values["totalPeakUpLim"]);
            }
        }

        /// <summary>
        /// 양의 값 하한
        /// </summary>
        public double PosPeakLoLim
        {
            get
            {
                return Convert.ToDouble(Values["posPeakLoLim"]);
            }
        }

        /// <summary>
        /// 양의 값 상한
        /// </summary>
        public double PosPeakUpLim
        {
            get
            {
                return Convert.ToDouble(Values["posPeakUpLim"]);
            }
        }
        
        /// <summary>
                 /// 음의 peak값 OK
                 /// </summary>
        public bool NegOk
        {
            get
            {
                return Convert.ToBoolean(Values["negOk"]);
            }
        }

        /// <summary>
        /// 합 peak값 OK
        /// </summary>
        public bool TotalOk
        {
            get
            {
                return Convert.ToBoolean(Values["totalOk"]);
            }
        }

        /// <summary>
        /// 양의 peak값 OK
        /// </summary>
        public bool PosOk
        {
            get
            {
                return Convert.ToBoolean(Values["posOk"]);
            }
        }

        /// <summary>
        /// 전체 peak값 OK
        /// </summary>
        public bool Ok
        {
            get
            {
                return Convert.ToBoolean(Values["ok"]);
            }
        }
    }
}
