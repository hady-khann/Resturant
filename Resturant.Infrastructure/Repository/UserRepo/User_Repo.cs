using AutoMapper;
using Resturant.Core.Enums;
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
    public class User_Repo : IUser_Repo, IDisposable
    {
        private readonly ResturantContext _context;
        public User_Repo(ResturantContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAllUsersINFOAsync(int page, int Records_count)
        {
            return _context.Users.Skip(page * Records_count).Take(Records_count);
        }

        public User GetUserINFOAsync(User userModel)
        {

            //get user as UserModel
            User User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower() && x.PassWord == userModel.PassWord).FirstOrDefault();

            return User;

        }

        public string FindUsername(UserDTO userModel)
        {
            //get user as UserModel
            User User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()).FirstOrDefault();

            return User.UserName;
        }

        public async void AddUserAsync(UserDTO usr)
        {

            await _context.Users.AddAsync((User)usr);
            await _context.SaveChangesAsync();

        }




















        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
