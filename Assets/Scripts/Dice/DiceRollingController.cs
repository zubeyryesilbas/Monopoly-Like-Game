using System;
using System.Collections;
using System.Collections.Generic;
using Dice;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollingController : MonoBehaviour
{
    [SerializeField] private Transform _rollPos;
    [SerializeField] private List<DiceBase> _dices = new List<DiceBase>();
    [SerializeField] private BoardController _boardController;
    [SerializeField] private DiceInputReader _diceInputReader;
    [SerializeField] private DiceFaceArranger _dice1, _dice2;
    [SerializeField] private Transform _diceHolder;
    [SerializeField] private Transform _diceFollowTarget;
    [SerializeField] private Button _diceButton;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = _diceHolder.transform.position - _diceFollowTarget.position;
        _diceButton.onClick.AddListener(RollDices);
    }

    public void RollDices()
    {
        _diceButton.interactable = false;
        _diceHolder.transform.position = _diceFollowTarget.transform.position + _offset;
        var faces = _diceInputReader.GetDicesFace();
        var face1 = faces.Item1;
        var face2 = faces.Item2;
        _dice1.SetDiceFace(face1);
        _dice2.SetDiceFace(face2);
        _dices[0].Roll();
        _dices[1].Roll();
         foreach (var item in _dices)
         {
             item.transform.position = _rollPos.position;
             item.Roll();
         }
    }
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSuscribe();
    }

    private void Subscribe()
    {
        foreach (var item in _dices)
        {
            item.OnRollingEnd += CheckDicesRollingEnd;
        }
    }

    private void UnSuscribe()
    {
        foreach (var item in _dices)
        {
            item.OnRollingEnd -= CheckDicesRollingEnd;
        }
    }
    private void CheckDicesRollingEnd()
    {
        var diceSum = 0;
        var allDicesRolledOut = true;
        foreach (var item in _dices)
        {
           if(item.IsRolling)
            {
                allDicesRolledOut = false;
            }
        }
        
        if (allDicesRolledOut)
        {
            _diceButton.interactable = true;
            Debug.Log("Rolling End");
            var tupple = _diceInputReader.GetDicesFace();

            diceSum = tupple.Item1 + tupple.Item2;
            _boardController.Move(diceSum);
        }
    }
}
