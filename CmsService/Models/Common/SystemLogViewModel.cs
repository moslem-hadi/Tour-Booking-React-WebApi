using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    /// <summary>
    /// مدل ارسالی برای ذخیره خطا
    /// </summary>
    public class SystemLogViewModel
    {
        public string Message { get; set; }

        public string Exception { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }
    }
}
