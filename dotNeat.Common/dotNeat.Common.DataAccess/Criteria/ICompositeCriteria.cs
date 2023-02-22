namespace dotNeat.Common.DataAccess.Criteria
{
    public interface ICompositeCriteria
        : ICriteria
    {
        ICriteria And(ICriteria criteria);
        ICriteria Or(ICriteria criteria);
        ICriteria Not(ICriteria criteria);
    }

    public interface ICompositeCriteria<TEntity> 
        : ICriteria<TEntity>
    {
        ICriteria<TEntity> And(ICriteria<TEntity> criteria);
        ICriteria<TEntity> Or(ICriteria<TEntity> criteria);
        ICriteria<TEntity> Not(ICriteria<TEntity> criteria);
    }
}
