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
    public  class UserRoleService : IUserRoleService
    {
        private readonly IRole_Repo _Roles;

        public UserRoleService(IRole_Repo roles)
        {
            _Roles = roles;
        }

        public UserDTO GetUserRoleDTO(User usr)
        {
            //var resualt = await _context.Users.Include(x => x.Role).Where(u => u.Id == userId).Select(userId => new UserDTO
            //{
            //    UserName = userId.UserName,
            //    Role = userId.Role.RoleName
            //}).FirstOrDefaultAsync();


            var User_Role = _Roles.GetUserRoleBYID(usr.Id);

            UserDTO UserDTO_OBJ = new UserDTO
            {
                UserID = Guid.NewGuid(),
                UserName = "Hady_Khann",
                Role = "admin",
                //UserID = usr.Id,
                //UserName = usr.UserName,
                //Role = User_Role,
            };



            return UserDTO_OBJ;
        }
    }
}
