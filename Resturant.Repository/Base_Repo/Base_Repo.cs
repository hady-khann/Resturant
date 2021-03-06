using Microsoft.EntityFrameworkCore;
using Resturant.DBModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resturant.Repository.Base
{
    public class Base_Repo<TEntity, TContext> : IBase_Repo<TEntity> where TEntity : class where TContext : DbContext
    {
        protected TContext _context { get; set; }
        private DbSet<TEntity> contextDB;

        public Base_Repo(TContext context)
        {
            _context = context;
            this.contextDB = _context.Set<TEntity>();
        }



        public async Task Insert(TEntity entity)
        {
            await contextDB.AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
            contextDB.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            contextDB.Update(entity);
        }




        public async Task CreateRange(IEnumerable<TEntity> entities)
        {
            await contextDB.AddRangeAsync(entities);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            contextDB.RemoveRange(entities);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            contextDB.UpdateRange(entities);
        }


        #region Find

     
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await contextDB.AsNoTracking().ToListAsync();
        }
        public IQueryable<TEntity> FindAll()
        {
            return contextDB.AsNoTracking();
        }

        public async Task<TEntity> FindByID(Object id)
        {
            return await contextDB.FindAsync((Guid)id);
        }
        #endregion





        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> filter = null, PaginationDTO Page = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (Page != null)
            {
                return await query.Skip(Page.Skip).Take(Page.Take).AsNoTracking().ToListAsync(); 
            }
            else
                return await query.AsNoTracking().ToListAsync();


        }

    }
}
