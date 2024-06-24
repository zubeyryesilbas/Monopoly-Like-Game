using System.Collections;
using System.Collections.Generic;
using Dice;
using UnityEngine;

public class AnimatedDice : DiceBase
{
    [SerializeField] private Animation _anim;
    public override void Roll()
    {
        _isRolling = true;
        _anim.Play();
    }

    public void TriggerDiceEnd()
    {      
        Debug.Log("Dice Rolling END");
        _isRolling = false;
        OnRollingEnd?.Invoke();
    }
    public override int GetTopFace()
    {
        return 0;
    }
    
}
