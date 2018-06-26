using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace AichiIryoKenpoHokenjigyo.Class
{
    public class C_KiisDatetime
    {
        public string Kiis_元号Code { get; set; }
        public string Kiis_年月日 { get; set; }
        public string Kiis_Datetime { get; set; }
        public string Datetime_JP { get; set; }
        public string Datetime_JP_Kani { get; set; }
        public DateTime TypeofDatetime { get; set; }
        public string Datetime_GR { get; set; }
        public string Datetime_GRwithSlash { get; set; }
        public string Datetime_JP_元号 { get; set; }




    }

    public static class DateTimeOperation
    {
        private static readonly int FiscalYearStartingMonth = 4;

        public static int GetCurrentYear()
        {
            try
            {
                var Today = DateTime.Now;

                if (Today.Month == 4)
                {

                    var DialogResult = MessageBox.Show($"{FiscalYear(Today)}年度で処理してもよろしいですか？", "処理年度の確認",MessageBoxButton.OKCancel);

                    if (DialogResult == MessageBoxResult.OK)
                    {
                        return FiscalYear(Today);
                    }

                    else
                    {
                        MessageBox.Show($"前年度分({FiscalYear(Today) - 1}年度）として処理します");

                        return FiscalYear(Today) - 1;
                    }
                }
                else
                {
                    return FiscalYear(Today);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 該当年月の日数を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static int DaysInMonth(this DateTime dt)
        {
            return DateTime.DaysInMonth(dt.Year, dt.Month);
        }

        /// <summary>
        /// 月初日を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>Datetime</returns>
        public static DateTime BeginOfMonth(this DateTime dt)
        {
            return dt.AddDays((dt.Day - 1) * -1);
        }

        /// <summary>
        /// 月末日を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DaysInMonth(dt));
        }

        /// <summary>
        /// 時刻を落として日付のみにする
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>DateTime</returns>
        public static DateTime StripTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        /// <summary>
        /// 日付を落として時刻のみにする
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="base_date">DateTime* : 基準日</param>
        /// <returns>DateTime</returns>
        public static DateTime StripDate(this DateTime dt, DateTime? base_date = null)
        {
            base_date = base_date ?? DateTime.MinValue;
            return new DateTime(base_date.Value.Year, base_date.Value.Month, base_date.Value.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// 該当日付の年度を返す
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="startingMonth">int? : 年度の開始月</param>
        /// <returns>int</returns>
        public static int FiscalYear(this DateTime dt, int? startingMonth = null)
        {
            return (dt.Month >= (startingMonth ?? FiscalYearStartingMonth)) ? dt.Year : dt.Year - 1;
        }

        public static int FiscalYear(this string dt, int? startingMonth = null)
        {
            var date = dt.GetKiisGRDatetime().TypeofDatetime;
            return (date.Month >= (startingMonth ?? FiscalYearStartingMonth)) ? date.Year : date.Year - 1;
        }

        public static string convertGregorio2JP_ggyyMMdd(this DateTime _date)
        {
            try
            {

                CultureInfo ct = new CultureInfo("ja-JP", true);
                ct.DateTimeFormat.Calendar = new JapaneseCalendar();

                string result = _date.ToString("ggyy年MM月dd日", ct);

                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public static string convertGregorio2JP_ggyy(DateTime _date)
        {
            try
            {

                CultureInfo ct = new CultureInfo("ja-JP", true);
                ct.DateTimeFormat.Calendar = new JapaneseCalendar();

                string result = _date.ToString("ggyy年", ct);

                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }


        public static string convertGregorio2JP_MM(DateTime _date)
        {
            try
            {

                CultureInfo ct = new CultureInfo("ja-JP", true);
                ct.DateTimeFormat.Calendar = new JapaneseCalendar();

                string result = _date.ToString("MM月", ct);

                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public static string convertGregorio2KiisWareki(this DateTime _date)
        {
            string wareki = convertGregorio2JP_ggyyMMdd(_date);

            string i = null;

            if (wareki.Substring(0, 2) == "平成")
            {
                i = "7" + wareki.Replace("平成", "").Replace("年", "").Replace("月", "").Replace("日", "");
            }

            else if (wareki.Substring(0, 2) == "昭和")
            {
                i = "5" + wareki.Replace("昭和", "").Replace("年", "").Replace("月", "").Replace("日", "");
            }

            else if (wareki.Substring(0, 2) == "大正")
            {
                i = "3" + wareki.Replace("大正", "").Replace("年", "").Replace("月", "").Replace("日", "");
            }

            return i;

        }


        public static DateTime newTypeOfDatetimeAddSlash(this string args)
        {
            try
            {
                return DateTime.Parse(args.Substring(0, 4) + "/" + args.Substring(4, 2) + "/" + args.Substring(6, 2));

            }

            catch (NullReferenceException ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Kiis形式の和暦を（e.g 7-280901)19790131 のようなShortDatetimeString を返します。
        /// </summary>
        /// <param name="argDate"></param>
        /// <returns></returns>
        public static string convertDateOfKiisWareki_to_ggyymmdd(this string argDate)
        {
            try
            {
                string i = null;

                argDate = argDate.Replace("-", "").Replace("年", "").Replace("月", "").Replace("日", "");

                if (argDate.Substring(0, 1) == "3")
                {
                    i = "大正" + argDate.Substring(1, 2) + "年" + argDate.Substring(3, 2) + "月" + argDate.Substring(5, 2) + "日";
                }

                else if (argDate.Substring(0, 1) == "5")
                {
                    i = "昭和" + argDate.Substring(1, 2) + "年" + argDate.Substring(3, 2) + "月" + argDate.Substring(5, 2) + "日";
                }

                else if (argDate.Substring(0, 1) == "7")
                {
                    i = "平成" + argDate.Substring(1, 2) + "年" + argDate.Substring(3, 2) + "月" + argDate.Substring(5, 2) + "日";
                }

                return DateTime.Parse(i).ToShortDateString().Replace("/", "");

            }

            catch (Exception)
            {

                return null;

            }

        }

        /// <summary>
        /// 正規表現を使用して、日付型をC_KiisDatetimeクラスに格納して返します。
        /// 基本的にどの書式の日付型をいれてもOK
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static C_KiisDatetime GetKiisGRDatetime(this string date)
        {
            try
            {
                C_KiisDatetime KD = new C_KiisDatetime();

                //0-00年00月00日のKiisDatetimeを入力した場合
                if (Regex.IsMatch(date, @"\d{1}[-]\d{2}[年]\d\d[月]\d\d[日]"))
                {
                    var GR = date.convertDateOfKiisWareki_to_ggyymmdd();

                    KD.Datetime_GR = GR;

                }
                //2016/01/31のようなスラッシュがあるタイプ
                else if (Regex.IsMatch(date, @"[0-9]{4}[/][0-9]{1,2}[/][0-9]{1,2}"))
                {
                    var GR = DateTime.Parse(date).ToShortDateString().Replace("/", "");

                    KD.Datetime_GR = GR;

                }
                //20160101 のようなスラッシュがない西暦
                else if (Regex.IsMatch(date, @"\d\d\d\d\d\d\d\d"))
                {
                    var GR = date;

                    KD.Datetime_GR = GR;

                }
                //7280101 のようなKiis型
                else if (Regex.IsMatch(date, @"\d\d\d\d\d\d\d"))
                {
                    var GR = date.convertDateOfKiisWareki_to_ggyymmdd();

                    KD.Datetime_GR = GR;

                }
                //0000年00月00日　の西暦型
                else if (Regex.IsMatch(date, "^[0-9]{4}[年][0-9]{1,2}[月][0-9]{1,2}[日]$"))
                {
                    var GR = DateTime.Parse(date).ToShortDateString().Replace("/", "");

                    KD.Datetime_GR = GR;
                }

                //平成00年00月00日 の和暦型
                else if (Regex.IsMatch(date, @"..[0-9]{1,2}[年][0-9]{1,2}[月][0-9]{1,2}[日]"))
                {
                    var conv = DateTime.Parse(date);

                    var GR = conv.ToShortDateString().Replace("/", "");

                    KD.Datetime_GR = GR;

                }

                //レセプト等の診療分　の型
                else if (Regex.IsMatch(date, @"\d{1}[-]\d{2}[年]\d\d[月診療分]"))
                {
                    string temp = date.Replace("診療分", "") + "01日";

                    var GR = temp.convertDateOfKiisWareki_to_ggyymmdd();

                    KD.Datetime_GR = GR;
                }

                KD.Kiis_元号Code = KD.Datetime_GR.newTypeOfDatetimeAddSlash().convertGregorio2KiisWareki().Substring(0, 1);
                KD.Kiis_年月日 = KD.Datetime_GR.newTypeOfDatetimeAddSlash().convertGregorio2KiisWareki().Substring(1, 6);
                KD.Datetime_JP = KD.Datetime_GR.newTypeOfDatetimeAddSlash().convertGregorio2JP_ggyyMMdd();
                KD.Datetime_GRwithSlash = KD.Datetime_GR.newTypeOfDatetimeAddSlash().ToShortDateString();
                KD.TypeofDatetime = KD.Datetime_GR.newTypeOfDatetimeAddSlash();
                KD.Datetime_JP_元号 = KD.Datetime_JP.Substring(0, 2);
                KD.Kiis_Datetime = KD.Kiis_元号Code + KD.Kiis_年月日;
                KD.Datetime_JP_Kani = KD.ConvertJPDatetimeKaniVer();
                return KD;
            }
            catch
            {
                C_KiisDatetime KD = new C_KiisDatetime();

                KD.Datetime_GR = "";
                KD.Datetime_JP = "";
                KD.Datetime_JP_元号 = "";
                KD.Kiis_元号Code = "";
                KD.Datetime_GRwithSlash = "";

                return KD;
            }
        }

        public static string ConvertJPDatetimeKaniVer(this C_KiisDatetime date)
        {
            try
            {
                var tempDate = date.Datetime_JP.Replace("年", ".").Replace("月", ".").Replace("日", "").Replace("昭和", "S").Replace("平成", "H");

                return tempDate;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static C_KiisDatetime GetKiisGRDatetime(this DateTime date)
        {
            var _d = date.ToShortDateString();

            return _d.GetKiisGRDatetime();
        }

        public static string GetEraCode(this string EraName)
        {
            try
            {
                if (EraName == "昭")
                {
                    return "5";
                }
                else if (EraName == "平")
                {
                    return "7";
                }
                else if (EraName == "大")
                {
                    return "3";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ConvertRecieptDateToKiisDate(this string Date)
        {
            try
            {
                if (Date.Length == 7)
                {
                    //年月日までの場合
                    if (Date.Substring(0, 1) == "3")
                    {
                        ///昭和
                        return "5" + Date.Substring(1, 6);
                    }
                    else if (Date.Substring(0, 1) == "4")
                    {
                        ///昭和
                        return "7" + Date.Substring(1, 6);
                    }
                }

                else if (Date.Length == 5)
                {
                    //年月日までの場合
                    if (Date.Substring(0, 1) == "3")
                    {
                        ///昭和
                        return "5" + Date.Substring(1, 4);
                    }
                    else if (Date.Substring(0, 1) == "4")
                    {
                        ///昭和
                        return "7" + Date.Substring(1, 4);
                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string ConvertKiisDateToRecieptDate(this string Date)
        {
            try
            {
                if (Date.Length == 7)
                {
                    //年月日までの場合
                    if (Date.Substring(0, 1) == "5")
                    {
                        ///昭和
                        return "3" + Date.Substring(1, 6);
                    }
                    else if (Date.Substring(0, 1) == "7")
                    {
                        ///昭和
                        return "4" + Date.Substring(1, 6);
                    }
                }

                else if (Date.Length == 5)
                {
                    //年月日までの場合
                    if (Date.Substring(0, 1) == "5")
                    {
                        ///昭和
                        return "3" + Date.Substring(1, 4);
                    }
                    else if (Date.Substring(0, 1) == "7")
                    {
                        ///昭和
                        return "4" + Date.Substring(1, 4);
                    }
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string replaceGregorioDateforyyyymm(this string date)
        {
            try
            {
                return $"{date.Substring(0, 4)}年{date.Substring(4, 2)}月";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
