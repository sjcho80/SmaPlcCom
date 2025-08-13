using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using XGCommLib;

namespace SmaPlc
{
    /// <summary>
    /// LS XG Com plc
    /// </summary>
    public class LsXGComm : PlcBase
    {
        #region 타입 정의
        private class ConParams
        {
            /// <summary>
            /// IP address
            /// </summary>
            public string IpAdd { get; set; }

            /// <summary>
            /// 포트 넘버
            /// </summary>
            public long Port { get; set; }
        }

        /// <summary>
        /// LS XG 메모리 주소 분석
        /// </summary>
        private class XgMemAdd
        {
            /// <summary>
            /// 디바이스 타입 (X, M, L 등)
            /// </summary>
            public char DeviceType { get; set; }

            /// <summary>
            /// 데이터 오프셋
            /// </summary>
            public long Offset { get; set; }

            /// <summary>
            /// 데이터 사이즈
            /// </summary>
            public long Size { get; set; } = 1;

            /// <summary>
            /// 비트 넘번
            /// </summary>
            public int BitNumber { get; set; } = 0;

            /// <summary>
            /// W0123.3의 형식 데이터
            /// </summary>
            public bool WordBitData { get; set; }
        }
        #endregion

        #region 변수 선언

        /// <summary>
        /// PLC object
        /// </summary>
        private XGCommSocket _plc = null;
        #endregion

        #region override
        /// <summary>
        /// 연결을 종료 한다
        /// </summary>
        public override void Close()
        {
            if (_plc != null)
            {
                _plc.Disconnect();
            }
        }

        /// <summary>
        /// 오픈 파라미터를 분석한다
        /// </summary>
        /// <param name="conParams"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ConvertOpenParameter(string conParams, out object item)
        {
            if (conParams == "")
            {
                item = null;
                _errMsg = "{IP Address}, {port (long)}";
                return false;
            }

            string[] strs = conParams.Split(',');
            if (strs.Length != 2)
            {
                item = null;
                _errMsg = "insufficient plc connecting parameters";
                return false;
            }

            ConParams op = new ConParams();
            if (!IPAddress.TryParse(strs[0], out IPAddress add))
            {
                item = null;
                _errMsg = $"Cannot parse ip address: {strs[0]}";
                return false;
            }
            op.IpAdd = strs[0];

            if (!long.TryParse(strs[1], out long port))
            {
                item = null;
                _errMsg = $"Cannot parse port: {strs[1]}";
                return false;
            }

            _errMsg = "";
            op.Port = port;
            item = op;
            return true;
        }

        /// <summary>
        /// 통신을 오픈한다
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
            _plc = new XGCommSocket();
            uint res = _plc.Connect(op.IpAdd, op.Port);

            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                _errMsg = "";
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                return false;
            }
        }

        /// <summary>
        /// Bit의 값을 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadBit(string add, out bool data)
        {
            if (!GetBitParams(add, out XgMemAdd xgAdd))
            {
                data = false;
                return false;
            }

            if (xgAdd.WordBitData)
            {
                ushort[] d = new ushort[1];
                uint res = _plc.ReadDataWord(xgAdd.DeviceType, xgAdd.Offset, 1, false, d);
                if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = "";
                    data = ((d[0] >> xgAdd.BitNumber) & 0x1) > 0;
                    return true;
                }
                else
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    data = false;
                    return false;
                }
            }
            else
            {
                byte[] d = new byte[1];
                uint res = _plc.ReadDataBit(xgAdd.DeviceType, xgAdd.Offset, 1, d);
                if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = "";
                    data = d[0] > 0;
                    return true;
                }
                else
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    data = false;
                    return false;
                }
            }
        }

        /// <summary>
        /// 바이트 값을 읽어온다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="len"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadBytes(string startAdd, int len, out byte[] data)
        {
            if (!GetParams(startAdd, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse start address={startAdd}";
                data = null;
                return false;
            }

            byte[] buf = new byte[len];
            uint res = _plc.ReadDataByte(xgAdd.DeviceType, xgAdd.Offset, len, buf);
            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                _errMsg = "";
                data = buf;
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                data = null;
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
            if (!GetParams(add, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse address={add}";
                data = -1;
                return false;
            }

            ushort[] buf = new ushort[2];
            uint res = _plc.ReadDataWord(xgAdd.DeviceType, xgAdd.Offset, 2, false, buf);
            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                data = (buf[1] << 16) + buf[0];
                _errMsg = "";
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                data = -1;
                return false;
            }
        }

        /// <summary>
        /// Word 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool ReadWord(string add, out short data)
        {
            if (!GetParams(add, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse address={add}";
                data = -1;
                return false;
            }

            ushort[] buf = new ushort[1];
            uint res = _plc.ReadDataWord(xgAdd.DeviceType, xgAdd.Offset, 1, false, buf);
            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                _errMsg = "";
                data = (short) buf[0];
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                data = -1;
                return false;
            }
        }

        /// <summary>
        /// 비트를 작성한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteBit(string add, bool data)
        {
            if (!GetBitParams(add, out XgMemAdd xgAdd))
            {
                data = false;
                return false;
            }

            if (xgAdd.WordBitData)
            {
                ushort[] d = new ushort[1];
                uint res;
                res = _plc.ReadDataWord(xgAdd.DeviceType, xgAdd.Offset, 1, false, d);
                if (res != (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    return false;
                }

                int d1 = 0x1 << xgAdd.BitNumber;
                int d2 = d[0] & ~d1;
                if (data)
                {
                    d2 += d1;
                }
                d[0] = (ushort)d2;
                res = _plc.WriteDataWord(xgAdd.DeviceType, xgAdd.Offset, 1, false, d);
                if (res != (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    return false;
                }
                _errMsg = "";
                return true;
            }
            else
            {
                uint res = _plc.WriteDataBit(xgAdd.DeviceType, xgAdd.Offset, 1, new byte[] { Convert.ToByte(data) });
                if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = "";
                    return true;
                }
                else
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    return false;
                }
            }
        }

        /// <summary>
        /// DWord 데이터를 작성한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteDWord(string add, int data)
        {
            if (!GetParams(add, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse address={add}";
                return false;
            }
            ushort[] buf = new ushort[2];
            buf[0] = (ushort)(data & 0xFFFF);
            buf[1] = (ushort)(data >> 16);
            uint res = _plc.WriteDataWord(xgAdd.DeviceType, xgAdd.Offset, 2, false, buf);
            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                _errMsg = "";
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                return false;
            }
        }

        /// <summary>
        /// DWord 데이터를 작성한다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteDWords(string startAdd, int[] data)
        {
            if (!GetParams(startAdd, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse address={startAdd}";
                return false;
            }

            ushort[] buf = new ushort[2];
            for (int i = 0; i < data.Length; i++)
            {
                buf[0] = (ushort)(data[i] & 0xFFFF);
                buf[1] = (ushort)(data[i] >> 16);
                uint res = _plc.WriteDataWord(xgAdd.DeviceType, xgAdd.Offset + i * 2, 2, false, buf);
                if (res != (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
                {
                    _errMsg = _plc.GetReturnCodeString(res);
                    return false;
                }
            }

            _errMsg = "";
            return true;
        }

        /// <summary>
        /// Word data를 작성한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteWord(string add, short data)
        {
            if (!GetParams(add, out XgMemAdd xgAdd))
            {
                _errMsg = $"Fail to parse address={add}";
                return false;
            }

            uint res = _plc.WriteDataWord(xgAdd.DeviceType, xgAdd.Offset, 1, false, new ushort[] { (ushort)data });
            if (res == (uint)XGCOMM_FUNC_RESULT.RT_XGCOMM_SUCCESS)
            {
                _errMsg = "";
                return true;
            }
            else
            {
                _errMsg = _plc.GetReturnCodeString(res);
                data = -1;
                return false;
            }
        }
        #endregion

        #region 메소드
        /// <summary>
        /// word/byte 파라미터를 반환한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="devType"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static bool GetParams(string add, out XgMemAdd xgAdd)
        {
            if (add.Length < 2)
            {
                xgAdd = null;
                return false;
            }

            if (long.TryParse(add.Substring(1), out long offset))
            {
                xgAdd = new XgMemAdd
                {
                    Offset = offset,
                    DeviceType = add[0]
                };
                return true;
            }
            else
            {
                xgAdd = null;
                return false;
            }
        }

        /// <summary>
        /// Bit 주소 변환
        /// </summary>
        /// <param name="add"></param>
        /// <param name="xgAdd"></param>
        /// <returns></returns>
        private static bool GetBitParams(string add, out XgMemAdd xgAdd)
        {
            string[] str = add.Split('.');
            if (add.Length < 3)
            {
                xgAdd = null;
                return false;
            }

            if (str.Length == 2)
            {
                // W0123.3
                if (!long.TryParse(str[0].Substring(1), out long offset))
                {
                    xgAdd = null;
                    return false;
                }
                if (!int.TryParse(str[1], out int bitNum))
                {
                    xgAdd = null;
                    return false;
                }
                xgAdd = new XgMemAdd
                {
                    DeviceType = str[0][0],
                    Offset = offset,
                    BitNumber = bitNum,
                    WordBitData = true
                };
                return true;
            }
            else
            {
                // M0123F
                if (!long.TryParse(add.Substring(1, add.Length - 2), out long offset))
                {
                    xgAdd = null;
                    return false;
                }

                if (!int.TryParse(add.Substring(add.Length - 1), System.Globalization.NumberStyles.HexNumber, null, out int bitNum))
                {
                    xgAdd = null;
                    return false;
                }

                xgAdd = new XgMemAdd
                {
                    DeviceType = add[0],
                    Offset = offset,
                    BitNumber = bitNum,
                    WordBitData = false
                };
                return true;
            }
        }
        #endregion
    }
}
