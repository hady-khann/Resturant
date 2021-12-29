﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.UW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    /// <summary>
    /// ////////////////////////////////////////////////////////// finished / Tested
    /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]
    public class ManageFoodTypesController : ControllerBase
    {
        private readonly Response _response;
        private _IUW _UW;

        public ManageFoodTypesController(Response response, _IUW UW)
        {
            _response = response;
            _UW = UW;
        }



        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetFoodTypes")]
        public Global_Response_DTO<IEnumerable<FoodType>> GetFoodTypes(PaginationDTO page)
        {
            return _response.Global_Result<IEnumerable<FoodType>>(_UW._Base<FoodType>().FindAll().Skip(page.Skip).Take(page.Take).ToList());
        }
        [HttpGet]
        [Route("GetFoodTypesByID")]
        public async Task<Global_Response_DTO<FoodType>> GetFoodTypesByID([FromBody] Guid Id)
        {
            return _response.Global_Result(await _UW._Base<FoodType>().FindByID(Id));
        }
        [HttpGet]
        [Route("GetFoodTypesByName")]
        public Global_Response_DTO<FoodType> GetFoodTypesByName([FromBody] String Name)
        {
            return _response.Global_Result(_UW._Base<FoodType>().FindByConditionAsync(x => x.Type == Name).Result.FirstOrDefault());
        }

        // POST api/<ManageFoodTypesController>
        [HttpPost]
        [Route("AddFoodType")]
        public async void Post([FromBody] FoodType FTypes)
        {
            FTypes.Id = Guid.NewGuid();
            await _UW._Base<FoodType>().Insert(FTypes);
            _UW.SaveDB();
                
        }

        // PUT api/<ManageFoodTypesController>/5
        [HttpPut]
        [Route("UpdateFoodType")]

        public void Put([FromBody] FoodType FTypes)
        {
            _UW._Base<FoodType>().Update(FTypes);
            _UW.SaveDB();
        }

        // DELETE api/<ManageFoodTypesController>/5
        [HttpDelete]
        [Route("DeleteFoodType")]

        public void Delete([FromBody] FoodType FTypes)
        {
            _UW._Base<FoodType>().Delete(FTypes);
            _UW.SaveDBAsync();
        }
    }
}
