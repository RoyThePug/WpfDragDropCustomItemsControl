using System;
using System.Collections.Generic;
using WpfDragDropCustomItemsControlApp.Model;

namespace WpfDragDropCustomItemsControlApp.Extension;

public class TestModelComparer : IEqualityComparer<TestModel>
{
    public bool Equals(TestModel x, TestModel y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(TestModel obj)
    {
        return (obj.Name != null ? obj.Name.GetHashCode() : 0);
    }
    
    public static IEqualityComparer<TestModel> Comparer { get; } = new TestModelComparer();
}