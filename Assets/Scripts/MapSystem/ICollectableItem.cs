using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public interface ICollectableItem 
{
    Tuple<ItemType, int> OnCollect();

}
