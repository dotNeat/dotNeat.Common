namespace dotNeat.Common.DataAccess.Specification
{
    using dotNeat.Common.DataAccess.Criteria;

    public interface ISpecification
    {
        public enum Outcome
        {
            Undetermined,
            NoneOrOne,
            One,
            Many,
        }

        Outcome ExpectedOutcome { get; }
        bool UseSplitQuery { get; }
    }
    public interface ISpecification<TEntity>
        : ISpecification
    {
        ICriteria<TEntity>? DataFilterSpec { get; }
        ISortingOrder<TEntity>? DataSortingSpec { get; }
        IExtraDataInclusion<TEntity>? ExtraDataInclusionSpec{ get; }
        IPagination? PaginationSpec { get; }
    }
}
