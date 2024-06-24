using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dice;
using UnityEngine;

public class PhysicalDiceRoller : MonoBehaviour
{
   [SerializeField] private PhysicalDice _dice1, _dice2;
   [SerializeField] private Transform _spawnPos;
   [SerializeField] private Transform _throwTarget;
   [SerializeField] private AnimatedDice _animatedice1, _animatedice2;
   public void Roll()
   {  
      _dice1.transform.position = _spawnPos.position;
      _dice2.transform.position = _spawnPos.position;
      _dice1.SetTarget(_throwTarget);
      _dice2.SetTarget(_throwTarget);
      _dice1.Roll();
      _dice2.Roll();
      StartCoroutine(StartRoll());
      
   }

   public void AnimatedRoll()
   {
      _animatedice1.Roll();
      _animatedice2.Roll();
   }

   private IEnumerator StartRoll()
   {
      yield return new WaitForSeconds(0.03f);
      _dice1.GetComponent<DiceRecorder>().StartRecord();
      _dice2.GetComponent<DiceRecorder>().StartRecord();
   }
}
