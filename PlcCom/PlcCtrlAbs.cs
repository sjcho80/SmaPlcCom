using System;
using System.Collections.Generic;
using System.Text;

namespace PlcCom
{
    public abstract class PlcCtrlAbs
    {
        /// <summary>
        /// 마지막 에러
        /// </summary>
        protected string _lastError { get; set; }

        /// <summary>
        /// 마지막 에러 메시지를 반환한다
        /// </summary>
        public string GetLastError { get { return _lastError; } }

        //public abstract bool GetStringData();
    }
}
