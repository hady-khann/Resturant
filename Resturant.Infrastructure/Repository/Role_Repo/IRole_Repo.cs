using Resturant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository.Role_Repo
{
    public  interface IRole_Repo
    {
        IEnumerable<Role> GetAllRoles();
        string GetRoleBYID(Guid guid);

        Guid GetRoleByName(String Name);

    }
}
