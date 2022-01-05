using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace Resturant.WebAPI.Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root,Resturant")]
    public class ManageFoodsController : ControllerBase
    {


        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly _IUW _UW;
        private GlobalMethods _GMethods;

        public ManageFoodsController(Response response, IMapper mapper, _IUW uW, GlobalMethods gMethods)
        {
            _response = response;
            _Mapper = mapper;
            _UW = uW;
            _GMethods = gMethods;
        }






        // GET: 
        [HttpGet]
        [Route("GetMyTypeFoods")]
        public async Task<Global_Response_DTO<IEnumerable<FoodDTO>>> GetMyTypeFoods([FromBody] PaginationDTO page)
        {
            return _response.Global_Result(_Mapper.Map<IEnumerable<FoodDTO>> ( await _UW._Base<Food>().FindByConditionAsync(x=>x.Type.Type == _GMethods.GETCurrentUser().ResType)));
        }

        [HttpGet]
        [Route("GetRoleByID")]
        public async Task<Global_Response_DTO<RoleDTO>> GetRoleByID(Guid Id)
        {
            return _response.Global_Result(_Mapper.Map<RoleDTO>(await _UW._Base<Role>().FindByID(Id)));
        }

        // POST 
        [HttpPost]
        public async void Post([FromBody] RoleDTO role)
        {
            await _UW._Base<Role>().Insert(_Mapper.Map<Role>(role));
            await _UW.SaveDBAsync();

        }

        // PUT  
        [HttpPut]
        public async void Put([FromBody] RoleDTO role)
        {
            _UW._Base<Role>().Update(_Mapper.Map<Role>(role));
            await _UW.SaveDBAsync();
        }

        // DELETE 
        [HttpDelete]
        public async void Delete([FromBody] RoleDTO role)
        {
            _UW._Base<Role>().Delete(_Mapper.Map<Role>(role));
            await _UW.SaveDBAsync();
        }


    }
}
