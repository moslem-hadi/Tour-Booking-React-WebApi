using CmsApiService.Models;
using CmsApiService.Services.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsApiService.Infrastructure.Helper;

namespace CmsApiService.Services
{
    public class ProductService: IProductService
    { 

        private readonly IDapperService<object> _dapperService;
        private IConfiguration _configuration { get; }
        private readonly ILogger<ProductService> _logger;
        public ProductService(IConfiguration configuration, ILogger<ProductService> logger, Dapper.IDapperService<object> dapperService)
        {
            _configuration = configuration;
            _dapperService = dapperService;
            _logger = logger;
        }

         
        public List<ProductGridViewModel> GetProducts(ProductQueryViewModel query)
        {
            try
            {
                string sql = $@"
                    select   {(query.Count == 0 ? "" : "top(@count)")}  p.ID, p.Title,p.Description,p.Pic, p.Slug, g.ID as CityId,
                        g.Title as CityTitle , g.Slug as CitySlug 
                    FROM product p 
                    JOIN ProductGroup g ON g.ID= p.GroupID
                    WHERE g.Show = 1 AND p.IsActive=1 AND [TypeValue]= @type
                    Order BY ID desc";

                var products = _dapperService.Query<ProductGridViewModel>(sql, new
                {
                    count = query.Count,
                    type = (int)query.ProductType
                }).ToList();
                
                return products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ProductSearchListViewModel> SearchProducts(ProductSearchViewModel criteria)
        {
            //System.Threading.Thread.Sleep(4000);
            string enDate = null;
            if (!string.IsNullOrWhiteSpace(criteria.Date) && criteria.Date.Split("-").Count()==3)
                enDate = criteria.Date.Replace("-","/").ToMiladi()?.ToShortDateString();
            try
            {
                string sql = $@"SearchProducts";

                var products = _dapperService.Query<ProductSearchListViewModel>(sql, criteria, System.Data.CommandType.StoredProcedure).ToList();

                return products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ProductPageViewModel GetProduct(string slug)
        { 
             
            try
            {
                string sql = $@"GetProductInfo";

                var product = _dapperService.Query<ProductPageViewModel>(sql, new { slug}, System.Data.CommandType.StoredProcedure).FirstOrDefault();
                if (product != null)
                {
                    //ممکنه فقط یک رکورد یا چندتا یکسان توی اون جدول باشه
                    product.MinPrice = product.DefaultPrice < product.MinPrice ? product.DefaultPrice : product.MinPrice;
                    product.MaxPrice = product.DefaultPrice >product.MaxPrice ? product.DefaultPrice : product.MaxPrice;
                }
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
