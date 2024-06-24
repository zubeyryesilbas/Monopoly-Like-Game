using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFlyFeedBack : PoolableObject
{
    private TweenManager _tweenManager;
    public Vector3 pos;

    private void Awake()
    {
        _tweenManager = TweenManager.Instance;
    }

    public void Fly(Vector3 targetPos)
    { 
        Vector3 startPos = transform.localPosition;
        var endPos = targetPos;
        var duration = 0.5f;
        _tweenManager.AddTween(new Vector3Tween(
            startPos,
            endPos,
            0.5f,
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
                        transform.localPosition = value;
                    },
                    () =>
                    {
                        gameObject.SetActive(false);
                    }));
            }
        ));
        
    }

    public override void OnObjectSpawn()
    {
        //throw new NotImplementedException();
    }

    public override void OnObjectDespawn()
    {
       // throw new NotImplementedException();
    }
}
