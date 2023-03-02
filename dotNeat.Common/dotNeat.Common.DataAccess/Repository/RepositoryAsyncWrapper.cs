namespace dotNeat.Common.DataAccess.Repository
{
    using System;
    using System.Threading.Tasks;

    using dotNeat.Common.DataAccess.Entity;

    public class RepositoryAsyncWrapper<TEntity, TEntityId>
        : ReadOnlyRepositoryAsyncWrapper<TEntity, TEntityId>
        , IAsyncRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private readonly IRepository<TEntity, TEntityId> _repository;

        public RepositoryAsyncWrapper(IRepository<TEntity, TEntityId> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public async Task<IRepository<TEntity, TEntityId>> AddAsync(TEntity entity)
        {
            return await Task.Run(() => _repository.Add(entity));
        }

        public async Task<IRepository<TEntity, TEntityId>> AddGraphAsync(TEntity entity)
        {
            return await Task.Run(() => _repository.AddGraph(entity));
        }

        public async Task<IRepository<TEntity, TEntityId>> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => _repository.Update(entity));
        }

        public async Task<IRepository<TEntity, TEntityId>> DeleteAsync(TEntity entity)
        {
            return await Task.Run(() => _repository.Delete(entity));
        }

        public async Task<IRepository<TEntity, TEntityId>> DeleteAsync(TEntityId id)
        {
            return await Task.Run(() => _repository.Delete(id));
        }

        public async Task<IRepository<TEntity, TEntityId>> ClearAsync()
        {
            return await Task.Run(() => _repository.Clear());
        }
    }
}
