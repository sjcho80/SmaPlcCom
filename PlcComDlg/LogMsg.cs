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
        /// Log 발생 시간
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 에러코드
        /// </summary>
        public int ErrCode = -1;

        /// <summary>
        /// 로그
        /// </summary>
        public string Message;
    }
}
