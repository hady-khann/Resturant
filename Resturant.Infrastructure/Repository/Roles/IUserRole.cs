using Resturant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository.Roles
{
    interface IUserRole
    {
        string GetUserRoleBYID(Guid guid);
        IEnumerable<Role> GetAllRoles();

    }
}
