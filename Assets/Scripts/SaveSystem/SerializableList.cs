using System;
using System.Collections.Generic;

[Serializable]
public class SerializableList<T>
{
    public List<T> items;

    public SerializableList(List<T> items)
    {
        this.items = items;
    }
}