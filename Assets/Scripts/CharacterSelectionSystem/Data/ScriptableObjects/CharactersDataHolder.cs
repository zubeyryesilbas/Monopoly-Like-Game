using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharactersDataHolder", menuName = "Characters Data")]
public class CharactersDataHolder :ScriptableObject
{
    public List<CharacterData> CharacterData = new List<CharacterData>();
}
