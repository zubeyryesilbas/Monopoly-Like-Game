using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class MovementInstance : MonoBehaviour,IMovement
{
    public Action OnComplete;
    [SerializeField] private TweenParameters _tweenParameters;

    private void Start()
    {
        _tweenManager = TweenManager.Instance;
    }

    public void Move(Vector3 targetPosition)
    {
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
