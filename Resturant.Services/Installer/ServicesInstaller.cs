using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resturant.Services.Srvc_repo.Interfaces;
using Resturant.Services.Srvc_repo.Implements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using Resturant.Services.Srvc_Internal.Auth.Hasher;

namespace Resturant.Services.Installer
{
    public class ServicesInstaller
    {
        public static void Install(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IHasher, Hasher>();

            services.AddSingleton<HttpContextAccessor>();
            services.AddHttpContextAccessor();

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


        }

      


    }
}
