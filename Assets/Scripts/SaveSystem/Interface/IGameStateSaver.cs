using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStateSaver
{
    void SaveGameState(CharacterType charcterType, List<ItemData> itemsEarned);
    (CharacterType, List<ItemData>) LoadGameState();
}
