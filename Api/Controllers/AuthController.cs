using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers.v1
{
    [ApiVersion("1")]
    //[ApiVersion("2")]//هم 1 و هم 2
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public string GetToken()
        {
            return "GetToken";
        }
    }
}