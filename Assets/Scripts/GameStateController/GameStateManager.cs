using System.Collections.Generic;
using Item;
using UnityEngine;

public class GameStateManager :Singleton<GameStateManager>
{
    private ICharacterManager _characterManager;
    private IItemManager _itemManager;
    private IGameStateSaver _gameStateSaver;

    public void Initialize(ICharacterManager characterManager , IItemManager itemManager , IGameStateSaver gameStateSaver)
    {
        _characterManager = characterManager;
        _itemManager = itemManager;
        _gameStateSaver = gameStateSaver;
        LoadGameState();
    }
    private void OnApplicationQuit()
    {
        SaveGameState();
    }
    
    private void SaveGameState()
    {   
        Debug.Log("Game Saved");
        _gameStateSaver.SaveGameState(_characterManager.CharacterType, _itemManager.ItemsEarned);
    }

    private void LoadGameState()
    {
        var (charcterType, itemsEarned) = _gameStateSaver.LoadGameState();
        if (itemsEarned == null)
        {
            itemsEarned = new List<ItemData>()
            {
                new ItemData(ItemType.Apple, 0),
                new ItemData(ItemType.Pears, 0),
                new ItemData(ItemType.Strawberry, 0)
            };
            Debug.Log("ItemsNull");
        }
            
        _itemManager.UpdateItems(itemsEarned);
    }
}