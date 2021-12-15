using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageFoodsController : ControllerBase
    {
        private IUOW _UOW;

        public ManageFoodsController(IUOW uOW)
        {
            _UOW = uOW;
        }




        // GET
        [HttpGet]
        public IEnumerable<Food> GetAllFoods(PaginationDTO Page)
        {
            var Foods = _UOW._Base<Food>().FindAll().Skip(Page.Skip).Take(Page.Take).ToList();
            return Foods;
        }
        [HttpGet]

        public  async Task<Food> GetFoodByID(Guid Id)
        {
            var Foods = await _UOW._Base<Food>().FindByID(Id);
            return Foods;

        }


        // POST api/<ManageFoodsController>
        [HttpPost]
        public void Post([FromBody] Food food)
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
