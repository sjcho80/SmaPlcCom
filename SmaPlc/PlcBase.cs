using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaPlc
{
    /// <summary>
    /// PLC Base abstract class
    /// </summary>
    public abstract class PlcBase
    {
        #region 타입 정의
        /// <summary>
        /// PLC 타입
        /// </summary>
        public enum PlcTypes
        {
            /// <summary>
            /// Melsec MX Component 통신
            /// </summary>
            MelsecMxComponent,
            /// <summary>
            /// Melsec Mc Protocol 통신
            /// </summary>
            MelsecMcProtocol,
            /// <summary>
            /// Simens S7 통신
            /// </summary>
            SimensS7,
        }
        #endregion

        #region 변수 선언
        /// <summary>
        /// 마지막 에러
        /// </summary>
        protected string _errMsg { get; set; }

        /// <summary>
        /// 마지막 에러 코드
        /// </summary>
        protected int _errCode { get; set; }
        #endregion

        #region 메소드.Abstract.통신
        /// <summary>
        /// 연결 파라미터 양식을 변환한다
        /// </summary>
        /// <param name="conParams"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool ConvertOpenParameter(string conParams, out object item);

        /// <summary>
        /// 파라미터
        /// </summary>
        /// <param name="conParams"></param>
        /// <returns></returns>
        public abstract bool Open(string conParams);

        /// <summary>
        /// 통신 종료
        /// </summary>
        public abstract void Close();
        #endregion

        #region 메소드.Abstract.Read
        /// <summary>
        /// Bit 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool ReadBit(string add, out bool data);

        /// <summary>
        /// Word 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool ReadWord(string add, out short data);

        /// <summary>
        /// DWord 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool ReadDWord(string add, out int data);

        /// <summary>
        /// Byte 데이터를 읽어온다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="len"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool ReadBytes(string startAdd, int len, out byte[] data);
        #endregion

        #region 메소드.Abstract.Write
        /// <summary>
        /// Bit 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool WriteBit(string add, bool data);

        /// <summary>
        /// Word 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool WriteWord(string add, short data);

        /// <summary>
        /// DWord 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool WriteDWord(string add, int data);

        /// <summary>
        /// Word 데이터들을 쓴다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool WriteDWords(string startAdd, int[] data);
        #endregion

        #region 메소드
        /// <summary>
        /// 마지막 에러 메시지를 반환한다
        /// </summary>
        public string LastErrMsg { get { return _errMsg; } }

        /// <summary>
        /// 마지막 에러 코드를 반환한다
        /// </summary>
        public int LastErrCode { get { return _errCode; } }
        #endregion
    }
}
