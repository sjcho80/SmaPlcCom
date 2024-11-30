using System;
using System.Linq;
using System.Text.RegularExpressions;

using S7.Net;


namespace PlcCom
{
    /// <summary>
    /// PLC 통신 제어 프로그램: 지멘스 S7을 기준으로 작성
    /// </summary>
    public class PlcCtrl
    {
        #region 변수선언
        /// <summary>
        /// 마지막 에러
        /// </summary>
        protected string _lastError { get; set; }

        /// <summary>
        /// 마지막 에러 메시지를 반환한다
        /// </summary>
        public string GetLastError { get { return _lastError; } }

        /// <summary>
        /// S7 PLC Object
        /// </summary>
        private Plc _plc = null;
        #endregion

        #region 통신 제어 함수
        /// <summary>
        /// 통신을 open 한다
        /// </summary>
        /// <returns></returns>
        public bool Open(string ipAdd, CpuType ct = CpuType.S71500, Int16 rack = 0, Int16 slot = 0)
        {
            try
            {
                _plc = new Plc(ct, ipAdd, rack, slot);
                _plc.Open();
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 통신을 종료 한다
        /// </summary>
        public void Close()
        {
            if (_plc != null)
            {
                _plc.Close();
            }
        }
        #endregion

        #region Read 함수
        /// <summary>
        /// 주소의 word 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadWord(string add, out ushort data)
        {
            data = 0;
            try
            {
                data = (ushort)_plc.Read(add.ToUpper());
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 주소의 word 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadDWord(string add, out uint data)
        {
            data = 0;
            try
            {
                data = (uint)_plc.Read(add.ToUpper());
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 주소의 bit 데이터를 읽어온다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadBit(string add, out bool data)
        {
            data = false;
            try
            {
                data = (bool)_plc.Read(add.ToUpper());
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Stat address에서 length 만큼 바이트 단위로 읽어온다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="len"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadBytes(string startAdd, int len, out byte[] data)
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
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                data = new byte[0];
                _lastError = ex.Message;
                return false;
            }
        }
        #endregion

        #region Write 함수
        /// <summary>
        /// 주소의 bit 데이터를 쓴다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteBit(string add, bool data)
        {
            try
            {
                _plc.Write(add.ToUpper(), data);
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 주소에 데이터를 작성한다
        /// </summary>
        /// <param name="add"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteWord(string add, short data)
        {
            try
            {
                _plc.Write(add.ToUpper(), data);
                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Word 데이터를 작성한다
        /// </summary>
        /// <param name="startAdd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteWords(string startAdd, int[] data)
        {
            try
            {
                //byte[] byData = new byte[data.Length * 2]; // 32비트->16비트 데이터로 변경하여 전송한다
                //for (int i = 0; i < data.Length; i++)
                //{
                //    int nData = data[i];
                //    byData[i * 2] = (byte)(nData & 0xFFFF);             // udata
                //    byData[i * 2 + 1] = (byte)((nData >> 16) & 0xFFFF); // ldata
                //}

                //Parse(startAdd.ToUpper(), out DataType dt, out int dbNum, out VarType vt, out int add, out int bitNum);
                //_plc.WriteBytes(dt, dbNum, add, byData);

                Parse(startAdd.ToUpper(), out DataType dt, out int dbNum, out VarType vt, out int add, out int bitNum);
                for (int i = 0; i < data.Length; i++)
                {
                    _plc.Write(dt, dbNum, add + i * 4, data[i]);
                }

                return true;
            }
            catch (PlcException pe)
            {
                _lastError = pe.Message;
                return false;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
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
