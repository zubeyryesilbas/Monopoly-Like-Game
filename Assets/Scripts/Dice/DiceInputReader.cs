using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceInputReader : MonoBehaviour
{
   [SerializeField] private InputValidator _input1, _input2;

   public Tuple<int, int> GetDicesFace()
   {
      return new Tuple<int, int>(_input1.GetValidFace(), _input2.GetValidFace());
   }
}
