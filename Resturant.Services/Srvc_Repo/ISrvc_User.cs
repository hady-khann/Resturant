using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc_Repo
{
    public interface ISrvc_User
    {
        IEnumerable<User> GetResturantRequestedUsers();
    }
}
