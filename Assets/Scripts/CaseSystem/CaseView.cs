using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CaseView : MonoBehaviour
{
    
    [SerializeField] private InventoryItem _inventoryItemPrefab;
    [SerializeField] private Transform _inventoryHolder;
    [SerializeField] private Button _caseButton;
    private Dictionary<ItemType, InventoryItem> _inventoryItems = new Dictionary<ItemType, InventoryItem>();

    public  Action OnCaseButtonClicked;

    private void Awake()
    {
        _caseButton.onClick.AddListener(() => OnCaseButtonClicked?.Invoke());
    }

    public void Initialize(SpriteTypeHolderSO spriteHolder, IEnumerable<ItemType> itemTypes)
    {
        foreach (var type in itemTypes)
        {
            var itemInstance = Instantiate(_inventoryItemPrefab, transform.position, Quaternion.identity, _inventoryHolder);
            var sprite = spriteHolder.Sprites.FirstOrDefault(x => x.ItemType == type).SpriteGui;
            itemInstance.Initialize(sprite, 0);
            _inventoryItems[type] = itemInstance;
        }
    }

    public void UpdateItemCount(ItemType type, int count)
    {
        if (_inventoryItems.ContainsKey(type))
        {
            _inventoryItems[type].UpdateItemCount(count);
        }
    }
}
