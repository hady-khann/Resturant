using Resturant.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.Repository.UOW;
using Resturant.Repository.Base;
using Resturant.DataAccess.Context;

namespace Resturant.Repository.Installer
{
    public class RepossitoryInstaller
    {

        public static void Install(IServiceCollection services, IConfiguration Configuration)
        {


            services.AddScoped<IUOW, Resturant.Repository.UOW.UOW>();

            //services.AddScoped<IBase_Repo<TEntity>, Base_Repo<TEntity, ResturantContext>>();


            services.AddScoped<IRole_Repo, Role_Repo>();
            services.AddScoped<IUser_Repo, User_Repo>();

        }
    }
}
