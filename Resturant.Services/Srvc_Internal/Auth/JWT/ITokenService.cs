using Microsoft.AspNetCore.Http;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc_Internal.Auth.JWT
{
    public interface ITokenService
    {
        string AuthenticateUser(string key, string issuer, ViwUserInfoDTO userInfoDTO);
        //string GenerateJSONWebToken(string key, string issuer, UserDTO user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
