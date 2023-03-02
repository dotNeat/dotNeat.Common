namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Entity;

    using System;
    using System.Threading.Tasks;

    public interface IAsyncRepository<TEntity, TEntityId> 
        : IAsyncReadOnlyRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        //Task<TEntityDerivative> CreateAsync<TEntityDerivative>()
        //    where TEntityDerivative :  TEntity;

        Task<IRepository<TEntity, TEntityId>> AddAsync(TEntity entity);

        Task<IRepository<TEntity, TEntityId>> AddGraphAsync(TEntity entity);

        Task<IRepository<TEntity, TEntityId>> UpdateAsync(TEntity entity);

        Task<IRepository<TEntity, TEntityId>> DeleteAsync(TEntity entity);

        Task<IRepository<TEntity, TEntityId>> DeleteAsync(TEntityId id);

        Task<IRepository<TEntity, TEntityId>> ClearAsync();
    }
}
