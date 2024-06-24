using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
   [SerializeField] private Image _itemImage;
   [SerializeField] private TextMeshProUGUI _itemText;
    private int _itemCount;

    public void Initialize(Sprite sprite , int amount )
    {
        _itemImage.sprite = sprite;
        UpdateItemCount(amount);
    }
    public void UpdateItemCount(int count)
    {
        _itemText.text = "" + count;
    }
}
