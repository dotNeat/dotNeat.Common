namespace dotNeat.Common.DataAccess.Specification
{
    using dotNeat.Common.DataAccess.Criteria;

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class Specification<TEntity>
        : ISpecification<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeExpressions = new();
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
            IInclusionSpec<TEntity>? inclusionSpec,
            IPagination? pagination,
            ISpecification.Outcome expectedOutcome = ISpecification.Outcome.Undetermined,
            bool useSplitQuery = false
            )
        {
            FilterCriteria = filterCriteria;
            Pagination = pagination;
            SortingOrder = sortingOrder;
            InclusionSpec = inclusionSpec;
            ExpectedOutcome = expectedOutcome;
            UseSplitQuery = useSplitQuery;
        }

        public ISpecification.Outcome ExpectedOutcome { get; private set; }
        public bool UseSplitQuery { get; private set; }
        public ICriteria<TEntity>? FilterCriteria { get; private set; }
        public ISortingOrder<TEntity>? SortingOrder { get; private set; }
        public IInclusionSpec<TEntity>? InclusionSpec { get; private set; }
        public IPagination? Pagination { get; private set; }


        public Specification<TEntity> SetFilterCriteria(ICriteria<TEntity>? criteria)
        {
            FilterCriteria= criteria;
            return this;
        }

        public Specification<TEntity> SetSortingOrder(ISortingOrder<TEntity>? sortingOrder)
        {
            SortingOrder = sortingOrder;
            return this;
        }

        public Specification<TEntity> SetInclusionSpec(IInclusionSpec<TEntity>? inclusionSpec)
        {
            InclusionSpec = inclusionSpec;
            return this;
        }

        public Specification<TEntity> SetPagination(IPagination? pagination)
        {
            Pagination= pagination;
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
