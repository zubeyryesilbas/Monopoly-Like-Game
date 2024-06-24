using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuiAnimationManager :Singleton<GuiAnimationManager>, IGuiAnimatonManager
{
    [SerializeField]
    private Dictionary<AnimationNames, BaseGuiAnimation> _animationsDic =
        new Dictionary<AnimationNames, BaseGuiAnimation>();
    public void Register(BaseGuiAnimation guiAnimation)
    {
       _animationsDic.Add( guiAnimation.AnimationName ,guiAnimation);
    }

    public void Show(AnimationNames animationName)
    {
        var animation = _animationsDic[animationName];
       animation.gameObject.SetActive(true);
       animation.OnShow();
    }

    public void Hide(AnimationNames animationName)
    {
        var animation = _animationsDic[animationName];
        animation.OnHide();
    }

   
}
