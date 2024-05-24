using System;
using dotNeat.Common.DataAccess.Entity;
using dotNeat.Common.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace dotNeat.Common.DataAccess.EFCore.Repository
{
    public class EFCoreRepository<TEntity, TEntityId>
        : ReadOnlyEFCoreRepository<TEntity, TEntityId>
        , IRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        public EFCoreRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        #region IRepository<TEntity, TEntityId>

        public IAsyncRepository<TEntity, TEntityId> AsAsyncRepository
        {
            get
            {
                if (_asyncRepo == null)
                {
                    _asyncRepo = new RepositoryAsyncWrapper<TEntity, TEntityId>(this);
                }
                return _asyncRepo;
            }
        }
        private IAsyncRepository<TEntity, TEntityId>? _asyncRepo = null;

        public IRepository<TEntity, TEntityId> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return this;
        }

        public IRepository<TEntity, TEntityId> AddGraph(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity, TEntityId> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return this;
        }

        public IRepository<TEntity, TEntityId> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return this;
        }

        public IRepository<TEntity, TEntityId> Delete(TEntityId id)
        {
            var entity = _dbSet.Find(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
            }
            return this;
        }

        public IRepository<TEntity, TEntityId> Clear()
        {
            _dbSet.RemoveRange(_dbSet);
            return this;
        }

        #endregion IRepository<TEntity, TEntityId>
    }
}

