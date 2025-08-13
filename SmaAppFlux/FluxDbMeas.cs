using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmaCtrl;

using C1.Win.C1FlexGrid;
using System.Data.SQLite;

namespace SmaFlux
{
    /// <summary>
    /// 측정 데이터 관리 DB 클래스
    /// </summary>
    public class FluxDbMeas
    {
        #region 타입 정의
        /// <summary>
        /// OK/NG
        /// </summary>
        public enum OkNg
        {
            Ng,
            Ok,
        }

        /// <summary>
        /// 측정 DB 검색 조건
        /// </summary>
        [Serializable]
        public class MeasConds
        {
            #region 변수 선언
            /// <summary>
            /// 검색 시작 날짜
            /// </summary>
            public DateTime StartDate { get; set; }

            /// <summary>
            /// 검색 시작 날짜 설정
            /// </summary>
            public bool EnableStartDate { get; set; } = false;

            /// <summary>
            /// 검색 종료 날짜
            /// </summary>
            public DateTime EndDate { get; set; }

            /// <summary>
            /// 검색 종료 날짜 설정
            /// </summary>
            public bool EnableEndDate { get; set; } = false;

            /// <summary>
            /// 페이지 넘버
            /// </summary>
            public int PageNum { get; set; } = 0;

            /// <summary>
            /// 페이지 당 아이템 수
            /// </summary>
            public int PageSize { get; set; } = 100;

            /// <summary>
            /// 오늘 측정한 아이템만 출력
            /// </summary>
            public bool TodayOnly { get; set; } = false;

            /// <summary>
            /// 최소 DB ID
            /// </summary>
            public int MaxItems { get; set; } = 0;

            /// <summary>
            /// 모델 넘버 리스트
            /// </summary>
            public int[] ModelNumbers { get; set; } = new int[] { };
            #endregion

            #region 메소드
            /// <summary>
            /// 기본 설정 파일 경로
            /// </summary>
            public static string FilePath
            {
                get
                {
                    return Path.Combine(BaseDir, @"MeasConds.xml");
                }
            }

            /// <summary>
            /// 파일을 로드한다
            /// </summary>
            /// <param name="tc"></param>
            /// <param name="errMsg"></param>
            public static bool Load(out MeasConds tc, out string errMsg)
            {
                return SmaSettings.LoadXml(FilePath, out tc, out errMsg);
            }

            /// <summary>
            /// 파일을 저장한다
            /// </summary>
            /// <param name="errMsg"></param>
            public bool Save(out string errMsg)
            {
                return SmaSettings.SaveXml(this, FilePath, out errMsg);
            }
            #endregion
        }

        /// <summary>
        /// 대쉬보드 검색 조건
        /// </summary>
        [Serializable]
        public class DashConds
        {
            #region 타입 정의
            /// <summary>
            /// 데이터 그룹 조건
            /// </summary>
            public enum GroupConds
            {
                None,
                StartOfDay,
                StartOfMonth,
                StartOfYear,
            }
            #endregion

            #region 변수 선언

            /// <summary>
            /// 검색 시작 날짜
            /// </summary>
            public DateTime StartDate { get; set; } = DateTime.Now;

            /// <summary>
            /// 검색 시작 설정
            /// </summary>
            public bool StartEnable { get; set; } = false;

            /// <summary>
            /// 검색 종료 날짜
            /// </summary>
            public DateTime EndDate { get; set; } = DateTime.Now;

            /// <summary>
            /// 검색 종료 설정
            /// </summary>
            public bool EndEnable { get; set; } = false;

            /// <summary>
            /// 모델
            /// </summary>
            public int[] ModelNumbers { get; set; } = new int[] { };

            /// <summary>
            /// 그룹 조건
            /// </summary>
            public GroupConds GroupCond { get; set; } = GroupConds.None;

            /// <summary>
            /// 음의 피크값 출력
            /// </summary>
            public bool NegativePeak { get; set; } = false;

            /// <summary>
            /// 총 피크값 출력
            /// </summary>
            public bool TotalPeak { get; set; } = false;

            /// <summary>
            /// 양의 피크값 출력
            /// </summary>
            public bool PositivePeak { get; set; } = false;

            #endregion

            #region 메소드
            /// <summary>
            /// 기본 설정 파일 경로
            /// </summary>
            public static string FilePath
            {
                get
                {
                    return Path.Combine(BaseDir, @"DashConds.xml");
                }
            }

            /// <summary>
            /// 파일을 로드한다
            /// </summary>
            /// <param name="tc"></param>
            /// <param name="errMsg"></param>
            public static bool Load(out DashConds tc, out string errMsg)
            {
                return SmaSettings.LoadXml(FilePath, out tc, out errMsg);
            }

            /// <summary>
            /// 파일을 저장한다
            /// </summary>
            /// <param name="errMsg"></param>
            public bool Save(out string errMsg)
            {
                return SmaSettings.SaveXml(this, FilePath, out errMsg);
            }
            /// <summary>
            /// 히스토그램 데이터를 읽어온다
            /// </summary>
            /// <param name="dbPath"></param>
            /// <param name="dc"></param>
            /// <param name="dt"></param>
            /// <param name="query"></param>
            /// <param name="errMsg"></param>
            /// <returns></returns>
            public static bool LoadHistogramData(string dbPath, DashConds dc, out DataTable dt, out string sql, out string errMsg)
            {
                // 파일 존재 여부 체크
                if (!File.Exists(dbPath))
                {
                    sql = "";
                    errMsg = $"Db file={dbPath}가 존재하지 않습니다.";
                    dt = null;
                    return false;
                }

                // 데이터 로드
                string connStr = $@"Data Source={dbPath};mode=readonly;";
                dt = new DataTable();
                sql = "";
                try
                {
                    bool allPeaks = !dc.NegativePeak & !dc.TotalPeak & !dc.PositivePeak;
                    List<string> peaks = new List<string>();
                    if (dc.NegativePeak || allPeaks)
                    {
                        peaks.Add("negPeak");
                    }
                    if (dc.TotalPeak || allPeaks)
                    {
                        peaks.Add("totalPeak");
                    }
                    if (dc.PositivePeak || allPeaks)
                    {
                        peaks.Add("posPeak");
                    }
                    sql = $"SELECT {string.Join(",", peaks.ToArray())} FROM measurement WHERE";

                    // 시작 날짜
                    if (dc.StartEnable)
                    {
                        sql += $" labdate >= date('{dc.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else
                    {
                        sql += $" TRUE";
                    }

                    // 종료 날짜
                    if (dc.EndEnable)
                    {
                        sql += $" AND labdate < date('{dc.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else
                    {
                        sql += $" AND TRUE";
                    }

                    // 모델
                    if (dc.ModelNumbers.Length > 0)
                    {
                        sql += $" AND profileNum IN ({string.Join(",", dc.ModelNumbers.Select(n => n.ToString()).ToArray())})";
                    }

                    using (var conn = new SQLiteConnection(connStr))
                    {
                        conn.Open();
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
            /// 트래킹 데이터를 읽어온다
            /// </summary>
            /// <param name="dbPath"></param>
            /// <param name="dc"></param>
            /// <param name="dt"></param>
            /// <param name="query"></param>
            /// <param name="errMsg"></param>
            /// <returns></returns>
            public static bool LoadTrackingData(string dbPath, DashConds dc, out DataTable dt, out string query, out string errMsg)
            {
                // 파일 존재 여부 체크
                if (!File.Exists(dbPath))
                {
                    query = "";
                    errMsg = $"Db file={dbPath}가 존재하지 않습니다.";
                    dt = null;
                    return false;
                }

                // 데이터 로드
                string connStr = $@"Data Source={dbPath};mode=readonly;";
                dt = new DataTable();
                string sql = "";
                try
                {
                    bool allPeaks = !dc.NegativePeak & !dc.TotalPeak & !dc.PositivePeak;
                    List<string> peaks = new List<string>();
                    if (dc.NegativePeak || allPeaks)
                    {
                        if (dc.GroupCond == GroupConds.None)
                        {
                            peaks.Add("negPeak as npavg");
                        }
                        else
                        {
                            peaks.Add("avg(negPeak) as npavg");
                            //peaks.Add("max(negPeak) as npmax");
                            //peaks.Add("min(negPeak) as npmin");
                        }
                    }
                    if (dc.TotalPeak || allPeaks)
                    {
                        if (dc.GroupCond == GroupConds.None)
                        {
                            peaks.Add("totalPeak as tpavg");
                        }
                        else
                        {
                            peaks.Add("avg(totalPeak) as tpavg");
                            //peaks.Add("max(totalPeak) as tpmax");
                            //peaks.Add("min(totalPeak) as tpmin");
                        }
                    }
                    if (dc.PositivePeak || allPeaks)
                    {
                        if (dc.GroupCond == GroupConds.None)
                        {
                            peaks.Add("posPeak as ppavg");
                        }
                        else
                        {
                            peaks.Add("max(posPeak) as ppmax");
                            //peaks.Add("avg(posPeak) as ppavg");
                            //peaks.Add("min(posPeak) as ppmin");
                        }
                    }
                    sql = $"SELECT labdate, {string.Join(",", peaks.ToArray())} FROM measurement WHERE";

                    // 시작 날짜
                    if (dc.StartEnable)
                    {
                        sql += $" labdate >= date('{dc.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else
                    {
                        sql += $" TRUE";
                    }

                    // 종료 날짜
                    if (dc.EndEnable)
                    {
                        sql += $" AND labdate < date('{dc.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else
                    {
                        sql += $" AND TRUE";
                    }

                    // 모델
                    if (dc.ModelNumbers.Length > 0)
                    {
                        sql += $" AND profileNum IN ({string.Join(",", dc.ModelNumbers.Select(n => n.ToString()).ToArray())})";
                    }

                    // 그룹
                    if (dc.GroupCond == GroupConds.StartOfDay)
                    {
                        sql += " GROUP by date(labdate, 'start of day')";
                    }
                    else if (dc.GroupCond == GroupConds.StartOfMonth)
                    {
                        sql += " GROUP by date(labdate, 'start of month')";
                    }
                    else if (dc.GroupCond == GroupConds.StartOfYear)
                    {
                        sql += " GROUP by date(labdate, 'start of year')";
                    }

                    using (var conn = new SQLiteConnection(connStr))
                    {
                        conn.Open();
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, conn);
                        ad.Fill(dt);
                    }
                    query = sql;
                }
                catch (Exception ex)
                {
                    query = sql;
                    errMsg = ex.Message;
                    dt = null;
                    return false;
                }

                errMsg = "";
                return true;
            }
            #endregion
        }
        #endregion

        #region 변수선언
        #endregion

        #region 메소드
        /// <summary>
        /// 기본 디렉토리
        /// </summary>
        public static string BaseDir
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"SCMI\SMA FLUX\Database");
            }
        }

        /// <summary>
        /// 기본 DB 파일 경로
        /// </summary>
        public static string FilePath
        {
            get
            {
                return Path.Combine(BaseDir, @"sma flux meas.db");
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
                    string sql = "CREATE TABLE 'measurement' ("
                        + "'id'    INTEGER NOT NULL UNIQUE,"
                        + "'labdate'    TEXT,"
                        + "'modelNumber' INTEGER,"
                        + "'modelName'  TEXT,"
                        + "'negPeak'   REAL,"
                        + "'posPeak'   REAL,"
                        + "'totalPeak'   REAL,"
                        + "'posOk'   INTEGER,"
                        + "'negOk'   INTEGER,"
                        + "'totalOk'   INTEGER,"
                        + "'ok'   INTEGER,"
                        + "'barcode'  TEXT,"
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
        /// 측정 DB를 로드한다
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="sc"></param>
        /// <param name="dt"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool Load(string dbPath, MeasConds sc, out DataTable dt, out string errMsg)
        {
            // 파일 존재 여부 체크
            if (!File.Exists(dbPath))
            {
                errMsg = $"Db file={dbPath}가 존재하지 않습니다.";
                dt = null;
                return false;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 데이터 로드
            string connStr = $@"Data Source={dbPath};mode=readonly;";
            dt = new DataTable();
            try
            {
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    string sql = "SELECT * FROM measurement WHERE";

                    // 시작 날짜
                    string sDateCond, eDateCond;
                    if (sc.TodayOnly)
                    {
                        sDateCond = $"labdate >= date('{DateTime.Now.ToString("yyyy-MM-dd 00:00:00")}')";
                    }
                    else
                    {
                        if (sc.EnableStartDate)
                        {
                            sDateCond = $"labdate >= date('{sc.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                        }
                        else
                        {
                            sDateCond = "TRUE";
                        }
                    }
                    sql += " " + sDateCond;

                    // 종료 날짜
                    if (!sc.TodayOnly && sc.EnableEndDate)
                    {
                        eDateCond = $"labdate < date('{sc.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else
                    {
                        eDateCond = "TRUE";
                    }
                    sql += " AND " + eDateCond;

                    // 모델
                    if (sc.ModelNumbers.Length > 0)
                    {
                        sql += $" AND modelNumber IN ({string.Join(",", sc.ModelNumbers.Select(n => n.ToString()).ToArray())})";
                    }

                    // 순서 및 최대 아이템 수
                    sql += " ORDER BY ID DESC";

                    // SqlDataAdapter 초기화
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(sql, conn);
                    ad.Fill(sc.PageNum * sc.PageSize, sc.PageSize, dt);
                }
                sw.Stop();
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                dt = null;
                sw.Stop();
                return false;
            }

        }

        /// <summary>
        /// 측정 DB를 로드한다
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="sc"></param>
        /// <param name="dt"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool Delete(string dbPath, List<int> ids, out string errMsg)
        {
            // 파일 존재 여부 체크
            if (!File.Exists(dbPath))
            {
                errMsg = $"Db file={dbPath}가 존재하지 않습니다.";
                return false;
            }

            // 파일 존재 여부 체크
            if (ids.Count == 0)
            {
                errMsg = $"삭제할 id가 존재하지 않습니다.";
                return false;
            }

            // 데이터 로드
            string connStr = $@"Data Source={dbPath};";
            try
            {
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    string sql = $"Delete FROM measurement WHERE id IN ({string.Join(",", ids)})";
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
        /// DB에 아이템을 삽입한다
        /// </summary>
        /// <param name="md"></param>
        /// <param name="pd"></param>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        public bool InsertDb(FluxData md, DataTable dt, string dbPath, out string errMsg)
        {
            string strConn = $@"Data Source={dbPath}";
            SQLiteCommand cmd;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    conn.Open();

                    List<string> dbCols = new List<string>();
                    cmd = new SQLiteCommand("PRAGMA table_info(measurement);", conn);
                    var dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        string colName = dr["name"].ToString();
                        if (colName != "id")
                        {
                            dbCols.Add(colName);
                        }
                    }
                    dr.Close();

                    string sql = $"INSERT INTO measurement ({string.Join(",", dbCols)}) VALUES ({string.Join(",", dbCols.Select(x => "@" + x).ToArray())})";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@labdate", md.LabDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@modelNumber", md.ModelNumber);
                    cmd.Parameters.AddWithValue("@modelName", md.ModelName);
                    cmd.Parameters.AddWithValue("@posPeak", md.PosPeak);
                    cmd.Parameters.AddWithValue("@negPeak", md.NegPeak);
                    cmd.Parameters.AddWithValue("@totalPeak", md.TotalPeak);
                    cmd.Parameters.AddWithValue("@posOk", md.PosOk);
                    cmd.Parameters.AddWithValue("@negOk", md.NegOk);
                    cmd.Parameters.AddWithValue("@totalOk", md.TotalOk);
                    cmd.Parameters.AddWithValue("@ok", md.Ok);
                    foreach (DataRow r in dt.Rows)
                    {
                        if (dbCols.Exists(x => x == r["dbColumnName"].ToString()))
                        {
                            cmd.Parameters.AddWithValue("@" + r["dbColumnName"], r["data"]);
                        }
                    }
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
        /// Grid를 style 한다
        /// </summary>
        /// <param name="fg"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool SetFlexGridStyle(ref C1FlexGrid fg, DataTable dt)
        {
            var dict = Enum.GetValues(typeof(OkNg)).Cast<OkNg>().ToDictionary(x => (long)x, x => x.ToString());

            fg.BeginUpdate();
            fg.DataSource = dt;
            fg.Cols["labdate"].DataType = typeof(DateTime);
            fg.Cols["modelNumber"].Caption = "Model number";
            fg.Cols["modelName"].Caption = "Model name";
            fg.Cols["posPeak"].Caption = "Pos. peak";
            fg.Cols["negPeak"].Caption = "Neg. peak";
            fg.Cols["totalPeak"].Caption = "Total peak";
            fg.Cols["posPeak"].Format = "f2";
            fg.Cols["negPeak"].Format = "f2";
            fg.Cols["totalPeak"].Format = "f2";
            fg.Cols["posOk"].Caption = "Pos. OK";
            fg.Cols["posOk"].DataMap = new System.Collections.Hashtable(dict); ;
            fg.Cols["negOk"].Caption = "Neg. OK";
            fg.Cols["negOk"].DataMap = new System.Collections.Hashtable(dict); ;
            fg.Cols["totalOk"].Caption = "Total OK";
            fg.Cols["totalOk"].DataMap = new System.Collections.Hashtable(dict); ;
            fg.Cols["ok"].Caption = "OK";
            fg.Cols["ok"].DataMap = new System.Collections.Hashtable(dict); ;

            fg.Footers.Fixed = true;
            fg.Footers.Descriptions.Clear();
            fg.Footers.Descriptions.Add(new FooterDescription() { Caption = "MAX" });
            fg.Footers.Descriptions[0].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Max, Column = fg.Cols["posPeak"].Index });
            fg.Footers.Descriptions[0].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Max, Column = fg.Cols["negPeak"].Index });
            fg.Footers.Descriptions[0].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Max, Column = fg.Cols["totalPeak"].Index });
            fg.Footers.Descriptions.Add(new FooterDescription() { Caption = "MIN" });
            fg.Footers.Descriptions[1].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Min, Column = fg.Cols["posPeak"].Index });
            fg.Footers.Descriptions[1].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Min, Column = fg.Cols["negPeak"].Index });
            fg.Footers.Descriptions[1].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Min, Column = fg.Cols["totalPeak"].Index });
            fg.Footers.Descriptions.Add(new FooterDescription() { Caption = "CNT" });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Count, Column = fg.Cols["id"].Index });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.None, Column = fg.Cols["labdate"].Index, Caption = "Models" });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.CountDistinct, Column = fg.Cols["modelNumber"].Index });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Sum, Column = fg.Cols["posOk"].Index });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Sum, Column = fg.Cols["negOk"].Index });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Sum, Column = fg.Cols["totalOk"].Index });
            fg.Footers.Descriptions[2].Aggregates.Add(new AggregateDefinition() { Aggregate = AggregateEnum.Sum, Column = fg.Cols["ok"].Index });
            fg.AutoSizeCols();
            fg.EndUpdate();
            return true;
        }
        #endregion
    }
}
