namespace dotNeat.Common.DataAccess.Criteria
{
    using System;
    using System.Diagnostics;

    public class ExpressionCriteria<TEntity> 
        : CompositeCriteria<TEntity>
    {
        private readonly Func<TEntity, bool> expression;

        public ExpressionCriteria(
            Func<TEntity, bool> expression 
            )
        {
            Debug.Assert(expression != null);

            this.expression = expression;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return expression(entity);
        }
    }
}
