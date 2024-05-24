namespace dotNeat.Common.Patterns.CapabilitiesPattern
{
    using System;

    using dotNeat.Common.Patterns.EventsPattern;

    public interface ICapability
    {
        bool IsAvailable { get; }
        event EventHandler<DataChangeEventArgs<bool>>? IsAvailableChanging;
        event EventHandler<DataChangeEventArgs<bool>>? IsAvailableChanged;
    }
}
