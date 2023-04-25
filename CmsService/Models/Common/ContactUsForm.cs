using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    /// <summary>
    /// فرم تماس با ما
    /// </summary>
    public class ContactUsForm
    {
        public string Name { get; set; }
        public string  Mobile { get; set; }
        public string Message { get; set; }
    }
}
