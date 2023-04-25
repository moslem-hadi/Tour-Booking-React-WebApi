using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Entities
{
    [Table("SystemLogs")]
    public class SystemLog
    {
        public SystemLog( )
        {
            RegDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }

        public DateTime RegDate { get; set; }
    }
}
