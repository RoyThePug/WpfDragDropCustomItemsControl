using System.Collections;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace WpfDragDropCustomItemsControlApp.Behavior;

public class BehaviorInStyleAttacher
{
    public static readonly DependencyProperty BehaviorsProperty =
        DependencyProperty.RegisterAttached(
            "Behaviors",
            typeof(IEnumerable),
            typeof(BehaviorInStyleAttacher),
            new UIPropertyMetadata(null, OnBehaviorsChanged));

    public static IEnumerable GetBehaviors(DependencyObject dependencyObject)
    {
        return (IEnumerable) dependencyObject.GetValue(BehaviorsProperty);
    }

    public static void SetBehaviors(
        DependencyObject dependencyObject, IEnumerable value)
    {
        dependencyObject.SetValue(BehaviorsProperty, value);
    }

    private static void OnBehaviorsChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is IEnumerable == false)
            return;

        var newBehaviorCollection = e.NewValue as IEnumerable;

        BehaviorCollection behaviorCollection = Interaction.GetBehaviors(depObj);
        behaviorCollection.Clear();
        
        if (newBehaviorCollection != null)
        {
            foreach (Microsoft.Xaml.Behaviors.Behavior behavior in newBehaviorCollection)
            {
                // need to make a copy of behavior in order to attach it to several controls
                var copy = behavior.Clone() as Microsoft.Xaml.Behaviors.Behavior;
                behaviorCollection.Add(copy);
            }
        }
    }
}