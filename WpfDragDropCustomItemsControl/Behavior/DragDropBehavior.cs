using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using WpfDragDropCustomItemsControlApp.Extension;
using WpfDragDropCustomItemsControlApp.CustomControl;

namespace WpfDragDropCustomItemsControlApp.Behavior;

public class DragDropBehavior : Behavior<ItemsContainerCustomControl>
{
    private AdornerLayer layer;
    private ItemCustomControl _currentControl;
    private int _insertIndex = -1;

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.MouseMove += AssociatedObjectOnMouseMoveHandler;
        AssociatedObject.DragOver += AssociatedObjectOnDragOverHandler;
        AssociatedObject.Drop += AssociatedObjectOnDropHandler;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        AssociatedObject.MouseMove -= AssociatedObjectOnMouseMoveHandler;
        AssociatedObject.DragOver -= AssociatedObjectOnDragOverHandler;
        AssociatedObject.Drop -= AssociatedObjectOnDropHandler;
    }

    private void AssociatedObjectOnDragOverHandler(object sender, DragEventArgs e)
    {
        var currentContext = ((FrameworkElement) e.OriginalSource).DataContext;

        var data = e.Data.GetData(DataFormats.Serializable);

        if (currentContext != null && data != null)
        {
            if (!data.Equals(currentContext))
            {
                _currentControl = AssociatedObject.Items.FirstOrDefault(x => x.DataContext.Equals(currentContext));

                if (_currentControl != null)
                {
                    _insertIndex = AssociatedObject.Items.IndexOf(_currentControl);

                    AssociatedObject.Items.RemoveAll(x => x.DataContext.Equals(data));
                    
                    AssociatedObject.Items.Insert(_insertIndex, new ItemCustomControl() {DataContext = data, Opacity = 0.5});
                }
            }
        }
    }

    private void AssociatedObjectOnMouseMoveHandler(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && (!(e.OriginalSource is Thumb)))
        {
            var data = new DataObject();
            data.SetData(DataFormats.Serializable, ((FrameworkElement) e.OriginalSource).DataContext);
            var result = DragDrop.DoDragDrop((FrameworkElement) e.OriginalSource, data, DragDropEffects.Move);

            if (result == DragDropEffects.None)
            {
                var proxyItem = AssociatedObject.Items.FirstOrDefault(x => x.Opacity < 1.0);

                if (proxyItem != null)
                {
                    AssociatedObject.Items.Remove(proxyItem);
                    AssociatedObject.Items.Insert(_insertIndex, new ItemCustomControl() {DataContext = proxyItem.DataContext});
                }
            }
        }
    }

    private void AssociatedObjectOnDropHandler(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.Serializable) is object data)
        {
            AssociatedObject.Items.RemoveAll(x => x.DataContext.Equals(data));
            
            AssociatedObject.Items.Insert(_insertIndex, new ItemCustomControl() {DataContext = data});
        }
    }
}