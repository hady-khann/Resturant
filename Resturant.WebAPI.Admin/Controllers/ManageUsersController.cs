using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.UW;
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
        /// <summary>
        /// ////////////////////////////////////////////////////////// finished
        /// </summary>
    [Route("Admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root")]

    public class ManageUsersController : ControllerBase
    {
        private readonly Response _response;
        private GlobalMethods _GMethods;
        private readonly IMapper _Mapper;
        private _IUW _UW;

        public ManageUsersController(Response response, GlobalMethods gMethods, IMapper mapper, _IUW UW)
        {
            _response = response;
            _GMethods = gMethods;
            _Mapper = mapper;
            _UW = UW;
        }


        #region GetUser

        // GET: api/<ManageUsersController>
        [HttpGet]
        [Route("GetAllUserslInfo")]
        public Global_Response_DTO<IEnumerable<ViwUsersInfo>> GetAllUserslInfo(PaginationDTO pagination)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var AllUsersInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.AccessLevel > CurrentUser.Level).Result;

            return _response.Global_Result(AllUsersInfo);

        }


        [HttpGet]
        [Route("GetUserByID")]
        public async Task<Global_Response_DTO<ViwUsersInfo>> GetUserByID(Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo =await _UW._Base<ViwUsersInfo>().FindByID(Id);
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                return _response.Global_Result(UserInfo);
            }
            return _response.Global_Result<ViwUsersInfo>(null);
        }
        [HttpGet]
        [Route("GetUserByName")]
        public Global_Response_DTO<ViwUsersInfo> GetUserByName(string Name)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x=>x.UserName==Name).Result.FirstOrDefault();
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                return _response.Global_Result(UserInfo);
            }
            return _response.Global_Result<ViwUsersInfo>(null);
        }


        [HttpGet]
        [Route("GetRoles")]
        public async Task<Global_Response_DTO<IEnumerable<Role>>> GetRoles()
        {
            var CurrentUser = _GMethods.GETCurrentUser();

            return _response.Global_Result<IEnumerable<Role>>(await _UW._Base<Role>().FindByConditionAsync(x => x.AccessLevel > CurrentUser.Level));
        }
        #endregion

        // PUT api/<ManageUsersController>/5
        [HttpPut]
        [Route("UpdateUser")]
        public async void Updateuser([FromBody] ViwUsersInfo UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = await _UW._Base<ViwUsersInfo>().FindByID(UserInfoDTO.Id);
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                _UW._Base<User>().Update(_Mapper.Map<User>(UserInfo));
                await _UW.SaveDBAsync();
            }
        }

        // DELETE api/<ManageUsersController>/5
        [HttpDelete]
        [Route("DeleteUser")]
        public async void Delete([FromBody] ViwUsersInfo UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            ViwUsersInfo UserInfo = await _UW._Base<ViwUsersInfo>().FindByID(UserInfoDTO.Id);
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                User usr = await _UW._Base<User>().FindByID(UserInfoDTO.Id);
                _UW._Base<User>().Delete(usr);
            }
            await _UW.SaveDBAsync();
        }
    }
}
