namespace dotNeat.Common.Patterns
{
    using System;

    public interface IEntity
    {
        object ID { get; }
    }

    public interface IEntity<TID>
        : IEntity
        where TID : IComparable//, IEquatable<TID>
    {
        new TID ID { get; }
    }

}
