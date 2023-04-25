using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Classes
{
    public class ProductTypesJson
    {
        /// <summary>
        /// مقدار قابل ذخیره
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// اسم انگلیسی
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// عکس
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// عنوان فارسی
        /// </summary>
        public string Title { get; set; }
    }
}