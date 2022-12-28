using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfDragDropCustomItemsControlApp.CustomControl;
using Microsoft.Xaml.Behaviors;
using WpfDragDropCustomItemsControlApp.CustomAdorner;

namespace WpfDragDropCustomItemsControlApp.Behavior;

public class EmptyItemsControlAdornerBehavior : Behavior<ItemsControl>
{
    #region Field

    private TemplatedAdorner _emptyItemsAdorner { get; set; }

    #endregion

    #region Dependency Property

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        nameof(Data), typeof(object), typeof(EmptyItemsControlAdornerBehavior), new PropertyMetadata(default(object)));

    public object Data
    {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
        nameof(DataTemplate), typeof(DataTemplate), typeof(EmptyItemsControlAdornerBehavior), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate DataTemplate
    {
        get => (DataTemplate) GetValue(DataTemplateProperty);
        set => SetValue(DataTemplateProperty, value);
    }

    #endregion

    protected override void OnAttached()
    {
        base.OnAttached();

        _emptyItemsAdorner = new TemplatedAdorner(AssociatedObject, this.DataTemplate, this.Data);

        var collectionViewSource = CollectionViewSource.GetDefaultView(this.AssociatedObject.Items);
        if (collectionViewSource != null)
        {
            collectionViewSource.CollectionChanged += ItemsChanged;
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        var collectionViewSource = CollectionViewSource.GetDefaultView(this.AssociatedObject.ItemsSource);
        if (collectionViewSource != null)
        {
            collectionViewSource.CollectionChanged -= ItemsChanged;
        }
    }

    private void ItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _emptyItemsAdorner.Visibility = AssociatedObject.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}