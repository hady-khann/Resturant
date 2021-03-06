using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Resturant.DataAccess.Context;
using Resturant.Repository.Interfaces;
using Resturant.Repository.Base;

namespace Resturant.Repository.UW
{
    public interface _IUW
    {
        
        IBase_Repo<TEntity> _Base<TEntity>() where TEntity : class;
        
        ResturantContext _context { get; }
        IUser_Repo _User { get; }
        IRole_Repo _Role { get; }

        IUserInfo_Repo _UserInfo { get; }

        Task SaveDBAsync();
        void SaveDB();

    }
}
