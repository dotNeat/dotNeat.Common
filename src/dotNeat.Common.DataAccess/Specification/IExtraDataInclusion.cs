namespace dotNeat.Common.DataAccess.Specification
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IExtraDataInclusion<TEntity>
    {
        IReadOnlyCollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    }
}
