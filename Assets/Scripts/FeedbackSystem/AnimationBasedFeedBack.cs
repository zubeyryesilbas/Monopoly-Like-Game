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
        StartCoroutine(DisableAnimCoroutine());
    }

    private IEnumerator DisableAnimCoroutine()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
    
    
}
