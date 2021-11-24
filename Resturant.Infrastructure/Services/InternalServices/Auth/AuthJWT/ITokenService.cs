using Microsoft.AspNetCore.Http;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services.InternalServices.Auth.AuthJWT
{
    public interface ITokenService
    {
        string AuthenticateUser(string key, string issuer, UserDTO userDTO);
        //string GenerateJSONWebToken(string key, string issuer, UserDTO user);
        bool IsTokenValid(string key, string issuer, string token);
        void WriteJwtSessionToHttpContext(HttpContextAccessor httpContextAccessor, string key, string issuer, string token);
    }
}
