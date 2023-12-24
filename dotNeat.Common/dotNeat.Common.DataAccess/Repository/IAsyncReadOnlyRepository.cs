namespace dotNeat.Common.DataAccess.Repository
{
    using Criteria;
    using Entity;
    using Specification;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAsyncReadOnlyRepository<TEntity, in TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        Task<bool> ContainsEntityAsync(TEntityId id);

        Task<ulong> CountEntitiesAsync();

        Task<ulong> CountEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative : class, TEntity;

        Task<ulong> CountEntitiesAsync(ICriteria<TEntity> criteria);

        Task<ulong> CountEntitiesAsync<TEntityDerivative>(ICriteria<TEntityDerivative> criteria)
            where TEntityDerivative : class, TEntity;

        Task<TEntity?> GetEntityAsync(TEntityId id);

        Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync();

        Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync(ISpecification<TEntity> spec);

        Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesAsync<TEntityDerivative>(ISpecification<TEntityDerivative> spec)
            where TEntityDerivative : class, TEntity;

    }
}
