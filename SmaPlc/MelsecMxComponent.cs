using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ActUtlTypeLib;

namespace SmaPlc
{
    /// <summary>
    /// MELSEC MX Component를 이용하여 PLC와 통신
    /// </summary>
    public class MelsecMxComponent : PlcBase
    {
        /// <summary>
        /// PLC unit
        /// </summary>
        private ActUtlType _plc = null;

        #region 통신 제어
        /// <summary>
        /// 통신 오픈
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool Open(string parameter)
        {
            if (!ConvertOpenParameter(parameter, out object item))
            {
                _errMsg = $"Cannot parse station number: {parameter}";
                _errCode = -1;
                return false;
            }
            int stationNum = (int)item;

            try
            {
                _plc = new ActUtlType();
                _plc.ActLogicalStationNumber = stationNum;

                _errCode = _plc.Open();
                if (_errCode == 0)
                {
                    return true;
                }
                else
                {
                    _errMsg = $"Cannot connect logicstation {stationNum}: return code {_errCode}";
                    return false;
                }
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

        /// <summary>
        /// 연결 파라미터를 변환한다
        /// </summary>
        /// <param name="conParams"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ConvertOpenParameter(string conParams, out object item)
        {
            item = null;
            if (conParams == "")
            {
                _errMsg = "{Station number(int)}";
                return false;
            }

            if (!int.TryParse(conParams, out int stationNum))
            {
                _errMsg = "Cannot convert connection parameter";
                return false;
            }
            item = (object)stationNum;
            return true;
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
            try
            {
                _errCode = _plc.ReadDeviceRandom(add, 1, out int nData);
                if (_errCode != 0)
                {
                    _errMsg = $"ReadBit failure: address={add}, return code={_errCode}";
                    return false;
                }
                data = nData > 0;
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
                _errCode = _plc.ReadDeviceRandom(add, 1, out int nData);
                if (_errCode != 0)
                {
                    _errMsg = $"ReadWord failure: address={add}, return code={_errCode}";
                    return false;
                }
                data = (short)nData;
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
                _errCode = _plc.ReadDeviceRandom(add, 1, out int nData);
                if (_errCode != 0)
                {
                    _errMsg = $"ReadDWord failure: address={add}, return code={_errCode}";
                    return false;
                }
                data = (int)nData;
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
            try
            {
                int dataLen = len / 2;
                short[] d = new short[dataLen];
                _errCode = _plc.ReadDeviceBlock2(startAdd, dataLen, out d[0]);

                if (_errCode != 0)
                {
                    _errMsg = $"ReadBit failure: address={startAdd}, return code={_errCode}";
                    data = null;
                    return false;
                }

                List<byte> ls = new List<byte>();
                for (int i = 0; i < d.Length; i++)
                {
                    byte[] b = BitConverter.GetBytes(d[i]);
                    ls.AddRange(b);
                }
                data = ls.ToArray();
                return true;
            }
            catch (Exception ex)
            {
                data = new byte[0];
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
            int nData;
            if (data)
            {
                nData = 1;
            }
            else
            {
                nData = 0;
            }
            return WriteDevice(add, nData);
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
                _errCode = _plc.WriteDeviceBlock(startAdd, d16.Length, ref d16[0]);
                if (_errCode != 0)
                {
                    _errMsg = $"WriteDWords failure: start address={startAdd}, return code={_errCode}";
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
                _errCode = _plc.WriteDeviceRandom(add.ToUpper(), 1, data);
                if (_errCode != 0)
                {
                    _errMsg = $"WriteWord failure: address={add}, retrun code={_errCode}";
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
    }
}
