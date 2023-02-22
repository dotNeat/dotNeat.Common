namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;

    using System;
    using System.Collections.Generic;

    public interface IReadOnlyRepository<TEntity, TEntityId> 
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        bool ContainsEntity(TEntityId id);

        long CountEntities();

        long CountEntities<TEntityDerivative>()
            where TEntityDerivative : class, TEntity;

        long CountEntities(ICriteria<TEntity> entitySpec);

        TEntity? GetEntity(TEntityId id);

        IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>() 
            where TEntityDerivative : class, TEntity;

        IReadOnlyCollection<TEntity> GetEntities(ICriteria<TEntity> entitySpec);

        IReadOnlyCollection<TEntity> GetEntities();

        /// <summary>
        /// Gets the entities page.
        /// </summary>
        /// <param name="entitySpec">The entity spec.</param>
        /// <param name="pageNumber">The page number. 1-based.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <returns>IReadOnlyCollection&lt;TEntity&gt;.</returns>
        IReadOnlyCollection<TEntity> GetEntitiesPage(
            ICriteria<TEntity> entitySpec,
            long pageNumber, 
            long pageSize, 
            out long totalPages
            );

        /// <summary>
        /// Gets the entities page.
        /// </summary>
        /// <typeparam name="TEntityDerivative">The type of the t entity derivative.</typeparam>
        /// <param name="pageNumber">The page number. 1-based.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <returns>IReadOnlyCollection&lt;TEntityDerivative&gt;.</returns>
        IReadOnlyCollection<TEntityDerivative> GetEntitiesPage<TEntityDerivative>(
            long pageNumber, 
            long pageSize, 
            out long totalPages
            ) 
            where TEntityDerivative : class, TEntity;

        /// <summary>
        /// Gets the entities page.
        /// </summary>
        /// <param name="pageNumber">The page number. 1-based.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <returns>IReadOnlyCollection&lt;TEntity&gt;.</returns>
        IReadOnlyCollection<TEntity> GetEntitiesPage(
            long pageNumber, 
            long pageSize,
            out long totalPages
            );

    }
}
