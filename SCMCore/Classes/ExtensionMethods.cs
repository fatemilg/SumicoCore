using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace SCMCore.ExtensionMethod
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// این متد حرف 'ك' و 'ي'را به 'ک' و 'ی'   تبدیل می کند
        /// </summary>
        /// <param name="Lname"></param>
        /// <returns></returns>
        public static string FixFarsi(this string Lname)
        {
            return Lname.Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
        }
        public static string RemoveExtraCharForSEO(this string perfixe)
        {
            Dictionary<string, string> map = new Dictionary<string, string>()
            {
            {"&","-"},
            {" ","-"},
            {"<","-"},
            {">","-"},
            {"/","-"}
            };
            var regex = new Regex(string.Join("|", map.Keys));
            var newStr = regex.Replace(perfixe, m => map[m.Value]);
            return newStr;
        }
        public static string FixCurrency(this string Currency)
        {
            return Currency.Replace("_", "").Replace(",", "");
        }
        /// <summary>
        /// tabdil string be GUID
        /// </summary>
        /// <param name="Lname"></param>
        /// <returns></returns>
        public static Guid StringToGuid(this string txt)
        {
            Guid num_;
            Guid.TryParse(txt, out num_);
            return num_;
        }
        public static int StringToInt(this string num)
        {
            int num_;
            int.TryParse(num, out num_);
            return num_;
        }
        public static float StringToFloat(this string num)
        {
            float num_;
            float.TryParse(num, out num_);
            return num_;
        }
        public static decimal StringToDecimal(this string num)
        {
            decimal num_;
            decimal.TryParse(num, out num_);
            return num_;
        }
        public static DateTime ShamsiDateTime(this string date)
        {
            PersianCalendar pc = new PersianCalendar();
            GregorianCalendar gc = new GregorianCalendar();
            DateTime Result = new DateTime();
            DateTime Temp = new DateTime();
            string[] str = date.Split('/');
            if (str.Length != 3)
                return new DateTime(1900, 1, 1);
            if (str[0].Length == 4)
            {
                date = str[0] + "/" + str[1].PadLeft(2, '0') + "/" + str[2].PadLeft(2, '0');
            }
            else if (str[0].Length > 3)
            {
                date = str[2].Substring(0, 4) + "/" + str[1].PadLeft(2, '0') + "/" + str[0].PadLeft(2, '0');
            }
            else
            {
                date = str[2].Substring(0, 4) + "/" + str[0].PadLeft(2, '0') + "/" + str[1].PadLeft(2, '0');
            }
            Temp = Convert.ToDateTime(date);
            Result = pc.ToDateTime(gc.GetYear(Temp), gc.GetMonth(Temp), gc.GetDayOfMonth(Temp), 0, 0, 0, 0);
            return Result;

        }
        public static DateTime StrShamsiToDateMiladi(this string date)
        {
            string[] str = date.Split('/');
            if (str.Length != 3)
                return new DateTime(1900, 1, 1);
            DateTime dt = new DateTime();
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            if (str[0].Length == 4)
            {
                dt = p.ToDateTime(str[0].StringToInt(), str[1].StringToInt(), str[2].StringToInt(), 0, 0, 0, 0);
            }
            else
            {
                dt = p.ToDateTime(str[2].StringToInt(), str[1].StringToInt(), str[0].StringToInt(), 0, 0, 0, 0);
            }
            return dt;
        }
        public static DateTime StringToDateTime(this string date)
        {
            try
            {

                if (string.IsNullOrEmpty(date) || date.Trim() == "")
                    return new DateTime(1900, 01, 01);
                string[] arrDate = { };
                if (date.Contains("-"))
                {
                    arrDate = date.Split('-');
                }
                else if (date.Contains("/"))
                {
                    arrDate = date.Split('/');
                }

                string[] time = date.Split(' ');
                string h = "";
                string m = "0", s = "1";
                if (date.ToLower().Contains("m")) //iani am ya pm vojod dasht
                {

                    if (time[2].ToLower() == "am")
                    {
                        h = time[1].Split(':')[0];
                        if (h.StringToInt() == 12)
                        {
                            h = "0";
                        }
                    }
                    else if (time[2].ToLower() == "pm")
                    {
                        h = (time[1].Split(':')[0].StringToInt() + 12).ToString();
                        if (h.StringToInt() == 24)
                        {
                            h = "12";
                        }

                    }

                }
                else // am pm vojood nadarad
                {
                    h = time[1].Split(':')[0];

                }
                m = time[1].Split(':')[1];
                s = time[1].Split(':')[2];

                string Month = "", Day = "", Year = "";
                if (arrDate[0].Length < 3)  // yani formate tarikh D/M/Y bashad
                {
                    try
                    {
                        Month = arrDate[0].StringToInt().ToString();
                        Day = arrDate[1].StringToInt().ToString();
                        Year = arrDate[2].Split(' ')[0];
                        return new DateTime(Year.StringToInt(), Month.StringToInt(), Day.StringToInt(), h.StringToInt(), m.StringToInt(), s.StringToInt());
                    }
                    catch
                    {
                        Day = arrDate[0];
                        Month = arrDate[1];
                        Year = arrDate[2].Split(' ')[0];
                        return new DateTime(Year.StringToInt(), Month.StringToInt(), Day.StringToInt(), h.StringToInt(), m.StringToInt(), s.StringToInt());
                    }
                }
                else if (arrDate[0].Length > 3)  // yani formate tarikh Y/M/D bashad
                {
                    try
                    {
                        Year = arrDate[0];
                        Month = arrDate[1];
                        Day = arrDate[2].Split(' ')[0];
                        return new DateTime(Year.StringToInt(), Month.StringToInt(), Day.StringToInt(), h.StringToInt(), m.StringToInt(), s.StringToInt());
                    }
                    catch
                    {
                        Year = arrDate[0];
                        Day = arrDate[1];
                        Month = arrDate[2].Split(' ')[0];
                        return new DateTime(Year.StringToInt(), Month.StringToInt(), Day.StringToInt(), h.StringToInt(), m.StringToInt(), s.StringToInt());
                    }
                }
                else
                    return new DateTime(Year.StringToInt(), Month.StringToInt(), Day.StringToInt(), h.StringToInt(), m.StringToInt(), s.StringToInt());
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public static bool StringToBool(this string num)
        {
            if (num == "1") return true;
            bool num_;
            bool.TryParse(num, out num_);
            return num_;
        }
        public static bool ObjectToBool(this object txt)
        {
            if (txt == null) return false;
            bool num_;
            bool.TryParse(txt.ToString(), out num_);
            return num_;

        }
        public static Guid ObjectToGuid(this Object txt)
        {
            if (txt == null) return Guid.Empty;
            Guid num_;
            Guid.TryParse(txt.ToString(), out num_);
            return num_;
        }
        public static string ReturnDataSetField(this DataSet ds, string field)
        {
            try
            {
                if (Null_Ds(ds)) return "";
                return ds.Tables[0].Rows[0][field].ToString();
            }

            catch
            {
                return "";

            }

        }
        public static bool Null_Ds(this DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return true;
            return false;
        }
        public static string RemoveDot(this string str)
        {
            return str.Replace(".", "").Replace(",", "");
        }
        public static string RemoveDash(this string str)
        {
            return str.Replace("-", "");
        }
        //public static string SetDateFormat(this string date)
        //{
        //    if (date.Empty_string()) return "";
        //    string[] str = date.Split('/');
        //    if (str[2].Length == 4) date = str[2] + "/" + str[1] + "/" + str[0];
        //    return date;

        //}
        public static string SplitPrice(this object Number)
        {
            float num;
            float.TryParse(Number.ToString(), out num);
            return string.Format("{0:N0}", num);
        }
        //public static string GetPersianDate(this DateTime date)
        //{
        //    PersianCalendar jc = new PersianCalendar();
        //    return string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));
        //}
        //public static string GetDayOfWeekName(this DateTime date)
        //{
        //    switch (date.DayOfWeek)
        //    {
        //        case DayOfWeek.Saturday: return "شنبه";
        //        case DayOfWeek.Sunday: return "يکشنبه";
        //        case DayOfWeek.Monday: return "دوشنبه";
        //        case DayOfWeek.Tuesday: return "سه‏ شنبه";
        //        case DayOfWeek.Wednesday: return "چهارشنبه";
        //        case DayOfWeek.Thursday: return "پنجشنبه";
        //        case DayOfWeek.Friday: return "جمعه";
        //        default: return "";
        //    }
        //}
        //public static string GetMonthName(this DateTime date)
        //{
        //    PersianCalendar jc = new PersianCalendar();
        //    string pdate = string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));

        //    string[] dates = pdate.Split('/');
        //    int month = Convert.ToInt32(dates[1]);

        //    switch (month)
        //    {
        //        case 1: return "فررودين";
        //        case 2: return "ارديبهشت";
        //        case 3: return "خرداد";
        //        case 4: return "تير‏";
        //        case 5: return "مرداد";
        //        case 6: return "شهريور";
        //        case 7: return "مهر";
        //        case 8: return "آبان";
        //        case 9: return "آذر";
        //        case 10: return "دي";
        //        case 11: return "بهمن";
        //        case 12: return "اسفند";
        //        default: return "";
        //    }

        //}
        /// <summary>
        /// Convert DateTime to Shamsi Date (YYYY/MM/DD)
        /// </summary>
        public static string ToShamsiDateYMD(this DateTime date)
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDay = PC.GetDayOfMonth(date);
            return (intYear.ToString() + "/" + intMonth.ToString() + "/" + intDay.ToString());
        }
        /// <summary>
        /// Convert DateTime to Shamsi String 
        /// </summary>
        public static string ToShamsiDateString(this DateTime date)
        {

            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDayOfMonth = PC.GetDayOfMonth(date);
            DayOfWeek enDayOfWeek = PC.GetDayOfWeek(date);
            string strMonthName, strDayName;
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }

            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    strDayName = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    strDayName = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    strDayName = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    strDayName = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    strDayName = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    strDayName = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    strDayName = "چهارشنبه";
                    break;
                default:
                    strDayName = "";
                    break;
            }

            return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));

        }
        public static DateTime ToGerigorianDate(this DateTime dt)
        {
            return Persia.Calendar.ConvertToGregorian(dt.Year, dt.Month, dt.Day, Persia.DateType.Gerigorian);
        }
        public static void SelectedNodeExtension(this TreeView tv, string nodeValue)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                if (tn.Value == nodeValue)
                {
                    tn.Select();
                }
                else
                {



                    ExpandSelect(tn, nodeValue);
                }
            }
        }
        private static void ExpandSelect(TreeNode tn, string nodeValue)
        {
            foreach (TreeNode t in tn.ChildNodes)
            {
                if (t.Value == nodeValue)
                {
                    tn.Expand();
                    t.Select();
                }
                else
                {
                    ExpandSelect(t, nodeValue);
                }
            }
        }
        public static string EncryptData(this string str)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("password"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(str);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string DecryptString(this string str)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("password"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(str);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        public static string TamperProofStringEncode(this string value, string key)
        {


            byte[] clearBytes = Encoding.Unicode.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    value = Convert.ToBase64String(ms.ToArray());
                }
            }
            return value;
        }
        public static string TamperProofStringDecode(this string value, string key)
        {



            value = value.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    value = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return value;

        }
        public static string ListToStringWithSeprator(this List<string> Li, string seprator)
        {
            string result = "";
            foreach (string str in Li)
            {
                result += str;
                if (Li.Count - 1 != Li.IndexOf(str))
                {
                    result += seprator;
                }

            }
            return result;

        }
        public static string ListToString(this List<string> Li, string Seprator, string Perfix, string Suffix)
        {
            string result = "";
            foreach (string str in Li)
            {
                result += Perfix + str + Suffix;
                if (Li.Count - 1 != Li.IndexOf(str))
                {
                    result += Seprator;
                }
            }
            return result;

        }
        public static string ReturnDataSetField(this DataSet ds, string field, int TableIndex)
        {
            try
            {
                if (Null_Ds(ds)) return "";
                return ds.Tables[TableIndex].Rows[0][field].ToString();
            }
            catch
            {
                return "";
            }
        }
        public static string ReturnDataSetField(this DataSet ds, string field, int rowIndex, int TableIndex)
        {
            try
            {
                if (Null_Ds(ds)) return "";
                return ds.Tables[TableIndex].Rows[rowIndex][field].ToString();
            }
            catch
            {
                return "";
            }
        }
        public static string GetDisplayNameForEnum(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", value, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static string ExportGridToHTML(this DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<table style=\" width:100%\"  border = '1'>");
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th style=\"text-align:center\">");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td style=\"text-align:center\">");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</table>");
                return html.ToString();
            }
            else
            {
                return "";
            }
        }
        public static string ToHTMLWithoutHeader(this DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.Append("<table style=\" width:auto;display:block; border:none\">");
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td style=\"text-align:center\">");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
                html.Append("</table>");
                return html.ToString();
            }
            else
            {
                return "";
            }
        }
        public static string RemoveHTMLTags(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static System.Drawing.Image resizeImage(this System.Drawing.Image image, int? width, int? height)
        {
            if (height == null && width != null)
            {
                height = (image.Height * width) / image.Width;
            }
            if (height == null && width == null)
            {
                return image.GetThumbnailImage(0, 0, () => false, IntPtr.Zero);
            }

            return image.GetThumbnailImage(width.Value, height.Value, null, IntPtr.Zero);
        }

        public static string AddSmallUrl(this string input)
        {
            string[] strUrlParts = input.Split('\\');
            return input.Replace(strUrlParts[strUrlParts.Length - 1], @"Small\" + strUrlParts[strUrlParts.Length - 1]);
        }

    }
}
