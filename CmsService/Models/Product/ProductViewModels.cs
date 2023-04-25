using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    public class ProductGridViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Pic { get; set; }

        public string Slug { get; set; }

        public int CityId { get; set; }

        public string CityTitle { get; set; }

        public string CitySlug { get; set; }
    }

    public class ProductSearchListViewModel: ProductGridViewModel
    { 
        public int Price { get; set; }
        public int SelectedCount { get; set; } = 1;
        public bool IsSelected { get; set; } = false;
    }

    public class ProductQueryViewModel
    {
        public ProductType ProductType { get; set; }

        public int Count { get; set; }

    }
    public class ProductSearchViewModel
    {
        /// <summary>
        /// نوع محصول: گشت، تور و...
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// تاریخ
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// همراهی
        /// </summary>
        public int? CompType { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// نوع خودرو
        /// </summary>
        public int? Vehicle { get; set; }


        /// <summary>
        /// گروهی یا انفرادی
        /// </summary>
        public int? ActType { get; set; }

        /// <summary>
        /// مکان
        /// </summary>
        public int? Place { get; set; }
    }

    /// <summary>
    /// مدل صفحه نمایش جزئیات محصول
    /// </summary>
    public class ProductPageViewModel
    {

        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Pic { get; set; }

        public string Slug { get; set; }

        public int CityId { get; set; }

        public string CityTitle { get; set; }

        public string CitySlug { get; set; }

        public ProductType Type { get; set; }

        public List<ProductGallery> Gallery{ get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice{ get; set; }
        public int DefaultPrice{ get; set; }

    }


    public class ProductGallery
{
    public int ID{ get; set; }
        public string FileName { get; set; }
    public string Title { get; set; }

}


}
