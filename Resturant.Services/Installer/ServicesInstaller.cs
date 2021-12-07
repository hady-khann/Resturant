using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.Services.Srvc_repo.Interfaces;
using Resturant.Services.Srvc_repo.Implements;

namespace Resturant.Services.Installer
{
    public class ServicesInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IUserRoleService, UserRoleService>();

        }

      


    }
}
