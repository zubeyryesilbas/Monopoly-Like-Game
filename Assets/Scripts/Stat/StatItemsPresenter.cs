using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatItemsPresenter : MonoBehaviour
{
    [SerializeField] private StatItem _luckItem, _BonusItem;

    public void Initialize(float luck , float bonus)
    {
       _luckItem.Initialize(""+Mathf.RoundToInt(luck) );
       _BonusItem.Initialize("%" + Mathf.RoundToInt(bonus));
    }
}
