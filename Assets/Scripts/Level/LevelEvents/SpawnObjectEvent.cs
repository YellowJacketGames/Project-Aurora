using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectEvent : LevelEvent
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject prefabToSpawn;

    public override void OnEvent()
    {
        GameObject o = Instantiate(prefabToSpawn, null);
        o.transform.position = spawnPosition.position;
    }
}
