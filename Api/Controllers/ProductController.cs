using CmsApi.Infrastructure;
using CmsApi.Models;
using CmsApiService.Models;
using CmsApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApi.Controllers
{ 
    [ApiVersion("1")]
    [ServiceFilter(typeof(AuthenticateAttribute))]//اعتبار سنجی کاربر
    [ApiResultFilter]
    public class ProductController : BaseController
    {
        private readonly IMemoryCache _cache;

        private IProductService _productService { get; }

        public ProductController(IMemoryCache memoryCache, IProductService productService)
        {
            _cache = memoryCache;
            _productService = productService;
        }

        [HttpPost]
        public ApiResult<List<ProductGridViewModel>> GetProducts(ProductQueryViewModel query)
        {
             
            var result = _productService.GetProducts(query);
            if (result == null)
                return BadRequest();
            else
                return result;
        }
        [HttpPost]
        public ApiResult<List<ProductSearchListViewModel>> SearchProducts(ProductSearchViewModel query)
        { 

            var result= _productService.SearchProducts(query);
            if (result == null)
                return BadRequest();
            else
                return result;
        }

        [HttpGet]
        public ApiResult<ProductPageViewModel> GetProduct(string slug)
        {

            var result = _productService.GetProduct(slug);
            if (result == null)
                return BadRequest();
            else
                return result;
        }
    }
}
