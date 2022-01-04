using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.Repository.UW;
using Resturant.Services.Srvc;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using Resturant.Services.Srvc_Repo;

namespace Resturant.Services.Installer
{
    public class ServicesInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ISrvc,Srvc.Srvc>();

            //services.AddScoped<IBase_Repo<TEntity>, Base_Repo<TEntity, ResturantContext>>();


            services.AddScoped<ISrvc_UserRes,Srvc_UserRes>();
            services.AddScoped<ISrvc_Token, Srvc_Token>();
            services.AddScoped<_IUW,UW>();
        }

      


    }
}
