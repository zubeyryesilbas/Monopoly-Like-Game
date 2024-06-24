using UnityEngine;

[CreateAssetMenu(fileName = "NewTweenParameters", menuName = "Tween/Tween Parameters", order = 1)]
public class TweenParameters : ScriptableObject
{
    public float Duration;
    public float JumpFactor;
}