namespace dotNeat.Common.DataAccess.Specification
{
    using System.Collections.Generic;

    public class SortingOrder<TEntity>
        : ISortingOrder<TEntity>
    {
        private readonly List<SortingSpecification<TEntity>> _specifications = 
            new List<SortingSpecification<TEntity>>();

        public SortingOrder() 
        { 
        }

        public ISortingOrder<TEntity> Add(
            SortingSpecification<TEntity> specification
            )
        {
            _specifications.Add( specification );
            return this;
        }

        public IReadOnlyCollection<SortingSpecification<TEntity>> Specifications 
        {
            get { return _specifications;  }
        }

        IReadOnlyCollection<SortingSpecification> ISortingOrder.Specifications 
        {
            get { return _specifications; }
        }
    }
}
