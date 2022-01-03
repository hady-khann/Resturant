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



/// <summary>
/// ////////////////////////////////////////////////////////// finished / Tested 1 /
/// </summary>



namespace Resturant.WebAPI.Admin.Controllers
{
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
        public Global_Response_DTO<IEnumerable<ViwFood>> GetAllFoods(PaginationDTO Page)
        {
            var Foods = _UW._Base<ViwFood>().FindAll().Skip(Page.Skip).Take(Page.Take).ToList();
            return _response.Global_Result<IEnumerable<ViwFood>>(Foods);
        }

        // get food by id
        [HttpGet]
        [Route("GetFoodByID")]
        public Global_Response_DTO<ViwFood> GetFoodByID([FromBody]Guid Id)
        {
            var resual = _UW._Base<ViwFood>().FindByConditionAsync(x => x.Id == Id).Result.FirstOrDefault();
            return _response.Global_Result(resual);
        }

        [HttpGet]
        [Route("GetFoodByName")]
        public Global_Response_DTO<ViwFood> GetFoodByName([FromBody]String Name)
        {
            return _response.Global_Result(_UW._Base<ViwFood>().FindByConditionAsync(x => x.FoodName == Name).Result.FirstOrDefault());
        }


        // POST ----- add new food
        [HttpPost]
        [Route("AddFood")]
        public async void Add([FromBody] FoodDTO food)
        {
            await _UW._Base<Food>().Insert(_Mapper.Map<Food>(food));
            _UW.SaveDB();
        }

        // PUT ---- update food
        [HttpPut]
        [Route("UpdateFood")]

        public void Update([FromBody] FoodDTO food)
        {
            _UW._Base<Food>().Update(_Mapper.Map<Food>(food));
           _UW.SaveDB();
        }

        // DELETE -
        [HttpDelete]
        [Route("DeleteFood")]
        public void Delete([FromBody] FoodDTO foodDto)
        {
            _UW._Base<Food>().Delete(_Mapper.Map<Food>(foodDto));
            _UW.SaveDB();
        }
    }
}
