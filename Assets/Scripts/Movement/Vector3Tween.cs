using UnityEngine;
using System;
using System.Collections.Generic;

public class Vector3Tween
{
    public Action<Vector3> OnUpdate;
    public Action OnComplete;
    private Vector3 startValue;
    private Vector3 endValue;
    private float duration;
    private float elapsedTime;
    private Func<float, float, float, float> easingFunction;

    public Vector3Tween(Vector3 startValue, Vector3 endValue, float duration, Func<float, float, float, float> easingFunction, Action<Vector3> onUpdate, Action onComplete = null)
    {
        this.startValue = startValue;
        this.endValue = endValue;
        this.duration = duration;
        this.easingFunction = easingFunction;
        this.OnUpdate = onUpdate;
        this.OnComplete = onComplete;
        this.elapsedTime = 0f;
    }

    public bool Update(float deltaTime)
    {
        elapsedTime += deltaTime;
        if (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = easingFunction(0f, 1f, t);
            Vector3 value = Vector3.Lerp(startValue, endValue, easedT);
            OnUpdate?.Invoke(value);
            return false;
        }
        else
        {
            OnUpdate?.Invoke(endValue);
            OnComplete?.Invoke();
            return true;
        }
    }
}