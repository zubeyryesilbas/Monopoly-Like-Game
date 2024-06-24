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

    private void Awake()
    {
        _tweenManager = TweenManager.Instance;
    }

    public void Move(Vector3 targetPosition)
    {
        _animator.SetTrigger("Roll");
        TweenMove(targetPosition);
    }

    private void TriggerMove()
    {
        GetComponent<Animator>().SetTrigger("jump");
    }
    
    private TweenManager _tweenManager;

    void TweenMove(Vector3 pos)
    {
        Vector3 startPos = transform.position;
        var endPos = pos + startPos;
        endPos = endPos / 2;
        endPos += Vector3.up * _tweenParameters.JumpFactor;
        float duration = _tweenParameters.Duration;
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
                _tweenManager.AddTween(new Vector3Tween(transform.position , pos , duration ,Easing.EaseInQuad ,
                    (Vector3 value) =>
                    {
                        transform.position = value;
                    },
                    () =>
                    {
                        OnComplete?.Invoke();
                    }));
            }
        ));
    }
}
