using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Resturant.DBModels.DTO.Auth;

namespace Resturant.Services.Auth.JWT

{
    public class TokenService : ITokenService
    {

        private const double EXPIRY_DURATION_MINUTES = 30;
        public string AuthenticateUser(string key, string issuer, UserDTO userDTO)
        {
            try
            {
                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier,userDTO.UserName),
                    new Claim(ClaimTypes.Name, userDTO.UserName),
                    new Claim(ClaimTypes.Role, userDTO.Role),
                    new Claim("Id",userDTO.UserID.ToString()),
                    new Claim("Name", userDTO.UserName),
                    new Claim("Email", userDTO.Email),
                    new Claim("Role", userDTO.Role)
                    };


                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                    expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
            catch (Exception)
            {
                return null;
            }
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
            try
            {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
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

        public void WriteJwtSessionToHttpContext(HttpContextAccessor httpContextAccessor, string key, string issuer, string token)
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

                httpContextAccessor.HttpContext.Items["UserInfo"] = new UserDTO
                {
                    UserID = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value),
                    UserName = jwtToken.Claims.First(x => x.Type == "Name").Value,
                    Email = jwtToken.Claims.First(x => x.Type == "Email").Value,
                    Role = jwtToken.Claims.First(x => x.Type == "Role").Value,
                };
            }
            catch (Exception)
            {

            }
        }


    }
}
