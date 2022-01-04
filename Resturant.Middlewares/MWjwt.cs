using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Repository.Base;
using Resturant.DBModels.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Resturant.DBModels.DTO.Auth;
using Microsoft.AspNetCore.Builder;
using Resturant.Repository.UW;
using Resturant.DataAccess.Context;
using Resturant.Services.Srvc;

namespace Resturant.Middlewares
{
    public class MWjwt
    {

        private readonly RequestDelegate _next;
        private IConfiguration _config;

        private _IUW _UW;
        private ISrvc _Srvc;

        public MWjwt(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration config, _IUW UW, ISrvc Srvc)
        {
            _config = config;
            _UW = UW;
            _Srvc = Srvc;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                if (_Srvc._Token.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
                {
                    await AttachUserToContext(context, token);
                }

            await _next(context);

        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var key = _config["Jwt:Key"].ToString();
                var issuer = _config["Jwt:Issuer"].ToString();
                var tokenHandler = new JwtSecurityTokenHandler();
                var mySecret = Encoding.UTF8.GetBytes(key);
                var mySecurityKey = new SymmetricSecurityKey(mySecret);


                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;


                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                var getUser = await _UW._Base<User>().FindByID((Guid)userId);



                Guid? resid = null;
                if (jwtToken.Claims.First(x => x.Type == "ResturantId").Value != "")
                {
                    resid = Guid.Parse(jwtToken.Claims.First(x => x.Type == "ResturantId").Value);
                }

                context.Items["ViwUserInfoDTO"] = new AuthDTO
                {
                    Id = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value),
                    UserName = jwtToken.Claims.First(x => x.Type == "Name").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "Email").Value,
                    Status = (jwtToken.Claims.First(x => x.Type == "Status").Value) == "True" ? true : false,
                    Role = jwtToken.Claims.First(x => x.Type == "Role").Value,
                    Level = int.Parse(jwtToken.Claims.First(x => x.Type == "Level").Value),
                    ResturantId = resid,
                };
            }
            catch (Exception)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }

    public static class MWjwtExtension
    {
        public static IApplicationBuilder UseMWjwt(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MWjwt>();
        }
    }

}
