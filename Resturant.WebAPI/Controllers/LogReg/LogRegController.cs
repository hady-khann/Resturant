using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resturant.Core.Models;
using Resturant.Infrastructure.Services.InternalServices.Auth.AuthJWT;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.WebAPI.MyServices;
using Resturant.Infrastructure.Repository.User_Repo;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Resturant.Infrastructure.DTO;
using Resturant.WebAPI.Controllers;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Controllers.LogReg
{
    
    public class LogRegController : BaseController
    {


        private readonly Srvc_LogReg _Srvc_LogReg;
        private readonly HttpContextAccessor _httpContextAccessor;


        public LogRegController(Srvc_LogReg _srvc_LogReg , HttpContextAccessor httpContextAccessor)
        {
            this._Srvc_LogReg = _srvc_LogReg;
            _httpContextAccessor = httpContextAccessor;
        }


        [AllowAnonymous]
        [Route("login")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public Global_Response_DTO<string> Login(UserDTO user)
        {
            try
            {
                var Login_Resualt_Token = _Srvc_LogReg.Login(user);
                var FullUserINFO = _Srvc_LogReg.GetUserInfo(user);
                user.UserID = FullUserINFO.Id;

                if (Login_Resualt_Token == "EmptyField" || Login_Resualt_Token == "Wrong" || Login_Resualt_Token == "NullDB")
                {
                    return Global_Controller_Result<String>(null, Login_Resualt_Token, false);
                }
                else
                {
                    _httpContextAccessor.HttpContext.Session.SetString("Token", Login_Resualt_Token.ToString());
                    return Global_Controller_Result<String>(Login_Resualt_Token, "Success", true);


                }
            }
            catch (Exception ex)
            {
                return Global_Controller_Result<String>(null, "ERROR : " + ex , false);
            }
        }

        [AllowAnonymous]
        [Route("Register")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public Global_Response_DTO<string> Register(UserDTO user)
        {
            try
            {
                var Reg_Resualt = _Srvc_LogReg.Register(user);


                if (Reg_Resualt == "Register")
                {
                    return Global_Controller_Result<String>(null, "Success", true);
                }
                else
                {
                    return Global_Controller_Result<String>(null, Reg_Resualt, false);

                }
            }
            catch (Exception ex)
            {
                return Global_Controller_Result<String>(null, "ERROR : " + ex, false);
            }
        }
       

        [Authorize(Roles = "Guest")]
        [Route("test")]
        [HttpPost]
        public Global_Response_DTO<UserDTO> test()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

                //var token = Request.HttpContext.Session.GetString("Token") ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");


                var UserFromcontext = _Srvc_LogReg.GetUserFromToken(_httpContextAccessor, token);

                return Global_Controller_Result<UserDTO>(UserFromcontext, "Success", true);
            }
            catch (Exception ex)
            {
                return Global_Controller_Result<UserDTO>(null, "ERROR : " + ex, false);
            }



        }



    }
}