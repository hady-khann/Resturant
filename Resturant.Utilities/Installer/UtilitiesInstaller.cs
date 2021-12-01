
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Resturant.Utilities.Installer
{
    public class ApiInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IFileManager, FileManager>();
            //services.AddSingleton<IHttpHelper, HttpHelper>();
            //services.AddSingleton<ICookieManager, CookieManager>();
            //services.AddSingleton<ISendMail, SendMail>();
        }

        public static IServiceProvider ConfigureServices(IServiceCollection service)
        {

            var serviceProvider = service.BuildServiceProvider();

            var accessor = serviceProvider.GetService<IHttpContextAccessor>();


            return serviceProvider;
        }
    }
}


