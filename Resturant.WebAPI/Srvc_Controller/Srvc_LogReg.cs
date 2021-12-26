using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UW;
using Resturant.Services.Srvc_Internal.Auth.Hasher;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resturant = Resturant.DBModels.Entities.Resturant;



namespace Resturant.WebAPI.Auth.Srvc_Controller
{
    public class Srvc_LogReg
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IHasher _hasher;
        private readonly _IUW _UW;
        private readonly IMapper _Mapper;
        private readonly HttpContextAccessor _httpContextAccessor;


        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, _IUW UW , IMapper Mapper , IHasher hasher,HttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _hasher = hasher;
            _UW = UW;
            _Mapper = Mapper;
            _tokenService = tokenService;
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
                var Find_Username_In_DB = _UW._User.IsUserExists(user);



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

        public string LoginUser(ViwUsersInfo user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
                {
                    return "EmptyField";
                }
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.PassWord;
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return "Hashing Fail";
                user.PassWord = hash;

                var Confirm_User_Pass = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.UserName == user.UserName && x.PassWord == user.UserName && x.RoleName == "Guest").Result.FirstOrDefault();


                if (Confirm_User_Pass != null)
                {
                    var UserId =(Guid) Confirm_User_Pass.Id;
                    var userInfoDTO = _UW._Base<ViwUsersInfo>().FindByID(UserId);


                    var generatedToken = _tokenService.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), _Mapper.Map<ViwUserInfoDTO>(userInfoDTO));

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

        public string LoginResturant(ViwUsersInfo user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
                {
                    return "EmptyField";
                }
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.PassWord;
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return "Hashing Fail";
                user.PassWord = hash;

                var Confirm_User_Pass = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.UserName == user.UserName && x.PassWord == user.UserName && x.RoleName == "Resturant").Result.FirstOrDefault();


                if (Confirm_User_Pass != null)
                {

                    var UserId = Confirm_User_Pass.Id;
                    var userInfo = _UW._Base<ViwUsersInfo>().FindByID(UserId).Result;

                    var resturant = _UW._Base<resturant>().FindByConditionAsync(x=>x.UserId==userInfo.Id).Result.FirstOrDefault();
                    var generatedToken = _tokenService.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), _Mapper.Map<ViwUserInfoDTO>(userInfo), resturant.Id);

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

                user.Id = Guid.NewGuid();
                user.Password = hash;

                //Get RoleID From Roles
                user.Role = _UW._Role.GetRoleByName("Guest").ToString();
                //Insert User
                _UW._Base<User>().Insert(_Mapper.Map<User>(user));
                _UW.SaveDB();
            }
            catch (Exception)
            {

            }
        }

        public ViwUsersInfo GetUserInfo(ViwUsersInfo user)
        {
            try
            {
                var User = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.UserName == user.UserName && x.PassWord == user.UserName).Result.FirstOrDefault();

                return User;
            }
            catch (Exception)
            {
                return null;
            }

        }



    }
}
