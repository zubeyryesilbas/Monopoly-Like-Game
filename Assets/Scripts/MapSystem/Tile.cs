using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tile :PoolableObject ,ITiledObject,IInteractible,ICollectableItem
{
    public Vector2Int Coordinate { get; set; }
    public ItemType Type => _itemType;
    private TweenManager _tweenManager;
    [SerializeField] private TextMeshPro _itemAmountText;
    [SerializeField] private GameObject _emptyTile, _looseAllTile, _normalTile, _multiplierTile;
    [SerializeField] private Animator _tileAnimator;
    public void SetTile(TileData tileData, Sprite sprite)
    {
        Coordinate = tileData.Coordinate;
        var type = tileData.ItemType;
        var amount = tileData.ItemAmount;
        switch (type)
        {
            case ItemType.Empty:
                _emptyTile.gameObject.SetActive(true);
                break;
            case ItemType.Looseall:
                _looseAllTile.gameObject.SetActive(true);
                break;
            case ItemType.X2:
                _multiplierTile.gameObject.SetActive(true);
                break;
            default: 
                _normalTile.gameObject.SetActive(true);
                _spriteRenderer.sprite = sprite;
                _itemAmountText.text = "X" + amount;
                break;
        }
        _itemType = type;
    }

    private ItemType _itemType;
    private int _itemCount;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void OnInteract()
    {   
        _tileAnimator.SetTrigger("Interact");
    }

    public void OnReset()
    {   
        _tileAnimator.SetTrigger("Reset");
    }

    public Tuple<ItemType, int> OnCollect()
    {
      //  return new Tuple<ItemType, int>(_itemType , )
      return null;
    }

    public override void OnObjectSpawn()
    {
        //OnReset();
    }

    public override void OnObjectDespawn()
    {
        //OnReset();
    }
}
