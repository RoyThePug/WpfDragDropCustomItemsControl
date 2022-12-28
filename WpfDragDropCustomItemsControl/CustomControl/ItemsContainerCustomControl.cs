using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfDragDropCustomItemsControlApp.CustomControl;

public class ItemsContainerCustomControl : Control
{
    public ObservableCollection<ItemCustomControl> Items { get; }

    #region Dependency Property

    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsContainerCustomControl), new PropertyMetadata(default(IEnumerable), OnItemsSourceChanged));

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ItemsContainerCustomControl control)
        {
            if (e.NewValue is INotifyCollectionChanged collection)
            {
                control.Subscribe(collection);
            }

            var source = (IList) e.NewValue;

            foreach (var data in source)
            {
                control.Items.Add(new ItemCustomControl() {DataContext = data});
            }
        }
    }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable) GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    #endregion

    #region Constructor

    static ItemsContainerCustomControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsContainerCustomControl), new FrameworkPropertyMetadata(typeof(ItemsContainerCustomControl)));
    }

    public ItemsContainerCustomControl()
    {
        Items = new ObservableCollection<ItemCustomControl>();
    }

    #endregion

    #region Method

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    private void Subscribe(INotifyCollectionChanged collection)
    {
        collection.CollectionChanged += ColumnsOnCollectionChanged;
    }

    #endregion

    #region Event Handler

    private void ColumnsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            if (e.NewItems != null && e.NewItems[0] is object dataContext)
            {
                Items.Add(new ItemCustomControl()
                {
                    DataContext = dataContext
                });
            }
        }

        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (e.OldItems != null && e.OldItems[0] is object dataContext)
            {
                var itemVisual = Items.FirstOrDefault(x => x.DataContext.Equals(dataContext));

                if (itemVisual != null)
                {
                    Items.Remove(itemVisual);
                }
            }
        }
    }

    #endregion
}