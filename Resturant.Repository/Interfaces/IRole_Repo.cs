using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Repository.Interfaces
{
    public  interface IRole_Repo
    {
        string GetRoleBYID(Guid guid);

        Guid GetRoleByName(String Name);

    }
}
