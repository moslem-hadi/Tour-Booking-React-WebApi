using CmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Infrastructure;

namespace CmsApi.Controllers.v2
{
    [ApiVersion("2")]
    public class TestController : Controllers.TestController
    {
         
        [HttpGet]
        public override string CheckVerion()
        {
            return "version 2";
        }


        [ApiResultFilter]
        [NonAction]//برای اینکه نشون بدیم دیگه توی این ورژن وجود نداره
        [HttpGet]
        public override string NonActionTest()
        {
            return base.NonActionTest();
        }

        public override ExampleViewModel ActionWithExampleModel(ExampleViewModel model)
        {
            return base.ActionWithExampleModel(model);
        }


        public override ActionResult Address(AddressDto addressDto)
        {
            return base.Address(addressDto);
        }


    }

}
