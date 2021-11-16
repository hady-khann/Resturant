using AutoMapper;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ResturantContext _context;
        private readonly IMapper _mapper;
        public UserRepository(IMapper mapper , ResturantContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAllUsersINFO(int page, int Records_count)
        {
            return _context.Users.Skip(page * Records_count).Take(Records_count);
        }

        public UserDTO GetUser(User userModel)
        {
            User resualt_User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower() && x.PassWord == userModel.PassWord).FirstOrDefault();
            
            

            return Usr;

        }
    }
}
