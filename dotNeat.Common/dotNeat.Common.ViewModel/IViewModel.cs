namespace dotNeat.Common.ViewModel
{
    using System.ComponentModel;


    public interface IViewModel
        : INotifyPropertyChanging
        , INotifyPropertyChanged
    {
        public string ViewModelName { get; }
    }
}
