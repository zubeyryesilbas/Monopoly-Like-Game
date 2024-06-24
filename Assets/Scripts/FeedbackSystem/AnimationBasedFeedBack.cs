using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationBasedFeedBack : Feedback
{
    [SerializeField]private Animator _animator;
    

    public override void Play()
    {
        _animator.enabled = true;
        DisableAnim();
    }

    private async void DisableAnim()
    {
        await Task.Delay(2000);
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
    
    
}
