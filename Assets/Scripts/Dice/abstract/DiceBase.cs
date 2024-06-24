using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice
{
    public abstract class DiceBase : MonoBehaviour
    {
        public abstract void Roll();
        public abstract int GetTopFace();
        public Action OnRollingEnd;
        public bool IsRolling => _isRolling;
        protected bool _isRolling;
    }
}

