using CmsApiService.Entities;
using CmsApiService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
  public  interface ICommonService
    {
        /// <summary>
        /// دریافت لیست تبلیغات. به خاطر بن نشدن تبلیغات از کلماتی مشابه
        /// ad , ads , advertise ,....
        /// استفاده نشده
        /// </summary>
        /// <param name="query">مدل درخواستی</param>
        List<AdvertismentBasicViewModel> GetPictures(AdvertismentQueryViewModel query);


        /// <summary>
        /// دریافت اطلاعاات مورد نیاز صفحه اول سایت
        /// </summary>
        /// <returns></returns>
        FirstPageDataViewModel GetFirstPageData();
        List<MenuLink> GetMenuLinks(MenuPosition position);
        PageViewModel GetPage(string slug);
        bool SubmitContactUs(ContactUsForm model);

        /// <summary>
        /// افزودن لاگ
        /// </summary>
        /// <param name="model"></param>
        void AddSystemLog(SystemLogViewModel model);

        /// <summary>
        /// گرفتن اطلاعات کاربر با توکن لوگین
        /// </summary>
        /// <param name="token">توکن لوگین</param>
        /// <returns></returns>
        UserBasicViewModel GetUserByAuthToken(string token);
    }
}
