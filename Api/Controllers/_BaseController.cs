using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{ 
    //[ApiVersion("1")] چون از سوگر استفاده میکنیم، این اتریبیوت باید حتما روی کنترلر ها باشه.
    [Route("/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// توکن کاربر که موقع درخواست ذخیره میکنیم توی این متغیر و در همه کنترل ها در دسترس هست
        /// </summary>
        public string ApiToken => RouteData.Values["ApiToken"] as string;
    }
}