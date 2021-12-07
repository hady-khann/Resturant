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
using Resturant.Repository.UOW;
using Resturant.DataAccess.Context;

namespace Resturant.Middlewares
{
    public class MWjwt
    {

        private readonly RequestDelegate _next;
        private IConfiguration _config;
        
        private IUOW _UOW;

        public MWjwt(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration config ,IUOW uow)
        {
            _config = config;
            _UOW = uow;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            if (token != null)
                await AttachUserToContext(context, token);

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
                var getUser = await _UOW._Base<User>().FindByID((Guid)userId);


                context.Items["UserInfo"] = new UserDTO
                {
                    UserID = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value),
                    UserName = jwtToken.Claims.First(x => x.Type == "Name").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "Email").Value,
                    Role = jwtToken.Claims.First(x => x.Type == "Role").Value,
                }; 
            }
            catch (Exception ex)
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
