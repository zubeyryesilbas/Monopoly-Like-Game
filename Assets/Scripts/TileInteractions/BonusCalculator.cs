using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BonusCalculator 
{
   public static int CalculateFinal(int baseAmount, int bonusPercentage)
   {
      return baseAmount + Mathf.RoundToInt(baseAmount * bonusPercentage / 100);
   }
   
   public static int CalculateBonus(int baseAmount, int bonusPercentage)
   {
      return  Mathf.RoundToInt(baseAmount * bonusPercentage / 100);
   }
}
