namespace dotNeat.Common.DataAccess.Repository.EFCore
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReadOnlyEFCoreRepository<TEntity, TEntityId>
        : IReadOnlyRepository<TEntity, TEntityId>
        , IAsyncReadOnlyRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private readonly Guid _repoInstanceId;
        internal readonly DbContext _dbContext;
        internal readonly DbSet<TEntity> _dbSet;

        public ReadOnlyEFCoreRepository(DbContext dbContext)
		{
            Debug.Assert(dbContext is not null, $"{nameof(dbContext)} must not be null!");

            _repoInstanceId = Guid.NewGuid();
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();

            Debug.Assert(_dbSet is not null, $"{nameof(_dbSet)} must not be null!");
		}

        #region IReadOnlyRepository

        public bool ContainsEntity(TEntityId id)
        {
            return _dbSet.Find(id) != null;
        }

        public long CountEntities()
        {
            return _dbSet.LongCount();
        }

        public long CountEntities(ICriteria<TEntity> entitySpec)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetEntities(ICriteria<TEntity> entitySpec)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetEntities()
        {
            return _dbSet.ToArray();
        }

        public IReadOnlyCollection<TEntity> GetEntitiesPage(ICriteria<TEntity> entitySpec, long pageNumber, long pageSize, out long totalPages)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetEntitiesPage(long pageNumber, long pageSize, out long totalPages)
        {
            throw new NotImplementedException();
        }

        public TEntity? GetEntity(TEntityId id)
        {
            return _dbSet.Find(id);
        }

        long IReadOnlyRepository<TEntity, TEntityId>.CountEntities<TEntityDerivative>()
        {
            return _dbSet.OfType<TEntityDerivative>().LongCount();
        }

        IReadOnlyCollection<TEntityDerivative> IReadOnlyRepository<TEntity, TEntityId>.GetEntities<TEntityDerivative>()
        {
            return _dbSet.OfType<TEntityDerivative>().ToArray();
        }

        IReadOnlyCollection<TEntityDerivative> IReadOnlyRepository<TEntity, TEntityId>.GetEntitiesPage<TEntityDerivative>(long pageNumber, long pageSize, out long totalPages)
        {
            throw new NotImplementedException();
        }

        #endregion IReadOnlyRepository

        #region IAsyncReadOnlyRepository

        public Task<bool> ContainsEntityAsync(TEntityId id)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<long> CountEntitiesAsync(ICriteria<TEntity> entitySpec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync(ICriteria<TEntity> entitySpec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetEntitiesPageAsync(ICriteria<TEntity> entitySpec, long pageNumber, long pageSize, out long totalPages)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesPageAsync<TEntityDerivative>(long pageNumber, long pageSize, out long totalPages)

        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetEntitiesPageAsync(long pageNumber, long pageSize, out long totalPages)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> GetEntityAsync(TEntityId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<long> CountEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative :  TEntity
        {
            return await _dbSet.OfType<TEntityDerivative>().LongCountAsync();
        }

        public async Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative :  TEntity
        {
            return await _dbSet.OfType<TEntityDerivative>().ToArrayAsync();
        }

        #endregion IAsyncReadOnlyRepository
    }
}

