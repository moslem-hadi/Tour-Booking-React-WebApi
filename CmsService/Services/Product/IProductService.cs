using CmsApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
  public  interface IProductService
    {
        /// <summary>
        /// دریافت لیست تنظیمات تسک ها
        /// </summary>
        List<ProductGridViewModel> GetProducts(ProductQueryViewModel query);
        List<ProductSearchListViewModel> SearchProducts(ProductSearchViewModel query);
        ProductPageViewModel GetProduct(string slug);
    }
}
