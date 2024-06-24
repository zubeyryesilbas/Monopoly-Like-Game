using System;
using System.Collections;
using System.Collections.Generic;
using Dice;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DiceRollingController : MonoBehaviour
{
    [SerializeField] private List<DiceBase> _dices = new List<DiceBase>();
    [SerializeField] private List<DicePair> _pairs = new List<DicePair>();
    [SerializeField] private BoardController _boardController;
    [SerializeField] private DiceInputReader _diceInputReader;
    [SerializeField] private DiceFaceArranger _dice1, _dice2;
    [SerializeField] private Transform _diceHolder;
    [SerializeField] private Transform _diceFollowTarget;
    [SerializeField] private Button _diceButton;
    private bool _diceRollEnded;
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
        var random = UnityEngine.Random.Range(0, _pairs.Count);
        foreach (var item in _pairs)
        {
            item.gameObject.SetActive(false);
        }
        _pairs[random].gameObject.SetActive(true);
        _pairs[random].RollPair(face1,face2);
        _dices = new List<DiceBase>();
        _dices.Add(_pairs[random].Dice1);
        _dices.Add(_pairs[random].Dice2);

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
        EventManager.Instance.AddEventListener(EventConstants.BoardEvents.ONLASTSTEPCOMPLETED , (data)=>CheckLastStepCompleted() );
        foreach (var item in _pairs)
        {
            item.Dice1.OnRollingEnd += CheckDicesRollingEnd;
            item.Dice2.OnRollingEnd += CheckDicesRollingEnd;
        }
    }

    private void CheckLastStepCompleted()
    {
        if (_diceRollEnded)
        {
            _diceButton.interactable = true;
        }
    }
    private void UnSuscribe()
    {   
        EventManager.Instance.RemoveEventListener(EventConstants.BoardEvents.ONLASTSTEPCOMPLETED , (data)=>CheckLastStepCompleted() );
        foreach (var item in _pairs)
        {
            item.Dice1.OnRollingEnd -= CheckDicesRollingEnd;
            item.Dice2.OnRollingEnd -= CheckDicesRollingEnd;
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
            _diceRollEnded = true;
            Debug.Log("Rolling End");
            var tupple = _diceInputReader.GetDicesFace();

            diceSum = tupple.Item1 + tupple.Item2;
            _boardController.Move(diceSum);
        }
    }
}
