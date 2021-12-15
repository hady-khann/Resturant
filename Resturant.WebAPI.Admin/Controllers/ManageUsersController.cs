using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resturant.CoreBase.Global_Methods;
using AutoMapper;

using Resturant.WebAPI.Admin.Srvc_Controller;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resturant.WebAPI.Admin.Controllers
{
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageUsersController : ControllerBase
    {
        private readonly Response _response;
        private GlobalMethods _GMethods;
        private readonly IMapper _Mapper;

        private Srvc_Users _Srvc;
        private IUOW _UOW;

        public ManageUsersController(Response response, GlobalMethods gMethods, IMapper mapper, Srvc_Users srvc, IUOW uOW)
        {
            _response = response;
            _GMethods = gMethods;
            _Mapper = mapper;
            _Srvc = srvc;
            _UOW = uOW;
        }








        #region GetUser

        // GET: api/<ManageUsersController>
        [HttpGet]
        [Route("GetAllUserslInfo")]
        public Global_Response_DTO<IEnumerable<UserInfoDTO>> GetAllUserslInfo(PaginationDTO pagination)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var AllUsersInfo = _Mapper.Map<IEnumerable<UserInfoDTO>>(_UOW._Base<UsersInfo>().FindAll().Where(x => x.AccessLevel > CurrentUser.Level).ToList());

            return _response.Global_Result<IEnumerable<UserInfoDTO>>(AllUsersInfo);

        }
        [HttpGet]
        [Route("GetUserByID")]
        public async Task<Global_Response_DTO<UserInfoDTO>> GetUserByID(Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _Mapper.Map<UserInfoDTO>(await _UOW._Base<UserInfoDTO>().FindByID(Id));
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                return _response.Global_Result<UserInfoDTO>(UserInfo);
            }
            return _response.Global_Result<UserInfoDTO>(null);


        }
        [HttpGet]
        [Route("GetRoles")]
        public Global_Response_DTO<IEnumerable<Role>> GetRoles()
        {
            var CurrentUser = _GMethods.GETCurrentUser();

            return _response.Global_Result<IEnumerable<Role>>(_UOW._Base<Role>().FindAll().Where(x => x.AccessLevel > CurrentUser.Level).ToList());
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
