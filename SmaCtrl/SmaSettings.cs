using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmaCtrl
{
    public abstract class SmaSettings
    {
        #region 변수선언
        /// <summary>
        /// 최대 로그 라인 수
        /// </summary>
        [Category("[01] Log")]
        [DisplayName("Maximum lines")]
        [Description("로그 창에 출력되는 최대 줄 수")]
        [DefaultValue(100)]
        public int LogMaximumLine { get; set; } = 100;

        /// <summary>
        /// 최대 로그 라인 수
        /// </summary>
        [Category("[01] Log")]
        [DisplayName("Maximum db viewer items")]
        [Description("로그 뷰어 (리스트)에 출력되는 출력 아이템 수")]
        [DefaultValue(500)]
        public int LogMaximumDbItem { get; set; } = 500;

        /// <summary>
        /// 최대 로그 저장 수
        /// </summary>
        [Category("[01] Log")]
        [DisplayName("Maximum Db hold days")]
        [Description("로그 DB에 저장되는 아이템의 유지 기간")]
        [DefaultValue(0)]
        public int LogHoldDays { get; set; } = 0;

        /// <summary>
        /// 모든 이벤트 로그를 저장한다
        /// </summary>
        [Category("[01] Log")]
        [DisplayName("Save all log to db")]
        [Description("True: 모든 이벤트 로그 저장/false: error 로그만 저장")]
        [DefaultValue(true)]
        public bool SaveAllLogToDb { get; set; } = true;

        /// <summary>
        /// 자동 시작
        /// </summary>
        [Category("[00] Control")]
        [DisplayName("Auto start")]
        [DefaultValue(false)]
        [Description("프로그램 제어를 자동으로 시작")]
        public bool AutoStart { get; set; } = false;

        /// <summary>
        /// 측정 시작 전 MES 초기화
        /// </summary>
        [Category("[00] Control")]
        [DisplayName("Clear MES before measurement")]
        [DefaultValue(false)]
        [Description("측정 시작 전 MES 데이터를 초기화")]
        public bool ClearMesBeforeMeasurement { get; set; } = false;

        /// <summary>
        /// 통신 중 에러발생 회복 방법
        /// </summary>
        [Category("[00] Control")]
        [DisplayName("Recover PLC Communication error")]
        [DefaultValue(false)]
        [Description("True: PLC Ready 상태에서 오류 발생 시 PLC 연결 상태로 돌아감\r\n" +
            "False: PLC 연결 해제")]
        public bool GoToPlcConnectionDuringReady { get; set; } = false;
        #endregion

        #region 메소드
        /// <summary>
        /// XML 설정을 저장한다
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveXml<T>(T ss, string path, out string errMsg)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(ss.GetType());
                using (TextWriter writer = new StreamWriter(path))
                {
                    ser.Serialize(writer, ss);
                    writer.Close();
                }
                errMsg = $"{typeof(T).ToString()} is saved in {path}";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// XML 설정을 로드한다
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ss"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool LoadXml<T>(string path, out T ss, out string errMsg)
        {
            FileStream fs = null;
            bool res;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                fs = new FileStream(path, FileMode.Open);
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create(fs);
                ss = (T)serializer.Deserialize(reader);
                errMsg = $"{typeof(T).ToString()} is load from {path}";
                res = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                res = false;
                ss = default(T);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return res;
        }


        /// <summary>
        /// 기본 파일 경로
        /// </summary>
        /// <param name="appFolderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string DefaultFilePath(string appFolderName, string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $@"SCMI\{appFolderName}\{fileName}.Xml");
        }
        #endregion
    }
}
