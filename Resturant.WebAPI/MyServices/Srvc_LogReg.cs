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
using Resturant.Infrastructure.Repository.Role_Repo;

namespace Resturant.WebAPI.MyServices
{
    public class Srvc_LogReg
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUser_Repo _User;
        private readonly IRole_Repo _Role;
        private readonly IUserRoleService _userroleservice;

        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUser_Repo userRepository, IUserRoleService userroleservice , IRole_Repo Role)
        {
            _config = config;
            _tokenService = tokenService;
            _User = userRepository;
            _userroleservice = userroleservice;
            _Role = Role;
        }

        public string Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
            {
                return "EmptyField";
            }

            var Confirm_User_In_DB = _User.GetUserINFOAsync(user);



            if (Confirm_User_In_DB != null)
            {

                UserDTO userDTO = _userroleservice.GetUserRoleDTO(Confirm_User_In_DB);
                var generatedToken = _tokenService.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDTO);

                if (generatedToken != null)
                {
                    return generatedToken;
                }
                else
                {
                    return "Wrong";
                }
            }
            else
            {
                    return "NullDB";
            }

        }

        public string Register(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
            {
                return "EmptyField";
            }

            var Find_Username_In_DB = _User.IsUserExists(user);



            if (Find_Username_In_DB != null)
            {
                return "UserExists";
            }
            else
            {
                Reg_Operation(user);
                return "Register";
            }

        }


        private void Reg_Operation(UserDTO user)
        {
            user.Role = _Role.GetRoleByName("Guest").ToString();
            _User.AddUserAsync(user);
        }


    }
}
