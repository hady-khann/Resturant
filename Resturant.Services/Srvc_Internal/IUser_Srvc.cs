using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc_Internal
{
    public interface IUser_Srvc
    {
        IEnumerable<User> GetResturantRequestedUsers();
    }
}
