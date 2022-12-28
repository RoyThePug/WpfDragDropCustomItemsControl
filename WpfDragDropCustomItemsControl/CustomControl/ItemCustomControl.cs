using System.Windows;
using System.Windows.Controls;

namespace WpfDragDropCustomItemsControlApp.CustomControl;

public class ItemCustomControl : Control
{
    #region Dependency Property

    public static readonly DependencyProperty CustomNameProperty = DependencyProperty.Register(
        nameof(CustomName), typeof(string), typeof(ItemCustomControl), new PropertyMetadata(default(string)));

    public string CustomName
    {
        get => (string) GetValue(CustomNameProperty);
        set => SetValue(CustomNameProperty, value);
    }
    
    #endregion
    
    static ItemCustomControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemCustomControl), new FrameworkPropertyMetadata(typeof(ItemCustomControl)));
    }
}