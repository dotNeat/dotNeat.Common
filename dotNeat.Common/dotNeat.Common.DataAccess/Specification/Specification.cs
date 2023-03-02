namespace dotNeat.Common.DataAccess.Specification
{

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using dotNeat.Common.DataAccess.Criteria;

    using static dotNeat.Common.DataAccess.Specification.ISortingOrder;

    public class Specification<TEntity>
        : ISpecification<TEntity>
    {

        protected Specification()
            : this(null,null,null,null) 
        { 
        }
        
        protected Specification(
            ICriteria<TEntity>? filterCriteria,
            ISortingOrder<TEntity>? sortingOrder,
            IExtraDataInclusion<TEntity>? inclusionSpec,
            IPagination? pagination,
            ISpecification.Outcome expectedOutcome = ISpecification.Outcome.Undetermined,
            bool useSplitQuery = false
            )
        {
            DataFilterSpec = filterCriteria;
            DataPaginationSpec = pagination;
            DataSortingSpec = sortingOrder;
            ExtraDataInclusionSpec = inclusionSpec;
            ExpectedOutcome = expectedOutcome;
            UseSplitQuery = useSplitQuery;
        }

        #region read-only properties

        public ISpecification.Outcome ExpectedOutcome { get; private set; }
        public bool UseSplitQuery { get; private set; } = false;
        public ICriteria<TEntity>? DataFilterSpec { get; private set; }
        public ISortingOrder<TEntity>? DataSortingSpec { get; private set; }
        public IExtraDataInclusion<TEntity>? ExtraDataInclusionSpec { get; private set; }
        public IPagination? DataPaginationSpec { get; private set; }

        #endregion read-only 

        #region fluent property setters

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

        public Specification<TEntity> SetDataFilterSpec(ICriteria<TEntity>? criteria)
        {
            DataFilterSpec= criteria;
            return this;
        }

        public Specification<TEntity> SetDataSortingSpec(ISortingOrder<TEntity>? sortingOrder)
        {
            DataSortingSpec = sortingOrder;
            return this;
        }

        public Specification<TEntity> SetExtraDataInclusionSpec(IExtraDataInclusion<TEntity>? inclusionSpec)
        {
            ExtraDataInclusionSpec = inclusionSpec;
            return this;
        }

        public Specification<TEntity> SetDataPaginationSpec(IPagination? pagination)
        {
            DataPaginationSpec = pagination;
            return this;
        }

        #endregion fluent property setters

        #region fluent builder API

        public static Specification<TEntity> Create() 
        { 
            return new Specification<TEntity>(); 
        }

        public Specification<TEntity> WithExpectedOutcome(ISpecification.Outcome expectedOutcome) 
        {
            return this.SetExpectedOutcome(expectedOutcome);
        }

        public Specification<TEntity> WithUseSplitQuery(bool flag)
        {
            return this.SetUseSplitQuery(flag);
        }

        public Specification<TEntity> WithDataFilterSpec(ICriteria<TEntity> criteria)
        {
            return this.SetDataFilterSpec(criteria);
        }

        public Specification<TEntity> WithDataFilterSpec(Func<TEntity, bool> criteria)
        {
            return this.WithDataFilterSpec(new ExpressionCriteria<TEntity>(criteria));
        }

        public Specification<TEntity> WithDataSortingSpec(ISortingOrder<TEntity> sortingOrder)
        {
            return this.SetDataSortingSpec(sortingOrder);
        }

        public Specification<TEntity> WithDataSortingSpec(SortingSpecification<TEntity>[] sortingSpecifications)
        {
            SortingOrder<TEntity> sortingOrder = new SortingOrder<TEntity>(sortingSpecifications);
            return this.WithDataSortingSpec(sortingOrder);
        }

        public Specification<TEntity> WithDataSortingSpec(
            Expression<Func<TEntity, object>> sortByExpression,
            Direction sortDirection = Direction.Ascending
            )
        {
            return this.WithDataSortingSpec(
                new[] { 
                    new SortingSpecification<TEntity>(sortByExpression, sortDirection) 
                });
        }

        public Specification<TEntity> WithExtraDataInclusionSpec(IExtraDataInclusion<TEntity> spec)
        {
            return this.SetExtraDataInclusionSpec(spec);
        }

        public Specification<TEntity> WithExtraDataInclusionSpec(Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var spec = new ExtraDataInclusion<TEntity>(includeExpressions);
            return this.WithExtraDataInclusionSpec(spec);
        }

        public Specification<TEntity> WithExtraDataInclusionSpec(Expression<Func<TEntity, object>> includeExpression)
        {
            var spec = new ExtraDataInclusion<TEntity>(new[] { includeExpression });
            return this.WithExtraDataInclusionSpec(spec);
        }

        public Specification<TEntity> WithDataPaginationSpec(IPagination spec)
        {
            return this.SetDataPaginationSpec(spec);
        }

        public Specification<TEntity> WithDataPaginationSpec(ulong pageNumber, ulong pageSize)
        {
            return this.SetDataPaginationSpec(new Pagination(pageNumber: pageNumber, pageSize: pageSize));
        }

        #endregion fluent builder API 
    }
}
