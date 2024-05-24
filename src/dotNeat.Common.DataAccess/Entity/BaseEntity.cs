namespace dotNeat.Common.DataAccess.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEntity<TEntityId>
        : IEntity<TEntityId>
        where TEntityId : IEquatable<TEntityId>, IComparable
    {

        protected BaseEntity(TEntityId id)
        {
            ID = id;
        }


        [Key]
        public TEntityId ID { get; set; }

        [NotMapped]
        object IEntity.ID
        {
            get { return ID; }
            set { ID = (TEntityId)value; }
        }

        [NotMapped]
        TEntityId IEntity<TEntityId>.ID 
        { 
            get { return ID; }
            set { ID = value; }
        }
    }
}
