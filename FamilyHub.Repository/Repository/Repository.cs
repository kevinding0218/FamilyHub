using FamilyHub.Data;
using FamilyHub.Data.Logging;
using FamilyHub.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IUnitOfWork where TEntity : class
    {
        protected IUserInfo _userInfo;
        protected DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        public Repository(IUserInfo userInfo, DbContext context)
        {
            _userInfo = userInfo;
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        #region READ
        //var students = repository.Get(x => x.FirstName = "Bob",q => q.OrderBy(s => s.LastName));
        public async Task<TEntity> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).SingleOrDefaultAsync();
            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        #endregion

        #region CREATE
        public void Add(TEntity entity)
        {
            AttachCreatedOn(entity);

            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(e =>
            {
                AttachCreatedOn(e);
            });

            _dbSet.AddRange(entities);
        }

        protected void AttachCreatedOn(TEntity e)
        {
            var auditEntity = e as IAuditableEntity;
            if (auditEntity != null)
            {
                auditEntity.CreatedBy = _userInfo.UID ?? auditEntity.CreatedBy;
                if (!auditEntity.CreatedOn.HasValue)
                    auditEntity.CreatedOn = DateTime.Now;
            }
        }
        #endregion

        #region UPDATE
        public void Update(TEntity entity)
        {
            AttachLastUpdatedOn(entity);

            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(e =>
            {
                AttachLastUpdatedOn(e);
            });

            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public void UpdateSingleWithTrackingProperties(TEntity entity, String[] Properties)
        {
            AttachLastUpdatedOn(entity);

            _dbContext.Attach(entity);
            Properties.ToList()
                .ForEach(
                    property => _dbContext.Entry(entity).Property(property).IsModified = true
            );
        }

        protected void AttachLastUpdatedOn(TEntity e)
        {
            var auditEntity = e as IAuditableEntity;
            if (auditEntity != null)
            {
                auditEntity.LastUpdatedBy = _userInfo.UID ?? auditEntity.LastUpdatedBy;
                if (!auditEntity.LastUpdatedOn.HasValue)
                    auditEntity.LastUpdatedOn = DateTime.Now;
            }
        }
        #endregion

        #region DELETE
        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
        #endregion

        #region ACTIVATE/DEACTIVATE
        public void Activate(TEntity entity)
        {
            var activeEntity = entity as IActivateEntity;
            if (activeEntity != null)
            {
                activeEntity.Active = true;
            }
            AttachLastUpdatedOn(entity);
        }

        public void Deactive(TEntity entity)
        {
            var activeEntity = entity as IActivateEntity;
            if (activeEntity != null)
            {
                activeEntity.Active = false;
            }
            AttachLastUpdatedOn(entity);
        }
        #endregion

        #region Unit Of Work
        protected virtual IEnumerable<ChangeLog> GetChanges()
        {
            var exclusions = _dbContext.Set<ChangeLogExclusion>().ToList();

            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Modified)
                    continue;

                var entityType = entry.Entity.GetType();

                if (exclusions.Where(item => item.EntityName == entityType.Name && item.PropertyName == "*").Count() == 1)
                    yield break;

                foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                {
                    // Validate if there is an exclusion for *.Property
                    if (exclusions.Where(item => item.EntityName == "*" && string.Compare(item.PropertyName, property.Name, true) == 0).Count() == 1)
                        continue;

                    // Validate if there is an exclusion for Entity.Property
                    if (exclusions.Where(item => item.EntityName == entityType.Name && string.Compare(item.PropertyName, property.Name, true) == 0).Count() == 1)
                        continue;

                    var originalValue = entry.Property(property.Name).OriginalValue;
                    var currentValue = entry.Property(property.Name).CurrentValue;

                    if (string.Concat(originalValue) == string.Concat(currentValue))
                        continue;

                    // todo: improve the way to retrieve primary key value from entity instance
                    var key = entry.Entity.GetType().GetProperties()[0].GetValue(entry.Entity, null).ToString();

                    yield return new ChangeLog
                    {
                        ClassName = entityType.Name,
                        PropertyName = property.Name,
                        Key = key,
                        OriginalValue = originalValue == null ? string.Empty : originalValue.ToString(),
                        CurrentValue = currentValue == null ? string.Empty : currentValue.ToString(),
                        UserID = _userInfo.UID.Value,
                        ChangeDate = DateTime.Now
                    };
                }
            }
        }

        public async Task<int> CommitChangesAsync()
        {
            var dbSet = _dbContext.Set<ChangeLog>();

            //foreach (var change in GetChanges().ToList())
            //{
            //    dbSet.Add(change);
            //}

            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
