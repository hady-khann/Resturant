using Microsoft.EntityFrameworkCore;
using Resturant.DataAccess.Context;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Implements
{
    public  class UserRoleService : IUserRoleService
    {
        private readonly ResturantContext _context;
  

        public UserRoleService(ResturantContext context)
        {
            _context = context;
        }


        public UserDTO GetUserRoleDTO(User usr)
        {

            try
            {
                User User = _context.Users.Where(x => x.UserName.ToLower() == usr.UserName.ToLower() && x.PassWord == usr.PassWord).Include(u => u.Role).FirstOrDefault();

                return (UserDTO)User;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
