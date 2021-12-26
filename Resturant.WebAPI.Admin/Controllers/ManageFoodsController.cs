using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.Base;
using Resturant.Repository.UW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    /// <summary>
    /// ////////////////////////////////////////////////////////// finished
    /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageFoodsController : ControllerBase
    {
        private readonly Response _response;
        private _IUW _UW;

        public ManageFoodsController(Response response, _IUW UW)
        {
            _response = response;
            _UW = UW;
        }




        // GET List
        [HttpGet]
        [Route("GetFoodAllFoods")]
        public Global_Response_DTO<IEnumerable<ViwFood>> GetAllFoods(PaginationDTO Page)
        {
            var Foods = _UW._Base<ViwFood>().FindAll().Skip(Page.Skip).Take(Page.Take).ToList();
            return _response.Global_Result<IEnumerable<ViwFood>>(Foods);
        }

        // get food by id
        [HttpGet]
        [Route("GetFoodByID")]
        public async Task<Global_Response_DTO<ViwFood>> GetFoodByID(Guid Id)
        {
            var Foods = await _UW._Base<ViwFood>().FindByID(Id);
            return _response.Global_Result(Foods);

        }

        [HttpGet]
        [Route("GetFoodByName")]
        public async Task<Global_Response_DTO<ViwFood>> GetFoodByName(String Name)
        {
            var Foods = await _UW._Base<ViwFood>().FindByConditionAsync(x=>x.FoodName==Name);
            return _response.Global_Result(Foods as ViwFood);
        }


        // POST ----- add new food
        [HttpPost]
        public async void Add([FromBody] Food food)
        {
            await _UW._Base<Food>().Insert(food);
            await _UW.SaveDBAsync();
        }

        // PUT ---- update food
        [HttpPut]
        public async void Update([FromBody] Food food)
        {
            _UW._Base<Food>().Update(food);
            await _UW.SaveDBAsync();
        }

        // DELETE -
        [HttpDelete]
        public async void Delete([FromBody] FoodDTO foodDto)
        {
            Food food = await _UW._Base<Food>().FindByID(foodDto.Id);
             _UW._Base<Food>().Delete(food);
            await _UW.SaveDBAsync();
        }
    }
}
