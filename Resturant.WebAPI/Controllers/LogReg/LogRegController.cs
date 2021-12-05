using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Resturant.DBModels.DTO.Auth;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Auth.Srvc_Controller
{
    public class LogRegController:ControllerBase
    {


        private readonly Srvc_LogReg _Srvc_LogReg;
        private readonly HttpContextAccessor _httpContext;
        private readonly Response _response;

        public LogRegController(Srvc_LogReg _srvc_LogReg,Response response, HttpContextAccessor httpContext)
        {
            this._Srvc_LogReg = _srvc_LogReg;
            _httpContext = httpContext;
            _response = response;
        }


        [AllowAnonymous]
        [Route("Auth/Register")]
        [HttpPost]
        public Global_Response_DTO<string> Register([FromBody] UserDTO user)
        {
            try
            {
                var Reg_Resualt = _Srvc_LogReg.Register(user);


                if (Reg_Resualt == "Register")
                {
                    return _response.Global_Controller_Result<String>(null, "Success", true);
                }
                else
                {
                    return _response.Global_Controller_Result<String>(null, Reg_Resualt, false);

                }
            }
            catch (Exception ex)
            {
                return _response.Global_Controller_Result<String>(null, "ERROR : " + ex, false);
            }
        }


        [AllowAnonymous]
        [Route("Auth/login")]
        [HttpPost]
        public Global_Response_DTO<string> Login([FromBody] UserDTO user)
        {
            try
            {
                var Login_Resualt_Token = _Srvc_LogReg.Login(user);
                var FullUserINFO = _Srvc_LogReg.GetUserInfo(user);
                user.UserID = FullUserINFO.Id;

                if (Login_Resualt_Token == "EmptyField" || Login_Resualt_Token == "Wrong" || Login_Resualt_Token == "NullDB")
                {
                    return _response.Global_Controller_Result<String>(null, Login_Resualt_Token, false);
                }
                else
                {
                    _httpContext.HttpContext.Session.SetString("Token", Login_Resualt_Token.ToString());
                    return _response.Global_Controller_Result<String>(Login_Resualt_Token, "Success", true);


                }
            }
            catch (Exception ex)
            {
                return _response.Global_Controller_Result<String>(null, "ERROR : " + ex, false);
            }
        }




        [Authorize(Roles = "Guest")]
        [Route("Auth/Test")]
        [HttpPost]
        public Global_Response_DTO<UserDTO> test()
        {
            try
            {
                //var token = Request.HttpContext.Session.GetString("Token") ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var UserFromcontext = _httpContext.HttpContext.Items["UserInfo"] as UserDTO;
                return _response.Global_Controller_Result<UserDTO>(UserFromcontext, "Success", true);
            }
            catch (Exception ex)
            {
                return _response.Global_Controller_Result<UserDTO>(null, "Error : " + ex, false);
            }
        }



    }
}