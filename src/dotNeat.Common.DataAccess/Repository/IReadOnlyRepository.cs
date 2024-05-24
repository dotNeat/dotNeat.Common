namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;
    using dotNeat.Common.DataAccess.Specification;

    using System;
    using System.Collections.Generic;

    public interface IReadOnlyRepository<TEntity, in TEntityId> 
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        IAsyncReadOnlyRepository<TEntity, TEntityId> AsAsyncReadOnlyRepository { get; }

        bool ContainsEntity(TEntityId id);

        ulong CountEntities();

        ulong CountEntities<TEntityDerivative>()
            where TEntityDerivative : class, TEntity;

        ulong CountEntities(ICriteria<TEntity> criteria);

        ulong CountEntities<TEntityDerivative>(ICriteria<TEntityDerivative> criteria)
            where TEntityDerivative : class, TEntity;

        TEntity? GetEntity(TEntityId id);

        IReadOnlyCollection<TEntity> GetEntities();

        IReadOnlyCollection<TEntity> GetEntities(ISpecification<TEntity> spec);

        IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>(ISpecification<TEntityDerivative> spec)
            where TEntityDerivative : class, TEntity;

    }
}
