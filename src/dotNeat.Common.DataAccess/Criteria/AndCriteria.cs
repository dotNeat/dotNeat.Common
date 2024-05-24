namespace dotNeat.Common.DataAccess.Criteria
{
    using System;

    internal class AndCriteria<TEntity> 
        : ICriteria<TEntity>
    {
        protected ICriteria<TEntity> Spec1 { get; }

        protected ICriteria<TEntity> Spec2 { get; }

        internal AndCriteria(
            ICriteria<TEntity> spec1, 
            ICriteria<TEntity> spec2
            )
        {
            if (spec1 == null)
                throw new ArgumentNullException(nameof(spec1));

            if (spec2 == null)
                throw new ArgumentNullException(nameof(spec2));

            Spec1 = spec1;
            Spec2 = spec2;
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
