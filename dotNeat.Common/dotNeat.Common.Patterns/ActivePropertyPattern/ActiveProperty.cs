namespace dotNeat.Common.Patterns.ActivePropertyPattern
{
    using dotNeat.Common.Patterns.EventsPattern;

    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public class ActiveProperty<T> 
        : IActiveProperty<T>
    {
        private readonly string _name;
        private T? _property;
        private readonly bool _shouldNotifySameValueUpdate;
        private readonly object _propertyHost;

        public ActiveProperty(
            object propertyHost, 
            string propertyName
            )
            : this(propertyHost, propertyName, false)
        {
        }

        public ActiveProperty(
            object propertyHost, 
            string propertyName, 
            bool shouldNotifySameValueUpdate
            )
        {
            Debug.Assert(propertyHost != null, "uninitialized argument: propertyHost!");
            Debug.Assert(!string.IsNullOrWhiteSpace(propertyName), "invalid propertyName value!");

            _propertyHost = propertyHost;
            _name = propertyName;
            _shouldNotifySameValueUpdate = shouldNotifySameValueUpdate;
        }

        protected virtual void OnPropertyValueChanging(
            DataChangeEventArgs<T?> propertyChangeEventArgs
            )
        {
            this.ValueChanging?.Raise(this, propertyChangeEventArgs);
            this.PropertyChanging?.Raise(this._propertyHost, this._name);
        }

        protected virtual void OnPropertyValueChanged(
            DataChangeEventArgs<T?> propertyChangeEventArgs
            )
        {
            this.ValueChanged?.Raise(this._propertyHost, propertyChangeEventArgs);
            this.PropertyChanged?.Raise(this, this._name);
        }

        #region IActiveProperty<TProperty> implicit

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _name;

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>
        /// The property value.
        /// </value>
        public T? Property
        {
            get => _property;
            set
            {
                if (!_shouldNotifySameValueUpdate)
                {
                    if (_property == null && value == null)
                        return;
                    if (_property != null && _property.Equals(value))
                        return;
                }
                DataChangeEventArgs<T?> ea = new(
                    _property, 
                    value
                    );
                OnPropertyValueChanging(ea);
                _property = value;
                OnPropertyValueChanged(ea);
            }
        }

        /// <summary>
        /// Occurs when property value changing.
        /// </summary>
        public event EventHandler<DataChangeEventArgs<T?>>? ValueChanging;

        /// <summary>
        /// Occurs when property value changed.
        /// </summary>
        public event EventHandler<DataChangeEventArgs<T?>>? ValueChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// Occurs when a property value changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion IActiveProperty<TProperty> implicit

        #region IActiveProperty<TProperty> explicit

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string IActiveProperty<T>.Name => this.Name;

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// <value>
        /// The property value.
        /// </value>
        T? IActiveProperty<T>.Value
        {
            get => this.Property;
            set => this.Property = value;
        }

        /// <summary>
        /// Occurs when property value changing.
        /// </summary>
        event EventHandler<DataChangeEventArgs<T?>>? IActiveProperty<T>.ValueChanging
        {
            add => this.ValueChanging += value;
            remove => this.ValueChanging -= value;
        }

        /// <summary>
        /// Occurs when property value changed.
        /// </summary>
        event EventHandler<DataChangeEventArgs<T?>>? IActiveProperty<T>.ValueChanged
        {
            add => this.ValueChanged += value;
            remove => this.ValueChanged -= value;
        }

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        event PropertyChangingEventHandler? INotifyPropertyChanging.PropertyChanging
        {
            add => this.PropertyChanging += value;
            remove => this.PropertyChanging -= value;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add => this.PropertyChanged += value;
            remove => this.PropertyChanged -= value;
        }

        #endregion IActiveProperty<TProperty> explicit

    }
}
