using CmsApi.Infrastructure;
using CmsApi.Models;
using CmsApiService.Entities;
using CmsApiService.Models;
using CmsApiService.Services;
using Microsoft.AspNetCore.Cors;
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
    public class WebController : BaseController
    {
        private readonly IMemoryCache _cache;

        private ICommonService _commonService { get; }

        public WebController(IMemoryCache memoryCache, ICommonService commonService)
        {
            _cache = memoryCache;
            _commonService = commonService;
        }
        [AllowAnonymosHeader]
        public string Index()
        {
            return DateTime.Now.ToString();
        }
        [HttpPost]
        public ApiResult<List<AdvertismentBasicViewModel>> GetPictures(AdvertismentQueryViewModel query)
        {
            //System.Threading.Thread.Sleep(3000);
            if (!Enum.IsDefined(typeof(AdvertismentPlace), query.Place))
                return BadRequest();

            return _commonService.GetPictures(query);
        }
        [HttpGet]
        [ResponseCache(Duration = 363600, Location =ResponseCacheLocation.Any)] //== 1hour
        public FirstPageDataViewModel GetFirstPageData()
        {
            return _commonService.GetFirstPageData();
        }


        [HttpGet]
        [ResponseCache(Duration = 363600, Location = ResponseCacheLocation.Any)] //== 1hour
        public List<MenuLink> GetMenuLinks(MenuPosition position)
        {
            return _commonService.GetMenuLinks(position);
        }

        [HttpGet]
        public ApiResult<PageViewModel> GetPage(string slug)
        {
            var page= _commonService.GetPage(slug);
            if (page == null)
                return NotFound();
            return page;

        }


        [HttpPost]
        public ApiResult SubmitContactUs(ContactUsForm model)
        {
            var done= _commonService.SubmitContactUs(model);

            return new ApiResult
            {
                Success = done
            };
        }

        [HttpPost]
        public void Log(SystemLogViewModel model)
        {
            _commonService.AddSystemLog(model);
        }  
        
        [HttpGet]
        public UserBasicViewModel GetUserByAuthToken(string token)
        {
            return _commonService.GetUserByAuthToken(token);
        }


    }
}
