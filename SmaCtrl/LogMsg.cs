using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaCtrl
{
    /// <summary>
    /// Log 메시지 클래스
    /// </summary>
    public class LogMsg
    {
        #region 타입 정의
        /// <summary>
        /// Log source
        /// </summary>
        public enum Sources
        {
            PLC,
            DB,
            TCP,
            APP
        }
        #endregion

        #region 변수선언
        /// <summary>
        /// Log 발생 시간
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 에러코드
        /// </summary>
        public int Code { get; set; } = -1;

        /// <summary>
        /// 에러메시지
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 로그
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 로그 소스
        /// </summary>
        public Sources LogSource { get; set; }
        #endregion

        /// <summary>
        /// 로그 텍스트를 생성한다
        /// </summary>
        /// <param name="lm"></param>
        /// <returns></returns>
        public static string GetLogTxt(LogMsg lm)
        {
            return $"{lm.EventTime.ToString("yy/MM/dd HH:mm:ss")} {lm.LogSource.ToString()}) {lm.Message}\r\n";
        }

        /// <summary>
        /// 에러 텍스트를 생성한다
        /// </summary>
        /// <param name="lm"></param>
        /// <returns></returns>
        public static string GetErrTxt(LogMsg lm)
        {
            return $"\t-({lm.Code}) {lm.Comment}\r\n";
        }

        /// <summary>
        /// 로그 파일 경로
        /// </summary>
        public static string DefaultFilePath(string folderName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $@"SCMI\{folderName}\log.db");
        }

        /// <summary>
        /// DB를 경로에 저장한다.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool CreateLogDb(string folderPath, out string errMsg)
        {
            try
            {
                string dir = Path.GetDirectoryName(folderPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                SQLiteConnection.CreateFile(folderPath);
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source=" + folderPath))
                {
                    conn.Open();
                    string sql = @"CREATE TABLE ""log"" (
                        ""id"" INTEGER NOT NULL UNIQUE,
                        ""eventDate"" TEXT,
	                    ""code"" INTEGER,
	                    ""source"" INTEGER,
                        ""message"" TEXT,
                        ""comment"" TEXT,
	                    PRIMARY KEY(""id"" AUTOINCREMENT)
                        );";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 오래된 로그를 삭제한다
        /// </summary>
        /// <param name="holdDays"></param>
        /// <param name="folderPath"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool DeleteOldLog(int holdDays, string folderPath, out string errMsg)
        {
            DateTime date = DateTime.Now - TimeSpan.FromDays(holdDays);
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source=" + folderPath))
                {
                    conn.Open();
                    string sql = $"DELETE FROM LOG WHERE date(eventDate) < date('{date.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 로그를 DB에 저장한다
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool SaveLogDb(LogMsg lm, string folderPath, out string errMsg)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source=" + folderPath))
                {
                    conn.Open();
                    string sql = $"INSERT INTO log (eventDate, code, source, message, comment) values (@p1, @p2, @p3, @p4, @p5);";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.Add(new SQLiteParameter("@p1", lm.EventTime.ToString("yyyy-MM-dd HH:mm:ss")));
                    cmd.Parameters.Add(new SQLiteParameter("@p2", lm.Code));
                    cmd.Parameters.Add(new SQLiteParameter("@p3", lm.LogSource));
                    cmd.Parameters.Add(new SQLiteParameter("@p4", lm.Message));
                    cmd.Parameters.Add(new SQLiteParameter("@p5", lm.Comment));
                    cmd.ExecuteNonQuery();
                }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 로그 데이터베이스를 로드한다.
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="maxItemDisp"></param>
        /// <param name="dt"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool LoadFromDb(string dbPath, int maxItemDisp, out DataTable dt, out string errMsg)
        {
            // 파일 존재 여부 체크
            if (!File.Exists(dbPath))
            {
                errMsg = $"Db file={dbPath}가 존재하지 않습니다.";
                dt = null;
                return false;
            }

            // 데이터 로드
            string connStr = $@"Data Source={dbPath};";
            dt = new DataTable();
            try
            {
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    string sql = $"SELECT * FROM log ORDER BY ID DESC LIMIT {maxItemDisp}";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, conn);
                    ad.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                dt = null;
                return false;
            }

            errMsg = "";
            return true;
        }
    }
}
