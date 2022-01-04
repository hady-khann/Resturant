using AutoMapper;
using Resturant.DataAccess.Context;
using Resturant.DBModels.Entities;
using Resturant.Repository.UW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resturant = Resturant.DBModels.Entities.Resturant;

namespace Resturant.Services.Srvc_Repo
{
    public class Srvc_UserRes : ISrvc_UserRes
    {
        private readonly ResturantContext _context;
        private readonly IMapper _Mapper;
        private readonly _IUW _Uw;

        public Srvc_UserRes(ResturantContext context, IMapper mapper, _IUW uw)
        {
            _context = context;
            _Mapper = mapper;
            _Uw = uw;
        }

        public IEnumerable<ViwUsersInfo> GetResturantRequestedUsers()
        {
            var Requests = _context.Users.Where(x => !_context.Resturants.Any(b => b.UserId == x.Id) && x.Role.RoleName == "Resturant").AsQueryable();
            var RequestsInViewTable = _context.ViwUsersInfos.Where(x=> Requests.Any(b => b.Id == x.Id)).AsEnumerable();
            return RequestsInViewTable;
        }

        public void PromoteUserToResturant(Guid Id)
        {
            var user = _Uw._Base<ViwUsersInfo>().FindByConditionAsync(x=>x.Id==Id).Result.FirstOrDefault();
            var x = _Mapper.Map<resturant>(user);
        }

    }
}
