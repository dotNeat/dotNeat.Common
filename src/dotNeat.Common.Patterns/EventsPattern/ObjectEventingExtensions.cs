using System;

namespace dotNeat.Common.Patterns.EventsPattern
{
    public static class ObjectEventingExtensions
    {
        public static void RaiseDataChangeEventHandler<T>(this object? obj, EventHandler<DataChangeEventArgs<T>>? eventHandler, DataChangeEventArgs<T> eventArgs)
        {
            EventHandler<DataChangeEventArgs<T>>? eh = eventHandler;
            if (eh != null)
            {
                eh(obj, eventArgs);
            }
        }
    }
}
