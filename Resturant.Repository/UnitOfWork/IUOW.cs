using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Resturant.DataAccess.Context;
using Resturant.Interfaces;
using Resturant.Repository.Base;

namespace Resturant.Repository.UOW
{
    public interface IUOW
    {
        
        IBase_Repo<TEntity> _Base<TEntity>() where TEntity : class;
        
        ResturantContext _context { get; }
        IUser_Repo _User { get; }
        IRole_Repo _Role { get; }

        Task SaveDBAsync();
        void SaveDB();

    }
}
