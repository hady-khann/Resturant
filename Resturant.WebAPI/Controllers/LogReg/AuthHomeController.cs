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
using Resturant.DBModels.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Auth.Srvc_Controller
{
    public class AuthHomeController:ControllerBase
    {


        private readonly Srvc_LogReg _Srvc_LogReg;
        private readonly IHttpContextAccessor _httpContext;
        private readonly Response _response;

        public AuthHomeController(Srvc_LogReg _srvc_LogReg,Response response, IHttpContextAccessor httpContext)
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
                    return _response.Global_Result<String>(null);
                }
                else
                {
                    return _response.Global_Result<String>(null, Reg_Resualt);

                }
            }
            catch (Exception)
            {
                return _response.Global_Result<String>(null);
            }
        }


        [AllowAnonymous]
        [Route("Auth/login")]
        [HttpPost]
        public Global_Response_DTO<string> Login([FromBody] ViwUsersInfo user)
        {
            try
            {
                var Login_Resualt_Token = _Srvc_LogReg.Login(user);

                if (Login_Resualt_Token == "EmptyField" || Login_Resualt_Token == "Wrong" || Login_Resualt_Token == "NullDB")
                {
                    return _response.Global_Result<String>(null,Login_Resualt_Token);
                }
                else
                {
                    _httpContext.HttpContext.Request.Headers["Authorization"] = Login_Resualt_Token;
                    _httpContext.HttpContext.Session.SetString("Token",Login_Resualt_Token);
                    return _response.Global_Result<String>(Login_Resualt_Token, _httpContext.HttpContext.Request.Headers["Role"].ToString());
                }
            }
            catch (Exception)
            {
                return _response.Global_Result<String>(null);
            }
        }


        //[Authorize(Roles = "Guest")]
        [AllowAnonymous]
        [Route("Auth/Test")]
        [HttpPost]
        public Global_Response_DTO<AuthDTO> test()
        {
            try
            {
                //var token = Request.HttpContext.Session.GetString("Token") ?? Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var UserFromcontext = _httpContext.HttpContext.Items["ViwUserInfoDTO"] as AuthDTO;
                return _response.Global_Result<AuthDTO>(UserFromcontext);
            }
            catch (Exception)
            {
                return _response.Global_Result<AuthDTO>(null);
            }
        }



    }
}