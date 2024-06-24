using System.Collections.Generic;
using UnityEngine;

public class TweenManager : Singleton<TweenManager>
{
    private List<Vector3Tween> tweens = new List<Vector3Tween>();

    void Update()
    {
        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            if (tweens[i].Update(Time.deltaTime))
            {
                tweens.RemoveAt(i);
            }
        }
    }

    public void AddTween(Vector3Tween tween)
    {
        tweens.Add(tween);
    }
}