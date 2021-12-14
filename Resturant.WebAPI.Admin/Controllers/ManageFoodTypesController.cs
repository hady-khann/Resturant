﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("Admin/[controller]")]
    [ApiController]
    public class ManageFoodTypesController : ControllerBase
    {
        private readonly Response _response;
        private GlobalMethods _GMethods;
        private IUOW _UOW;

        public ManageFoodTypesController(Response response, GlobalMethods gMethods, IUOW uOW)
        {
            _response = response;
            _GMethods = gMethods;
            _UOW = uOW;
        }



        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetFoodTypes")]
        public async Task<Global_Response_DTO<IEnumerable<FoodType>>> GetFoodTypes(PaginationDTO pageing)
        {
            return _response.Global_Result<IEnumerable<FoodType>>(await _UOW._Base<FoodType>().FindAllAsync_Pagination(pageing));
        }
        [HttpGet]
        [Route("GetFoodTypesByID")]
        public async Task<Global_Response_DTO<FoodType>> GetFoodTypesByID(Guid Id)
        {
            return _response.Global_Result(await _UOW._Base<FoodType>().FindByID(Id));
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
