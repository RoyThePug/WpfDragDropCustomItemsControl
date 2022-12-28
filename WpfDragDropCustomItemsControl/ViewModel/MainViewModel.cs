using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDragDropCustomItemsControlApp.Model;

namespace WpfDragDropCustomItemsControlApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<TestModel> Data { get; set; }

        public MainViewModel()
        {
            Data = new ObservableCollection<TestModel>();
            // Data = new ObservableCollection<TestModel>
            // {
            //     new TestModel() {Name = "First Test"},
            //     new TestModel() {Name = "Second Test"},
            //     new TestModel() {Name = "Third Test"},
            //     new TestModel() {Name = "Fourth Test"},
            //     new TestModel() {Name = "Fifth Test"},
            // };
        }

        #region Command

        private bool CanInitExecute() => Data.Count == 0;

        [RelayCommand(CanExecute = nameof(CanInitExecute))]
        async Task InitTestData()
        {
            Data.Clear();

            Data.Add(new TestModel() {Name = "First Test"});
            Data.Add(new TestModel() {Name = "Second Test"});
            Data.Add(new TestModel() {Name = "Third Test"});
            Data.Add(new TestModel() {Name = "Fourth Test"});
            Data.Add(new TestModel() {Name = "Fifth Test"});

            removeTestModelCommand?.NotifyCanExecuteChanged();
        }

        private bool CanRemoveExecute() => Data.Count > 0;

        [RelayCommand(CanExecute = nameof(CanRemoveExecute))]
        async Task RemoveTestModel()
        {
            var item = Data.Last();

            Data.Remove(item);

            if (!Data.Any())
            {
                initTestDataCommand?.NotifyCanExecuteChanged();
            }
        }

        #endregion
    }
}