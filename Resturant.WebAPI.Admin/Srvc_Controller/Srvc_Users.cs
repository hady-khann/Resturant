using AutoMapper;
using Resturant.CoreBase.Global_Methods;
using Resturant.CoreBase.WebAPIResponse;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.WebAPI.Admin.Srvc_Controller
{
    public class Srvc_Users
    {
        private readonly Response _response;
        private readonly IMapper _Mapper;
        private GlobalMethods _GMethods;

        private IUOW _UOW;

        public Srvc_Users(Response response, IMapper mapper, GlobalMethods gMethods, IUOW uOW)
        {
            _response = response;
            _Mapper = mapper;
            _GMethods = gMethods;
            _UOW = uOW;
        }

        public async void PutUpdate(UserInfoDTO UpdateModel)
        {
            var CurrentUser = _GMethods.GETCurrentUser();

            if (UpdateModel.AccessLevel >= CurrentUser.Level)
            {
                _UOW._Base<UserInfoDTO>().Update(UpdateModel);
                await _UOW.SaveDBAsync();
            }

        }

    }
}
