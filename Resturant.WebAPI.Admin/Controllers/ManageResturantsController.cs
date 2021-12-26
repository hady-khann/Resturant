﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using resturant = Resturant.DBModels.Entities.Resturant;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    /// <summary>
    /// ////////////////////////////////////////////////////////// Finished
    /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageResturantsController : ControllerBase
    {

        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly _IUW _UW;
        public ManageResturantsController(Response response, IMapper mapper, _IUW UW)
        {
            _response = response;
            _Mapper = mapper;
            _UW = UW;
        }


        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetAllResturants")]
        public Global_Response_DTO<IEnumerable<ResturantDTO>> GetAllResturants(PaginationDTO page)
        {
            return _response.Global_Result(_Mapper.Map<IEnumerable<ResturantDTO>>(_UW._Base<resturant>().FindAll().Skip(page.Skip).Take(page.Take).ToList()));
        }

        [HttpGet]
        [Route("GetResturantsByID")]
        public async Task<Global_Response_DTO<ResturantDTO>> GetResturantByID(Guid Id)
        {
            return _response.Global_Result(_Mapper.Map<ResturantDTO>(await _UW._Base<resturant>().FindByID(Id)));
        }

        [HttpGet]
        [Route("GetResturantByName")]
        public Global_Response_DTO<ResturantDTO> GetResturantByName(string name)
        {
            return _response.Global_Result(_Mapper.Map<ResturantDTO>(_UW._Base<resturant>().FindByConditionAsync(x=>x.ResturantName==name)));
        }

        // POST 
        [HttpPost]
        public async void Post([FromBody] ResturantDTO resturantdto)
        {
            await _UW._Base<resturant>().Insert(_Mapper.Map<resturant>(resturantdto));
            await _UW.SaveDBAsync();

        }

        // PUT  
        [HttpPut]
        public async void Put([FromBody] ResturantDTO resturantdto)
        {
            _UW._Base<resturant>().Update(_Mapper.Map<resturant>(resturantdto));
            await _UW.SaveDBAsync();
        }

        // DELETE 
        [HttpDelete]
        public async void Delete([FromBody] ResturantDTO resturantdto)
        {
            _UW._Base<resturant>().Delete(_Mapper.Map<resturant>(resturantdto));
            await _UW.SaveDBAsync();
        }

    }
}
