using CmsApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Entities
{
    public class GlobalValueEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public GlobalValueTypes Type  { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public int? Priority { get; set; }
    }
}
