namespace dotNeat.Common.Patterns.EventsPattern
{
    using System;

    public class DataChangeEventArgs<T> : EventArgs
    {
        private readonly T _oldData;
        private readonly T _newData;

        private DataChangeEventArgs()
        {
        }

        public DataChangeEventArgs(T oldDataValue, T newDataValue)
        {
            _oldData = oldDataValue;
            _newData = newDataValue;
        }

        public T OldData
        {
            get { return _oldData; }
        }

        public T NewData
        {
            get { return _newData; }
        }
    }
}
