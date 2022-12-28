using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfDragDropCustomItemsControlApp.CustomAdorner;

public class TemplatedAdorner : Adorner
{
    #region Property

    private ContentPresenter ContentPresenter { get; set; }

    #endregion

    public TemplatedAdorner(UIElement adornedElement, DataTemplate contentDataTemplate, object content) : base(adornedElement)
    {
        this.ContentPresenter =
            new ContentPresenter
            {
                ContentTemplate = contentDataTemplate,
                Content = content,
                DataContext = content
            };

        var adornerLayer = AdornerLayer.GetAdornerLayer(this.AdornedElement);
      
        if (adornerLayer != null)
        {
            adornerLayer.Add(this);
        }
    }
    
    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int index)
    {
        return this.ContentPresenter;
    }

    protected override Size MeasureOverride(Size constraint)
    {
        return this.AdornedElement.RenderSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        ContentPresenter.Arrange(new Rect(new Point(0, 0), finalSize));
        return finalSize;
    }
}