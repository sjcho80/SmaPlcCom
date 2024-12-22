using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

using McProtocol.Mitsubishi;
using McProtocol;

namespace SmaPlc
{
    /// <summary>
    /// Melsec MC Protocol 통신
    /// </summary>
    public class MelsecMcProtocol : PlcBase
    {
        /// <summary>
        /// Connection parameters
        /// </summary>
        public class ConParams
        {
            /// <summary>
            /// IP 주소
            /// </summary>
            public IPAddress IpAdd;
            /// <summary>
            /// 포트 넘버
            /// </summary>
            public int Port;
            /// <summary>
            /// MC Frame
            /// </summary>
            public McFrame Mcf;
        }

        /// <summary>
        /// PLC unit
        /// </summary>
        private McProtocolTcp _plc = null;


        #region 통신 제어
        /// <summary>
        /// 연결 파라미터 문법을 체크한다
        /// </summary>
        /// <param name="conParams"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ConvertOpenParameter(string conParams, out object item)
        {
            item = null;
            if (conParams == "")
            {
                _errMsg = "{IP Address}, {port (short)}, ({MC frame (enum): MC1E, MC3E, MC4E})";
                return false;
            }

            string[] strs = conParams.Split(',');
            if (strs.Length < 2)
            {
                _errMsg = "insufficient plc connecting parameters";
                return false;
            }

            ConParams op = new ConParams();
            if (!IPAddress.TryParse(strs[0], out IPAddress add))
            {
                _errMsg = "insufficient plc parameters";
                return false;
            }
            op.IpAdd = add;

            if (!int.TryParse(strs[1], out int port))
            {
                _errMsg = $"Cannot try parse port: {strs[1]}";
                return false;
            }
            op.Port = port;

            if (strs.Length > 2)
            {
                if (!Enum.TryParse(strs[2], out McFrame mcf))
                {
                    _errMsg = $"Cannot try parse McFrame: {strs[2]}";
                    return false;
                }
                op.Mcf = mcf;
            }
            else
            {
                op.Mcf = McFrame.MC3E;
            }
            item = op;

            return true;
        }

        /// <summary>
        /// 통신 오픈
        /// </summary>
        /// <param name="conParams"></param>
        /// <returns></returns>
        public override bool Open(string conParams)
        {
            if (!ConvertOpenParameter(conParams, out object item))
            {
                return false;
            }
            var op = (ConParams)item;
            _plc = new McProtocolTcp(op.IpAdd.ToString(), op.Port, op.Mcf);

            try
            {
                var res = _plc.Open();
                if (res.IsFaulted)
                {
                    _errMsg = res.Exception.Message;
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// PLC 통신 닫기
        /// </summary>
        public override void Close()
        {
            if (_plc != null)
            {
                _plc.Close();
            }
        }
        #endregion

        #region Read
        /// <summary>
        /// Bit 읽기
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadBit(string add, out bool data)
        {
            data = false;
            bool isBit = ConvertToBitAdd(add, out string dataAdd, out int bitNum);
            try
            {
                var r = _plc.ReadDeviceBlock(dataAdd);
                if (r.IsFaulted)
                {
                    _errMsg = $"ReadBit failure: address={add}, message={r.Exception.Message}";
                    return false;
                }

                if (isBit)
                {
                    int mask = 0x01 << bitNum;
                    data = (r.Result[0] & mask) > 0;
                }
                else
                {
                    data = r.Result[0] > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Word 값을 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadWord(string add, out short data)
        {
            data = 0;
            try
            {
                var res = _plc.ReadDeviceBlock(add);
                if (res.IsFaulted)
                {
                    _errMsg = $"ReadWord failure: address={add} message={res.Exception.Message}";
                    return false;
                }
                data = (short)res.Result[0];
                return true;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// DWord 값을 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadDWord(string add, out int data)
        {
            data = 0;
            try
            {
                var res = _plc.ReadDeviceBlock(add);
                if (res.IsFaulted)
                {
                    _errMsg = $"ReadWord failure: address={add} message={res.Exception.Message}";
                    return false;
                }
                data = res.Result[0];
                return true;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 바이트 데이터를 읽어온다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="len"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadBytes(string startAdd, int len, out byte[] data)
        {
            data = null;
            try
            {
                McProtocolTcp.GetDeviceCode(startAdd, out PlcDeviceType t, out int addNum);
                var r = _plc.ReadDeviceBlockByte(t, addNum, len);
                if (r.IsFaulted)
                {
                    _errMsg = $"ReadBytes failure: address={startAdd}, message={r.Exception.Message}";
                    return false;
                }
                else
                {
                    data = r.Result;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region Write
        /// <summary>
        /// 비트 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteBit(string add, bool data)
        {
            bool isBit = ConvertToBitAdd(add, out string dataAdd, out int bitNum);
            if (isBit)
            {
                if (!ReadWord(dataAdd, out short n))
                {
                    return false;
                }

                int mask = 0x1 << bitNum;
                int n32 = n;
                if (data)
                {
                    n = (short)(n32 | mask);
                }
                else
                {
                    mask = ~mask;
                    n = (short)(n32 & mask);
                }
                return WriteDevice(dataAdd, n);
            }
            else
            {
                return WriteDevice(add, data? 1: 0);
            }

        }

        /// <summary>
        /// Word 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteWord(string add, short data)
        {
            return WriteDevice(add, data);
        }

        /// <summary>
        /// DWord 데이터를 전송한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteDWord(string add, int data)
        {
            return WriteDevice(add, data);
        }

        /// <summary>
        /// 다수 DWord 데이터를 쓴다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteDWords(string startAdd, int[] data)
        {
            try
            {
                int mesLen = data.Length;
                int[] d16 = new int[mesLen * 2]; // 32비트->16비트 데이터로 변경하여 전송한다
                for (int i = 0; i < mesLen; i++)
                {
                    d16[i * 2] = data[i] & 0xFFFF;             // udata
                    d16[i * 2 + 1] = (data[i] >> 16) & 0xFFFF; // ldata
                }
                var r = _plc.WriteDeviceBlock(startAdd, d16);
                if (r.IsFaulted)
                {
                    _errMsg = $"WriteDWords failure: address={startAdd}, message={r.Exception.Message}";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 메소드
        /// <summary>
        /// Device에 데이터를 작성한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool WriteDevice(string add, int data)
        {
            try
            {
                int[] d = new int[] { data };
                var r = _plc.WriteDeviceBlock(add, d);
                if (r.IsFaulted)
                {
                    _errMsg = $"Write failure: address={add}, message={r.Exception.Message}"; ;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 데이터 주소를 bit 주소로 변환한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="dataAdd"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        private bool ConvertToBitAdd(string add, out string dataAdd, out int bitNum)
        {
            string[] s = add.Split('.');
            if (s.Length <= 1)
            {
                dataAdd = add;
                bitNum = -1;
                return false;
            }

            dataAdd = s[0];
            if (int.TryParse(s[1], out int n))
            {
                bitNum = n;
                return true;
            }
            else
            {
                bitNum = -1;
                return false;
            }
        }
        #endregion
    }
}
