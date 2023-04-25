using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsApiService.Models;
using CmsApiService.Services;
using CmsApiService.Services.Dapper;
using Microsoft.Extensions.Configuration;

namespace CmsApiService.Infrastructure
{
    /// <summary>
    /// کلاس رجیستر کننده دپندنسی ها با استفاده از اتوفک
    /// autoFac
    /// </summary>
    public static class AutofacConfigurations
    {
        private static IContainer Container { get; set; }
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            #region مثال

            //مثال عادی
            //builder.RegisterType<RunTaskConfigService>().
            //        As<IRunTaskConfigService>();
            //builder.RegisterType<WindowsServiceLogService>().
            //        As<IWindowsServiceLogService>();


            //شبه کد برای کلاس های جنریک
            //ولی مثل اینکه کار نمیکنه!
            //builder.RegisterGeneric(typeof(DapperService<>)).As(typeof(IDapperService<>)); 

            #endregion


            //رجیستر کردن تمام کلاس هایی که از 
            //IScopedDependency
            //ارث بری کردن به صورت خودکار
            //این روش از نظر اجرا کمی کندتر هست. اما چون فقط یکبار موقع اجرای سرویس اتفاق میفته، مهم نیس
            var assembelies = typeof(IScopedDependency).Assembly;
            builder.RegisterAssemblyTypes(assembelies)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();



            //برای این کلاس های جنریک، نمیدونم چرا خودکار رجیستر نمیشدن! به این صورت دستی انجام شد.
            builder.RegisterType<DapperService<ProductGridViewModel>>().
                    As<IDapperService<ProductGridViewModel>>();
            builder.RegisterType<IConfiguration>() ;
    builder.RegisterType<DapperService<object>>().
                    As<IDapperService<object>>();


            Container = builder.Build();
            return Container;
        }
    }
}
