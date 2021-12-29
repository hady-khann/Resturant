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

namespace Resturant.WebAPI.Resturant.Controllers
{
    /// <summary>
    /// ////////////////////////////////////////////////////////// finished
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,Owner,Root,Resturant")]
    public class ManageOrdersController : ControllerBase
    {
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private readonly _IUW _UW;
        private GlobalMethods _GMethods;

        public ManageOrdersController(Response response, IMapper mapper, _IUW UW, GlobalMethods gMethods)
        {
            _response = response;
            _Mapper = mapper;
            _UW = UW;
            _GMethods = gMethods;
        }






        // GET: api/<ManageFoodTypesController>
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<Global_Response_DTO<IEnumerable<ViwOrder>>> GetResturantAllFoods(PaginationDTO page)
        {
            var user = _GMethods.GETCurrentUser();
            return _response.Global_Result(await _UW._Base<ViwOrder>().FindByConditionAsync(x => x.ResturantId==user.ResturantId,page));
        }


        [HttpGet]
        [Route("GetOrderByUserID")]
        public async Task<Global_Response_DTO<IEnumerable<ViwOrder>>> GetRoleByID(Guid Id)
        {
            var user = _GMethods.GETCurrentUser();
            return _response.Global_Result(await _UW._Base<ViwOrder>().FindByConditionAsync(x => x.ResturantId == user.ResturantId));

        }
        // PUT  
        [HttpPut]
        public async void Put([FromBody] string Status,Guid orderID)
        {
            var order = await _UW._Base<UserOrder>().FindByID(orderID);
            order.Status = Status;
            _UW._Base<UserOrder>().Update(order);
            await _UW.SaveDBAsync();
        }

        //// DELETE 
        //[HttpDelete]
        //public async void Delete([FromBody] ViwResturantFood resFoodDTO)
        //{
        //    _UW._Base<ViwResturantFood>().Delete(_Mapper.Map<ViwResturantFood>(resFoodDTO));
        //    await _UW.SaveDBAsync();
        //}
    }
}