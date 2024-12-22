using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcComDlg
{
    public class LogMsg
    {
        /// <summary>
        /// Log source
        /// </summary>
        public enum Sources
        {
            PLC,
            DB,
            TCP,
            APP
        }

        /// <summary>
        /// Log 발생 시간
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 에러코드
        /// </summary>
        public int ErrCode { get; set; } = -1;

        /// <summary>
        /// 에러메시지
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 로그
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 로그 소스
        /// </summary>
        public Sources LogSource;
    }
}
