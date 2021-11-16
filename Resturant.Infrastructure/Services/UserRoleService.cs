using Microsoft.EntityFrameworkCore;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services
{
    class UserRoleService
    {
        public class UserRoleService : IRoleService
        {
            private readonly ResturantContext context;

            public UserRoleService(ResturantContext context)
            {
                this.context = context;
            }

            public async Task<UserDTO> GetUserDTOAsync(Guid userId)
            {
                return await context.Users.Include(x => x.Role).Where(u => u.Id == userId).Select(userId => new UserDTO
                {
                    UserName = userId.UserName,
                    Role = userId.Role.RoleName
                }).FirstOrDefaultAsync();
            }

        }
    }
}
