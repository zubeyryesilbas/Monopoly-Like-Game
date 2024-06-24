using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class InventoryModel
{
    private readonly IItemManager _itemManager;
    public Dictionary<ItemType, Vector3> ItemPositions { get; private set; } = new Dictionary<ItemType, Vector3>();

    public InventoryModel(IItemManager itemManager)
    {
        _itemManager = itemManager;
    }

    public IEnumerable<ItemData> GetItems()
    {
        return _itemManager.ItemsEarned;
    }

    public void AddItems(ItemType type, int amount)
    {
        _itemManager.AddItems(type, amount);
    }

    public int GetItemAmount(ItemType type)
    {
        return _itemManager.ItemsEarned.FirstOrDefault(x => x.ItemType == type)?.Amount ?? 0;
    }

    public void SetItemPosition(ItemType type, Vector3 position)
    {
        ItemPositions[type] = position;
    }

    public Vector3 GetItemPosition(ItemType type)
    {
        return ItemPositions.ContainsKey(type) ? ItemPositions[type] : Vector3.zero;
    }
}