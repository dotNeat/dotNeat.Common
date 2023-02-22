namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Entity;

    using System;

    public interface IRepository<TEntity, TEntityId> 
        : IReadOnlyRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        TEntityDerivative Create<TEntityDerivative>() 
            where TEntityDerivative : class, TEntity;
        
        IRepository<TEntity, TEntityId> Add(TEntity entity);
        
        IRepository<TEntity, TEntityId> AddGraph(TEntity entity);
        
        IRepository<TEntity, TEntityId> Update(TEntity entity);

        IRepository<TEntity, TEntityId> Delete(TEntity entity);

        IRepository<TEntity, TEntityId> Delete(TEntityId id);

        IRepository<TEntity, TEntityId> Clear();
    }
}
