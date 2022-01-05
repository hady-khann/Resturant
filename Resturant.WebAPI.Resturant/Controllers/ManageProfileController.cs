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


/// <summary>
/// ////////////////////////////////////////////////////////// Finished / Tested 1 / 
/// </summary>
/// 

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





        [HttpGet]
        [Route("getMyprofile")]
        public Global_Response_DTO<ViwResturant> getMyprofile()
        {
            return _response.Global_Result(_UW._Base<ViwResturant>().FindByConditionAsync(x => x.Id == _GMethods.GETCurrentUser().ResturantId).Result.FirstOrDefault()); ;
        }  
        


        [HttpGet]
        [Route("GetTypes")]
        public Global_Response_DTO<IEnumerable<FoodTypeDTO>> GetTypes([FromBody] PaginationDTO page)
        {
            return _response.Global_Result(_Mapper.Map<IEnumerable<FoodTypeDTO>>(_UW._Base<FoodType>().FindAll().Skip(page.Skip).Take(page.Take).ToList()));
        }

        // POST 
        [HttpPut]
        [Route("UpdateMyprofile")]
        public void UpdateMyprofile([FromBody] ViwResturant ResDto)
        {
            var b = _Mapper.Map<resturant>(ResDto);
            _UW._Base<resturant>().Update(_Mapper.Map<resturant>(ResDto));
             _UW.SaveDB();
        }
    }
}
