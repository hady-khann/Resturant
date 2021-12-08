using Microsoft.EntityFrameworkCore;
using Resturant.DataAccess.Context;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.Interfaces;
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
        public IEnumerable<User> GetAllUsersINFO(PaginationDTO pagination, int AccessLevel)
        {
            var skip = pagination.PageNumber * pagination.RowNumber;
            var take = pagination.RowNumber;

            var result = _context.Users.Where(x => x.Role.AccessLevel >= AccessLevel).Include(R => R.Role.RoleName).Skip(skip).Take(take);
            return result.ToList(); ;



            //var Level = new Microsoft.Data.SqlClient.SqlParameter("Level", AccessLevel);
            //var queryStr = @"    select
            //                     [User].Id,[User].RoleId,[User].UserName,[User].Email,[User].Wallet,
            //                     [User].[Address],[User].[Status],[Roles].RoleName

            //                     from [User]
            //                     INNER JOIN  Roles
            //                     ON [dbo].[User].RoleId = Roles.ID where Roles.AccessLevel>=@Level          ";
            //var query = _context.Users.FromSqlRaw(queryStr,Level).Skip(skip).Take(take).ToList();
            //return query;



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
