using CmsApi.Infrastructure;
using CmsApi.Models;
using CmsApiService.Models;
using CmsApiService.Models.Order;
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
    public class OrderController : BaseController
    {
        private IOrderService _orderService { get; }

        public OrderController(IMemoryCache memoryCache, IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ApiResult<SubmitOrderResult> SubmitOrder(OrderFormDataModel model)
        {
            var val= _orderService.SubmitOrder(model);
            return new ApiResult<SubmitOrderResult>
            {
                Data = val,
                Success = val?.OrderId != null,
                Message = val?.OrderId != null ? null : "خطا در ثبت سفارش. لطفا بعدا مجددا امتحان کنید."
            };
        }
    }
}