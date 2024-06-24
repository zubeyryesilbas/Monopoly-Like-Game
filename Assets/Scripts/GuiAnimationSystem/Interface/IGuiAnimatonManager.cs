using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuiAnimatonManager
{
    void Register(BaseGuiAnimation guiAnimation);
    void Show(AnimationNames animationName);
    void Hide(AnimationNames animationName);
}
