using CmsApiService.Models;
using CmsApiService.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
  public  interface IOrderService
    {

        SubmitOrderResult SubmitOrder(OrderFormDataModel model);
    }
}
