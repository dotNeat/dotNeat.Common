namespace dotNeat.Common.Patterns.ClassificationPattern
{
    using System.Collections.Generic;

    public interface IIdentifiable
    {
        object ID { get; }
        IEnumerable<object> GetAllKnownIDs();
    }

    public interface IIdentifiable<out TID>
        : IIdentifiable
    {
        new TID ID { get; }

        new IEnumerable<TID> GetAllKnownIDs();
    }
}