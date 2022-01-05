using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UW;
using Resturant.Services.Srvc;
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
        private readonly IHasher _hasher;
        private readonly _IUW _UW;
        private readonly ISrvc _Srvc;
        private readonly IMapper _Mapper;
        private readonly HttpContextAccessor _httpContext;

        public Srvc_LogReg(IConfiguration config, IHasher hasher, _IUW uW, ISrvc srvc, IMapper mapper, HttpContextAccessor httpContext)
        {
            _config = config;
            _hasher = hasher;
            _UW = uW;
            _Srvc = srvc;
            _Mapper = mapper;
            _httpContext = httpContext;
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
                var Find_Username_In_DB = _UW._Base<User>().FindByConditionAsync(x=>x.UserName == user.UserName || x.Email == user.Email).Result.FirstOrDefault();



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

        public string Login(ViwUsersInfo user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
                {
                    return "EmptyField";
                }
                //Hash , Login Pass And Replace in UserDTO
                var Password = user.PassWord;
                //var Password = "res1";
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return "Hashing Fail";
                user.PassWord = hash;

                var usersInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.UserName == user.UserName && x.PassWord == user.PassWord).Result.FirstOrDefault();

                if (usersInfo != null  && usersInfo.Status == true)
                {
                    var UserId = (Guid)usersInfo.Id;

                    string generatedToken="";
                    if (usersInfo.RoleName == "Resturant")
                    {
                        var resturant = _UW._Base<resturant>().FindByConditionAsync(x => x.UserId == UserId).Result.FirstOrDefault();

                        if (resturant != null)
                            generatedToken = _Srvc._Token.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), usersInfo, resturant.Id);
                        else
                            return "Confirmation Progress";
                    }
                    else
                    {
                        generatedToken = _Srvc._Token.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), usersInfo);
                    }

                    if (generatedToken != null)
                    {
                        _httpContext.HttpContext.Request.Headers["Role"] = usersInfo.RoleName;
                        return generatedToken;
                    }
                    else
                    {
                        return "Wrong";
                    }
                }
                else if (user.Status == false)
                {
                    return "Deactivated";
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
                //var Password = user.Password;
                var Password = "res1";
                var hash = _hasher.Hash_Generator(Password);
                if (hash == null)
                    return;

                user.Id = Guid.NewGuid();
                user.Password = hash;
                user.Status = true;
                //Get RoleID From Roles
                user.RoleId = _UW._Base<Role>().FindByConditionAsync(x => x.RoleName == "Guest").Result.FirstOrDefault().Id;
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
