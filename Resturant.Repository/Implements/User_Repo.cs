using Microsoft.EntityFrameworkCore;
using Resturant.DataAccess.Context;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Resturant.Repository
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

        public User CheckUserPass(UserDTO userModel)
        {

            //get user as UserModel

            try
            {
                User User = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower() && x.PassWord == userModel.Password).AsNoTracking().FirstOrDefault();

                return User;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string IsUserExists(UserDTO userModel)
        {
            //get user as UserModel
            User usrName = _context.Users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()).AsNoTracking().FirstOrDefault();
            User usrEmail = _context.Users.Where(x => x.UserName.ToLower() == userModel.Email.ToLower()).AsNoTracking().FirstOrDefault();

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
            try
            {
                usr.UserID = Guid.NewGuid();
                await _context.Users.AddAsync((User)usr);
                _context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

    }
}
