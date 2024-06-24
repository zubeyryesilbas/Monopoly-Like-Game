using UnityEngine;

public abstract class PoolableObject : MonoBehaviour,IPoolableObject
{
    public PoolType PoolType { get; private set; }

    public virtual void Initialize(PoolType poolType)
    {
        PoolType = poolType;
    }

    public abstract void OnObjectSpawn();
    public abstract void OnObjectDespawn();
}