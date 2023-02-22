namespace dotNeat.Common.DataAccess.Specification
{
    using dotNeat.Common.DataAccess.Criteria;

    public interface ISpecification
    {
        public enum Outcome
        {
            Undetermined,
            Many,
            NoneOrOne,
            One,
        }

        Outcome ExpectedOutcome { get; }
        bool UseSplitQuery { get; }
    }
    public interface ISpecification<TEntity>
        : ISpecification
    {
        ICriteria<TEntity>? FilterCriteria { get; }
        ISortingOrder<TEntity>? SortingOrder { get; }
        IInclusionSpec<TEntity>? InclusionSpec{ get; }
        IPagination? Pagination { get; }
    }
}
