using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
[Serializable]
public struct TileData
{
   public ItemType ItemType;
   public int ItemAmount;
   public Vector2Int Coordinate;
}
