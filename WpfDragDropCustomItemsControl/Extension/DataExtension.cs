using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfDragDropCustomItemsControlApp.Extension;

public static class DataExtension
{
    public static int RemoveAll<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
    {
        var itemsToRemove = coll.Where(condition).ToList();

        foreach (var itemToRemove in itemsToRemove)
        {
            coll.Remove(itemToRemove);
        }

        return itemsToRemove.Count;
    }
}