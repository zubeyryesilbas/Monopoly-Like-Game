using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimatorBasedGuiAnim : BaseGuiAnimation
{
    protected Animator _animator;
    private IGuiAnimatonManager _guiAnimatonManager;

    private void Awake()
    {
        _guiAnimatonManager = GuiAnimationManager.Instance;
        _guiAnimatonManager.Register(this);
        _animator = GetComponent<Animator>();
        OnHideAction += (() =>
        {
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }

    public override void OnShow()
    {
        _animator.SetTrigger("Show");
    }

    public override void OnHide()
    {
        _animator.SetTrigger("Hide");
    }

    public void TriggerOnHide()
    {
        OnHideAction?.Invoke();
    }
}
