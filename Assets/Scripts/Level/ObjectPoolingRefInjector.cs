using System.Collections.Generic;
using Platforms;
using UnityEngine;

public class ObjectPoolingRefInjector : ObjectPooling
{
    protected override void Init()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.GetComponent<WaterDropEntity>().SetRef(this, pool.tag);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    public override void CreateNewPool(Pool newPool)
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
            obj.GetComponent<WaterDropEntity>().SetRef(this, newPool.tag);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        _poolDictionary.Add(newPool.tag, objectPool);
    }

}