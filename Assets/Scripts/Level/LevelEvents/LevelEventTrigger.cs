using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelEventTrigger : MonoBehaviour
{
    [SerializeField] private int eventIndexToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentLevelManager.TriggerEvent(eventIndexToTrigger);
        }
    }
}