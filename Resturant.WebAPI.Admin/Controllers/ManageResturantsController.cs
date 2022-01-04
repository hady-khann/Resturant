using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.Interfaces;
using Resturant.Repository.UW;
using Resturant.Services.Srvc;
using Resturant.Services.Srvc_Internal;
using Resturant.Services.Srvc_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resturant = Resturant.DBModels.Entities.Resturant;



    /// <summary>
    /// ////////////////////////////////////////////////////////// Finished / Tested 1 / 
    /// </summary>
    /// 


namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageResturantsController : ControllerBase
    {
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly ISrvc _Srvc;
        private readonly _IUW _UW;

        public ManageResturantsController(Response response, IMapper mapper, ISrvc srvc, _IUW uW)
        {
            _response = response;
            _Mapper = mapper;
            _Srvc = srvc;
            _UW = uW;
        }











        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetAllResturants")]
        public Global_Response_DTO<IEnumerable<ViwResturant>> GetAllResturants(PaginationDTO page)
        {
            return _response.Global_Result<IEnumerable<ViwResturant>>(_UW._Base<ViwResturant>().FindAll().Skip(page.Skip).Take(page.Take).ToList());
        }

        [HttpGet]
        [Route("GetResturantsByID")]
        public Global_Response_DTO<ViwResturant> GetResturantByID([FromBody] Guid Id)
        {
            return _response.Global_Result(_UW._Base<ViwResturant>().FindByConditionAsync(x => x.Id == Id).Result.FirstOrDefault());
        }

        [HttpGet]
        [Route("GetResturantByName")]
        public Global_Response_DTO<ViwResturant> GetResturantByName([FromBody] string name)
        {
            return _response.Global_Result(_UW._Base<ViwResturant>().FindByConditionAsync(x=>x.ResturantName==name).Result.FirstOrDefault());
        }


        [HttpGet]
        [Route("GetUserRequests")]
        public Global_Response_DTO<IEnumerable<ViwUsersInfo>> GetUserRequests()
        {
            var AllUsersInfo = _Srvc._UserRes.GetResturantRequestedUsers();
            return _response.Global_Result(AllUsersInfo);
        }

        [HttpPost]
        [Route("PromoteUserToResturant")]
        public void PromoteUserToResturant([FromBody] Guid ID)
        {
            _Srvc._UserRes.PromoteUserToResturant(ID);
            _Srvc.SaveDB();
        }

        [HttpPost]
        [Route("PromoteAllUserToResturant")]
        public void PromoteAllUserToResturant([FromBody] IEnumerable<Guid> ID)
        {
            foreach (var item in ID)
            {
                _Srvc._UserRes.PromoteUserToResturant(item);
            }
            _Srvc.SaveDB();
        }

        // PUT  
        [HttpPut]
        [Route("UpdateResturant")]
        public void Put([FromBody] ResturantDTO resturantdto)
        {
            _UW._Base<resturant>().Update(_Mapper.Map<resturant>(resturantdto));
            _UW.SaveDB();
        }

        // DELETE 
        [HttpDelete]
        [Route("DeleteResturant")]
        public void Delete([FromBody] ResturantDTO resturantdto)
        {
            _UW._Base<resturant>().Delete(_Mapper.Map<resturant>(resturantdto));
            _UW.SaveDB();
        }

    }
}
