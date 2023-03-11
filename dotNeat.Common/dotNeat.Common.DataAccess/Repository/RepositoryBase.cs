namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Entity;

    using System;

    public abstract class RepositoryBase<TEntity, TEntityId>
        : ReadOnlyRepositoryBase<TEntity, TEntityId>
        , IRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private readonly Guid _repoInstanceId = Guid.NewGuid();

        private IAsyncRepository<TEntity, TEntityId>? _asyncRepo = null;

        #region IRepository

        public IAsyncRepository<TEntity, TEntityId> AsAsyncRepository
        {
            get
            {
                _asyncRepo ??= new RepositoryAsyncWrapper<TEntity, TEntityId>(this);
                return _asyncRepo;
            }
        }

        public abstract IRepository<TEntity, TEntityId> Add(TEntity entity);
        public abstract IRepository<TEntity, TEntityId> AddGraph(TEntity entity);
        public abstract IRepository<TEntity, TEntityId> Update(TEntity entity);
        public abstract IRepository<TEntity, TEntityId> Delete(TEntity entity);
        public abstract IRepository<TEntity, TEntityId> Delete(TEntityId id);
        public abstract IRepository<TEntity, TEntityId> Clear();

        #endregion IRepository
    }
}
