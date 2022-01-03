using AutoMapper;
using Resturant.DataAccess.Context;
using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc_Internal
{
    public class User_Srvc : IUser_Srvc
    {
        private readonly ResturantContext _context;
        private readonly IMapper _Mapper;

        public User_Srvc(ResturantContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }

        public IEnumerable<User> GetResturantRequestedUsers()
        {
              var t =  _context.Users.Where(x=> !_context.Resturants.Any(b => b.UserId == x.Id)  &&  x.Role.RoleName=="Resturant" ).ToList() ;
            return t;
        }
    }
}
