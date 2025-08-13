using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

using SmaCtrl;
using SmaPlc;

namespace SmaFlux
{
    /// <summary>
    /// Flux 설정
    /// </summary>
    [Serializable]
    public class FluxSettings : SmaSettings
    {
        #region 타입 정의
        #endregion

        #region PLC
        [Category("[02]PLC")]
        [DisplayName("Control")]
        public PlcSettings PlcSettings { get; set; } = new PlcSettings();

        [Category("[02]PLC")]
        [DisplayName("Flag address")]
        public FluxPlcFlags PlcAddress { get; set; } = new FluxPlcFlags();

        [Category("[02]PLC")]
        [DisplayName("Product information")]
        public List<PlcProdInfors> PlcProdInfs { get; set; } = new List<PlcProdInfors>();

        [Category("[02]PLC")]
        [DisplayName("MES data")]
        public List<PlcMesInfors> PlcMesInfs { get; set; } = new List<PlcMesInfors>();

        /// <summary>
        /// 모델 넘버 주소
        /// </summary>
        [Category("[02]PLC")]
        [DisplayName("Model number")]
        [Description("Model number address")]
        public string PlcModelNumberAddress { get; set; }
        #endregion

        #region 측정 DB 설정
        [Category("[03]DB")]
        [Description("DB 파일의 경로")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string DbPath { get; set; }
        #endregion

        #region 프로그램 제어

        #endregion

        #region Miscellaneous
        /// <summary>
        /// 최대 모델의 수
        /// </summary>
        [Category("[04]Miscellaneous")]
        [DisplayName("Maximum models")]
        [Description("최대 모델 수 (프로그램 재시작 필요)")]
        [DefaultValue(10)]
        public int DbMaxModels { get; set; } = 10;

        /// <summary>
        /// DB 삽입 시 dashboard를 업데이트 한단
        /// </summary>
        [Category("[04]Miscellaneous")]
        [Description("DB 삽입 후 Dashboard를 자동으로 업데이트한다.")]
        [DefaultValue(true)]
        public bool LoadDashboardAfterInsert { get; set; } = true;

        /// <summary>
        /// DB 삽입 시 dashboard를 업데이트 한단
        /// </summary>
        [Category("[04]Miscellaneous")]
        [DisplayName("Site information")]
        [Description("프로그램 헤더에 출력되는 작업장 정보")]
        public string SiteInformation { get; set; } = "";
        #endregion

        #region 메소드
        /// <summary>
        /// 설정을 로드한다
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ss"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool Load(string filePath, out FluxSettings ss, out string errMsg)
        {
            return LoadXml(filePath, out ss, out errMsg);
        }
        #endregion

    }
}
