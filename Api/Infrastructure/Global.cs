using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApi
{
    //این ها بعدا از یه جیسون خونده بشه و توی یه کلاس ریخته بشه.
    public static class CONSTANTS
    {
        public static class MESSEGES
        {
            /// <summary>
            /// پارامترهای ورودی را بررسی کنید
            /// </summary>
            public static string CHECK_INPUT_PARAMETERS => "پارامترهای ورودی را بررسی کنید";

            /// <summary>
            /// محصول غیرفعال در درخواست...
            /// </summary>
            public static string INVALID_PRODUCT_IN_REQUEST => "محصول غیرفعال در درخواست: {0}";
        }
    }
}
