namespace dotNeat.Common.DataAccess.Specification
{
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Diagnostics;
    using System.Linq;

    public static class QueryableBuilder
    {
        public static IQueryable<TEntity> BuildQueryable<TEntity>(
            IQueryable<TEntity> queryableSeed, 
            ISpecification<TEntity> specification
            )
            where TEntity : class
        {
            IQueryable<TEntity> queryable = queryableSeed;

            if (specification.DataFilterSpec is not null)
            {
                switch (specification.ExpectedOutcome)
                {
                    case ISpecification<TEntity>.Outcome.Undetermined:
                    case ISpecification<TEntity>.Outcome.Many:
                        queryable = queryable.Where(i => specification.DataFilterSpec.IsSatisfiedBy(i));
                        break;
                    //TODO: refactore the alternative return object and implement as needed:
                    //case ISpecification<TEntity>.Outcome.NoneOrOne:
                    //    _ = queryable.SingleOrDefault(i => specification.FilterCriteria.IsSatisfiedBy(i));
                    //    break;
                    //case ISpecification<TEntity>.Outcome.One:
                    //    _ = queryable.Single(i => specification.FilterCriteria.IsSatisfiedBy(i));
                    //    break;
                    default: 
                        Debug.Assert(false, "This Outcome is not handled yet!");
                        break;
                }
            }

            if (specification.ExtraDataInclusionSpec is not null 
                && specification.ExtraDataInclusionSpec.IncludeExpressions.Count > 0
                )
            {
                queryable = specification.ExtraDataInclusionSpec.IncludeExpressions.Aggregate(
                    queryable, 
                    (current, includeExpression) => current.Include(includeExpression)
                    );
            }

            if (specification.DataSortingSpec is not null)
            {
                foreach(var item in specification.DataSortingSpec.Specifications)
                {
                    switch (item.SortingDirection)
                    {
                        case ISortingOrder.Direction.Ascending:
                            queryable = queryable.OrderBy(item.SortByExpression);
                            break; 
                        case ISortingOrder.Direction.Descending:
                            queryable = queryable.OrderByDescending(item.SortByExpression);
                            break;
                        default:
                            Debug.Assert(false, $"Unexpected {nameof(ISortingOrder.Direction)} value!");
                            break;
                    }
                }
            }

            if (specification.DataPaginationSpec is not null)
            {
                queryable = queryable
                    .Skip(Convert.ToInt32(specification.DataPaginationSpec.PageNumber) - 1)
                    .Take(Convert.ToInt32(specification.DataPaginationSpec.PageSize));
            }

            if (specification.UseSplitQuery)
            {
                queryable = queryable.AsSplitQuery();
            }

            return queryable;
        }
    }
}
