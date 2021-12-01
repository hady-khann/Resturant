using System;
using Resturant.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Resturant.StartupConfiguration.ApiInstaller
{
    internal class ApiInstaller
    {

        internal static void Install(IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
