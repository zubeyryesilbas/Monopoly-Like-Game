using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameStateSaver : IGameStateSaver
{
    public void SaveGameState(CharacterType charcterType, List<ItemData> itemsEarned)
    {
        PlayerPrefs.SetInt("CharacterType", (int)charcterType);

        string itemsJson = JsonUtility.ToJson(new SerializableList<ItemData>(itemsEarned));
        PlayerPrefs.SetString("ItemsEarned", itemsJson);

        PlayerPrefs.Save();
    }

    public (CharacterType, List<ItemData>) LoadGameState()
    {
        CharacterType charcterType = CharacterType.Colobus; 
        List<ItemData> itemsEarned = new List<ItemData>();

        if (PlayerPrefs.HasKey("CharacterType"))
        {
            charcterType = (CharacterType)PlayerPrefs.GetInt("CharacterType");
        }

        if (PlayerPrefs.HasKey("ItemsEarned"))
        {
            string itemsJson = PlayerPrefs.GetString("ItemsEarned");
            itemsEarned = JsonUtility.FromJson<SerializableList<ItemData>>(itemsJson).items; 
            
        }

        return (charcterType, itemsEarned);
    }
}