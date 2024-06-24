using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGuiAnimation : MonoBehaviour
{
    public abstract void OnShow();
    public abstract void OnHide();
    public Action OnHideAction;
    public Action OnShowAction;
    public AnimationNames AnimationName;
    
}
