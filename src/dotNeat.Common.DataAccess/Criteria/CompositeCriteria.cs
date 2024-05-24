namespace dotNeat.Common.DataAccess.Criteria
{
    public abstract class CompositeCriteria<TEntity> 
        : ICompositeCriteria<TEntity>
    {
        public abstract bool IsSatisfiedBy(TEntity entity);

        public ICriteria<TEntity> And(ICriteria<TEntity> criteria)
        {
            return new AndCriteria<TEntity>(this, criteria);
        }
        public ICriteria<TEntity> Or(ICriteria<TEntity> criteria)
        {
            return new OrCriteria<TEntity>(this, criteria);
        }
        public ICriteria<TEntity> Not(ICriteria<TEntity> criteria)
        {
            return new NotCriteria<TEntity>(criteria);
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
