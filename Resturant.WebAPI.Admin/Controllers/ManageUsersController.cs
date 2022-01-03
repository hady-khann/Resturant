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
    /// ////////////////////////////////////////////////////////// finished / Tested 1 / 
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
            IEnumerable<ViwUsersInfo> AllUsersInfo;



            if (CurrentUser.Level != 0)
            {
                AllUsersInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.AccessLevel > CurrentUser.Level).Result;
            }
            else
                AllUsersInfo = _UW._Base<ViwUsersInfo>().FindAll();


            return _response.Global_Result(AllUsersInfo);

        }


        [HttpGet]
        [Route("GetUserByID")]
        public Global_Response_DTO<ViwUsersInfo> GetUserByID([FromBody] Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.Id == Id).Result.FirstOrDefault();
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                return _response.Global_Result(UserInfo);
            }
            return _response.Global_Result<ViwUsersInfo>(null);
        }

        [HttpGet]
        [Route("GetUserByName")]
        public Global_Response_DTO<ViwUsersInfo> GetUserByName([FromBody] string Name)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.UserName == Name).Result.FirstOrDefault();
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                return _response.Global_Result(UserInfo);
            }
            return _response.Global_Result<ViwUsersInfo>(null);
        }

        [HttpGet]
        [Route("GetUserByRole")]
        public Global_Response_DTO<IEnumerable<ViwUsersInfo>> GetUserByRole([FromBody] Guid Id)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var User = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.RoleId == Id).Result;
            if (CurrentUser.Level.Value < User.FirstOrDefault().AccessLevel)
            {
                return _response.Global_Result(User);
            }
            return _response.Global_Result<IEnumerable<ViwUsersInfo>>(null);
        }



        [HttpGet]
        [Route("GetRoles")]
        public async Task<Global_Response_DTO<IEnumerable<RoleDTO>>> GetRoles()
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            IEnumerable<RoleDTO> AllRoles;


            if (CurrentUser.Level != 0)
            {
                AllRoles = _Mapper.Map<IEnumerable<RoleDTO>>(await _UW._Base<Role>().FindByConditionAsync(x => x.AccessLevel > CurrentUser.Level) );
            }
            else
                AllRoles = _Mapper.Map<IEnumerable<RoleDTO>>(_UW._Base<Role>().FindAll());

            return _response.Global_Result(AllRoles);

        }
        #endregion

        // PUT api/<ManageUsersController>/5
        [HttpPut]
        [Route("UpdateUser")]
        public void Updateuser([FromBody] ViwUsersInfo UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            var UserInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.Id == UserInfoDTO.Id).Result.FirstOrDefault();
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                var y = _Mapper.Map<User>(UserInfoDTO);
                _UW._Base<User>().Update(y);
                _UW.SaveDB();
            }
        }

        // DELETE api/<ManageUsersController>/5
        [HttpDelete]
        [Route("DeleteUser")]
        public void Delete([FromBody] ViwUsersInfo UserInfoDTO)
        {
            var CurrentUser = _GMethods.GETCurrentUser();
            ViwUsersInfo UserInfo = _UW._Base<ViwUsersInfo>().FindByConditionAsync(x => x.Id == UserInfoDTO.Id).Result.FirstOrDefault();
            if (CurrentUser.Level.Value < UserInfo.AccessLevel)
            {
                _UW._Base<User>().Delete(_Mapper.Map<User>(UserInfo));
            }
            _UW.SaveDB();
        }
    }
}
