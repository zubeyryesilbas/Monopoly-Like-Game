using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
[Serializable]
public class ItemData
{
    public ItemData(ItemType type, int amount)
    {
        Amount = amount;
        ItemType = type;
    }
    public int Amount;
    public ItemType ItemType;

    public void AddItem(int itemAmount)
    {
        Amount += itemAmount;
    }
}
