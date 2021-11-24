using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Infrastructure.Services.InternalServices.Auth.AuthJWT;
using Resturant.Infrastructure.Services.InternalServices.Auth.Hasher;
using Resturant.Infrastructure.Repository;
using Resturant.Infrastructure.DTO;
using Resturant.Infrastructure.Services;
using Resturant.Infrastructure.Repository.User_Repo;
using Resturant.Infrastructure.Repository.Role_Repo;
using Resturant.Infrastructure.Services.RepoServices.Srvs_UserRole;
using Resturant.Core.Models;
using Microsoft.EntityFrameworkCore;
using Resturant.WebAPI.MyServices;
using Resturant.WebAPI.Controllers;

namespace Resturant.WebAPI
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


            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resturant.WebAPI", Version = "v1" });
            });

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

           // services.AddSingleton<IHttpContextAccessor>();

            services.AddDbContext<ResturantContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUser_Repo, User_Repo>();
            services.AddScoped<IRole_Repo, Role_Repo>();

            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<Srvc_LogReg>();
            services.AddScoped<IHasher, Hasher>();

            services.AddSingleton<HttpContextAccessor>();
            services.AddHttpContextAccessor();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resturant.WebAPI v1"));
            }


            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseStaticFiles();


            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

           


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
