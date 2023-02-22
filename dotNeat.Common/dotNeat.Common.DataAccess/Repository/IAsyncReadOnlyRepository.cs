namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAsyncReadOnlyRepository<TEntity, TEntityId>
        where TEntity :  IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        Task<bool> ContainsEntityAsync(TEntityId id);

        Task<long> CountEntitiesAsync();

        Task<long> CountEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative :  TEntity;

        Task<long> CountEntitiesAsync(ICriteria<TEntity> entitySpec);

        Task<TEntity?> GetEntityAsync(TEntityId id);

        Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative :  TEntity;

        Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync(ICriteria<TEntity> entitySpec);

        Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync();

        Task<IReadOnlyCollection<TEntity>> GetEntitiesPageAsync(
            ICriteria<TEntity> entitySpec,
            long pageNumber, 
            long pageSize,
            out long totalPages
            );

        Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesPageAsync<TEntityDerivative>(
            long pageNumber,
            long pageSize,
            out long totalPages
            );

        Task<IReadOnlyCollection<TEntity>> GetEntitiesPageAsync(
            long pageNumber,
            long pageSize,
            out long totalPages
            );
    }
}
