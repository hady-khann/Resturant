using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resturant = Resturant.DBModels.Entities.Resturant;


namespace Resturant.Services.Srvc_Repo
{
    public interface ISrvc_UserRes
    {
        IEnumerable<ViwUsersInfo> GetResturantRequestedUsers();
        void PromoteUserToResturant(Guid Id);
    }
}
