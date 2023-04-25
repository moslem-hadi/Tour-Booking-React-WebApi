using CmsApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Entities
{
    /// <summary>
    /// لینک های منوها
    /// </summary>
    public class MenuLink
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int Priority { get; set; }
        public int ParentId { get; set; }
        public MenuPosition Position { get; set; }
    }
}
