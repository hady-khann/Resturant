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
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;

namespace Resturant.Services.Srvc_Internal.Auth.JWT
{
    public class Srvc_Token : ISrvc_Token
    {

        private const double EXPIRY_DURATION_MINUTES = 30;
        public string AuthenticateUser(string key, string issuer, ViwUsersInfo userInfoDTO , Guid? returantGUID = null)
        {
            try
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier,userInfoDTO.UserName),
                    new Claim(ClaimTypes.Name, userInfoDTO.UserName),
                    new Claim(ClaimTypes.Role, userInfoDTO.RoleName),
                    new Claim("Id",userInfoDTO.Id.ToString()),
                    new Claim("ResturantId",returantGUID.ToString()),
                    new Claim("Name", userInfoDTO.UserName),
                    new Claim("Email", userInfoDTO.Email),
                    new Claim("Role", userInfoDTO.RoleName),
                    new Claim("Level", userInfoDTO.AccessLevel.ToString()),
                    new Claim("Status", userInfoDTO.Status.ToString()),
                    };


                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                // todo : Expiration
                var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                    expires: DateTime.Now.AddYears(10), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
            catch (Exception)
            {
                return null;
            }
        }

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
    }
}
