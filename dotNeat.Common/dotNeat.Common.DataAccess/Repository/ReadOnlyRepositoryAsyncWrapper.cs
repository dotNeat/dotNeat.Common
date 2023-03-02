namespace dotNeat.Common.DataAccess.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;
    using dotNeat.Common.DataAccess.Specification;

    public class ReadOnlyRepositoryAsyncWrapper<TEntity, TEntityId>
        : IAsyncReadOnlyRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private readonly IReadOnlyRepository<TEntity, TEntityId> _repository;

        public ReadOnlyRepositoryAsyncWrapper(IReadOnlyRepository<TEntity, TEntityId> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ContainsEntityAsync(TEntityId id)
        {
            return await Task.Run(() => _repository.ContainsEntity(id));
        }

        public async Task<ulong> CountEntitiesAsync()
        {
            return await Task.Run(() => _repository.CountEntities());
        }

        public async Task<ulong> CountEntitiesAsync<TEntityDerivative>()
            where TEntityDerivative : class, TEntity
        {
            return await Task.Run(() => _repository.CountEntities<TEntityDerivative>());
        }

        public async Task<ulong> CountEntitiesAsync(ICriteria<TEntity> criteria)
        {
            return await Task.Run(() => _repository.CountEntities(criteria));
        }

        public async Task<ulong> CountEntitiesAsync<TEntityDerivative>(ICriteria<TEntityDerivative> criteria)
            where TEntityDerivative : class, TEntity
        {
            return await Task.Run(() => _repository.CountEntities(criteria));
        }

        public async Task<TEntity?> GetEntityAsync(TEntityId id)
        {
            return await Task.Run(() => _repository.GetEntity(id));
        }

        public async Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync()
        {
            return await Task.Run(() => _repository.GetEntities());
        }

        public async Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync(ISpecification<TEntity> spec)
        {
            return await Task.Run(() => _repository.GetEntities(spec));
        }

        public async Task<IReadOnlyCollection<TEntityDerivative>> GetEntitiesAsync<TEntityDerivative>(ISpecification<TEntityDerivative> spec)
            where TEntityDerivative : class, TEntity
        {
            return await Task.Run(() => _repository.GetEntities(spec));
        }
    }
}
