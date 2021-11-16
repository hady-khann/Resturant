using Microsoft.EntityFrameworkCore;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services
{
    class UserRoleService : IRoleService
    {
        private readonly ResturantContext _context;
        private readonly IUserRole _UserRoles;
        public UserRoleService(ResturantContext context , IUserRole _userRole)
        {
            _context = context;
            _UserRoles = _userRole;
        }

        public async Task<UserDTO> GetUserDTOAsync(Guid userId)
        {
            var resualt = await _context.Users.Include(x => x.Role).Where(u => u.Id == userId).Select(userId => new UserDTO
            {
                UserName = userId.UserName,
                Role = userId.Role.RoleName
            }).FirstOrDefaultAsync();

            

            return _UserRoles.GetUserRoleBYID(userId); ;
        }
    }
}
