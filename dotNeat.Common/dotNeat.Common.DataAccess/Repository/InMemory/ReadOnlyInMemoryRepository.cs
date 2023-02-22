namespace dotNeat.Common.DataAccess.Repository.InMemory
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class ReadOnlyInMemoryRepository<TEntity, TEntityId> 
        : IReadOnlyRepository<TEntity, TEntityId>
        where TEntity :  IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        protected readonly ConcurrentDictionary<TEntityId, TEntity> _entities = 
            new ConcurrentDictionary<TEntityId, TEntity>();

        public ReadOnlyInMemoryRepository(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!_entities.TryAdd(entity.ID, entity))
                {
                    Report($"The key with entity ID = {entity.ID} already exists!", true);
                }
            }
        }

        public bool ContainsEntity(TEntityId id)
        {
            return _entities.ContainsKey(id);
        }

        public long CountEntities()
        {
            return _entities.Count();
        }

        public long CountEntities<TEntityDerivative>()
            where TEntityDerivative :  TEntity
        {
            return _entities.Values.Where(i => i is TEntityDerivative).Count(); 
        }

        public long CountEntities(ICriteria<TEntity> entitySpec)
        {
            return _entities.Values.Where(i => entitySpec.IsSatisfiedBy(i)).Count();
        }

        public TEntity? GetEntity(TEntityId id)
        {
            if (_entities.TryGetValue(id, out TEntity? value))
            {
                return value;
            }
            return default(TEntity?);
        }

        public IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>()
            where TEntityDerivative :  TEntity
        {
            return _entities.Values
                .Where(i => i is TEntityDerivative)
                .Cast<TEntityDerivative>()
                .ToArray();
        }

        public IReadOnlyCollection<TEntity> GetEntities(ICriteria<TEntity> entitySpec)
        {
            return _entities.Values
                .Where(i => entitySpec.IsSatisfiedBy(i))
                .ToArray();
        }

        public IReadOnlyCollection<TEntity> GetEntities()
        {
            return _entities.Values
                .ToArray();
        }

        public IReadOnlyCollection<TEntity> GetEntitiesPage(
            ICriteria<TEntity> entitySpec,
            long pageNumber,
            long pageSize,
            out long totalPages
            )
        {
            var items = _entities.Values
                .Where(i => entitySpec.IsSatisfiedBy(i))
                .Skip(CalculateSkip(pageNumber, pageSize))
                .Take(Convert.ToInt32(pageSize))
                .ToArray();

            totalPages = CalculateTotalPages(items.Count(), pageSize);

            return items;
        }

        public IReadOnlyCollection<TEntityDerivative> GetEntitiesPage<TEntityDerivative>(
            long pageNumber,
            long pageSize,
            out long totalPages
            )
            where TEntityDerivative :  TEntity
        {
            var items = _entities.Values
                .Where(i => i is TEntityDerivative)
                .Skip(CalculateSkip(pageNumber, pageSize))
                .Take(Convert.ToInt32(pageSize))
                .Cast<TEntityDerivative>()
                .ToArray();

            totalPages = CalculateTotalPages(items.Count(), pageSize);

            return items;
        }

        public IReadOnlyCollection<TEntity> GetEntitiesPage(
            long pageNumber,
            long pageSize,
            out long totalPages
            )
        {
            var items = _entities.Values
                .Skip(CalculateSkip(pageNumber, pageSize))
                .Take(Convert.ToInt32(pageSize))
                .ToArray();

            totalPages = CalculateTotalPages(items.Count(), pageSize);

            return items;
        }

        private int CalculateSkip(long pageNumber, long pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return Convert.ToInt32(result);
        }

        private long CalculateTotalPages(long itemsCount, long itemsPerPage)
        {
            var result = Math.DivRem(itemsCount, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1 : 0);
        }

        protected void Report(string message, bool shouldAssert = false)
        {
            string traceMessage = $"{this.GetType().Name}: {message}";
            Trace.WriteLine(traceMessage);
            if (shouldAssert)
            {
                Debug.Assert(false, traceMessage);
            }
        }
    }
}
