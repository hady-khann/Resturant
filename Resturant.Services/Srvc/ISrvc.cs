using Resturant.DataAccess.Context;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using Resturant.Services.Srvc_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc
{
    public interface ISrvc
    {
        ResturantContext _context { get; }
        ISrvc_UserRes _UserRes { get; }
        ISrvc_Token _Token { get; }
       

        Task SaveDBAsync();
        void SaveDB();


    }
}
