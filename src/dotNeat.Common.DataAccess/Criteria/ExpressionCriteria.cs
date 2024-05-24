namespace dotNeat.Common.DataAccess.Criteria
{
    using System;
    using System.Diagnostics;

    public class ExpressionCriteria<TEntity> 
        : CompositeCriteria<TEntity>
    {
        private readonly Func<TEntity, bool> _expression;

        public ExpressionCriteria(
            Func<TEntity, bool> expression 
            )
        {
            Debug.Assert(expression != null);

            this._expression = expression;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return this._expression(entity);
        }
    }
}
