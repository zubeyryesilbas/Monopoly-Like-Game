using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpriteHolder", menuName = "ScriptableObjects/SpriteHolder", order = 1)]
public class SpriteTypeHolderSO : ScriptableObject
{
    public List<SpriteTypeData> Sprites = new List<SpriteTypeData>();
}
