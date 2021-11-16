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
using Resturant.Infrastructure.Repository.UserRepo;

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
            var Token_Login_Resualt = _Srvc_LogReg.Login(user);

            if (Token_Login_Resualt == "Empty" || Token_Login_Resualt == "Unauthorized" || Token_Login_Resualt == "Null")
            {
                return BadRequest();
            }
            else
            {
                HttpContext.Session.SetString("Token", Token_Login_Resualt);
                return null;
            }
        }



        // GET: api/<LogRegController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LogRegController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogRegController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogRegController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogRegController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
