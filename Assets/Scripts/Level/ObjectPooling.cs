using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<Pool> pools;
    protected Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        ClearPools();
    }

    protected virtual void Init()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void ClearPools()
    {
        //destroy each element in each pool queue
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public virtual void CreateNewPool(Pool newPool)
    {
        if (_poolDictionary.Count > 0)
            if (_poolDictionary.ContainsKey(newPool.tag))
            {
                Debug.LogWarning($"Pool with tag {newPool.tag}. Exists already! Cant add another one.");
                return;
            }


        pools.Add(newPool);
        var objectPool = new Queue<GameObject>();
        for (var i = 0; i < newPool.size; i++)
        {
            var obj = Instantiate(newPool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        _poolDictionary.Add(newPool.tag, objectPool);
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag)) return null;
        var objToSpawn = _poolDictionary[tag].Dequeue();
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);
        _poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
    }

    public Queue<GameObject> GetPool(string tag)
    {
        if (!_poolDictionary.ContainsKey(tag)) return null;
        return _poolDictionary[tag];
    }
}

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;

    public Pool(string tag, GameObject prefab, int size)
    {
        this.tag = tag;
        this.prefab = prefab;
        this.size = size;
    }
}