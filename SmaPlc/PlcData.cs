using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SmaPlc
{
    /// <summary>
    /// PLC Data
    /// </summary>
    public class PlcData
    {
        #region 타입정의
        /// <summary>
        /// 플래그
        /// </summary>
        public class Flag
        {
            /// <summary>
            /// bit 이벤트
            /// </summary>
            public enum Events
            {
                /// <summary>
                /// 상승 이벤트
                /// </summary>
                Rising,
                /// <summary>
                /// 하강 이벤트
                /// </summary>
                Falling,
                /// <summary>
                /// On 상태
                /// </summary>
                On,
                /// <summary>
                /// Off 상태
                /// </summary>
                Off
            }

            private bool _oldBit { get; set; } = false;

            private bool _newBit { get; set; } = false;

            public bool Initialized { get; set; } = true;

            /// <summary>
            /// 주소
            /// </summary>
            public string Address { get; set; } = "";

            /// <summary>
            /// 측정 값
            /// </summary>
            public bool Bit {
                get
                {
                    if (Event == Events.On || Event == Events.Rising)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            /// <summary>
            /// 비트 이벤트를 반환한다
            /// </summary>
            public Events Event
            {
                get
                {
                    if (_oldBit == true & _newBit == true)
                    {
                        return Events.On;
                    }
                    else if (_oldBit == true & _newBit == false)
                    {
                        return Events.Falling;
                    }
                    else if (_oldBit == false & _newBit == true)
                    {
                        return Events.Rising;
                    }
                    else
                    {
                        return Events.Off;
                    }
                }
            }

            /// <summary>
            /// Bit 설정
            /// </summary>
            /// <param name="newBit"></param>
            public void SetBit(bool newBit)
            {
                if (Initialized)
                {
                    _oldBit = newBit;
                    _newBit = newBit;
                    Initialized = false;
                }
                else
                {
                    _oldBit = _newBit;
                    _newBit = newBit;
                }
            }
        }

        /// <summary>
        /// 모델 정보
        /// </summary>
        public class ModelNumber
        {
            /// <summary>
            /// 주소
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 측정 값
            /// </summary>
            public int Number { get; set; } = 0;
        }
        #endregion

        #region 변수 선언
        /// <summary>
        /// 하트비트
        /// </summary>
        public Flag HeartBeat { get; set; } = new Flag();

        /// <summary>
        /// 측정 요청
        /// </summary>
        public Flag MeasReqBit { get; set; } = new Flag();

        /// <summary>
        /// 측정 완료 비트
        /// </summary>
        public Flag MeasFinBit { get; set; } = new Flag();

        /// <summary>
        /// OK 비트
        /// </summary>
        public Flag OkBit { get; set; } = new Flag();

        /// <summary>
        /// NG 비트
        /// </summary>
        public Flag NgBit { get; set; } = new Flag();

        /// <summary>
        /// 알람 비트
        /// </summary>
        public Flag AlarmBit { get; set; } = new Flag();

        /// <summary>
        /// 모델 넘버
        /// </summary>
        public ModelNumber Model { get; set; } = new ModelNumber();

        /// <summary>
        /// 제품 정보 데이터 테이블
        /// </summary>
        public DataTable ProdData { get; set; }
        #endregion

        /// <summary>
        /// 초기화
        /// </summary>
        public PlcData()
        {
            ProdData = new DataTable();
            ProdData.Columns.Add("name", typeof(string));
            ProdData.Columns.Add("dataType", typeof(PlcProdInfors.DataTypes));
            ProdData.Columns.Add("startAddress", typeof(string));
            ProdData.Columns.Add("dbColumnName", typeof(string));
            ProdData.Columns.Add("data", typeof(object));
        }

        /// <summary>
        /// 플래그를 초기화 한다
        /// </summary>
        protected void InitBaseFlags()
        {
            AlarmBit.Initialized = true;
            OkBit.Initialized = true;
            NgBit.Initialized = true;
            MeasFinBit.Initialized = true;
            MeasReqBit.Initialized = true;
            HeartBeat.Initialized = true;
        }

        /// <summary>
        /// 제품정보를 읽어온다
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="pa"></param>
        /// <param name="pd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ReadProdInfor(PlcBase pb, List<PlcProdInfors> pa, PlcData pd, out string errMsg)
        {
            var arr = pa.GroupBy(x => x.DbColumnName).Where(g => g.Count() > 1);
            if (arr.Count() >= 1)
            {
                errMsg = $"Db column={string.Join(",", arr.Select(x => x.Key).ToArray())} already exists";
                return false;
            }

            pd.ProdData.Rows.Clear();
            foreach (var pi in pa.Where(x => x.Enable))
            {
                if (pi.DataType == PlcProdInfors.DataTypes.Word)
                {
                    if (pb.ReadWord(pi.StartAddress, out short data))
                    {
                        pd.ProdData.Rows.Add(pi.Name, pi.DataType, pi.StartAddress, pi.DbColumnName, data);
                    }
                    else
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (pi.DataType == PlcProdInfors.DataTypes.DWord)
                {
                    if (pb.ReadDWord(pi.StartAddress, out int data))
                    {
                        pd.ProdData.Rows.Add(pi.Name, pi.DataType, pi.StartAddress, pi.DbColumnName, data);
                    }
                    else
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else if (pi.DataType == PlcProdInfors.DataTypes.Text)
                {
                    if (pb.ReadBytes(pi.StartAddress, pi.TextLength, out byte[] data))
                    {
                        string msg = Encoding.Default.GetString(data) + Environment.NewLine;
                        pd.ProdData.Rows.Add(pi.Name, pi.DataType, pi.StartAddress, pi.DbColumnName, msg);
                    }
                    else
                    {
                        errMsg = pb.LastErrMsg;
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException("기능이 구현되지 않음");
                }
            }
            errMsg = "";
            return true;
        }
    }
}
