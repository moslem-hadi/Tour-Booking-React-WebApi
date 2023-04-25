using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using CmsApi.Swagger;
using CmsApi.Infrastructure;
using CmsApiService.Services;
using CmsApi.Infrastructure.MiddleWare;
using CmsApiService.Services.Dapper;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using CmsApiService.Entities;

namespace CmsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOriginsName = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOriginsName,
                builder =>
                {
                    builder 
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();
                });


            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<AuthenticateAttribute>(); 
            services.AddScoped<IDapperService<object>, DapperService<object>>();
            services.AddScoped<IDapperService<SystemLog>, DapperService<SystemLog>>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IApiTokenService, ApiTokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICommonService, CommonService>();

            services.AddApiVersioning(options=>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();//نحوه پیاده سازی ورژنینگ. پیشفرض هم همینه. میشه مثلا api/v1/getall
                //options.AssumeDefaultVersionWhenUnspecified = true;//اگه ورژنی نداشتیم، پیشفرض رو استفاده کنه.
                //options.DefaultApiVersion = new ApiVersion(1,0);// که پیشفرض میشه ورژن 1
                //options.ReportApiVersions = true;//نشون دادن وضعیت ورژن ها در هدر هر درخواست
            });
              services.AddSwagger();
            
            services.Configure<MvcOptions>(options => {
                options.Filters.Add(new CorsAuthorizationFilterFactory(MyAllowSpecificOriginsName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(MyAllowSpecificOriginsName); 
            app.UseCustomExceptionHandler();//باید اولین میدلور باشه که بتونه همه خطاها رو بگیره
            //توی بالایی هندل میشه
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}
  


            app.UseHttpsRedirection();
            app.UseSwaggerAndUI();
            //app.UseMiddleware<RequestLoggingMiddleware>(); برای لاگ کردن ریکوئست و رسپانس..

            app.UseMvc();
        }
    }
     
}
