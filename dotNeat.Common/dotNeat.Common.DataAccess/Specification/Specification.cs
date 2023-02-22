namespace dotNeat.Common.DataAccess.Specification
{
    using dotNeat.Common.DataAccess.Criteria;

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class Specification<TEntity>
        : ISpecification<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeExpressions;
        private Expression<Func<TEntity, bool>>? _filterExpression;
        private Expression<Func<TEntity, object>>? _orderByExpression;
        private Expression<Func<TEntity, object>>? _orderByDescendingExpression;

        public Specification()
            : this(null,null,null,null) 
        { 
        }
        
        public Specification(
            ICriteria<TEntity>? filterCriteria,
            ISortingOrder<TEntity>? sortingOrder,
            IExtraDataInclusion<TEntity>? inclusionSpec,
            IPagination? pagination,
            ISpecification.Outcome expectedOutcome = ISpecification.Outcome.Undetermined,
            bool useSplitQuery = false
            )
        {
            DataFilterSpec = filterCriteria;
            PaginationSpec = pagination;
            DataSortingSpec = sortingOrder;
            ExtraDataInclusionSpec = inclusionSpec;
            ExpectedOutcome = expectedOutcome;
            UseSplitQuery = useSplitQuery;
        }

        public ISpecification.Outcome ExpectedOutcome { get; private set; }
        public bool UseSplitQuery { get; private set; }
        public ICriteria<TEntity>? DataFilterSpec { get; private set; }
        public ISortingOrder<TEntity>? DataSortingSpec { get; private set; }
        public IExtraDataInclusion<TEntity>? ExtraDataInclusionSpec { get; private set; }
        public IPagination? PaginationSpec { get; private set; }

        public Specification<TEntity> SetFilterCriteria(ICriteria<TEntity>? criteria)
        {
            DataFilterSpec= criteria;
            return this;
        }

        public Specification<TEntity> SetSortingOrder(ISortingOrder<TEntity>? sortingOrder)
        {
            DataSortingSpec = sortingOrder;
            return this;
        }

        public Specification<TEntity> SetInclusionSpec(IExtraDataInclusion<TEntity>? inclusionSpec)
        {
            ExtraDataInclusionSpec = inclusionSpec;
            return this;
        }

        public Specification<TEntity> SetPagination(IPagination? pagination)
        {
            PaginationSpec= pagination;
            return this;
        }

        public Specification<TEntity> SetExpectedOutcome(ISpecification.Outcome expectedOutcome)
        {
            ExpectedOutcome = expectedOutcome;
            return this;
        }

        public Specification<TEntity> SetUseSplitQuery(bool useSplitQuery)
        {
            UseSplitQuery = useSplitQuery;
            return this;
        }
    }
}
