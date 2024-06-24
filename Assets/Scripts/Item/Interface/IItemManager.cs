using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public interface IItemManager
{
    List<ItemData> ItemsEarned { get; }
    void UpdateItems(List<ItemData> items);
    void AddItems(ItemType itemType, int itemAmount);
}
