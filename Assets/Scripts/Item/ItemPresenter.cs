using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class ItemPresenter : MonoBehaviour
{
    [SerializeField] private InventoryItem _inventoryItem;
    [SerializeField] private Transform _inventoryHolder;
    private IItemManager _itemManager;
    private Dictionary<ItemType, InventoryItem> _inventoryItemDic = new Dictionary<ItemType, InventoryItem>();
    private BoardController _boardController;
    public Dictionary<ItemType, Vector3> ItemPositions = new Dictionary<ItemType, Vector3>();

    public void Initialize(IItemManager itemManager ,SpriteTypeHolderSO  spriteHolder , BoardController boardController)
    {   
        _itemManager = itemManager;
        _boardController = boardController;
        foreach (var item in _itemManager.ItemsEarned)
        {
            var type = item.ItemType;
            var amount = item.Amount;
            var itemInstance = Instantiate(_inventoryItem, transform.position, Quaternion.identity, _inventoryHolder);
            var sprite = spriteHolder.Sprites.FirstOrDefault(X => X.ItemType == type).SpriteGui;
            itemInstance.Initialize(sprite , amount);
            _inventoryItemDic.Add(item.ItemType , itemInstance);
        }

        foreach (var item in _inventoryItemDic)
        {
            ItemPositions.Add(item.Key , item.Value.transform.localPosition);
        }
    }
    
    
    public void UpdateItems(ItemType type , int amount)
    {   
        var item = _inventoryItemDic[type];
        _itemManager.AddItems(type, amount);
        var lastItemAmount = _itemManager.ItemsEarned.FirstOrDefault(x => x.ItemType == type).Amount;
        Debug.Log(lastItemAmount);
        _inventoryItemDic[type].UpdateItemCount(lastItemAmount);
    }
}
