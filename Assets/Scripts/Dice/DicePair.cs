using System.Collections;
using System.Collections.Generic;
using Dice;
using UnityEngine;

public class DicePair : MonoBehaviour
{
    [SerializeField] private DiceBase _dice1, _dice2;
    public DiceBase Dice1 => _dice1;
    public DiceBase Dice2 => _dice2;
    [SerializeField] private DiceFaceArranger _diceFace1, _diceFace2;

    public void RollPair(int face1 , int face2)
    {
        _dice1.Roll();
        _dice2.Roll();
        _diceFace1.SetDiceFace(face1);
        _diceFace2.SetDiceFace(face2);
    }
}
