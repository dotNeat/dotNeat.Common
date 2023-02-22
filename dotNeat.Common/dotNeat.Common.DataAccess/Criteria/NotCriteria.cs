namespace dotNeat.Common.DataAccess.Criteria
{
    using System.Diagnostics;

    internal class NotCriteria<TEntity> 
        : ICriteria<TEntity>
    {
        private readonly ICriteria<TEntity> _wrapped;

        protected ICriteria<TEntity> Wrapped
        {
            get
            {
                return _wrapped;
            }
        }

        internal NotCriteria(
            ICriteria<TEntity> spec 
            )
        {
            Debug.Assert(spec != null);

            _wrapped = spec;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return !Wrapped.IsSatisfiedBy(entity);
        }

        public bool IsSatisfiedBy(object entity)
        {
            if (entity is TEntity typeSafeEntity)
            {
                return IsSatisfiedBy(typeSafeEntity);
            }
            else
            {
                return false;
            }
        }
    }
}
