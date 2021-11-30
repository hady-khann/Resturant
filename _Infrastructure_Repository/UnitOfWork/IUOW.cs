using Resturant.Core.Models;
using Resturant.Infrastructure.Repository.Role_Repo;
using Resturant.Infrastructure.Repository.User_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Repository
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
