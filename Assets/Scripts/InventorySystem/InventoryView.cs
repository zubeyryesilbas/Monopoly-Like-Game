using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryItem _inventoryItemPrefab;
    [SerializeField] private Transform _inventoryHolder;
    private Dictionary<ItemType, InventoryItem> _inventoryItemDic = new Dictionary<ItemType, InventoryItem>();

    public void Initialize(SpriteTypeHolderSO spriteHolder, IEnumerable<ItemData> items)
    {
        foreach (var item in items)
        {
            var type = item.ItemType;
            var amount = item.Amount;
            var itemInstance = Instantiate(_inventoryItemPrefab, transform.position, Quaternion.identity, _inventoryHolder);
            var sprite = spriteHolder.Sprites.FirstOrDefault(x => x.ItemType == type).SpriteGui;
            itemInstance.Initialize(sprite, amount);
            _inventoryItemDic[type] = itemInstance;
        }
    }

    public void SetItemPosition(ItemType type, Vector3 position)
    {
        if (_inventoryItemDic.ContainsKey(type))
        {
            _inventoryItemDic[type].transform.localPosition = position;
        }
    }

    public void UpdateItemCount(ItemType type, int amount)
    {
        if (_inventoryItemDic.ContainsKey(type))
        {
            _inventoryItemDic[type].UpdateItemCount(amount);
        }
    }
}