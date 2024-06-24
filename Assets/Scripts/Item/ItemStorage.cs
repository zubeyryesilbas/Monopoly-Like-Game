using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage 
{
    public List<ItemData> Items = new List<ItemData>();

    public ItemStorage(List<ItemData> items)
    {
        Items = items;
    }
}
