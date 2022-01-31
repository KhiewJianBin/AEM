using System;
using System.Collections.Generic;

public static class ListExtension
{ 
    public static T RemoveLast<T>(this List<T> theList)
    {
        T local = theList[theList.Count - 1];
        theList.RemoveAt(theList.Count - 1);
        return local;
    }

    public static List<T> RemoveNulls<T>(this List<T> collection)
    {
        for (var i = collection.Count - 1; i > -1; i--)
        {
            if (collection[i] == null)
                collection.RemoveAt(i);
        }
        return collection;
    }
}
