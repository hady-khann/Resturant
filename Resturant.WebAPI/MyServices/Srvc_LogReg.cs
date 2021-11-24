using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Core.Models;
using Resturant.Infrastructure.Services.InternalServices.Auth;
using Resturant.Infrastructure.Services.InternalServices.Auth.AuthJWT;
using Resturant.Infrastructure.Services.InternalServices.Auth.Hasher;
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
        private readonly IHasher _hasher;
        private readonly IUserRoleService _userroleservice;
        private readonly HttpContextAccessor _httpContextAccessor;


        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUser_Repo userRepository, IUserRoleService userroleservice, IHasher hasher,
            IRole_Repo Role, HttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _tokenService = tokenService;
            _User = userRepository;
            _userroleservice = userroleservice;
            _Role = Role;
            _hasher = hasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Register(UserDTO user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
                {
                    return "EmptyField";
                }
                //checks if User Exists in DB
                var Find_Username_In_DB = _User.IsUserExists(user);



                if (Find_Username_In_DB != null)
                {
                    return "UserExists";
                }
                else
                {
                    //Send UserDTO to Register Operation to insert to DB
                    Reg_Operation(user);
                    return "Register";
                }
            }
            catch (Exception ex)
            {
                return "Exception : " + ex.Message;
            }

        }

        public string Login(UserDTO user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                {
                    return "EmptyField";
                }
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.Password;
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return "Hashing Fail";
                user.Password = hash;

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
            catch (Exception ex)
            {
                return "Exception : " + ex.Message;
            }

        }

        public UserDTO GetUserFromToken(HttpContextAccessor httpContextAccessor, String Token)
        {
            try
            {
                _tokenService.WriteJwtSessionToHttpContext(httpContextAccessor, _config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), Token);

                var usrDTO = _httpContextAccessor.HttpContext.Items["UserInfo"] as UserDTO;


                return usrDTO;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private void Reg_Operation(UserDTO user)
        {
            try
            {
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.Password;
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return;

                user.Password = hash;


                //Get RoleID From Roles
                user.Role = _Role.GetRoleByName("Guest").ToString();
                //Insert User
                _User.AddUserAsync(user);
            }
            catch (Exception ex)
            {

            }
        }

        public User GetUserInfo(UserDTO user)
        {
            try
            {
                var User = _User.GetUserINFOAsync(user);

                return User;
            }
            catch (Exception)
            {
                return null;
            }

        }



    }
}
