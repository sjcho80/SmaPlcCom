using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaPlc;

namespace PlcComDlg
{
    /// <summary>
    /// 통신 데이터
    /// </summary>
    [Serializable]
    public class ComPlcData : PlcData
    {
        /// <summary>
        /// PLC Message
        /// </summary>
        public class ComMsg
        {
            /// <summary>
            /// 메시지 종류
            /// </summary>
            public enum MsgTypes
            {
                /// <summary>
                /// 정의되지 않음
                /// </summary>
                Non,
                /// <summary>
                /// 주소에 값을 설정한다
                /// </summary>
                SetValue,
                /// <summary>
                /// 바코드를 읽어온다
                /// </summary>
                GetProductInfor,
                /// <summary>
                /// 토글-측정 요청 비트
                /// </summary>
                ToggleMeasReq,
                /// <summary>
                /// 토글-측정 요청 응답 비트
                /// </summary>
                ToggleMeasReqResp,
                /// <summary>
                /// 토글-측정 완료 비트
                /// </summary>
                ToggleMeasFin,
                /// <summary>
                /// 토글-Ok
                /// </summary>
                ToggleOk,
                /// <summary>
                /// 토글-Ng
                /// </summary>
                ToggleNg,
                /// <summary>
                /// 토글-Busy
                /// </summary>
                ToggleBusy,
                /// <summary>
                /// 토글-Alarm
                /// </summary>
                ToggleAlarm,
                /// <summary>
                /// MES 데이터를 업로드 한다
                /// </summary>
                UploadMesData,
                /// <summary>
                /// MES 데이터를 클리어 한다
                /// </summary>
                ClearMesData,
            }

            /// <summary>
            /// 주소
            /// </summary>
            public string Address { get; set; } = "";

            /// <summary>
            /// 값
            /// </summary>
            public int Value { get; set; } = 0;

            /// <summary>
            /// 
            /// </summary>
            public MsgTypes MsgType { get; set; } = MsgTypes.SetValue;
        }

        /// <summary>
        /// 측정 시작 시간
        /// </summary>
        public DateTime MeasStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// DB ID
        /// </summary>
        public long DbId { get; set; } = 0L;

        /// <summary>
        /// Database pass/fail
        /// </summary>
        [Category("MES Results")]
        public long DbPass { get; set; } = 0L;

        /// <summary>
        /// Failed ID
        /// </summary>
        [Category("MES Results")]
        public long DbFailedId { get; set; } = 0L;

        /// <summary>
        /// MES에 업로드 된 데이터
        /// </summary>
        public DataTable MesData { get; set; }

        /// <summary>
        /// BUSY 기능
        /// </summary>
        public Flag BusyBit { get; set; } = new Flag();

        /// <summary>
        /// 측정요청 신호 응답
        /// </summary>
        public Flag MeasReqRespBit { get; set; } = new Flag();

        /// <summary>
        /// 플래그 초기화
        /// </summary>
        public void InitFlags()
        {
            InitBaseFlags();
            BusyBit.Initialized = true;
            MeasReqRespBit.Initialized = true;
            MesData = new DataTable();
            MesData.Columns.Add("name", typeof(string));
            MesData.Columns.Add("dataName", typeof(string));
            MesData.Columns.Add("dbData", typeof(object));
            MesData.Columns.Add("mesData", typeof(object));
            MesData.Columns.Add("address", typeof(string));
            MesData.Columns.Add("dataType", typeof(PlcMesInfors.MesDataTypes));
        }
    }
}
