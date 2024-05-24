namespace dotNeat.Common.Patterns
{
    using System;

    public class EntityBase<TID>
        : IEntity<TID>
        , IEntity
        where TID : IComparable//, IEquatable<TID>
    {
        private readonly TID _ID;

        protected EntityBase(TID id)
        {
            this._ID = id;
        }

        public TID ID => this._ID;

        object IEntity.ID => this._ID;
    }
}
