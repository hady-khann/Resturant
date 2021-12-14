using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Resturant.CoreBase.Global_Methods;
using AutoMapper;
using Resturant.WebAPI.Admin.Srvc_Controller;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    public class ManageUsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _ContextAccessor;
        private readonly Response _response;
        private GlobalMethods _GMethods;
        private readonly IMapper _Mapper;
        private Srvc_Users _Srvc;
        private IUOW _UOW;

        public ManageUsersController(IHttpContextAccessor contextAccessor, Response response, GlobalMethods gMethods, IMapper mapper, IUOW uOW)
        {
            _ContextAccessor = contextAccessor;
            _response = response;
            _GMethods = gMethods;
            _Mapper = mapper;
            _UOW = uOW;
        }






        #region GetUser

        // GET: api/<ManageUsersController>
        [HttpGet]
        [Route("GetAllUserslInfo")]
        public Global_Response_DTO<IEnumerable<UserInfoDTO>> GetAllUserslInfo(PaginationDTO pagination)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var AllUsersInfo = _UOW._UserInfo.GetAllUsersINFO(pagination, CurrentUser.Level.Value);

            return _response.Global_Result<IEnumerable<UserInfoDTO>>(AllUsersInfo);

        }
        [HttpGet]
        [Route("GetUserByID")]
        public async Task<Global_Response_DTO<UserInfoDTO>> GetUserByID(Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _Mapper.Map<UserInfoDTO>(await _UOW._Base<UserInfoDTO>().FindByID(Id));
            return _response.Global_Result<UserInfoDTO>(UserInfo);

        }
        [HttpGet]
        [Route("GetRoles")]
        public async Task<Global_Response_DTO<IEnumerable<Role>>> GetRoles()
        {
           return _response.Global_Result<IEnumerable<Role>>(await _UOW._Base<Role>().FindAllAsync());
        }
        #endregion

        // PUT api/<ManageUsersController>/5
        [HttpPut]
        [Route("Update")]
        public void Update([FromBody] UserInfoDTO UserInfoDTO)
        {
            _Srvc.PutUpdate(UserInfoDTO);
        }

        // DELETE api/<ManageUsersController>/5
        [HttpDelete]
        public async void Delete([FromBody] UserInfoDTO UserInfoDTO)
        {
            _UOW._Base<UserInfoDTO>().Delete(UserInfoDTO);
            await _UOW.SaveDBAsync();
        }
    }
}
