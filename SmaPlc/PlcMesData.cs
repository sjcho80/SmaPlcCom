using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaPlc
{
    [Serializable]
    public class PlcMesInfors
    {
        /// <summary>
        /// 업로드 데이터 타입
        /// </summary>
        public enum MesDataTypes
        {
            Word,
            DWord,
        }

        /// <summary>
        /// 이름
        /// </summary>
        [Category("0_Name")]
        public string Name { get; set; }

        /// <summary>
        /// 업로드 주소
        /// </summary>
        [Category("1_PLC")]
        public string Address { get; set; }

        /// <summary>
        /// 업로드 데이터 타입
        /// </summary>
        [Category("1_PLC")]
        [DefaultValue(MesDataTypes.DWord)]
        public MesDataTypes DataType { get; set; } = MesDataTypes.DWord;

        /// <summary>
        /// 데이터 이름
        /// </summary>
        [Category("2_DB")]
        public string DataName { get; set; }

        /// <summary>
        /// 활성화 여부
        /// </summary>
        [Category("1_PLC")]
        [DefaultValue(false)]
        public bool Enable { get; set; } = false;


        /// <summary>
        /// 데이터 스케일
        /// </summary>
        [Category("1_PLC")]
        [DefaultValue(100d)]
        public double Scale { get; set; } = 100d;

        /// <summary>
        /// 주석
        /// </summary>
        [Category("3_Information")]
        public string Comment { get; set; }
    }
}
