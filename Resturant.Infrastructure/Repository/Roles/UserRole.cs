using Resturant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository.Roles
{
    class UserRole : IUserRole
    {
        private readonly ResturantContext _context;

        public UserRole(ResturantContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _context.Roles;
            return roles;
        }

        public string GetUserRoleBYID(Guid User_Role_guid)
        {
            var UserRole = _context.Roles.Where(x=>x.Id==User_Role_guid).FirstOrDefault().RoleName;
            return UserRole;
        }
    }
}
