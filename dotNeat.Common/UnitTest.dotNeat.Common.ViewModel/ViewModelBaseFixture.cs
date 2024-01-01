using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotNeat.Common.ViewModel;
using UnitTest.dotNeat.Common.ViewModel.Mocks;

namespace UnitTest.dotNeat.Common.ViewModel
{
    [TestClass()]
    public class ViewModelBaseFixture
    {
        private int _viewModelNameChangingCount = 0;
        private int _valuePropertyChangingCount = 0;
        private int _viewModelNameChangedCount = 0;
        private int _valuePropertyChangedCount = 0;

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModelTestMock.ViewModelName):
                    _viewModelNameChangedCount++;
                    break;
                case nameof(ViewModelTestMock.ValueProperty):
                    _valuePropertyChangedCount++;
                    break;

            }
        }

        private void ViewModel_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModelTestMock.ViewModelName):
                    _viewModelNameChangingCount++;
                    break;
                case nameof(ViewModelTestMock.ValueProperty):
                    _valuePropertyChangingCount++;
                    break;

            }
        }


        [TestInitialize]
        public void TestInit()
        {
            _viewModelNameChangingCount = 0;
            _valuePropertyChangingCount = 0;
            _viewModelNameChangedCount = 0;
            _valuePropertyChangedCount = 0;
        }

    [TestMethod()]
        public void DefaultViewModelNameTest()
        {
            var viewModel = new ViewModelTestMock();
            Assert.IsNotNull(viewModel.ViewModelName);
            Assert.AreEqual(typeof(ViewModelTestMock).FullName, viewModel.ViewModelName);
        }

        [TestMethod()]
        public void ReferenceTypePropertyTest()
        {
            var viewModel = new ViewModelTestMock();
            viewModel.PropertyChanging += ViewModel_PropertyChanging;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;

            //default value test
            Assert.IsNotNull(viewModel.ViewModelName);
            Assert.AreEqual(typeof(ViewModelTestMock).FullName, viewModel.ViewModelName);

            // newly assigned value test
            Assert.AreEqual(0, _viewModelNameChangingCount);
            Assert.AreEqual(0, _viewModelNameChangedCount);
            Assert.AreEqual(0, _valuePropertyChangingCount);
            Assert.AreEqual(0, _valuePropertyChangedCount);
            const string newViewModelName = "New Name";
            viewModel.ViewModelName = newViewModelName;
            Assert.IsNotNull(viewModel.ViewModelName);
            Assert.AreEqual(newViewModelName, viewModel.ViewModelName);
            Assert.AreEqual(1, _viewModelNameChangingCount);
            Assert.AreEqual(1, _viewModelNameChangedCount);
            Assert.AreEqual(0, _valuePropertyChangingCount);
            Assert.AreEqual(0, _valuePropertyChangedCount);

            viewModel.PropertyChanging -= ViewModel_PropertyChanging;
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        [TestMethod()]
        public void ValueTypePropertyTest()
        {
            var viewModel = new ViewModelTestMock();
            viewModel.PropertyChanging += ViewModel_PropertyChanging;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;

            //default value test
            Assert.IsNotNull(viewModel.ValueProperty);
            Assert.AreEqual(default(int), viewModel.ValueProperty);

            // newly assigned value test
            Assert.AreEqual(0, _viewModelNameChangingCount);
            Assert.AreEqual(0, _viewModelNameChangedCount);
            Assert.AreEqual(0, _valuePropertyChangingCount);
            Assert.AreEqual(0, _valuePropertyChangedCount);
            const int newViewModelValue = 15;
            viewModel.ValueProperty = newViewModelValue;
            Assert.IsNotNull(viewModel.ValueProperty);
            Assert.AreEqual(newViewModelValue, viewModel.ValueProperty);
            Assert.AreEqual(0, _viewModelNameChangingCount);
            Assert.AreEqual(0, _viewModelNameChangedCount);
            Assert.AreEqual(1, _valuePropertyChangingCount);
            Assert.AreEqual(1, _valuePropertyChangedCount);

            viewModel.PropertyChanging -= ViewModel_PropertyChanging;
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }
}