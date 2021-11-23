using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<User> GetAllUsersINFOAsync(int page, int Records_count)
        {
            return _context.Users.Skip(page * Records_count).Take(Records_count).AsNoTracking();
        }

        public User GetUserINFOAsync(User userModel)
        {

            //get user as UserModel


            User User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower() && x.PassWord == userModel.PassWord).AsNoTracking().FirstOrDefault();

            return User;

        }

        public string IsUserExists(UserDTO userModel)
        {
            //get user as UserModel
            User usrName = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()).AsNoTracking().FirstOrDefault();
            User usrEmail = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()).AsNoTracking().FirstOrDefault();

            if (usrName != null)
            {
                if (usrEmail != null)
                {
                    return "UserName And Email Exixts";
                }
                return "UserName Exixts";
            }
            else if (usrEmail != null)
            {
                if (usrName != null)
                {
                    return "UserName And Email Exixts";
                }
                return "Email Exixts";
            }
            else
            {
                return null;
            }
        }

        public async Task AddUserAsync(UserDTO usr)
        {
                usr.UserID = Guid.NewGuid();
                await _context.Users.AddAsync((User)usr);
                _context.SaveChanges();
        }

    }
}
