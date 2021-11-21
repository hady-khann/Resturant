using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Core.Models;
using Resturant.Infrastructure.Auth.AuthJWT;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.User_Repo;
using Resturant.Infrastructure.Services.Srvs_UserRole;




namespace Resturant.WebAPI.MyServices
{
    public class Srvc_LogReg
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUser_Repo _User;
        private readonly IUserRoleService _userroleservice;

        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUser_Repo userRepository, IUserRoleService userroleservice)
        {
            _config = config;
            _tokenService = tokenService;
            _User = userRepository;
            _userroleservice = userroleservice;
        }

        public string Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
            {
                return "Please Enter UserName And PassWord";
            }

            var Confirm_User_In_DB = _User.GetUserINFO(user);



            if (Confirm_User_In_DB != null)
            {

                UserDTO userDTO = _userroleservice.GetUserRoleDTO(user);
                var generatedToken = _tokenService.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDTO);

                if (generatedToken != null)
                {
                    return generatedToken;
                }
                else
                {
                return "Wrong Username Or Password";
                }
            }
            else
            {
                    return "User doesn't exists";
            }

        }



    }
}
