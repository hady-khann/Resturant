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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    /// <summary>
    /// ////////////////////////////////////////////////////////// Finished
    /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Root")]
    //[AllowAnonymous]

    public class ManageRolesController : ControllerBase
    {
       
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private _IUW _UW;

        public ManageRolesController(Response response, IMapper mapper, _IUW UW)
        {
            _response = response;
            _Mapper = mapper;
            _UW = UW;
        }



        // GET: 
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<Global_Response_DTO<IEnumerable<RoleDTO>>> GetRoles()
        {
            return _response.Global_Result(_Mapper.Map<IEnumerable<RoleDTO>>(await _UW._Base<Role>().FindAllAsync()));
        }

        [HttpGet]
        [Route("GetRoleByID")]
        public Global_Response_DTO<RoleDTO> GetRoleByID([FromBody] Guid Id)
        {
            return _response.Global_Result(_Mapper.Map<RoleDTO>(_UW._Base<Role>().FindByConditionAsync(x=>x.Id==Id).Result.FirstOrDefault()));
        }

        // POST 
        [HttpPost]
        [Route("AddRole")]
        public async void Post([FromBody] RoleDTO role)
        {
            await _UW._Base<Role>().Insert(_Mapper.Map<Role>(role));
            _UW.SaveDB();

        }

        // PUT  
        [HttpPut]
        [Route("UpdateRole")]
        public void Put([FromBody] RoleDTO role)
        {
             _UW._Base<Role>().Update(_Mapper.Map<Role>(role));
            _UW.SaveDB();
        }

        // DELETE 
        [HttpDelete]
        [Route("DeleteRole")]
        public void Delete([FromBody] RoleDTO role)
        {
            _UW._Base<Role>().Delete(_Mapper.Map<Role>(role));
             _UW.SaveDB();
        }


    }
}
