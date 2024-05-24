namespace dotNeat.Common.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ViewModelBase
        : IViewModel
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _name;

        protected ViewModelBase(string? name = null)
        {
            _name = name ?? this.GetType().FullName ?? this.GetType().Name;
        }

        public string ViewModelName
        {
            get => GetProperty(this._name);
            set => SetProperty( ref this._name, value);
        }

        protected T GetProperty<T>(T dataField)
        {
            return dataField;
        }

        protected void SetProperty<T>(
            ref T dataField, 
            T value, 
            [CallerMemberName] string? propertyName = null
            )
        //where T : struct
        {
            if (object.ReferenceEquals(dataField, value) || (dataField != null && dataField.Equals(value)))
            {
                return;
            }

            OnPropertyChanging(propertyName);
            dataField = value;
            OnPropertyChanged(propertyName);
        }

        //protected void SetProperty<T>(T dataField, T value, [CallerMemberName] string? propertyName = null)
        //    where T : class
        //{
        //    if (dataField == value)
        //    {
        //        return;
        //    }

        //    OnPropertyChanging(propertyName);
        //    dataField = value;
        //    OnPropertyChanged(propertyName);
        //}

        protected void OnPropertyChanging([CallerMemberName] string? propertyName = null) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
