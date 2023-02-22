namespace dotNeat.Common.DataAccess.Criteria
{
    public static class CriteriaExtensions
    {
        public static ICriteria<TEntity> And<TEntity>(this ICriteria<TEntity> spec, ICriteria<TEntity> otherSpec)
        {
            return new AndCriteria<TEntity>(spec, otherSpec);
        }

        public static ICriteria<TEntity> Or<TEntity>(this ICriteria<TEntity> spec, ICriteria<TEntity> otherSpec)
        {
            return new OrCriteria<TEntity>(spec, otherSpec);
        }

        public static ICriteria<TEntity> Not<TEntity>(this ICriteria<TEntity> spec)
        {
            return new NotCriteria<TEntity>(spec);
        }
    }
}
