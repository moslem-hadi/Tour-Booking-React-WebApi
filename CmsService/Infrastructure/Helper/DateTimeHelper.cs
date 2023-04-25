using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Infrastructure.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime? ToMiladi(this object input)
        {
            try
            {
                if (input==null)
                    return null;
                string datetime = input.ToString();
                int[] startdatestring = datetime.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
                DateTime date = new DateTime(startdatestring[0], startdatestring[1], startdatestring[2], new PersianCalendar());
                return date;
            }
            catch
            {
                return null;
            }
        }
    }
}
