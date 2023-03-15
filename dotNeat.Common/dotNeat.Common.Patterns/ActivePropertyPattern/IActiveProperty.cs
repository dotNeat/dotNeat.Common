namespace dotNeat.Common.Patterns.ActivePropertyPattern
{
    using dotNeat.Common.Patterns.EventsPattern;
    using System.ComponentModel;


    using System;

    /// <summary>
    /// Defines an active property interface.
    /// </summary>
    /// <remarks>
    /// A property is considered to be an active one if it is capable of notifying about its value changes.
    /// </remarks> 
    /// <typeparam name="T">The type of the property.</typeparam>
    public interface IActiveProperty<T> 
        : INotifyPropertyChanging
        , INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>
        /// The property value.
        /// </value>
        T? Value { get; set; }
        /// <summary>
        /// Occurs when property value changing.
        /// </summary>
        event EventHandler<DataChangeEventArgs<T?>>? ValueChanging;
        /// <summary>
        /// Occurs when property value changed.
        /// </summary>
        event EventHandler<DataChangeEventArgs<T?>>? ValueChanged;
    }
}
