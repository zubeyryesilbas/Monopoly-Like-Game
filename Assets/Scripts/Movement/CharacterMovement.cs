using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovement
{
    public Action OnComplete;
    public Action OnStart;
    [SerializeField] private TweenParameters _tweenParameters;
    [SerializeField] private Animator _animator;
    private TweenManager _tweenManager;

    private void Awake()
    {
        _tweenManager = TweenManager.Instance;
    }

    public void Move(Vector3 targetPosition ,int stepCount)
    {
        _animator.SetBool("Roll " , true);
        TweenMove(targetPosition , stepCount);
    }
    
    void TweenMove(Vector3 pos , int stepCount)
    {
        Vector3 startPos = transform.position;
        var endPos = pos + startPos;
        endPos = endPos / 2;
        endPos += Vector3.up * _tweenParameters.JumpFactor;
        float duration = _tweenParameters.Duration * stepCount;
        
        OnStart?.Invoke();
        _tweenManager.AddTween(new Vector3Tween(
            startPos,
            endPos,
            duration,
            Easing.EaseInOutQuad,
            (Vector3 value) =>
            {
                transform.position = value;
            },
            () =>
            {
                _tweenManager.AddTween(new Vector3Tween(transform.position , pos , duration ,Easing.Linear ,
                    (Vector3 value) =>
                    {
                        transform.position = value;
                    },
                    () =>
                    {   _animator.SetBool("Roll ",false);
                        OnComplete?.Invoke();
                    }));
            }
        ));
    }
}
