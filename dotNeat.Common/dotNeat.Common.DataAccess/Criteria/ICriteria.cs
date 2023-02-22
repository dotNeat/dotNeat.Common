namespace dotNeat.Common.DataAccess.Criteria
{
    public interface ICriteria
    {
        bool IsSatisfiedBy(object entity);
    }

    public interface ICriteria<TEntity>
        : ICriteria
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}
