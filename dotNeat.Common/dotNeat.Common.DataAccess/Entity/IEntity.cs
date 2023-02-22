namespace dotNeat.Common.DataAccess.Entity
{
    using System;

    public interface IEntity
    {
        object ID { get; set; }
    }

    public interface IEntity<TEntityId> 
        : IEntity
        where TEntityId : IEquatable<TEntityId>, IComparable
    {
        new TEntityId ID { get; set; }
    }
}
