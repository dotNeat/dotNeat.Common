namespace dotNeat.Common.DataAccess.Specification
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using static dotNeat.Common.DataAccess.Specification.ISortingOrder;

    public class SortingSpecification
    {
        public SortingSpecification(Direction sortingDirection = Direction.Ascending)
        {
            SortingDirection=sortingDirection;
        }

        public Direction SortingDirection { get; }
    }

    public class SortingSpecification<TEntity>
        : SortingSpecification
    {
        public SortingSpecification(
            Expression<Func<TEntity, object>> sortByExpression,
            Direction sortDirection = Direction.Ascending
            )
            : base(sortDirection)
        {
            SortByExpression = sortByExpression;
        }

        public Expression<Func<TEntity, object>> SortByExpression { get; }
    }

    public interface ISortingOrder
    {
        public enum Direction
        {
            Ascending,
            Descending
        }


        IReadOnlyCollection<SortingSpecification> Specifications { get; }
    }

    public interface ISortingOrder<TEntity>
        : ISortingOrder
    {

        new IReadOnlyCollection<SortingSpecification<TEntity>> Specifications { get; }

        ISortingOrder<TEntity> Add(SortingSpecification<TEntity> specification);
    }
}
