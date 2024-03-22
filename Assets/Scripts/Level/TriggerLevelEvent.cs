using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelEvent : MonoBehaviour
{
    [SerializeField] int levelIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.currentLevelManager.GetEvent(levelIndex).HasBeenTriggered())
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.currentLevelManager.TriggerEvent(levelIndex);
            }
            
        }
    }
}
