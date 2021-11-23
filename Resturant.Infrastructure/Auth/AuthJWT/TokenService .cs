using Microsoft.IdentityModel.Tokens;
using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Resturant.Infrastructure.Auth.AuthJWT;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resturant.Infrastructure.Auth.AuthJWT
{
    public class TokenService : ITokenService
    {

        private const double EXPIRY_DURATION_MINUTES = 30;
        public string AuthenticateUser(string key,string issuer, UserDTO userDTO)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,userDTO.UserID.ToString()),
                new Claim(ClaimTypes.Name, userDTO.UserName),
                new Claim(ClaimTypes.Email, userDTO.Email),
                new Claim(ClaimTypes.Role, userDTO.Role)
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


        //public string GenerateJSONWebToken(string key, string issuer, UserDTO user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(issuer, issuer,
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void WriteJwtSessionToHttpContext(HttpContext context, string key, string issuer, string token)
        {
            try
            {

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


                context.Items["UserInfo"] = new UserDTO
                {
                 UserID= Guid.Parse(jwtToken.Claims.First(x => x.Type == "NameIdentifier").Value),
                 UserName= jwtToken.Claims.First(x => x.Type == "Name").Value,
                Email=jwtToken.Claims.First(x => x.Type == "Email").Value,
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
}
