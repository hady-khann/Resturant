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
        private IUOW _UOW;

        public ManageUsersController(Response response, GlobalMethods gMethods, IMapper mapper, IUOW uOW)
        {
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
            var AllUsersInfo = _Mapper.Map<IEnumerable<UserInfoDTO>>(_UOW._Base<ViwUsersInfo>().FindAll().Where(x => x.AccessLevel > CurrentUser.Level).ToList());

            return _response.Global_Result<IEnumerable<UserInfoDTO>>(AllUsersInfo);

        }
        [HttpGet]
        [Route("GetUserByID")]
        public async Task<Global_Response_DTO<UserInfoDTO>> GetUserByID(Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _Mapper.Map<UserInfoDTO>(await _UOW._Base<ViwUsersInfo>().FindByID(Id));
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
        [Route("UpdateUser")]
        public async void Updateuser([FromBody] UserInfoDTO UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _Mapper.Map<UserInfoDTO>(await _UOW._Base<ViwUsersInfo>().FindByID(UserInfoDTO.Id));
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                _UOW._Base<User>().Update(_Mapper.Map<User>(UserInfo));
                await _UOW.SaveDBAsync();
            }
        }

        // DELETE api/<ManageUsersController>/5
        [HttpDelete]
        public async void Delete([FromBody] UserInfoDTO UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            UserInfoDTO UserInfo = _Mapper.Map<UserInfoDTO>(await _UOW._Base<ViwUsersInfo>().FindByID(UserInfoDTO.Id));
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                User usr = await _UOW._Base<User>().FindByID(_Mapper.Map<User>(UserInfoDTO).Id);
                _UOW._Base<User>().Delete(usr);
            }
            await _UOW.SaveDBAsync();
        }
    }
}
