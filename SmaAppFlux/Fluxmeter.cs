using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmaFlux
{
    /// <summary>
    /// 플럭스미터 제어 클래스
    /// </summary>
    public class Fluxmeter
    {
        /// <summary>
        /// 시리얼 포트
        /// </summary>
        private SerialPort _sp = new SerialPort();

        /// <summary>
        /// Fluxmeter에 값을 쓴다
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="query"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool WriteWithLock(string portName, string cmd, out string errMsg)
        {
            bool res;
            lock (_sp)
            {
                _sp.PortName = portName;
                _sp.BaudRate = 9600;
                _sp.DataBits = 7;
                _sp.StopBits = StopBits.One;
                _sp.Parity = Parity.Odd;
                _sp.ReadTimeout = 1001;
                try
                {
                    _sp.Open();
                    _sp.Write($"{cmd}\r\n");
                    errMsg = "";
                    res = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    res = false;
                }
                _sp.Close();
            }

            return res;
        }

        /// <summary>
        /// Fluxmeter에서 값을 읽어온다
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="query"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ReadWithLock(string portName, string query, out string msg)
        {
            bool res;
            lock(_sp)
            {
                _sp.PortName = portName;
                _sp.BaudRate = 9600;
                _sp.DataBits = 7;
                _sp.StopBits = StopBits.One;
                _sp.Parity = Parity.Odd;
                _sp.ReadTimeout = 1002;
                try
                {
                    _sp.Open();
                    _sp.Write($"{query}\r\n");
                    msg = _sp.ReadLine();
                    res = true;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    res = false;
                }
                _sp.Close();

            }
            return res;
        }

        /// <summary>
        /// Flux 값을 읽어온다
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="pkPos"></param>
        /// <param name="pkNeg"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool ReadFlux(string portName, out double pkPos, out double pkNeg, out string errMsg)
        {
            string msg;
            if (!ReadWithLock(portName, "PKPOS?", out msg))
            {
                errMsg = msg;
                pkPos = -1; pkNeg = -1;
                return false;
            }

            if (!double.TryParse(msg, out pkPos))
            {
                errMsg = $"Fail to convert positive peak {msg}";
                pkNeg = -1;
                return false;
            }

            Thread.Sleep(100);
            if (!ReadWithLock(portName, "PKNEG?", out msg))
            {
                errMsg = msg;
                pkPos = -1; pkNeg = -1;
                return false;
            }

            if (!double.TryParse(msg, out pkNeg))
            {
                errMsg = $"Fail to convert negative peak {msg}";
                pkNeg = -1;
                return false;
            }

            errMsg = "";
            return true;
        }
    }
}
