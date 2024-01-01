using dotNeat.Common.ViewModel;

namespace UnitTest.dotNeat.Common.ViewModel.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ViewModelTestMock
        : ViewModelBase
    {
        private int _valueProperty;
        public int ValueProperty
        {
            get => GetProperty(this._valueProperty);
            set => SetProperty(ref this._valueProperty, value);
        }
    }
}
