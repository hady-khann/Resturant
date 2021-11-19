using AutoMapper;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.Role_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository.User_Repo
{
    public class User_Repo : IUser_Repo
    {
        private readonly ResturantContext _context;
        public User_Repo(ResturantContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsersINFO(int page, int Records_count)
        {
            return _context.Users.Skip(page * Records_count).Take(Records_count);
        }

        public User GetUserINFO(User userModel)
        {

            //get user as UserModel
            User User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower() && x.PassWord == userModel.PassWord).FirstOrDefault();
           
            return User;

        }
    }
}
