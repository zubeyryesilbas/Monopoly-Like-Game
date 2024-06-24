using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct CharacterData
{
   public CharacterType CharcterType;
   public GameObject CharacterSelectionPrefab;
   public GameObject CharacterMovementPrefab;
   public string CharacterName;
   public CharacterStat CharacterStat;
}
