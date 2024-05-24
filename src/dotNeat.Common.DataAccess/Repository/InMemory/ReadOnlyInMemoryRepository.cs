namespace dotNeat.Common.DataAccess.Repository.InMemory
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;
    using dotNeat.Common.DataAccess.Specification;

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class ReadOnlyInMemoryRepository<TEntity, TEntityId>
        : ReadOnlyRepositoryBase<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
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

        #region base overrides

        public override bool ContainsEntity(TEntityId id)
        {
            return _entities.ContainsKey(id);
        }

        public override ulong CountEntities()
        {
            return Convert.ToUInt64(
                _entities.LongCount()
                );
        }

        public override ulong CountEntities<TEntityDerivative>() 
        {
            return Convert.ToUInt64(
                _entities.Values.Where(i => i is TEntityDerivative).LongCount()
                );
        }

        public override ulong CountEntities(ICriteria<TEntity> criteria)
        {
            return Convert.ToUInt64( 
                _entities.Values.Where(i => criteria.IsSatisfiedBy(i)).LongCount()
                );
        }

        public override ulong CountEntities<TEntityDerivative>(ICriteria<TEntityDerivative> criteria) 
        {
            return Convert.ToUInt64(
                _entities.Values.Where(i => criteria.IsSatisfiedBy(i)).LongCount()
                );
        }

        public override TEntity? GetEntity(TEntityId id)
        {
#if NETSTANDARD2_0
            if (_entities.TryGetValue(id, out TEntity? value))
            {
                return value;
            }
            return default;
#else
            return _entities.GetValueOrDefault(id);
#endif
        }

        public override IReadOnlyCollection<TEntity> GetEntities()
        {
            return _entities.Values
                .ToArray();
        }

        public override IReadOnlyCollection<TEntity> GetEntities(ISpecification<TEntity> spec)
        {
            IEnumerable<TEntity> entities = 
                _entities.Values
                .ToArray();

            entities = ApplySpec(entities, spec);

            return entities.ToArray();
        }

        public override IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>(ISpecification<TEntityDerivative> spec) 
        {
            IEnumerable<TEntityDerivative> entities =
                _entities.Values
                .Where(e => e is TEntityDerivative)
                .Cast<TEntityDerivative>()
                .ToArray();

            entities = ApplySpec(entities, spec);

            return entities.ToArray();
        }

#endregion base overrides

        #region private 

        private IEnumerable<T> ApplySpec<T>(IEnumerable<T> entities, ISpecification<T> spec)
            where T : IEntity<TEntityId>
        {
            entities = ApplySpec(entities, spec.DataFilterSpec);
            entities = ApplySpec(entities, spec.DataSortingSpec);
            entities = ApplySpec(entities, spec.DataPaginationSpec);
            entities = ApplySpec(entities, spec.ExtraDataInclusionSpec);
            ValidateOutcome(entities, spec.ExpectedOutcome);

            return entities.ToArray();
        }

        private IEnumerable<T> ApplySpec<T>(IEnumerable<T> entities, ICriteria<T>? spec)
            where T : IEntity<TEntityId>
        {
            if (spec is not null)
            {
                entities =
                    entities
                    .Where(i => spec.IsSatisfiedBy(i));
            }

            return entities;
        }

        private IEnumerable<T> ApplySpec<T>(IEnumerable<T> entities, ISortingOrder<T>? spec)
            where T : IEntity<TEntityId>
        {
            if (spec is not null)
            {
                foreach (var sortingSpec in spec.Specifications)
                {
                    switch (sortingSpec.SortingDirection)
                    {
                        case ISortingOrder.Direction.Ascending:
                            entities = entities.OrderBy(sortingSpec.SortByExpression.Compile());
                            break;
                        case ISortingOrder.Direction.Descending:
                            entities = entities.OrderByDescending(sortingSpec.SortByExpression.Compile());
                            break;
                        default:
                            Report(
                                $"Unexpected {nameof(ISortingOrder.Direction)} enum's value: {sortingSpec.SortingDirection}",
                                true
                                );
                            break;
                    }
                }
            }

            return entities;
        }

        private IEnumerable<T> ApplySpec<T>(IEnumerable<T> entities, IPagination? spec)
            where T : IEntity<TEntityId>
        {
            if (spec is not null)
            {
                entities = entities
                    .Skip(Convert.ToInt32(CalculateSkip(spec.PageNumber, spec.PageSize)))
                    .Take(Convert.ToInt32(spec.PageSize));
            }

            return entities;
        }

        protected virtual IEnumerable<T> ApplySpec<T>(IEnumerable<T> entities, IExtraDataInclusion<T>? spec)
            where T : IEntity<TEntityId>
        {
            return entities;
        }

        #endregion private 

    }
}
