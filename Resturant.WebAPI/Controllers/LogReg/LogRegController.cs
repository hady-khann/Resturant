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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Controllers.LogReg
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogRegController : ControllerBase
    {
        private readonly Srvc_LogReg _Srvc_LogReg;
        public LogRegController(Srvc_LogReg _srvc_LogReg)
        {
            this._Srvc_LogReg = _srvc_LogReg;
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var Login_Resualt_Token = _Srvc_LogReg.Login(user);

            if (Login_Resualt_Token == "Please Enter UserName And PassWord" || 
                Login_Resualt_Token == "Wrong Username Or Password" ||
                Login_Resualt_Token == "User doesn't exists")
            {
                return BadRequest(Login_Resualt_Token);
            }
            else
            {
                HttpContext.Session.SetString("Token", Login_Resualt_Token);
                return null;
            }
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(User user)
        {
            var Login_Resualt_Token = _Srvc_LogReg.Login(user);

            if (Login_Resualt_Token == "Empty" || Login_Resualt_Token == "Unauthorized" || Login_Resualt_Token == "Null")
            {
                return BadRequest(Login_Resualt_Token);
            }
            else
            {
                HttpContext.Session.SetString("Token", Login_Resualt_Token);
                return null;
            }
        }



    }
}
