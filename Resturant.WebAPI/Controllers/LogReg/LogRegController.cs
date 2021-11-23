using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Resturant.Core.Models;
using Resturant.Infrastructure.Auth.AuthJWT;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.WebAPI.MyServices;
using Resturant.Infrastructure.Repository.User_Repo;
using System.Net;
using Resturant.Infrastructure.DTO;
using Resturant.WebAPI.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Controllers.LogReg
{
    
    public class LogRegController : BaseController
    {


        private readonly Srvc_LogReg _Srvc_LogReg;


        public LogRegController(Srvc_LogReg _srvc_LogReg)
        {
            this._Srvc_LogReg = _srvc_LogReg;
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public Global_Response_DTO<string> Login(User user)
        {
            var Login_Resualt_Token = _Srvc_LogReg.Login(user);


            if (Login_Resualt_Token == "EmptyField" || Login_Resualt_Token == "Wrong" || Login_Resualt_Token == "NullDB")
            {
                return Global_Controller_Result<String>(null, Login_Resualt_Token,false);
            }
            else
            {
                HttpContext.Session.SetString("Token", Login_Resualt_Token);
                return Global_Controller_Result<String>(null, "Success", true);


            }
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public Global_Response_DTO<string> Register(UserDTO user)
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



    }
}