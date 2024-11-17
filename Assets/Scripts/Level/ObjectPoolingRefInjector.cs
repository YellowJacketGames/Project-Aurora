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
                obj.GetComponent<WaterDropEntity>().SetRef(this);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }
}