﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    /// ////////////////////////////////////////////////////////// Finished
    /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageResturantsFoodsController : ControllerBase
    {

        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly _IUW _UW;

        public ManageResturantsFoodsController(Response response, IMapper mapper, _IUW UW)
        {
            _response = response;
            _Mapper = mapper;
            _UW = UW;
        }



        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetResturantAllFoods")]
        public async Task<Global_Response_DTO<IEnumerable<ViwResturantFoodDTO>>> GetResturantAllFoods(Guid ResID)
        {
            return _response.Global_Result(_Mapper.Map<IEnumerable<ViwResturantFoodDTO>>(await _UW._Base<ViwResturantFood>().FindByConditionAsync(x=>x.IdResturant== ResID)));
        }

        [HttpGet]
        [Route("GetResFoodByID")]
        public async Task<Global_Response_DTO<ViwResturantFoodDTO>> GetFoodByID(Guid ResID,Guid FoodID)
        {
            return _response.Global_Result(_Mapper.Map<ViwResturantFoodDTO>(await _UW._Base<ViwResturantFood>().FindByConditionAsync(x=>x.IdResturant==ResID && x.IdFood==FoodID)));
        }
        [HttpGet]
        [Route("GetResFoodByName")]
        public async Task<Global_Response_DTO<ViwResturantFoodDTO>> GetFoodByName(Guid ResID, string Name)
        {
            return _response.Global_Result(_Mapper.Map<ViwResturantFoodDTO>(await _UW._Base<ViwResturantFood>().FindByConditionAsync(x => x.IdResturant == ResID && x.FoodName == Name)));
        }

        // POST 
        [HttpPost]
        public async void Post([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            await _UW._Base<ViwResturantFood>().Insert(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UW.SaveDBAsync();

        }

        // PUT  
        [HttpPut]
        public async void Put([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            _UW._Base<ViwResturantFood>().Update(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UW.SaveDBAsync();
        }

        // DELETE 
        [HttpDelete]
        public async void Delete([FromBody] ViwResturantFoodDTO resFoodDTO)
        {
            _UW._Base<ViwResturantFood>().Delete(_Mapper.Map<ViwResturantFood>(resFoodDTO));
            await _UW.SaveDBAsync();
        }

    }
}
