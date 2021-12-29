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
        private readonly IMapper _Mapper;
        private _IUW _UW;

        public ManageFoodsController(Response response, IMapper mapper, _IUW uW)
        {
            _response = response;
            _Mapper = mapper;
            _UW = uW;
        }






        // GET List
        [HttpGet]
        [Route("GetAllFoods")]
        public Global_Response_DTO<IEnumerable<ViwFood>> GetAllFoods(PaginationDTO? Page)
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
        [Route("AddFood")]
        public async void Add([FromBody] FoodDTO food)
        {
            await _UW._Base<Food>().Insert(_Mapper.Map<Food>(food));
            await _UW.SaveDBAsync();
        }

        // PUT ---- update food
        [HttpPut]
        [Route("UpdateFood")]

        public async void Update([FromBody] FoodDTO food)
        {
            _UW._Base<Food>().Update(_Mapper.Map<Food>(food));
            await _UW.SaveDBAsync();
        }

        // DELETE -
        [HttpDelete]
        [Route("DeleteFood")]
        public async void Delete([FromBody] FoodDTO foodDto)
        {
            Food food = await _UW._Base<Food>().FindByID(foodDto.Id);
             _UW._Base<Food>().Delete(food);
            await _UW.SaveDBAsync();
        }
    }
}
