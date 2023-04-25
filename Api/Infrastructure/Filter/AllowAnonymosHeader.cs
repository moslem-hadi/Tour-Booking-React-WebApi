using Microsoft.AspNetCore.Mvc.Filters;

namespace CmsApi.Infrastructure
{
    /// <summary>
    /// برای غیرفعال کردن اعتبارسنجی روی اکشن ها، این اتریبیوت روی اکشن گذاشته بشه
    /// </summary>
    internal class AllowAnonymosHeader : ActionFilterAttribute
    {
    }
}