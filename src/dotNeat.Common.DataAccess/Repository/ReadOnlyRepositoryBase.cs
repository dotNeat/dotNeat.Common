namespace dotNeat.Common.DataAccess.Repository
{
    using dotNeat.Common.DataAccess.Criteria;
    using dotNeat.Common.DataAccess.Entity;
    using dotNeat.Common.DataAccess.Specification;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public abstract class ReadOnlyRepositoryBase<TEntity, TEntityId>
        : IReadOnlyRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        private IAsyncReadOnlyRepository<TEntity, TEntityId>? _asyncRepo = null;

        #region IReadOnlyRepository

        public IAsyncReadOnlyRepository<TEntity, TEntityId> AsAsyncReadOnlyRepository
        {
            get
            {
                _asyncRepo ??= new ReadOnlyRepositoryAsyncWrapper<TEntity, TEntityId>(this);
                return _asyncRepo;
            }
        }

        public abstract bool ContainsEntity(TEntityId id);
        public abstract ulong CountEntities();
        public abstract ulong CountEntities<TEntityDerivative>() 
            where TEntityDerivative : class, TEntity;
        public abstract ulong CountEntities(ICriteria<TEntity> criteria);
        public abstract ulong CountEntities<TEntityDerivative>(ICriteria<TEntityDerivative> criteria) 
            where TEntityDerivative : class, TEntity;
        public abstract TEntity? GetEntity(TEntityId id);
        public abstract IReadOnlyCollection<TEntity> GetEntities();
        public abstract IReadOnlyCollection<TEntity> GetEntities(ISpecification<TEntity> spec);
        public abstract IReadOnlyCollection<TEntityDerivative> GetEntities<TEntityDerivative>(ISpecification<TEntityDerivative> spec) 
            where TEntityDerivative : class, TEntity;

        #endregion IReadOnlyRepository

        #region protected

        protected void ValidateOutcome<T>(IEnumerable<T> entities, ISpecification.Outcome expectedOutcome)
            where T : IEntity<TEntityId>
        {
            switch (expectedOutcome)
            {
                case ISpecification.Outcome.Undetermined:
                case ISpecification.Outcome.Many:
                    // all good!
                    break;
                case ISpecification.Outcome.One:
                    Report($"Wrong outcome! Expected single entity, but actual is {entities.LongCount()} entities.", true);
                    break;
                case ISpecification.Outcome.OneOrNone:
                    Report($"Wrong outcome! Expected one OR no entity, but actual is {entities.LongCount()} entities.", true);
                    break;
            }
        }

        protected ulong CalculateSkip(ulong pageNumber, ulong pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        protected ulong CalculateTotalPages(ulong itemsCount, ulong itemsPerPage)
        {
#if NET8_0_OR_GREATER
            var result = Math.DivRem(itemsCount, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1UL : 0);
#else
            long quotient = Math.DivRem(Convert.ToInt64(itemsCount), Convert.ToInt64(itemsPerPage), out long remainder);
            return Convert.ToUInt64(quotient) + (remainder > 0 ? 1UL : 0UL);
#endif
            
        }

        protected void Report(string message, bool shouldAssert = false)
        {
            string traceMessage = $"{GetType().Name}: {message}";
            Trace.WriteLine(traceMessage);
            if (shouldAssert)
            {
                Debug.Assert(false, traceMessage);
            }
        }

#endregion protected
    }
}
