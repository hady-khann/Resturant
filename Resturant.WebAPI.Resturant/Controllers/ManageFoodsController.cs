using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Admin,Manager,Owner,Root")]
    public class ManageFoodsController : ControllerBase
    {

        [Route("Admin/[controller]")]
        [ApiController]
        [Authorize(Roles = "Root")]

        public class ManageRolesController : ControllerBase
        {

            private readonly Response _response;
            private readonly IMapper _Mapper;
            private IUOW _UOW;

            public ManageRolesController(Response response, IMapper mapper, IUOW uOW)
            {
                _response = response;
                _Mapper = mapper;
                _UOW = uOW;
            }



            // GET: 
            [HttpGet]
            [Route("GetAllRoles")]
            public async Task<Global_Response_DTO<IEnumerable<RoleDTO>>> GetRoles()
            {
                return _response.Global_Result(_Mapper.Map<IEnumerable<RoleDTO>>(await _UOW._Base<Role>().FindAllAsync()));
            }

            [HttpGet]
            [Route("GetRoleByID")]
            public async Task<Global_Response_DTO<RoleDTO>> GetRoleByID(Guid Id)
            {
                return _response.Global_Result(_Mapper.Map<RoleDTO>(await _UOW._Base<Role>().FindByID(Id)));
            }

            // POST 
            [HttpPost]
            public async void Post([FromBody] RoleDTO role)
            {
                await _UOW._Base<Role>().Insert(_Mapper.Map<Role>(role));
                await _UOW.SaveDBAsync();

            }

            // PUT  
            [HttpPut]
            public async void Put([FromBody] RoleDTO role)
            {
                _UOW._Base<Role>().Update(_Mapper.Map<Role>(role));
                await _UOW.SaveDBAsync();
            }

            // DELETE 
            [HttpDelete]
            public async void Delete([FromBody] RoleDTO role)
            {
                _UOW._Base<Role>().Delete(_Mapper.Map<Role>(role));
                await _UOW.SaveDBAsync();
            }

        }
    }
}
