using Microsoft.AspNetCore.Http;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc_Internal.Auth.JWT
{
    public interface ISrvc_Token
    {
        string AuthenticateUser(string key, string issuer, ViwUsersInfo userInfo, Guid? returantGUID = null);
        //string GenerateJSONWebToken(string key, string issuer, UserDTO user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
