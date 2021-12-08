using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    public class ManageUsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _ContextAccessor;
        private readonly Response _response;

        private IUOW _UOW;

        public ManageUsersController(IUOW uOW, Response res, IHttpContextAccessor contextAccessor)
        {
            _UOW = uOW;
            _response = res;
            _ContextAccessor = contextAccessor;
        }



        // GET: api/<ManageUsersController>
        [HttpGet]
        [Route("GetAllUserslInfo")]
        public  Global_Response_DTO<IEnumerable<User>> GetAllUserslInfo(PaginationDTO pagination)
        {
            var UserInfo = _ContextAccessor.HttpContext.Items["UserInfo"] as UserDTO;
            var FullUsersInfo = _UOW._User.GetAllUsersINFO(pagination , UserInfo.Level.Value);
            return _response.Global_Result<IEnumerable<User>>(FullUsersInfo,"Success",true);

        }
        [HttpGet]
        [Route("GetUserByID")]

        public Task<User> GetUserByID(Guid Id)
        {
            var UserInfo = _UOW._Base<User>().FindByID(Id);
            return UserInfo;

        }

        // POST api/<ManageUsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageUsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageUsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
