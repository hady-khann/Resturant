using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
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
    public class ManageFoodTypesController : ControllerBase
    {
        private readonly Response _response;
        private IUOW _UOW;

        public ManageFoodTypesController(Response response, IUOW uOW)
        {
            _response = response;
            _UOW = uOW;
        }



        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetFoodTypes")]
        public Global_Response_DTO<IEnumerable<FoodType>> GetFoodTypes(PaginationDTO page)
        {
            return _response.Global_Result<IEnumerable<FoodType>>(_UOW._Base<FoodType>().FindAll().Skip(page.Skip).Take(page.Take).ToList());
        }
        [HttpGet]
        [Route("GetFoodTypesByID")]
        public async Task<Global_Response_DTO<FoodType>> GetFoodTypesByID(Guid Id)
        {
            return _response.Global_Result(await _UOW._Base<FoodType>().FindByID(Id));
        }
        [HttpGet]
        [Route("GetFoodTypesByName")]
        public async Task<Global_Response_DTO<FoodType>> GetFoodTypesByID(String Name)
        {
            return _response.Global_Result(await _UOW._Base<FoodType>().FindByConditionAsync(x=>x.Type==Name) as FoodType);
        }

        // POST api/<ManageFoodTypesController>
        [HttpPost]
        public async void Post([FromBody] FoodType FTypes)
        {
            await _UOW._Base<FoodType>().Insert(FTypes);
            await _UOW.SaveDBAsync();
                
        }

        // PUT api/<ManageFoodTypesController>/5
        [HttpPut]
        public async void Put([FromBody] FoodType FTypes)
        {
            _UOW._Base<FoodType>().Update(FTypes);
            await _UOW.SaveDBAsync();
        }

        // DELETE api/<ManageFoodTypesController>/5
        [HttpDelete]
        public async void Delete([FromBody] FoodType FTypes)
        {
            _UOW._Base<FoodType>().Delete(FTypes);
            await _UOW.SaveDBAsync();
        }
    }
}
