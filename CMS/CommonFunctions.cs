    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using PARSGREEN;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CMS
{
    public enum UserTypes
    {
        Normal = 0,
        Hotel = 1,
        Agency = 2,
        Driver = 3
    }
    public enum GlobalValueTypes
    {
        /// <summary>
        /// نوع همراهی
        /// </summary>
        Hamrahi = 1,

        /// <summary>
        /// محل
        /// </summary>
        Place = 2,

        /// <summary>
        /// نوع وسیله نقلیه
        /// </summary>
        Vehicle = 3,

        /// <summary>
        /// نوع محصول (گشت، تور، ترانسفر)
        /// </summary>
        ProductType = 4,

        /// <summary>
        /// نوع فعالیت: گروهی و انفرادی
        /// </summary>
        ActivityType=5
    }

    public enum ProductType
    {
        Gasht = 1,
        Transfer = 2,
        Tour = 3
    }
    public  enum ActivityTypeEnum
    {
        /// <summary>
        /// گروهی
        /// </summary>
        Group = 1,

        /// <summary>
        /// انفرادی
        /// </summary>
        Individual = 2
    }


    public enum OrderStatus
    {
        UnRead = 0,
        /// <summary>
        /// در حال بررسی
        /// </summary>
        Read = 1,
        Done = 2,
        Canceled = 3
    }
    public enum CreditTransactionType
    {
        /// <summary>
        /// افزودن وجه
        /// </summary>
        Plus=0,

        /// <summary>
        /// کسر وجه
        /// </summary>
        Minus=1
    }
    public static class CommonFunctions
    {

        public static TimeSpan TokenExpireDay => new TimeSpan(3 , 0 , 0 ,0);
        public static DateTime TokenExpireDateTime => DateTime.Now.Add(TokenExpireDay);

        public static string TokenCookieName => "aspnetusertoken";

        public static class CreditEneity
        {
            public static string PayRequest => "PayRequest";
            public static string AdminAdd => "AdminAdd";
        }
        public class ReserveInfo
        {
            public int Day { get; set; }
            public DateTime Start { get; set; }
            public int VillaID { get; set; }

            public int Price { get; set; }
            public ReserveInfo(DateTime start,int day,int vallaID,int price)
            {
                Start = start;
                VillaID = vallaID;
                Day = day;
                Price = price;
            }
        }

        public static string OrderStatusName(object val)
        {
            if (val == null)
                throw new NullReferenceException();
            if (!int.TryParse(val.ToString(), out int intval))
                throw new Exception("مقدار int نیست");
            switch (intval)
            {
                case (int)OrderStatus.UnRead:
                    return "جدید";
                case (int)OrderStatus.Read:
                    return "در حال بررسی";
                case (int)OrderStatus.Done:
                    return "انجام شد";
                case (int)OrderStatus.Canceled:
                    return "کنسل شده";
                default: throw new Exception("مقدار ناموجود");
            }
        }

        public static int GetUserCurrentBalance(int userId)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                return _data.CreditTransactions.Where(a => a.UserID == userId).Sum(a => (int?)a.Mablagh) ?? 0;
            }
        }

        static readonly object _object = new object();
        public static bool AddTransaction(string desc, CreditTransactionType type, int userid,  int mablagh, string entityType=null, int? entityId=null)
        {
            lock (_object)
            {

                try
                {
                    using (DataAccessDataContext _data = new DataAccessDataContext())
                    {
                        var current = _data.CreditTransactions.Where(a => a.UserID == userid).Sum(a => (int?)a.Mablagh) ?? 0;
                        CreditTransaction trans = new CreditTransaction()
                        {
                            Description = desc,
                            EntityId = entityId,
                            EntityType = entityType,
                            Mablagh = mablagh,
                            RegDate = DateTime.Now,
                            Type = (type == CreditTransactionType.Plus),
                            UserID = userid,
                            AfterBalance = (type == CreditTransactionType.Plus)
                                        ? current + mablagh
                                        : current - mablagh,
                            BeforeBalance = current
                        };
                        _data.CreditTransactions.InsertOnSubmit(trans);
                        _data.SubmitChanges();
                        return true;
                    }
                }
                catch { return false; }
            }
        }

        public static string ProductTypeName(object val)
        {
            if (val == null)
                throw new NullReferenceException();
            if (!int.TryParse(val.ToString(), out int intval))
                throw new Exception("مقدار int نیست");
            switch (intval)
            {
                case (int)ProductType.Gasht:
                    return "گشت";
                case (int)ProductType.Tour:
                    return "تور";
                case (int)ProductType.Transfer:
                    return "ترانسفر";
                default: throw new Exception("مقدار ناموجود");
            }
        }

        public static string UserTypeName(object val)
        {
            if (val == null)
                throw new NullReferenceException();
            if (!int.TryParse(val.ToString(), out int intval))
                throw new Exception("مقدار int نیست");
            switch (intval)
            {
                case (int)UserTypes.Agency:
                    return "آژانس";
                case (int)UserTypes.Driver:
                    return "راننده";
                case (int)UserTypes.Hotel:
                    return "هتل";
                case (int)UserTypes.Normal:
                    return "عادی";
                default: throw new Exception("مقدار ناموجود");

            }
        }

        public static string GetSettingVal(string name, bool encoded = false, bool loadFromDatabase = false)
        {
            string retVal = "";
            if (!loadFromDatabase)
            {
                SystemConfig sys = SystemConfig.Instance;
                retVal = sys.GetConfigVale(name, encoded);
                if (encoded)
                    retVal = Encrypt.DecryptString(retVal);
            }
            else
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {

                    if (encoded)
                    {
                        string EncriptedName = Encrypt.EncryptString(name);

                        retVal = Encrypt.DecryptString(GetSettingShortValue(EncriptedName));

                    }
                    else
                        retVal = GetSettingShortValue(name);


                }
            }

            return retVal;
        }
        public static string GetSettingShortValue(string shortName)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                var item = _data.settingValues.FirstOrDefault(a => a.Short.ToLower() == shortName.ToLower());
                if (item == null)
                    return "";
                return item.ShortValue;
            }


        }
        
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        public static string BytesToString(object input)
        {
            int byteCount = 0;//اگه لانگ باشه بیشتر جا میگیره
            int.TryParse(input as string, out byteCount);

            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        /// <summary>
        /// درست کردن متا صبحات
        /// </summary>  
        public static string GenerateMetaTag(string title, string description, bool allowIndexPage, bool allowFollowLinks, string author = "", string lastmodified = "", string expires = "never", string language = "fa" )
        {
            int MaxLenghtTitle = 60;
            int MaxLenghtDescription = 170;
            string FaviconPath = "~/cdn/ui/favicon.ico";
            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();
            description = description.Substring(0, description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();

            var meta = "";
            meta += string.Format("<title>{0}</title>\n", title);
            meta += string.Format("<link rel=\"shortcut icon\" href=\"{0}\"/>\n", FaviconPath);
            meta += string.Format("<meta http-equiv=\"content-language\" content=\"{0}\"/>\n", language);
            meta += string.Format("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/>\n");
            meta += string.Format("<meta charset=\"utf-8\"/>\n");
            meta += string.Format("<meta name=\"description\" content=\"{0}\"/>\n", description);
           // meta += string.Format("<meta http-equiv=\"Cache-control\" content=\"{0}\"/>\n", EnumExtensions.EnumHelper<CacheControlType>.GetEnumDescription(cacheControlType.ToString()));
            meta += string.Format("<meta name=\"robots\" content=\"{0}, {1}\" />\n", allowIndexPage ? "index" : "noindex", allowFollowLinks ? "follow" : "nofollow");
            meta += string.Format("<meta name=\"expires\" content=\"{0}\"/>\n", expires);

            if (!string.IsNullOrEmpty(lastmodified))
                meta += string.Format("<meta name=\"last-modified\" content=\"{0}\"/>\n", lastmodified);

            if (!string.IsNullOrEmpty(author))
                meta += string.Format("<meta name=\"author\" content=\"{0}\"/>\n", author);

            //------------------------------------Google & Bing Doesn't Use Meta Keywords ...
            //meta += string.Format("<meta name=\"keywords\" content=\"{0}\"/>\n", keywords);

            return meta;
        }

        public static Boolean CheckMeliCode(this String nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (String.IsNullOrEmpty(nationalCode))
                return false;
                //throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                return false;
                //throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                return false;
                //throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode))
                return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }
         
        public static string SafeSqlLiteral(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }
        public static string TransPayMode(object input)
        {
            string retVal = input.ToString();
            switch (input.ToString())
            {
                case "NotPaid":
                    retVal = "عدم پرداخت";
                    break;
                case "SaderatPos":
                    retVal = "کارتخوان صادرات";
                    break;
                case "Saman":
                    retVal = "درگاه سامان";
                    break;
                case "Melat":
                    retVal = "درگاه ملت";
                    break;
                case "CashRooh":
                    retVal = "نقدی روحبخش";
                    break;
                case "CashHady":
                    retVal = "نقدی هادی";
                    break;
                case "Check":
                    retVal = "چک";
                    break;

                default:
                    break;
            }
            return retVal;
        }
        public static bool SendSMS(string Numbers, string Text)
        {
           

            try
            {
                string sig = "F5921909-4F22-4A7D-B654-DDC6A0D600D6";
                //string user = GetSettingShortValue("SMSUser");
                //string pass = GetSettingShortValue("SMSPass");
                string number = "5000282313";//GetSettingShortValue("SMSNumber");


                if (string.IsNullOrEmpty(sig))
                    return false;

                int stat = 0;
                string[] statnum = null;
                PARSGREEN.SMS.SendGroupSMS(sig, number,new string[]{ Numbers}, Text, false, null, ref stat, ref statnum);

                if (stat != 1)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public static string HashEmailForGravatar(object input)
        {
            try
            {
                string email = "";
                email = input.ToString(); 
                MD5 md5Hasher = MD5.Create();
                 
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
                 
                StringBuilder sBuilder = new StringBuilder();  
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();   
            }
            catch { return ""; }
        }

        public static string SubStringText(object InputText, object StartIndex, object Length)
        {
            string StrText = InputText.ToString();
            int StrLenght = Convert.ToInt32(Length);
            if (StrText.Length > StrLenght)
            {
                return StrText.Substring(Convert.ToInt32(StartIndex), StrLenght)+"...";
            }
            else
            {
                return StrText;
            }
        }
        public static string String2date(object input, int Mode, string type)
        {
            //using http://www.persiadevelopers.com/articles/Persia.NET.aspx
            //همنطور که در جدول فوق مشاهده می شود ۶ فرمت آخری مربوط به فرمتهای ساعت می شوند. این فرمتها می توانند به همراه فرمت تاریخ نیز به کار روند مانند کد زیر:
            if (input == null)
                return "-";
            //string str = solarDate.ToString("H,w");
            Persia.SolarDate solarDate = Persia.Calendar.ConvertToPersian(DateTime.Parse(input.ToString()));
            if (Mode == 1)
            {
                /*
                 D = X  روز پیش
                 TY = امروز، دیروز، x   روز پیش
                 p = اکنون، x  دقیقه پیش، x  ساعت پیش
                 */
                return solarDate.ToRelativeDateString(type);
            }
            else if (Mode == 2)
            {
                /*
                 D = ۱۳۸۹/۹/۳۰
                 d = ۸۹/۹/۳۰
                 W = سه شنبه  ۱۳۸۹/۹/۳۰
                 M = ۳۰ آذر ۱۳۸۹
                 H =   ۱۹  : ۵۰
                 N = سه شنبه  ۳۰ آذر ۱۳۸۹
                 n= سه شنبه  ۳۰ آذر ۸۹
                 */
                if (type == "H")
                {
                    string tm = solarDate.ToString(type);
                    return tm.Remove(tm.LastIndexOf(':'));
                }
                return solarDate.ToString(type);
            }
            else
                return solarDate.ToString();
        }

        public static string ToPersianDateTime(this DateTime datetime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#") + " - " + datetime.Hour.ToString("0#") + ":" + datetime.Minute.ToString("0#");
        }

        public static string ToPersianDate(object input)
        {
            DateTime datetime = DateTime.Parse(input.ToString());
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#");
        }
        public static string ToShortPersian(this DateTime datetime)
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.GetYear(datetime).ToString() + "/" +
                    persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                    persianCalendar.GetDayOfMonth(datetime).ToString("0#");
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime ToMiladi(object input)
        {

            string datetime = input.ToString();
            int[] startdatestring = datetime.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
            DateTime date = new DateTime(startdatestring[0], startdatestring[1], startdatestring[2], new PersianCalendar());
            return date;
        }


        public static string SiteRoot
        {
            get
            {
                return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host).Trim('/') + "/";
            }
        }

        public static string ToPersianWithNamedMonth(this DateTime datetime)
        {
            string retVal = string.Empty;
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.GetDayOfMonth(datetime).ToString() + " " +
                    PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " +
                    persianCalendar.GetYear(datetime).ToString();
            }
            catch
            {
                retVal = string.Empty;
            }
            return retVal;
        }
        public static string ToPersianWithNamedMonth2(this DateTime datetime)
        {
            string retVal = string.Empty;
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                return persianCalendar.GetDayOfMonth(datetime).ToString() + " " +
                    PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " + "ماه" + " " +
                    persianCalendar.GetYear(datetime).ToString();
            }
            catch
            {
                retVal = string.Empty;
            }
            return retVal;
        }
        public static string PersianMonthName(byte month)
        {
            string retVal = string.Empty;
            switch (month)
            {
                case 1:
                    retVal = "فروردین";
                    break;
                case 2:
                    retVal = "اردیبهشت";
                    break;
                case 3:
                    retVal = "خرداد";
                    break;
                case 4:
                    retVal = "تیر";
                    break;
                case 5:
                    retVal = "مرداد";
                    break;
                case 6:
                    retVal = "شهریور";
                    break;
                case 7:
                    retVal = "مهر";
                    break;
                case 8:
                    retVal = "آبان";
                    break;
                case 9:
                    retVal = "آذر";
                    break;
                case 10:
                    retVal = "دی";
                    break;
                case 11:
                    retVal = "بهمن";
                    break;
                case 12:
                    retVal = "اسفند";
                    break;
            }
            return retVal;
        }

        public static string PersianDayOfWeek(this DateTime datetime)
        {
            string retVal = string.Empty;

            switch (datetime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    retVal = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    retVal = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    retVal = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    retVal = "یکشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    retVal = "سه شنبه";
                    break;
                case DayOfWeek.Thursday:
                    retVal = "پنجشنبه";
                    break;
                case DayOfWeek.Wednesday:
                    retVal = "چهارشنبه";
                    break;
                default:
                    break;
            }

            return retVal;
        }

        public static int PersianYear(this DateTime datetime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime);
        }
        public static string PersianMonth2(this DateTime datetime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " +
                persianCalendar.GetYear(datetime).ToString();
        }
        public static int PersianMonth(this DateTime datetime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetMonth(datetime);
        }
        public static int PersianDay(this DateTime datetime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(datetime);
        }
        public static string PersianTime(this DateTime datetime)
        {
            return datetime.Hour.ToString() + ":" + datetime.Minute.ToString();
        }



    
        public static string ValidPersian(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                str = Regex.Replace(Regex.Replace(str, "ك", "ک", RegexOptions.IgnoreCase), "ي", "ی",RegexOptions.IgnoreCase); 
                return str;

            }
        }
        public static string SecureText(this string str)
        {
            return HttpContext.Current.Server.HtmlEncode(str);
        }

        public static string SecureText(this object str)
        {
            return HttpContext.Current.Server.HtmlEncode(Convert.ToString(str));
        }
        public static string ConvertNumLa2Fa(string num)
        {
            string result = string.Empty;
            foreach (char c in num.ToCharArray())
            {
                switch (c)
                {
                    case '0':
                        result += "٠";
                        break;
                    case '1':
                        result += "١";
                        break;
                    case '2':
                        result += "٢";
                        break;
                    case '3':
                        result += "٣";
                        break;
                    case '4':
                        result += "٤";
                        break;
                    case '5':
                        result += "٥";
                        break;
                    case '6':
                        result += "٦";
                        break;
                    case '7':
                        result += "٧";
                        break;
                    case '8':
                        result += "٨";
                        break;
                    case '9':
                        result += "٩";
                        break;
                    default:
                        result += c;
                        break;

                }
            }
            return result;
        }
        public static string GetPlainTextFromHtml(string Html)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
            }
            catch { return ""; }
        }
        public static string ReplaceSpace(object str)
        {
            var slug = RemoveAccent(str.ToString()).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            //زیر 45 کاراکتر باشه بهتره
            // slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim();

            return slug;
        }

        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string SetCama(object InputText)
        {
            string num = "0";
            try
            {
                if (InputText == null || string.IsNullOrEmpty(InputText.ToString()))
                    return "0";
                num = InputText.ToString();
                double number = 0;
                double.TryParse(InputText.ToString(), out number);
                string res = string.Format("{0:###,###.####}", number);
                if (string.IsNullOrEmpty(res)) return "0";
                else
                    return res;
            }
            catch
            {
                return num;
            }
        }
        public static string SetCamaHezar(object InputText)
        {
            string num = "0";
            try
            {
                if (InputText == null || string.IsNullOrEmpty(InputText.ToString()))
                    return "0";
                num = InputText.ToString();
                double number = 0;
                double.TryParse(InputText.ToString(), out number);


                string res = string.Format("{0:###,###.####}", number/1000);
                if (string.IsNullOrEmpty(res)) return "0";
                else
                    return res;
            }
            catch
            {
                return num;
            }
        }
        public static string ReplaceSpaceForSearch(string str)
        {
            string strreturn = str;
            try
            {
                str = str.Trim();
                strreturn = str.Replace(" ", "-");
                strreturn = strreturn.Replace("|", "-");
                strreturn = strreturn.Replace("+", "-");
                strreturn = strreturn.Replace("،", "-");
                strreturn = strreturn.Replace("--", "-");
                strreturn = strreturn.Replace("|", "-");
                strreturn = strreturn.Replace("...", "-");
                strreturn = strreturn.Replace("..", "-");
                strreturn = strreturn.Replace(".", "-");
                strreturn = strreturn.Replace("++", "-");
                return strreturn;
            }
            catch { }
            return strreturn;
        }

        public static string ReplaceSpaceForSearch2(string str)
        {
            string strreturn = str;
            try
            {
                str = str.Trim();
                strreturn = str.Replace(" ", "+");
                strreturn = strreturn.Replace("|", "+");
                strreturn = strreturn.Replace("-", "+");
                strreturn = strreturn.Replace("،", "+");
                strreturn = strreturn.Replace("--", "+");
                strreturn = strreturn.Replace("|", "+");
                strreturn = strreturn.Replace("...", "+");
                strreturn = strreturn.Replace("..", "+");
                strreturn = strreturn.Replace("++", "+");
                return strreturn;
            }
            catch { }
            return strreturn;
        }
        public static string SubStringHtml(object InputHtml, object StartIndex, object Length)
        {
            return SubStringText(GetPlainTextFromHtml(InputHtml.ToString()), StartIndex, Length);
        }

      
        //private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        //public static string RandomString(int size)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    char ch;
        //    for (int i = 0; i < size; i++)
        //    {
        //        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //        builder.Append(ch);
        //    }

        //    return builder.ToString().ToLower();
        //}


        private static readonly Random _rng = new Random();
        private static  string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer).ToLower();
        }

      
        public static string GetSettingTextValue(string shortName)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                var item = _data.settingValues.FirstOrDefault(a => a.Short.ToLower() == shortName.ToLower());
                if (item == null)
                    return "";
                return item.TextValue;
            }
        }



         

        public static bool SendMail(string mail, string title, string text, bool useTemplate, string filepath, string ReplyTo)
        {
            bool isSeant = false;
            DataAccessDataContext _data = new DataAccessDataContext();
            try
            {
                string Body = "";
                MailMessage obMsg = new MailMessage();
                string mailservice = GetSettingVal("mailservice");
                string WebsiteName = GetSettingVal("WebsiteName");
                string WebsiteUrl = GetSettingVal("WebsiteUrl");
                string WebsiteMail = GetSettingVal("WebsiteMail");
                SmtpClient ob = new SmtpClient(mailservice, int.Parse(GetSettingVal("mailserviceport")));

                MailAddress sendermail = new MailAddress(WebsiteMail, WebsiteName, System.Text.Encoding.UTF8);
                System.Net.NetworkCredential objNC = new System.Net.NetworkCredential(WebsiteMail, GetSettingVal("WebsiteMailPass"));

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.BodyEncoding = System.Text.Encoding.UTF8;
                obMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                if (mailservice.ToLower() == "smtp.gmail.com")
                    ob.EnableSsl = true;
                ob.Credentials = objNC;


                if (!string.IsNullOrEmpty(filepath))
                {
                    Attachment attach = new Attachment(filepath);
                    string fileExt = System.IO.Path.GetExtension(filepath);
                    attach.ContentDisposition.FileName = "atachedFile" + fileExt;
                    attach.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    obMsg.Attachments.Add(attach);
                }


                System.IO.StreamReader st = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/commonmail.html"));

                if (useTemplate)
                    Body = st.ReadToEnd()
                        .Replace("[[WebsiteUrl]]", WebsiteUrl)
                        .Replace("[[WebsiteName]]", WebsiteName)
                        .Replace("[[title]]", title)
                        .Replace("[[text]]", text);
                else
                    Body = text;
                obMsg.Subject = title;

                if (!string.IsNullOrEmpty(ReplyTo))
                    obMsg.ReplyTo = new MailAddress(ReplyTo);


                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.To.Add(new MailAddress(mail));
                obMsg.Body = Body;
                obMsg.IsBodyHtml = true;
                ob.Send(obMsg);

                isSeant = true;
            }
            catch { isSeant = false; }
            return isSeant;
        }
        public static bool SendSmtpMail(string mail, string title, string text)
        {
            bool isSeant = false;
            SmtpClient smtpClient = new SmtpClient(GetSettingVal("mailservice"), int.Parse(GetSettingVal("mailserviceport")));

            smtpClient.Credentials = new System.Net.NetworkCredential(GetSettingVal("websitemail"), GetSettingVal("websitemailpass"));
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mailmsg = new MailMessage();


            //Setting From , To and CC
            mailmsg.From = new MailAddress(GetSettingVal("WebsiteMail"), GetSettingVal("WebsiteName"), System.Text.Encoding.UTF8);
            mailmsg.To.Add(new MailAddress(mail));
            mailmsg.IsBodyHtml = true;
            mailmsg.Subject = title;
            mailmsg.Body = text;
            try
            {
                smtpClient.Send(mailmsg);
                isSeant = true;
            }
            catch { isSeant = false; }
            return isSeant;

        }

        //public static string Encrypt(string clearText)
        //{
        //    string EncryptionKey = "webtina.ir";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}

        //public static string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "webtina.ir";
         
        //        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //        using (Aes encryptor = Aes.Create())
        //        {
        //            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //            encryptor.Key = pdb.GetBytes(32);
        //            encryptor.IV = pdb.GetBytes(16);
        //            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //            {
        //                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //                {
        //                    cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                    cs.Close();
        //                }
        //                cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //            }
        //        }
                
            
        //    return cipherText;
        //}

        public static bool IsEmail(string email)
        {
            const string MatchEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (email != null)
            {
                bool a = System.Text.RegularExpressions.Regex.IsMatch(email, MatchEmailPattern);
                return a;
            }
            else return false;
        }
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        public static void AddViewCount()
        {
            DateTime today = DateTime.Now;

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                try
                {
                    int cnt = _data.siteStats.Where(a => a.Day == today).Count();
                    if (cnt == 0)
                    {

                        siteStat tmp = new siteStat();
                        tmp.ViewCount = 1;
                        tmp.FileCount = 0;
                        tmp.MarketingCount = 0;
                        tmp.MarketingEarning = 0;
                        tmp.RegisterCount = 0;
                        tmp.SaleCount = 0;
                        tmp.SalesPrice = 0;
                        tmp.SiteEarning = 0;
                        tmp.UserEarning = 0;
                        tmp.Day = today;

                        _data.siteStats.InsertOnSubmit(tmp);
                        _data.SubmitChanges();
                    }
                    else if (cnt == 1)
                    {
                        siteStat tmp = _data.siteStats.Single(a => a.Day == today);
                        tmp.ViewCount += 1;
                        _data.SubmitChanges();
                    }
                }
                catch { }

            }
        }


        public static void AddFileCount()
        {
            DateTime today = DateTime.Now;

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int cnt = _data.siteStats.Where(a => a.Day == today).Count();
                if (cnt == 0)
                {

                    siteStat tmp = new siteStat();
                    tmp.ViewCount = 0;
                    tmp.FileCount = 1;
                    tmp.MarketingCount = 0;
                    tmp.MarketingEarning = 0;
                    tmp.RegisterCount = 0;
                    tmp.SaleCount = 0;
                    tmp.SalesPrice = 0;
                    tmp.SiteEarning = 0;
                    tmp.UserEarning = 0;
                    tmp.Day = today;

                    _data.siteStats.InsertOnSubmit(tmp);
                    _data.SubmitChanges();
                }
                else if (cnt == 1)
                {
                    siteStat tmp = _data.siteStats.Single(a => a.Day == today);
                    tmp.FileCount += 1;
                    _data.SubmitChanges();
                }

            }
        }

        public static void AddReigsterCount()
        {
            DateTime today = DateTime.Now;

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int cnt = _data.siteStats.Where(a => a.Day == today).Count();
                if (cnt == 0)
                {

                    siteStat tmp = new siteStat();
                    tmp.ViewCount = 0;
                    tmp.FileCount = 0;
                    tmp.MarketingCount = 0;
                    tmp.MarketingEarning = 0;
                    tmp.RegisterCount =1;
                    tmp.SaleCount = 0;
                    tmp.SalesPrice = 0;
                    tmp.SiteEarning = 0;
                    tmp.UserEarning = 0;
                    tmp.Day = today;

                    _data.siteStats.InsertOnSubmit(tmp);
                    _data.SubmitChanges();
                }
                else if (cnt == 1)
                {
                    siteStat tmp = _data.siteStats.Single(a => a.Day == today);
                    tmp.RegisterCount += 1;
                    _data.SubmitChanges();
                }

            }
        }



        public static void AddPayStat(int siteEarn, int price )
        {
            DateTime today = DateTime.Now;

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                int cnt = _data.siteStats.Where(a => a.Day == today).Count();
                if (cnt == 0)
                {

                    siteStat tmp = new siteStat();
                    tmp.ViewCount = 0;
                    tmp.FileCount = 0; 
                    tmp.RegisterCount = 0;
                    tmp.SaleCount = 1;
                    tmp.SalesPrice = price;
                    tmp.SiteEarning = siteEarn; 
                    tmp.Day = today;

                    _data.siteStats.InsertOnSubmit(tmp);
                    _data.SubmitChanges();
                }
                else if (cnt == 1)
                {
                    siteStat tmp = _data.siteStats.Single(a => a.Day == today);
                   
                     
                    tmp.SaleCount += 1;
                    tmp.SalesPrice += price;
                    tmp.SiteEarning += siteEarn; 
                    _data.SubmitChanges();
                }

            }
        }
        public static string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }

        public static System.Drawing.Image FixedSize(System.Drawing.Image image, int Width, int Height, bool needToFill)
        {
            #region много арифметики
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                System.Drawing.Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                System.Drawing.Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }


        /// <summary>
        /// برای دسترسی به تاریخ های بین 2 تاریخ
        /// </summary>
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;

            //نحوه استفاده
            //foreach (DateTime day in EachDay(StartDate, EndDate))
            //https://stackoverflow.com/questions/1847580/how-do-i-loop-through-a-date-range
        }

        public static string ConvertNumFa2En(this string str)
        {
            return str.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
        }


        public static string ConvertNumFa2La(string num)
        {
            string result = string.Empty;
            foreach (char c in num.ToCharArray())
            {
                int i = System.Convert.ToInt32(c);
                switch (i)
                {
                    case 1776:
                        result += "0";
                        break;
                    case 1777:
                        result += "1";
                        break;
                    case 1778:
                        result += "2";
                        break;
                    case 1779:
                        result += "3";
                        break;
                    case 1780:
                        result += "4";
                        break;
                    case 1781:
                        result += "5";
                        break;
                    case 1782:
                        result += "6";
                        break;
                    case 1783:
                        result += "7";
                        break;
                    case 1784:
                        result += "8";
                        break;
                    case 1785:
                        result += "9";
                        break;
                    default:
                        result += c;
                        break;

                }
            }
            return result;
        }

        public static string GenerateUserAuth(int accountID)
        {
            var Random = new Random();
            var IdentityAlgoritm = Random.Next(2, 9);
            var IdentityDefault = new Random().Next(1234567, 3126548);
            var IdentityCalculated = IdentityDefault - accountID;
            var FinalValue = string.Format("{0:0000000}{2}{1}", IdentityDefault, (
            IdentityCalculated * IdentityAlgoritm), IdentityAlgoritm);

            var Bytes = System.Text.Encoding.ASCII.GetBytes(FinalValue);
            return "A" + HttpContext.Current.Server.UrlEncode(Convert.ToBase64String(Bytes));
        }
        public static string GetTokenByAccountId(int userId)
        {
            using (DataAccessDataContext _data= new DataAccessDataContext())
            {
                return _data.LoginTokens.FirstOrDefault(a => a.UserId == userId)?.Token;
            }
        }
         
        public static usersdata GetUserByToken(string token)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                var userId= _data.LoginTokens.FirstOrDefault(a => a.Token == token)?.UserId;
                if (userId != null)
                    return _data.usersdatas.FirstOrDefault(a => a.ID == userId);

                return null;
            }
        }
        public static bool IsLoginTokenValid(string token)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                return (_data.LoginTokens.FirstOrDefault(a => a.Token== token && a.ExpireDate>DateTime.Now)?.Token)!=null;
            }
        }



    }
}