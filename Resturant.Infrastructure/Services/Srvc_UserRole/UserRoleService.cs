using Microsoft.EntityFrameworkCore;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.Role_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services.Srvs_UserRole
{
    public  class UserRoleService : IUserRoleService, IDisposable
    {
        private readonly IRole_Repo _Roles;
        private readonly ResturantContext _context;
  

        public UserRoleService(IRole_Repo roles, ResturantContext context)
        {
            _context = context;
            _Roles = roles;
        }


        public UserDTO GetUserRoleDTO(User usr)
        {

            User User = _context.Users.Where(x => x.UserName.ToLower() == usr.UserName.ToLower() && x.PassWord == usr.PassWord).Include(u => u.Role).FirstOrDefault();

            return (UserDTO) User;
        }





        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
