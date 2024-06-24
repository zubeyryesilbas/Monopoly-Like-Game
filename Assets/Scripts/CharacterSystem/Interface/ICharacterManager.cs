using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{   
    CharacterStat CharacterStat { get; }
    CharacterType CharacterType { get; }
    void UpdateCharacter(CharacterType charcterType , CharacterStat characterStat);
}
