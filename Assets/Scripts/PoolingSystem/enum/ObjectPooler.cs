using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField] private PoolHolderSO _poolHolderSo;
    private Dictionary<PoolType, Queue<IPoolableObject>> _poolDictionary;
    private List<Pool> _pools = new List<Pool>();
    
    private void Awake()
    {
        _poolDictionary = new Dictionary<PoolType, Queue<IPoolableObject>>();
        _pools = new List<Pool>(_poolHolderSo.Pools);
        foreach (var pool in _pools)
        {
            InitializePool(pool);
        }
    }

    private void InitializePool(Pool pool)
    {
        Queue<IPoolableObject> objectPool = new Queue<IPoolableObject>();

        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            IPoolableObject poolableObject = obj.GetComponent<IPoolableObject>();
            if (poolableObject != null)
            {
                poolableObject.Initialize(pool.poolType);
                objectPool.Enqueue(poolableObject);
            }
            else
            {
                Debug.LogError("Prefab does not implement IPoolableObject interface.");
            }
        }

        _poolDictionary.Add(pool.poolType, objectPool);
    }

    public GameObject SpawnFromPool(PoolType poolType, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(poolType))
        {
            Debug.LogWarning("Pool with type " + poolType + " doesn't exist.");
            return null;
        }

        Queue<IPoolableObject> objectPool = _poolDictionary[poolType];

        if (objectPool.Count == 0)
        {
            Pool pool = _pools.Find(p => p.poolType == poolType);
            if (pool != null)
            {
                InitializePool(pool);
            }
        }

        IPoolableObject poolableObject = objectPool.Dequeue();
        GameObject objectToSpawn = ((MonoBehaviour)poolableObject).gameObject;

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolableObject.OnObjectSpawn();

        objectPool.Enqueue(poolableObject);

        return objectToSpawn;
    }

    public void DespawnToPool(GameObject objectToDespawn)
    {
        IPoolableObject poolableObject = objectToDespawn.GetComponent<IPoolableObject>();
        if (poolableObject != null)
        {
            poolableObject.OnObjectDespawn();
        }

        objectToDespawn.SetActive(false);
    }

    public void ResizePool(PoolType poolType, int newSize)
    {
        if (!_poolDictionary.ContainsKey(poolType))
        {
            Debug.LogWarning("Pool with type " + poolType + " doesn't exist.");
            return;
        }

        Queue<IPoolableObject> objectPool = _poolDictionary[poolType];
        Pool pool = _pools.Find(p => p.poolType == poolType);

        if (pool != null)
        {
            int currentSize = objectPool.Count;
            if (newSize > currentSize)
            {
                for (int i = 0; i < newSize - currentSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    IPoolableObject poolableObject = obj.GetComponent<IPoolableObject>();
                    if (poolableObject != null)
                    {
                        poolableObject.Initialize(pool.poolType);
                        objectPool.Enqueue(poolableObject);
                    }
                }
            }
            else if (newSize < currentSize)
            {
                for (int i = 0; i < currentSize - newSize; i++)
                {
                    if (objectPool.Count > 0)
                    {
                        IPoolableObject poolableObject = objectPool.Dequeue();
                        Destroy(((MonoBehaviour)poolableObject).gameObject);
                    }
                }
            }
        }
    }

    public void CleanupPool(PoolType poolType)
    {
        if (!_poolDictionary.ContainsKey(poolType))
        {
            Debug.LogWarning("Pool with type " + poolType + " doesn't exist.");
            return;
        }

        Queue<IPoolableObject> objectPool = _poolDictionary[poolType];

        while (objectPool.Count > 0)
        {
            IPoolableObject poolableObject = objectPool.Dequeue();
            Destroy(((MonoBehaviour)poolableObject).gameObject);
        }

        _poolDictionary.Remove(poolType);
    }
}
