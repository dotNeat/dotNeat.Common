namespace dotNeat.Common.DataAccess.Repository.EFCore
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;
    using dotNeat.Common.DataAccess.Specification;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class ReadOnlyEFCoreRepository<TEntity, TEntityId>
        : ReadOnlyRepositoryBase<TEntity, TEntityId>
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

        #region base overrides

        public override bool ContainsEntity(TEntityId id)
        {
            return _dbSet.Find(id) != null;
        }

        public override ulong CountEntities()
        {
            return Convert.ToUInt64(
                _dbSet.LongCount()
                );
        }

        public override ulong CountEntities<TEntityDerivative>()
        {
            return Convert.ToUInt64(
                _dbContext.Set<TEntityDerivative>().LongCount()
                );
        }

        public override ulong CountEntities(ICriteria<TEntity> criteria)
        {
            return Convert.ToUInt64(
                _dbSet
                .Where(e => criteria.IsSatisfiedBy(e))
                .LongCount()
                );
        }

        public override ulong CountEntities<TEntityDerivative>(ICriteria<TEntityDerivative> criteria)
        {
            return Convert.ToUInt64(
                _dbContext.Set<TEntityDerivative>()
                .Where(e => criteria.IsSatisfiedBy(e))
                .LongCount()
                );
        }

        public override TEntity? GetEntity(TEntityId id)
        {
            return _dbSet.Find(id);
        }

        public override IReadOnlyCollection<TEntity> GetEntities()
        {
            return _dbSet.ToArray();
        }

        public override IReadOnlyCollection<TEntity> GetEntities(ISpecification<TEntity> spec)
        {
            return QueryableBuilder
                .BuildQueryable(_dbSet, spec)
                .ToArray();
        }

        public override IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>(ISpecification<TEntityDerivative> spec)
        {
            return QueryableBuilder
                .BuildQueryable(_dbContext.Set<TEntityDerivative>(), spec)
                .ToArray();
        }

        #endregion base overrides
    }
}

