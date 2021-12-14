using Resturant.DBModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Repository.Base
{
    public interface IBase_Repo<TEntity>
    {
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);



        Task<IEnumerable<TEntity>> FindAllAsync_Pagination(PaginationDTO pagination);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity> FindByID(Object id);




        Task CreateRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);




        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);



    }
}
