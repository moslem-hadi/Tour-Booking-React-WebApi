using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    public class FirstPageDataViewModel
    {
        public FirstPageDataViewModel()
        {
            Cities = new List<TitleValueViewModel>();
            HamrahiTypes = new List<TitleValueViewModel>();
            PlaceTypes = new List<TitleValueViewModel>();
            VehicleTypes = new List<TitleValueViewModel>();
            ActivityTypes = new List<TitleValueViewModel>();
        }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
        /// <summary>
        /// seo title
        /// </summary>
        public string WebsiteTitle { get; set; }
        /// <summary>
        /// seo desc
        /// </summary>
        public string WebsiteDescription { get; set; }
        public string FirstPageHeader { get; set; }

        public string SliderPic { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubTitle { get; set; }
        public string SliderText { get; set; }

        public List<TitleValueViewModel> Cities { get; set; }
        public List<TitleValueViewModel> HamrahiTypes { get; set; }
        public List<TitleValueViewModel> PlaceTypes { get; set; }
        public List<TitleValueViewModel> VehicleTypes { get; set; }
        public List<TitleValueViewModel> ActivityTypes { get; set; }

        /// <summary>
        /// تعداد نمایش تورهای در صفحه اول
        /// </summary>
        public int FirstPageProductCount { get; set; }

    }
}
