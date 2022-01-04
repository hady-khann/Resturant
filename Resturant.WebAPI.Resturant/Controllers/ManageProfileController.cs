using AutoMapper;
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

namespace Resturant.WebAPI.Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root,Resturant")]
    public class ManageProfileController : ControllerBase
    {
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly _IUW _UW;
        private GlobalMethods _GMethods;

        public ManageProfileController(Response response, IMapper mapper, _IUW UW, GlobalMethods gMethods)
        {
            _response = response;
            _Mapper = mapper;
            _UW = UW;
            _GMethods = gMethods;
        }





        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("getMyprofile")]
        public Global_Response_DTO<ResturantDTO> GetRoleByID()
        {
            return _response.Global_Result(_Mapper.Map<ResturantDTO>(_UW._Base<resturant>().FindByConditionAsync(x => x.Id == _GMethods.GETCurrentUser().ResturantId).Result.FirstOrDefault())); ;
        }

        // POST 
        [HttpPost]
        public void Post([FromBody] ResturantDTO ResDto)
        {
            _UW._Base<resturant>().Update(_Mapper.Map<resturant>(ResDto));
             _UW.SaveDBAsync();
        }
    }
}
