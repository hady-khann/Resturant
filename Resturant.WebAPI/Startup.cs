using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;




using Resturant.StartupConfiguration.StartupInstaller;

using Resturant.WebAPI.Auth.Srvc_Controller;

namespace Resturant.WebAPI.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resturant.WebAPI", Version = "v1" });
            });

            #region Install Configure Services
            StartupConfigurationInstaller.Install_Configure_Services(services, Configuration);
            #endregion



            #region Internal Injections
            services.AddScoped<Srvc_LogReg>(); 
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            StartupConfigurationInstaller.Install_Configure(app);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resturant.WebAPI v1"));
            }

            StartupConfigurationInstaller.Install_Configure(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }
}
