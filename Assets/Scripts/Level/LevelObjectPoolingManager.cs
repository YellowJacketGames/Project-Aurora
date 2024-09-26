using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectPoolingManager : MonoBehaviour
{
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Start()
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

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag)) return null;
        var objToSpawn = _poolDictionary[tag].Dequeue();
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation; 
        
        _poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn; 
    }
    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
    }
}    


[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size; 
}