using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using S7.Net;

namespace SmaPlc
{
    /// <summary>
    /// 지멘스 S7 PLC 제어
    /// </summary>
    public class SiemensS7 : PlcBase
    {
        /// <summary>
        /// S7 연결 파라미터
        /// </summary>
        public class ConParams
        {
            /// <summary>
            /// CPU 타입
            /// </summary>
            public CpuType Cpu;
            /// <summary>
            /// IP 주소
            /// </summary>
            public IPAddress Ip;
            /// <summary>
            /// Rack 넘버
            /// </summary>
            public short Rack;
            /// <summary>
            /// Slot 넘버
            /// </summary>
            public short Slot;
        }

        /// <summary>
        /// PLC object
        /// </summary>
        private Plc _plc = null;

        #region 통신 제어

        /// <summary>
        /// 파라미터 변경
        /// </summary>
        /// <param name="conParams"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool ConvertOpenParameter(string conParams, out object item)
        {
            item = null;
            if (conParams == "")
            {
                _errMsg = "{CPU type (enum): S7200|S7300|...}, {IP Address}, {Rack (short)}, {Slot (short)}";
                return false;
            }

            string[] strs = conParams.Split(',');
            if (strs.Length != 4)
            {
                _errMsg = "insufficient plc parameters";
                return false;
            }

            var cp = new ConParams();

            if (!Enum.TryParse(strs[0], out CpuType ct))
            {
                _errMsg = $"Cannot try parse CPU type: {strs[0]}";
                return false;
            }
            cp.Cpu = ct;

            if (!IPAddress.TryParse(strs[1], out IPAddress ip))
            {
                _errMsg = $"Cannot try parse IP Address: {strs[0]}";
                return false;
            }
            cp.Ip = ip;

            if (!short.TryParse(strs[2], out short rack))
            {
                _errMsg = $"Cannot try parse rack number: {strs[2]}";
                return false;
            }
            cp.Rack = rack;

            if (!short.TryParse(strs[3], out short slot))
            {
                _errMsg = $"Cannot try parse slot number: {strs[2]}";
                return false;
            }
            cp.Slot = slot;
            return true;
        }

        /// <summary>
        /// 통신 연결
        /// </summary>
        /// <param name="conParams"></param>
        /// <returns></returns>
        public override bool Open(string conParams)
        {
            if (!ConvertOpenParameter(conParams, out object item))
            {
                return false;
            }
            ConParams cp = (ConParams)item;
            
            try
            {
                _plc = new Plc(cp.Cpu, cp.Ip.ToString(), cp.Rack, cp.Slot);
                _plc.Open();
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
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
            try
            {
                data = (bool)_plc.Read(add.ToUpper());
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
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
                ushort udata = (ushort)_plc.Read(add.ToUpper());
                data = (short)udata;
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
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
                uint udata = (uint)_plc.Read(add.ToUpper());
                data = (int)_plc.Read(add.ToUpper());
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
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
                Parse(startAdd.ToUpper(), out DataType dt, out int dbNum, out VarType vt, out int add, out int bitNum);
                data = _plc.ReadBytes(dt, dbNum, add, len);
                return true;
            }
            catch (PlcException pe)
            {
                data = new byte[0];
                _errMsg = pe.Message;
                return false;
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
            try
            {
                _plc.Write(add.ToUpper(), data);
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
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
            try
            {
                _plc.Write(add.ToUpper(), data);
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
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
                Parse(startAdd.ToUpper(), out DataType dt, out int dbNum, out VarType vt, out int add, out int bitNum);
                for (int i = 0; i < data.Length; i++)
                {
                    _plc.Write(dt, dbNum, add + i * 4, data[i]);
                }

                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// DWord 데이터를 전송한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteDWord(string add, int data)
        {
            try
            {
                _plc.Write(add.ToUpper(), data);
                return true;
            }
            catch (PlcException pe)
            {
                _errMsg = pe.Message;
                return false;
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
        /// 주소를 parse 한다
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dataType"></param>
        /// <param name="dbNumber"></param>
        /// <param name="varType"></param>
        /// <param name="startAdr"></param>
        /// <param name="bitNumber"></param>
        public static void Parse(string input, out DataType dataType, out int dbNumber, out VarType varType, out int startAdr, out int bitNumber)
        {
            bitNumber = -1;
            dbNumber = 0;

            switch (input.Substring(0, 2))
            {
                case "DB":
                    string[] strings = input.Split(new char[] { '.' });
                    if (strings.Length < 2)
                        throw new InvalidAddressException("To few periods for DB address");

                    dataType = DataType.DataBlock;
                    dbNumber = int.Parse(strings[0].Substring(2));
                    startAdr = int.Parse(strings[1].Substring(3));

                    string dbType = strings[1].Substring(0, 3);
                    switch (dbType)
                    {
                        case "DBB":
                            varType = VarType.Byte;
                            return;
                        case "DBW":
                            varType = VarType.Word;
                            return;
                        case "DBD":
                            varType = VarType.DWord;
                            return;
                        case "DBX":
                            bitNumber = int.Parse(strings[2]);
                            if (bitNumber > 7)
                                throw new InvalidAddressException("Bit can only be 0-7");
                            varType = VarType.Bit;
                            return;
                        default:
                            throw new InvalidAddressException();
                    }
                case "IB":
                case "EB":
                    // Input byte
                    dataType = DataType.Input;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "IW":
                case "EW":
                    // Input word
                    dataType = DataType.Input;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "ID":
                case "ED":
                    // Input double-word
                    dataType = DataType.Input;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                case "QB":
                case "AB":
                case "OB":
                    // Output byte
                    dataType = DataType.Output;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "QW":
                case "AW":
                case "OW":
                    // Output word
                    dataType = DataType.Output;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "QD":
                case "AD":
                case "OD":
                    // Output double-word
                    dataType = DataType.Output;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                case "MB":
                    // Memory byte
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Byte;
                    return;
                case "MW":
                    // Memory word
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.Word;
                    return;
                case "MD":
                    // Memory double-word
                    dataType = DataType.Memory;
                    dbNumber = 0;
                    startAdr = int.Parse(input.Substring(2));
                    varType = VarType.DWord;
                    return;
                default:
                    switch (input.Substring(0, 1))
                    {
                        case "E":
                        case "I":
                            // Input
                            dataType = DataType.Input;
                            varType = VarType.Bit;
                            break;
                        case "Q":
                        case "A":
                        case "O":
                            // Output
                            dataType = DataType.Output;
                            varType = VarType.Bit;
                            break;
                        case "M":
                            // Memory
                            dataType = DataType.Memory;
                            varType = VarType.Bit;
                            break;
                        case "T":
                            // Timer
                            dataType = DataType.Timer;
                            dbNumber = 0;
                            startAdr = int.Parse(input.Substring(1));
                            varType = VarType.Timer;
                            return;
                        case "Z":
                        case "C":
                            // Counter
                            dataType = DataType.Counter;
                            dbNumber = 0;
                            startAdr = int.Parse(input.Substring(1));
                            varType = VarType.Counter;
                            return;
                        default:
                            throw new InvalidAddressException(string.Format("{0} is not a valid address", input.Substring(0, 1)));
                    }

                    string txt2 = input.Substring(1);
                    if (txt2.IndexOf(".") == -1)
                        throw new InvalidAddressException("To few periods for DB address");

                    startAdr = int.Parse(txt2.Substring(0, txt2.IndexOf(".")));
                    bitNumber = int.Parse(txt2.Substring(txt2.IndexOf(".") + 1));
                    if (bitNumber > 7)
                        throw new InvalidAddressException("Bit can only be 0-7");
                    return;
            }
        }
        #endregion
    }
}
