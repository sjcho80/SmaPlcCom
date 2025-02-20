using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcComDlg
{
    /// <summary>
    /// 통신 데이터
    /// </summary>
    [Serializable]
    public class PlcData
    {
        /// <summary>
        /// 주요 동작 flags
        /// </summary>
        public class CtrlFlags
        {
            /// <summary>
            /// PLC 측정 요청
            /// </summary>
            public bool MeasReq { get; set; } = false;

            /// <summary>
            /// PLC 측정 요청
            /// </summary>
            public bool MeasReqOld { get; set; } = false;

            /// <summary>
            /// PLC 측정 요청
            /// </summary>
            public bool MeasReqResp { get; set; } = false;

            /// <summary>
            /// PLC 측정 요청
            /// </summary>
            public bool MeasFin { get; set; } = false;

            /// <summary>
            /// PLC 하트비트
            /// </summary>
            public bool HeartBeat { get; set; } = false;

            /// <summary>
            /// PLC OK 값
            /// </summary>
            public bool Ok { get; set; } = false;

            /// <summary>
            /// PLC NG 값
            /// </summary>
            public bool Ng { get; set; } = false;

            /// <summary>
            /// PLC BUSY 값
            /// </summary>
            public bool Busy { get; set; } = false;

            /// <summary>
            /// PLC Alarm 값
            /// </summary>
            public bool Alarm { get; set; } = false;
        }

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
                /// 주소에 값을 설정한다
                /// </summary>
                SetValue,
                /// <summary>
                /// 바코드를 읽어온다
                /// </summary>
                GetProductInfor,
                /// <summary>
                /// 모델 넘버를 읽어온다
                /// </summary>
                GetModelNumber,
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
        /// MES 데이텀
        /// </summary>
        public class MesDatum
        {
            /// <summary>
            /// 이름
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 측정값
            /// </summary>
            public double MeasValue { get; set; }

            /// <summary>
            /// MES 업로드 값
            /// </summary>
            public long MesValue { get; set; }
        }

        public CtrlFlags Flags = new CtrlFlags();

        /// <summary>
        /// PLC Model number
        /// </summary>
        public int PlcModelNumber { get; set; } = 0;

        /// <summary>
        /// 측정 시작 시간
        /// </summary>
        public DateTime MeasStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 측정완료
        /// </summary>
        public bool MeasFinFromTcp { get; set; } = false;

        /// <summary>
        /// 바코드
        /// </summary>
        public string Barcode { get; set; } = "";

        /// <summary>
        /// DB ID
        /// </summary>
        public long DbId { get; set; } = 0L;

        /// <summary>
        /// 제품 정보 값
        /// </summary>
        public List<object> ProductInforVals = new List<object>();

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
        /// Database MES 업로드 값
        /// </summary>
        [Category("MES Results")]
        public List<MesDatum> DbMesVals { get; set; } = new List<MesDatum>();
    }
}
