using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfDragDropCustomItemsControlApp.Model
{
    public partial class TestModel : ObservableObject
    {
        [ObservableProperty] string name;
    }
}