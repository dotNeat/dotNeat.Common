namespace dotNeat.Common.DataAccess.Repository.InMemory
{
    using dotNeat.Common.DataAccess.Entity;

    using System;
    using System.Collections.Generic;

    public class InMemoryRepository<TEntity, TEntityId>
        : ReadOnlyInMemoryRepository<TEntity, TEntityId>
        , IRepository<TEntity, TEntityId>
        where TEntity :  IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private InMemoryRepository()
            : this(Array.Empty<TEntity>())
        {
        }

        public InMemoryRepository(IEnumerable<TEntity> entities)
            :base(entities)
        {
        }

        public virtual TEntityDerivative Create<TEntityDerivative>()
            where TEntityDerivative :  TEntity
        {
            throw new NotImplementedException();
            //return default(TEntityDerivative);
        }

        public IRepository<TEntity, TEntityId> Add(TEntity entity)
        {
            if (_entities.ContainsKey(entity.ID))
            {
                Report($"An attempt to add a duplicate entry with ID = {entity.ID}!", true);
            }
            _entities[entity.ID] = entity;

            return this;
        }

        public IRepository<TEntity, TEntityId> AddGraph(TEntity entity)
        {
            if (_entities.ContainsKey(entity.ID))
            {
                Report($"An attempt to add a duplicate entry with ID = {entity.ID}!", true);
            }
            _entities[entity.ID] = entity;

            return this;
        }

        public IRepository<TEntity, TEntityId> Update(TEntity entity)
        {
            if (!_entities.ContainsKey(entity.ID))
            {
                Report($"An attempt to update non-existing entry with ID = {entity.ID}!", true);
            }
            _entities[entity.ID] = entity;

            return this;
        }

        public IRepository<TEntity, TEntityId> Delete(TEntity entity)
        {
            Delete(entity.ID);

            return this;
        }

        public IRepository<TEntity, TEntityId> Delete(TEntityId id)
        {
            if (!_entities.TryRemove(id, out TEntity? entity))
            {
                Report($"An attempt to delete non-existing entry with ID = {id}!", true);
            }

            return this;
        }

        public IRepository<TEntity, TEntityId> Clear()
        {
            _entities.Clear();

            return this;
        }


    }
}
