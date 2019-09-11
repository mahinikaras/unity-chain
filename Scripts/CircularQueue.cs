using System.Collections.Generic;

/// <summary>
/// A circular queue that doesn't delete its elements.
/// </summary>
public class CircularQueue <T>
{
    List<T> list;
    int listPointer = 0;

    public CircularQueue()
    {
        list = new List<T>();
    }

    public T Next()
    {
        listPointer--;
        if (listPointer < 0)
            listPointer = list.Count - 1;
        return list[listPointer];
    }

    public void Add(T obj)
    {
        list.Add(obj);
    }

    public void Reset()
    {
        listPointer = (list.Count - 1);
    }
}
