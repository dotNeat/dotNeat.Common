namespace dotNeat.Common.DataAccess.Criteria
{
    using System.Diagnostics;

    internal class OrCriteria<TEntity> 
        : ICriteria<TEntity>
    {
        private readonly ICriteria<TEntity> _spec1;
        private readonly ICriteria<TEntity> _spec2;

        protected ICriteria<TEntity> Spec1
        {
            get
            {
                return _spec1;
            }
        }

        protected ICriteria<TEntity> Spec2
        {
            get
            {
                return _spec2;
            }
        }

        internal OrCriteria(
            ICriteria<TEntity> spec1, 
            ICriteria<TEntity> spec2
            )
        {
            Debug.Assert( spec1 != null );
            Debug.Assert( spec2 != null );

            _spec1 = spec1;
            _spec2 = spec2;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Spec1.IsSatisfiedBy(entity) || Spec2.IsSatisfiedBy(entity);
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
