using System;
using System.Linq;
using System.Linq.Expressions;

namespace WinterEngine.DataAccess.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                string includeProperties = "", Boolean readOnly = false,
                                Expression<Func<TEntity, Object>>[] includeExpressions = null);
        void SubmitChanges();
    }
}
