using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventoryView _inventoryView;
    public void Initialize(IItemManager itemManager ,SpriteTypeHolderSO spriteTypeHolderSo ,EventManager eventManager )
    {
        var model = new InventoryModel(itemManager);
        var inventoryPresenter = new InventoryPresenter(model, _inventoryView, spriteTypeHolderSo, eventManager);
    }
}
