using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models
{
    /// <summary>
    /// نوع کاربری
    /// </summary>
    public enum UserTypes
    {
        Normal = 0,
        Hotel = 1,
        Agency = 2,
        Driver = 3
    }
    public enum GlobalValueTypes
    {
        /// <summary>
        /// نوع همراهی
        /// </summary>
        Hamrahi = 1,

        /// <summary>
        /// محل
        /// </summary>
        Place = 2,

        /// <summary>
        /// نوع وسیله نقلیه
        /// </summary>
        Vehicle = 3,

        /// <summary>
        /// نوع محصول (گشت، تور، ترانسفر)
        /// </summary>
        ProductType = 4,

        /// <summary>
        /// نوع فعالیت: گروهی و انفرادی
        /// </summary>
        ActivityType = 5
    }

    /// <summary>
    /// انواع محصولات ما در سایت
    /// </summary>
    public enum ProductType
    {
        Gasht = 1,
        Transfer = 2,
        Tour = 3
    }
    public enum ActivityTypeEnum
    {
        /// <summary>
        /// گروهی
        /// </summary>
        Group = 1,

        /// <summary>
        /// انفرادی
        /// </summary>
        Individual = 2
    }

    /// <summary>
    /// مکان نمایش تبلیغات
    /// </summary>
    public enum AdvertismentPlace
    {
        /// <summary>
        /// صفحه اصلی
        /// </summary>
        FirstPage = 1,

        /// <summary>
        /// سمت چپ همه شهر ها
        /// </summary>
        AllCitiesPage = 3,


        /// <summary>
        /// سمت چپ شهر خاص
        /// </summary>
        SpecificCityPage = 4

    }
    public enum Vehicle
    {
        Bus = 1,
        Train = 2,
        AirPlain = 3

    }
    public enum OrderStatus
    {
        UnRead = 0,
        Read = 1,
        Done =2,
        Canceled=3
    }


    public enum MenuPosition
    {
        NavBar=1,
        Footer=2
    }
}
