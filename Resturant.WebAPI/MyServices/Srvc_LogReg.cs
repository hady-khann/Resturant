using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Core.Models;
using Resturant.Infrastructure.Services.InternalServices.Auth.AuthJWT;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.User_Repo;
using Resturant.Infrastructure.Services.RepoServices.Srvs_UserRole;
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
        private readonly HttpContextAccessor _httpContextAccessor;


        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUser_Repo userRepository, IUserRoleService userroleservice ,
            IRole_Repo Role, HttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _tokenService = tokenService;
            _User = userRepository;
            _userroleservice = userroleservice;
            _Role = Role;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Login(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return "EmptyField";
            }

            var Confirm_User_In_DB = _User.GetUserINFOAsync(user);



            if (Confirm_User_In_DB != null)
            {

                UserDTO userDTO = _userroleservice.GetUserRoleDTO(Confirm_User_In_DB);
                userDTO.UserID = Confirm_User_In_DB.Id;


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



        public UserDTO GetUserFromToken(HttpContextAccessor httpContextAccessor,String Token)
        {
            _tokenService.WriteJwtSessionToHttpContext(httpContextAccessor, _config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(),Token);

            var usrDTO = _httpContextAccessor.HttpContext.Items["UserInfo"] as UserDTO;


            return usrDTO;


        }
        private void Reg_Operation(UserDTO user)
        {
            user.Role = _Role.GetRoleByName("Guest").ToString();
            _User.AddUserAsync(user);
        }

        public User GetUserInfo(UserDTO user)
        {
            var User = _User.GetUserINFOAsync(user);

            return User;

        }



    }
}
