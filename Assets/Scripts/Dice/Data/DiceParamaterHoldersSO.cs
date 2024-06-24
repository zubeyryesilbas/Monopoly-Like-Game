using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice
{
    [CreateAssetMenu(fileName = "New Dice Settings", menuName = "Game/Dice Settings")]
    public class DiceParamaterHoldersSO : ScriptableObject
    {
        public float MaxRollForce;
        public float MaxTorque;
    }
}
