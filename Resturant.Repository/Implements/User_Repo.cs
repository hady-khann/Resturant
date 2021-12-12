using AutoMapper;
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
        private readonly IMapper _Mapper;

        public User_Repo(ResturantContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
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
                usr.Id = Guid.NewGuid();
                await _context.Users.AddAsync(_Mapper.Map<User>(usr));
                _context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

    }
}
