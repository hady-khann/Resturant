using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.DataAccess.Context;
using Resturant.Middlewares;

namespace Resturant.StartupConfiguration.StartupInstaller
{
    public static class StartupConfigurationInstaller
    {
        public static void Install_Configure_Services(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Installer.DataAccessInstaller.Install(services, configuration);
            Repository.Installer.RepossitoryInstaller.Install(services, configuration);
            Services.Installer.ServicesInstaller.Install(services, configuration);
            Utilities.Installer.UtilitiesInstaller.Install(services, configuration);


            ApiInstaller.ApiInstaller.Install(services, configuration);



        }
        public static void Install_Configure(IApplicationBuilder app)
        {
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next.Invoke();
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
