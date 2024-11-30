using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcComDlg
{
    /// <summary>
    /// TCP 통신 데이터
    /// </summary>
    [Serializable]
    public class TcpData
    {
        /// <summary>
        /// TCP Message
        /// </summary>
        public class ComMsg
        {
            /// <summary>
            /// TCP Message
            /// </summary>
            public string Message { get; set; } = "";
        }

        /// <summary>
        /// TCP Write buffer
        /// </summary>
        public byte[] WriteBuf { get; set; }

        /// <summary>
        /// TCP read buffer
        /// </summary>
        public byte[] ReadBuf { get; set; } = new byte[1024];

        /// <summary>
        /// TCP read buffer 길이
        /// </summary>
        public int ReadBufLen { get; set; } = 0;

        /// <summary>
        /// TCP 통신 마지막 에러 메시지
        /// </summary>
        public string ComLastErrMsg { get; set; } = "";

        /// <summary>
        /// TCP 마지막 통신시간
        /// </summary>
        public DateTime LastCommTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 측정 시작 시간
        /// </summary>
        public DateTime MeasStartTime { get; set; } = DateTime.Now;
    }
}
