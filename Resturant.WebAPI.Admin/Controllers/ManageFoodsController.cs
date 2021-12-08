using Microsoft.AspNetCore.Mvc;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.Base;
using Resturant.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    public class ManageFoodsController : ControllerBase
    {
        private IUOW _UOW;

        public ManageFoodsController(IUOW uOW)
        {
            _UOW = uOW;
        }



        // GET
        [HttpGet]
        public Task<IEnumerable<User>> GetAllUserslInfo(PaginationDTO pagination)
        {
            var FullUsersInfo = _UOW._Base<User>().FindAllAsync_Pagination(pagination);
            return FullUsersInfo;
        }
        [HttpGet]

        public Task<IEnumerable<User>> GetUserByID()
        {
            var FullUsersInfo = _UOW._Base<User>().FindAllAsync();
            return FullUsersInfo;

        }





        // GET api/<ManageFoodsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManageFoodsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageFoodsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageFoodsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
