using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WinterEngine.DataAccess.Interface;

namespace WinterEngine.DataAccess.Repositories
{
    public class GenericRepository<CContext, TEntity> : IGenericRepository<TEntity>
        where TEntity : class
        where CContext : DbContext
    {
        private CContext _entities;

        public GenericRepository(CContext context)
        {
            _entities = context;
        }

        public CContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _entities.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                               string includeProperties = "", Boolean readOnly = false, Expression<Func<TEntity, Object>>[] includeExpressions = null)
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (includeExpressions != null)
            {
                includeExpressions.Select(s => query = query.Include(s));
            }

            if (orderBy != null)
            {
                return readOnly ? orderBy(query.AsNoTracking()) : orderBy(query);
            }

            return readOnly ? query.AsNoTracking() : query;
        }

        public virtual void SubmitChanges()
        {
            _entities.SaveChanges();
        }


    }
}
