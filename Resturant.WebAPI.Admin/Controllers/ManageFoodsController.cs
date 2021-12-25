using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
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
        private readonly Response _response;
        private GlobalMethods _GMethods;
        private readonly IMapper _Mapper;
        private IUOW _UOW;

        public ManageFoodsController(Response response, GlobalMethods gMethods, IMapper mapper, IUOW uOW)
        {
            _response = response;
            _GMethods = gMethods;
            _Mapper = mapper;
            _UOW = uOW;
        }




        // GET List
        [HttpGet]
        public IEnumerable<Food> GetAllFoods(PaginationDTO Page)
        {
            var Foods = _UOW._Base<Food>().FindAll().Skip(Page.Skip).Take(Page.Take).ToList();
            return Foods;
        }

        // get food by id
        [HttpGet]
        public async Task<Food> GetFoodByID(Guid Id)
        {
            var Foods = await _UOW._Base<Food>().FindByID(Id);
            return Foods;

        }


        // POST ----- add new food
        [HttpPost]
        public void Post([FromBody] Food food)
        {
        }

        // PUT ---- update food
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE -
        [HttpDelete]
        public void Delete(int x)
        {

            //User usr = await _UOW._Base<Food>().FindByID(_Mapper.Map<Food>(FoodDTO).Id);
            //_UOW._Base<User>().Delete(usr);
        }
    }
}
