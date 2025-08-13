using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C1.Win.C1FlexGrid;
using System.Data.SQLite;

namespace SmaFlux
{
    /// <summary>
    /// 플럭스미터 측정 프로파일
    /// </summary>
    public class FluxDbProfile
    {
        /// <summary>
        /// 기본 DB 경로
        /// </summary>
        public static string DefaultDbPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"SCMI\SMA FLUX\Database\sma flux profile.db");
            }
        }

        /// <summary>
        /// 1000: DB를 생성한다
        /// </summary>
        /// <returns></returns>
        public static bool CreateDb(string path, out string errMsg)
        {
            // DB 파일 생성
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                SQLiteConnection.CreateFile(path);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            // 테이블 및 column 생성
            string connStr = $@"Data Source={path}";
            try
            {
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string sql = "CREATE TABLE 'profile' ("
                        + "'id'    INTEGER NOT NULL UNIQUE,"
                        + "'name' TEXT,"
                        + "'modelNumber' INTEGER NOT NULL DEFAULT 0,"
                        + "'modelName' TEXT,"
                        + "'portName' TEXT NOT NULL DEFAULT 'COM0',"
                        + "'resetInt' INTEGER NOT NULL DEFAULT 0,"
                        + "'negPeakLoLim' REAL NOT NULL DEFAULT 0,"
                        + "'negPeakUpLim' REAL NOT NULL DEFAULT 250,"
                        + "'totalPeakLoLim' REAL NOT NULL DEFAULT 0,"
                        + "'totalPeakUpLim' REAL NOT NULL DEFAULT 250,"
                        + "'posPeakLoLim' REAL NOT NULL DEFAULT 0,"
                        + "'posPeakUpLim' REAL NOT NULL DEFAULT 250,"
                        + "'scale' REAL NOT NULL DEFAULT 1,"
                        + "'comment' TEXT,"
                        + "PRIMARY KEY('id' AUTOINCREMENT)"
                        + "); ";
                    var cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            errMsg = "";
            return true;
        }

        /// <summary>
        /// DB를 로드한다
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="dt"></param>
        /// <param name="errMsg"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static bool Load(string dbPath, out DataTable dt, out string errMsg)
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

                    string sql = "SELECT * FROM profile Order by id ASC";

                    // SqlDataAdapter 초기화
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


        /// <summary>
        /// DB를 저장한다
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="errMsg"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public static bool SaveDatabase(DataTable dt, out string errMsg, out int errCode)
        {
            string connStr = $@"Data Source={FluxDbProfile.DefaultDbPath}";
            string sql;
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connStr))
                {
                    con.Open();
                    sql = "SELECT * FROM profile";
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, con);
                    SQLiteCommandBuilder cb = new SQLiteCommandBuilder(ad);
                    ad.DeleteCommand = cb.GetDeleteCommand();
                    ad.UpdateCommand = cb.GetUpdateCommand();
                    ad.InsertCommand = cb.GetInsertCommand();
                    ad.Update(dt);

                    dt.Clear();
                    ad.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                errCode = 2003;
                return false;
            }

            errMsg = "";
            errCode = 0;
            return true;
        }

        /// <summary>
        /// Grid 스타일 설정
        /// </summary>
        /// <param name="fg"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool SetFlexGridStyle(ref C1FlexGrid fg, DataTable dt)
        {
            fg.BeginUpdate();
            fg.DataSource = dt;
            fg.Cols["id"].Visible = false;
            fg.Cols["name"].Caption = "Name";
            fg.Cols["modelNumber"].Caption = "Model number";
            fg.Cols["modelName"].Caption = "Model name";
            fg.Cols["portName"].Caption = "COM port";
            fg.Cols["negPeakLoLim"].Caption = "Negative -limit";
            fg.Cols["negPeakLoLim"].Format = "f2";
            fg.Cols["negPeakUpLim"].Caption = "Negative +limit";
            fg.Cols["negPeakUpLim"].Format = "f2";
            fg.Cols["totalPeakLoLim"].Caption = "Total -limit";
            fg.Cols["totalPeakLoLim"].Format = "f2";
            fg.Cols["totalPeakUpLim"].Caption = "Total +limit";
            fg.Cols["totalPeakUpLim"].Format = "f2";
            fg.Cols["posPeakLoLim"].Caption = "Positive -limit";
            fg.Cols["posPeakLoLim"].Format = "f2";
            fg.Cols["posPeakUpLim"].Caption = "Positive +limit";
            fg.Cols["posPeakUpLim"].Format = "f2";
            fg.Cols["scale"].Caption = "Scale";
            fg.Cols["scale"].Format = "f3";
            fg.Cols["comment"].Caption = "Comment";
            fg.AutoSizeCols();
            fg.EndUpdate();

            return true;
        }
    }
}
