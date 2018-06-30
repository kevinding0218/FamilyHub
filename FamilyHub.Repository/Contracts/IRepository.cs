using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FamilyHub.Repository.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region READ
        //var students = repository.Get(x => x.FirstName = "Bob",q => q.OrderBy(s => s.LastName));
        Task<TEntity> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);
        #endregion

        #region CREATE
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        #endregion

        #region UPDATE
        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void UpdateSingleWithTrackingProperties(TEntity entity, String[] Properties);
        #endregion

        #region DELETE
        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion

        #region ACTIVATE/DEACTIVATE
        void Activate(TEntity entity);
        void Deactive(TEntity entity);
        #endregion
    }
}
