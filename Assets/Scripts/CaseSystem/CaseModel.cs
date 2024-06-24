using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

public class CaseModel 
{
    public Dictionary<ItemType, int> ItemAmounts { get; private set; }
    private readonly IItemManager _itemManager;
    private readonly ICharacterManager _characterManager;

    public CaseModel(IItemManager itemManager, ICharacterManager characterManager)
    {
        _itemManager = itemManager;
        _characterManager = characterManager;
        ItemAmounts = new Dictionary<ItemType, int>();

        foreach (var item in _itemManager.ItemsEarned)
        {
            ItemAmounts[item.ItemType] = 0;
        }
    }

    public void AddItem(TileData data)
    {
        int bonus = _characterManager.CharacterStat.Luck;
        int baseAmount = data.ItemAmount;
        int increase = Mathf.RoundToInt(baseAmount * bonus / 100f);
        int finalAmount = baseAmount + increase;
        
        ItemAmounts[data.ItemType] += finalAmount;
    }

    public void MultiplyItems(int multiplier)
    {
        var keys = ItemAmounts.Keys.ToList();
        foreach (var key in keys)
        {
            ItemAmounts[key] *= multiplier;
        }
    }

    public void ResetItems()
    {
        var keys = ItemAmounts.Keys.ToList();
        foreach (var key in keys)
        {
            ItemAmounts[key] = 0;
        }
    }

    public IEnumerable<ItemType> GetItemTypes()
    {
        return ItemAmounts.Keys;
    }

    public void ResetItem(ItemType itemType)
    {
        ItemAmounts[itemType] = 0;
    }
}
