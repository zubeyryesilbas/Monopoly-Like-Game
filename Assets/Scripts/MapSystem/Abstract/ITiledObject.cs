using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public interface ITiledObject
{
    Vector2Int Coordinate { get; set; }
    ItemType Type { get; }
    void SetTile(TileData tileData, Sprite sprite);
}
