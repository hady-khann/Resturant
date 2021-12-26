﻿using AutoMapper;
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

namespace Resturant.WebAPI.Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root,Resturant")]
    public class ManageOrdersController : ControllerBase
    {
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly IUOW _UOW;
        private GlobalMethods _GMethods;

        public ManageOrdersController(Response response, IMapper mapper, IUOW uOW, GlobalMethods gMethods)
        {
            _response = response;
            _Mapper = mapper;
            _UOW = uOW;
            _GMethods = gMethods;
        }






        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetResturantAllFoods")]
        public async Task<Global_Response_DTO<IEnumerable<ViwResturantFoodDTO>>> GetResturantAllFoods(Guid ResID)
        {
            var user = _GMethods.GETCurrentUser();
            return _response.Global_Result(_Mapper.Map<IEnumerable<ViwResturantFoodDTO>>(await _UOW._Base<ViwResturantFood>().FindByConditionAsync(x => x.IdResturant == ResID)));
        }

        [HttpGet]
        [Route("GetResFoodByID")]
        public async Task<Global_Response_DTO<ViwResturantFoodDTO>> GetFoodByID(Guid ResID, Guid FoodID)
        {
            return _response.Global_Result(_Mapper.Map<ViwResturantFoodDTO>(await _UOW._Base<ViwResturantFood>().FindByConditionAsync(x => x.IdResturant == ResID && x.IdFood == FoodID)));
        }
        [HttpGet]
        [Route("GetResFoodByName")]
        public async Task<Global_Response_DTO<ViwResturantFoodDTO>> GetFoodByName(Guid ResID, string Name)
        {
            return _response.Global_Result(_Mapper.Map<ViwResturantFoodDTO>(await _UOW._Base<ViwResturantFood>().FindByConditionAsync(x => x.IdResturant == ResID && x.FoodName == Name)));
        }

        // POST 
        [HttpPost]
        public async void Post([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            await _UOW._Base<ViwResturantFood>().Insert(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UOW.SaveDBAsync();

        }

        // PUT  
        [HttpPut]
        public async void Put([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            _UOW._Base<ViwResturantFood>().Update(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UOW.SaveDBAsync();
        }

        // DELETE 
        [HttpDelete]
        public async void Delete([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            _UOW._Base<ViwResturantFood>().Delete(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UOW.SaveDBAsync();
        }
    }
}