namespace dotNeat.Common.Patterns
{
    using System;

    public interface IEntity
    {
        object ID { get; }
    }

    public interface IEntity<TID>
        : IEntity
        where TID : IComparable //IEquatable<TID>
    {
        new TID ID { get; }
    }

    public class EntityBase<TID>
        : IEntity<TID>, IEntity
        where TID : IComparable //IEquatable<TID>
    {
        private readonly TID _ID;

        private EntityBase()
        {
        }

        protected EntityBase(TID id)
        {
            this._ID = id;
        }

        public TID ID => this._ID;

        object IEntity.ID => this._ID;
    }
}
