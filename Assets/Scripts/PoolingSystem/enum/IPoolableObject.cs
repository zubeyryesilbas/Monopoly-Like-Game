public interface IPoolableObject
{
    PoolType PoolType { get; }
    void OnObjectSpawn();
    void OnObjectDespawn();
    void Initialize(PoolType type);
}