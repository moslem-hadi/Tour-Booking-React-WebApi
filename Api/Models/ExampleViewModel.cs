using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApi.Models
{
    public class ExampleViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    /// <summary>
    /// پیاده سازی این اینترفیس برای تعیین مقادیر مثال پیشفرش برای سوگر
    /// </summary>
    public class ExampleViewModelRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ExampleViewModel
            {
                Age = 30,
                Name = "Modlem"
            };
        }
    }
    /// <summary>
    /// برای مثال خروجی اکشن
    /// </summary>
    public class ExampleViewModelResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ExampleViewModel
            {
                Age = 10,
                Name = "مقدار مثال خروجی"
            };
        }
    }
}
