﻿namespace dotNeat.Common.Patterns.EventsPattern
{
    using System;

    public class DataEventArgs<T> 
        : EventArgs
    {
        private readonly T? _data;

        public DataEventArgs(T? data)
        {
            _data = data;
        }

        public T? Data
        {
            get { return _data; }
        }
    }
}
