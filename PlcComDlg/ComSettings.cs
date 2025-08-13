using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;

using SmaPlc;
using SmaCtrl;

namespace PlcComDlg
{
    /// <summary>
    /// 통신설정 클래스
    /// </summary>
    [Serializable]
    public class ComSettings : SmaSettings
    {
        #region 타입정의

        /// <summary>
        /// 설정 DB 정보
        /// </summary>
        [Serializable]
        [TypeConverter(typeof(DbInforSettingsConverter))]
        public class DbInforSettings
        {
            /// <summary>
            /// 설정 DB descripter
            /// </summary>
            public class DbInforSettingsConverter : ExpandableObjectConverter
            {
                public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
                {
                    if (destType == typeof(string) && value is DbInforSettings)
                    {
                        DbInforSettings sdi = (DbInforSettings)value;

                        return $"{sdi.CondItems.Count} items, {sdi.Path}";
                    }
                    return base.ConvertTo(context, culture, value, destType);
                }
            }

            /// <summary>
            /// 설정 DB MES 업로드 아이템
            /// </summary>
            [Serializable]
            public class CondItemInf : PlcMesInfors
            {
                /// <summary>
                /// 텍스트로부터 1개의 정수를 읽어온다
                /// </summary>
                /// <param name="buffer"></param>
                /// <param name="format"></param>
                /// <param name="arg0"></param>
                /// <returns></returns>
                [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
                public static extern int sscanf(string buffer, string format, __arglist);

                /// <summary>
                /// 스캔하고자 하는 DB Id
                /// </summary>
                [Category("Information")]
                [Description("DB Id")]
                [DefaultValue(0)]
                public long Id { get; set; } = 0;

                /// <summary>
                /// Err
                /// </summary>
                [Category("Information")]
                [Description("Model number")]
                [DefaultValue(0)]
                public long ModelNumber { get; set; } = 0;


                /// <summary>
                /// 파라미터 순서 (쉼표로 구분됨)
                /// </summary>
                [Category("Control")]
                [Description("Parameter format")]
                [DefaultValue("SPM A MAX, %f")]
                public string ParameterFormat { get; set; } = "SPM A MAX, %lf";

                /// <summary>
                /// 파라미터 order
                /// </summary>
                [Category("Control")]
                [Description("Parameter order")]
                [DefaultValue(1)]
                public int ParameterOrder { get; set; } = 1;

                /// <summary>
                /// Parse parameter
                /// </summary>
                /// <param name="paramText"></param>
                /// <param name="paramVal"></param>
                /// <param name="errMsg"></param>
                /// <returns></returns>
                public bool ParseParam(string paramText, out double paramVal, out string errMsg)
                {
                    try
                    {
                        double arg0 = 0, arg1 = 0, arg2 = 0;
                        double[] args = new double[256];
                        if (ParameterOrder < 1 || ParameterOrder > 2)
                        {
                            throw new Exception($"파라미터 order 제한 {ParameterOrder}");
                        }
                        int res = sscanf(paramText, ParameterFormat, __arglist(ref arg0, ref arg1, ref arg2));

                        paramVal = -1;
                        if (ParameterOrder == 1)
                        {
                            paramVal = arg0;
                        }
                        else if (ParameterOrder == 2)
                        {
                            paramVal = arg1;
                        }
                        else if (ParameterOrder == 3)
                        {
                            paramVal = arg2;
                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg = $"Condition 텍스트 추출 실패 ({paramText}, {ParameterFormat}): {ex.Message}";
                        paramVal = 0;
                        return false;
                    }
                    errMsg = "";
                    return true;
                }
            }

            /// <summary>
            /// Db 경로
            /// </summary>
            [DisplayName("DB Path")]
            [Description("Settings database path")]
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            public string Path { get; set; } = "";

            /// <summary>
            /// MES 업로드 아이템
            /// </summary>
            [DisplayName("Upload items")]
            [Description("Settings database path")]
            public List<CondItemInf> CondItems { get; set; } = new List<CondItemInf>();
        }

        /// <summary>
        /// 측정 DB 정보
        /// </summary>
        [Serializable]
        [TypeConverter(typeof(DbInforMeasConverter))]
        public class DbInforMeas
        {
            /// <summary>
            /// 측정 DB descripter
            /// </summary>
            public class DbInforMeasConverter : ExpandableObjectConverter
            {
                public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
                {
                    if (destType == typeof(string) && value is DbInforMeas)
                    {
                        DbInforMeas sdi = (DbInforMeas)value;

                        return $"{sdi.MeasCols.Count} items, {sdi.Path}";
                    }
                    return base.ConvertTo(context, culture, value, destType);
                }
            }

            /// <summary>
            /// 설정 DB MES 업로드 아이템
            /// </summary>
            [Serializable]
            public class MeasColInf
            {
                /// <summary>
                /// 이름
                /// </summary>
                [Category("Information")]
                [Description("Column name")]
                [DefaultValue("")]
                public string Name { get; set; } = "";

                /// <summary>
                /// 활성화 여부
                /// </summary>
                [Category("Control")]
                [Description("활성화 여부")]
                [DefaultValue(false)]
                public bool Enable { get; set; } = false;

                /// <summary>
                /// 데이터 스케일
                /// </summary>
                [Category("Control")]
                [Description("Data scale")]
                [DefaultValue(1000)]
                public int Scale { get; set; } = 1000;

                /// <summary>
                /// 업로드 데이터 주소
                /// </summary>
                [Category("Control")]
                [Description("Address")]
                [DefaultValue("")]
                public string Address { get; set; } = "";
            }

            /// <summary>
            /// Db 경로
            /// </summary>
            [DisplayName("DB Path")]
            [Description("Settings database path")]
            [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
            public string Path { get; set; } = "";

            /// <summary>
            /// MES 업로드 아이템
            /// </summary>
            [DisplayName("MES upload items")]
            [Description("Settings database path")]
            public List<PlcMesInfors> MeasCols { get; set; } = new List<PlcMesInfors>();
        }
        
        
        #endregion

        #region Control
        /// <summary>
        /// SMA APP 이름
        /// </summary>
        [Category("[00] Control")]
        [DisplayName("Application name")]
        [Description("SMA 어플리케이션 이름")]
        public string SmaAppName { get; set; } = "sma-4100r";
        #endregion

        #region PLC
        /// <summary>
        /// PLC 설정
        /// </summary>
        [Category("[02] PLC")]
        [DisplayName("Control settings")]
        [Description("프로그램 제어 설정")]
        public ComPlcSettings PlcSettings { get; set; } = new ComPlcSettings();

        /// <summary>
        /// PLC flag address
        /// </summary>
        [Category("[02] PLC")]
        [DisplayName("Flag address")]
        [Description("시스템 제어 관련 flag 주소")]
        public ComPlcFlags PlcAddress { get; set; } = new ComPlcFlags();

        /// <summary>
        /// 모델넘버 주소
        /// </summary>
        [Category("[02] PLC")]
        [DisplayName("Model number address")]
        [Description("Model number가 기록된 주소 (Word)")]
        public string PlcAddModelNumber { get; set; } = "";

        /// <summary>
        /// PLC 데이터 목록
        /// </summary>
        [Category("[02] PLC")]
        [DisplayName("Product informatino list")]
        [Description("제품 정보 리스트")]
        public List<PlcProdInfors> ProductInfors { get; set; } = new List<PlcProdInfors>();
        #endregion

        #region Database
        /// <summary>
        /// Database settings 정보
        /// </summary>
        [Category("[04] Database")]
        [DisplayName("Settings database")]
        public DbInforSettings DbSettings { get; set; } = new DbInforSettings();

        /// <summary>
        /// Database settings 정보
        /// </summary>
        [Category("[04] Database")]
        [DisplayName("Measurement database")]
        public DbInforMeas DbMeas { get; set; } = new DbInforMeas();
        #endregion

        #region TCP
        /// <summary>
        /// SMA PC IP Address
        /// </summary>
        [Category("[03] TCP")]
        [DisplayName("Control")]
        public ComTcpSettings TcpSettings { get; set; } = new ComTcpSettings();
        #endregion


        #region 메소드
        /// <summary>
        /// 설정을 로드한다
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="cs"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool Load(string filePath, out ComSettings cs, out string errMsg)
        {
            return LoadXml(filePath, out cs, out errMsg);
        }
        #endregion
    }
}
