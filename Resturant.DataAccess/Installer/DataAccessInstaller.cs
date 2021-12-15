using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.DataAccess.Context;


namespace Resturant.DataAccess.Installer
{
    public class DataAccessInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<_>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

        }
    }
}
