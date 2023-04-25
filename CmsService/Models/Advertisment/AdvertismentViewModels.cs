using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    /// <summary>
    /// مدل خروجی تبلیغات.
    /// </summary>
    public class AdvertismentBasicViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public string Link { get; set; }
        public int Place { get; set; }
        /// <summary>
        /// تعداد ستون های در بوتسترپ
        /// </summary>
        public string ColumnSize { get; set; }
        public string Priority { get; set; }
    }
    /// <summary>
    /// مدل درخواست تبلیغات
    /// </summary>
    public class AdvertismentQueryViewModel
    {
        /// <summary>
        /// مکان تبلیغات
        /// </summary>
        public AdvertismentPlace Place { get; set; }

        /// <summary>
        /// تعداد مورد نیاز. 0 یعنی همه
        /// </summary>
        public int Count { get; set; }

    }
}
