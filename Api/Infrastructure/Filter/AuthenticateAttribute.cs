using CmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using CmsApiService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApi.Infrastructure
{
    /// <summary>
    /// اعتبار سنجی درخواست کننده
    /// </summary>
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        const int TOKEN_LENGTH = 36;
        private readonly IApiTokenService _apiTokenService;
        /// <summary>
        /// برای بررسی توکی که کاربر توی هدر ارسال کرده
        /// </summary>
        /// <param name="apiTokenService"></param>
        public AuthenticateAttribute(IApiTokenService apiTokenService)
        {
            _apiTokenService = apiTokenService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var allowAnonymosHeader = filterContext.ActionDescriptor.FilterDescriptors.Any(a=>a.Filter.GetType() == typeof(AllowAnonymosHeader));
            if (allowAnonymosHeader)
                return;
                var req = filterContext.HttpContext.Request;
            if (String.IsNullOrEmpty(req.Headers["Authorization"]))
            {
                UnAuthenticated(filterContext);
            }
            else
            {
                var credentials = req.Headers["Authorization"];
                var remoteIpAddress = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();

                if ((IsAuthenticatedAsync(credentials, remoteIpAddress).Result))
                {
                    var apiToken = credentials.ToString().Split(' ')[1];

                    filterContext.RouteData.Values.Add("ApiToken", apiToken);
                }
                else { UnAuthenticated(filterContext, $"دسترسی برای این توکن و آدرس {remoteIpAddress} مجاز نیست"); }
            }
        }
        private void UnAuthenticated(ActionExecutingContext filterContext, string message = null)
        {
            var apiResult = new ApiResult(false, ApiResultStatusCode.UnAuthenticated, message);
            filterContext.HttpContext.Response.StatusCode = 401;
            filterContext.Result = new JsonResult(apiResult);
        }

        private async Task<bool> IsAuthenticatedAsync(string credentials, string url)
        {
            //credential  به این صورته=> "Bearer 76631C1F-3510-49DB-BD48-F44BF8S47842"
            // که ما قسمت گویید رو میخوایم فقط

            if (string.IsNullOrWhiteSpace(credentials) || !credentials.ToLower().Contains("bearer") || credentials.Split(' ')[1].Length != TOKEN_LENGTH)
                return false;

            return (await _apiTokenService.IsTokenValid(credentials.Split(' ')[1], url));
        }
    }
}
