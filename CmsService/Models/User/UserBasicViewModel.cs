using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    /// <summary>
    /// اطلاعات کاربر بعد از گرفتن با توکن
    /// </summary>
    public class UserBasicViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Mobile { get; set; }
    }
}
