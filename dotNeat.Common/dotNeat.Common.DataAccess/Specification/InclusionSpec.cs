namespace dotNeat.Common.DataAccess.Specification
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class InclusionSpec<TEntity>
        : IInclusionSpec<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _expressions = 
            new List<Expression<Func<TEntity, object>>>();

        public InclusionSpec(
            IEnumerable<Expression<Func<TEntity, object>>>? includeExpressions = null
            )
        {
            if (includeExpressions is not null)
            {
                _expressions.AddRange(includeExpressions);
            }
        }

        public IReadOnlyCollection<Expression<Func<TEntity, object>>> IncludeExpressions 
        {
            get { return _expressions; }
        }

        public InclusionSpec<TEntity> AddIncludeExpression(Expression<Func<TEntity, object>> includeExpression)
        {
            _expressions.Add(includeExpression);
            return this;
        }
    }
}
