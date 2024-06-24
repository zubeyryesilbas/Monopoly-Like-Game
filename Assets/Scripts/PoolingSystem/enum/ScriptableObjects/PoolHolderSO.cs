using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "PoolHolder", order = 1)]
public class PoolHolderSO : ScriptableObject
{
    public List<Pool> Pools;
}