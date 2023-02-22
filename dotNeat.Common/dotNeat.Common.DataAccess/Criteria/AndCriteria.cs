namespace dotNeat.Common.DataAccess.Criteria
{
    using System;

    internal class AndCriteria<TEntity> 
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

        internal AndCriteria(
            ICriteria<TEntity> spec1, 
            ICriteria<TEntity> spec2
            )
        {
            if (spec1 == null)
                throw new ArgumentNullException("spec1");

            if (spec2 == null)
                throw new ArgumentNullException("spec2");

            _spec1 = spec1;
            _spec2 = spec2;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Spec1.IsSatisfiedBy(entity) && Spec2.IsSatisfiedBy(entity);
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
