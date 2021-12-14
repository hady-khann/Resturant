using System;
using Resturant.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.CoreBase.WebAPIResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Resturant.DBModels.AutoMaping;

namespace Resturant.StartupConfiguration.ApiInstaller
{
    internal class ApiInstaller
    {

        internal static void Install(IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDistributedMemoryCache();
            services.AddScoped<Response>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddSingleton<HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            services.AddHttpContextAccessor();

            #region JWT Bearer
            services.AddAuthentication(auth =>
              {
                  auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

              })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

            });
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(ApiInstaller));

            var mappingConfig = new MapperConfiguration(configire =>
            {
                configire.AddProfile(new AutoMaping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }
    }
}
