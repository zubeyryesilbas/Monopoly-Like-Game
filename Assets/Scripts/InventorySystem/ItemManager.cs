using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class ItemManager : IItemManager
{
    private List<ItemData> _itemsEarned = new List<ItemData>();
    public List<ItemData> ItemsEarned => _itemsEarned;
    public void UpdateItems(List<ItemData> items)
    {
        if (items.Count >0)
        {
            _itemsEarned = new List<ItemData>(items);
        }
        else
        {
            _itemsEarned = new List<ItemData>()
            {
                new ItemData(ItemType.Apple, 0),
                new ItemData(ItemType.Pears, 0),
                new ItemData(ItemType.Strawberry, 0)
            };
        }
    }

    public void AddItems(ItemType itemType, int itemAmount)
    {
        _itemsEarned.FirstOrDefault(x => x.ItemType == itemType).AddItem(itemAmount);
       
    }
}