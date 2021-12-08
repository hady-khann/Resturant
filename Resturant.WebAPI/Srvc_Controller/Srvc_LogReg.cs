using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
using Resturant.Services.Srvc_Internal.Auth.Hasher;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using Resturant.Services.Srvc_repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Resturant.WebAPI.Auth.Srvc_Controller
{
    public class Srvc_LogReg
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IHasher _hasher;
        private readonly IUOW _UOW;
        private readonly IUserRoleService _userroleservice;
        private readonly HttpContextAccessor _httpContextAccessor;


        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUOW uow ,
            IUserRoleService userroleservice, IHasher hasher,HttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _hasher = hasher;
            _UOW = uow;
            _tokenService = tokenService;
            _userroleservice = userroleservice;
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
                var Find_Username_In_DB = _UOW._User.IsUserExists(user);



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

                var Confirm_User_Pass = _UOW._User.CheckUserPass(user);


                if (Confirm_User_Pass != null)
                {

                    UserDTO userDTO = _userroleservice.GetUserRoleDTO(Confirm_User_Pass);


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

        private void Reg_Operation(UserDTO user)
        {
            try
            {
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.Password;
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return;

                user.UserID = Guid.NewGuid();
                user.Password = hash;

                //Get RoleID From Roles
                user.Role = _UOW._Role.GetRoleByName("Guest").ToString();
                //Insert User
                _UOW._Base<User>().Insert((User)user);
                _UOW.SaveDB();
            }
            catch (Exception)
            {

            }
        }

        public User GetUserInfo(UserDTO user)
        {
            try
            {
                var User = _UOW._User.CheckUserPass(user);

                return User;
            }
            catch (Exception)
            {
                return null;
            }

        }



    }
}
