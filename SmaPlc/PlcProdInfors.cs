using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaPlc
{
    /// <summary>
    /// PLC 제품 정보
    /// </summary>
    [Serializable]
    public class PlcProdInfors
    {
        /// <summary>
        /// 제품 정보 타입
        /// </summary>
        public enum DataTypes
        {
            Text,
            Word,
            DWord,
        }

        /// <summary>
        /// 이름
        /// </summary>
        [Category("0_Name")]
        public string Name { get; set; }

        /// <summary>
        /// 데이터 타입
        /// </summary>
        [Category("1_PLC")]
        public DataTypes DataType { get; set; }

        /// <summary>
        /// 시작 주소
        /// </summary>
        [Category("1_PLC")]
        public string StartAddress { get; set; }

        /// <summary>
        /// 문자 길이
        /// </summary>
        [Category("1_PLC")]
        public int TextLength { get; set; }

        /// <summary>
        /// Database column 이름
        /// </summary>
        [Category("2_DB")]
        public string DbColumnName { get; set; }

        /// <summary>
        /// 주석
        /// </summary>
        [Category("3_Information")]
        public string Comment { get; set; }

        /// <summary>
        /// 활성화 여부
        /// </summary>
        [Category("1_PLC")]
        [DefaultValue(false)]
        public bool Enable { get; set; } = false;
    }
}
