using UnityEngine;

public class TriggerAchievementInvoker : MonoBehaviour
{
    [SerializeField] protected SteamAchievements achievement;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player")) return;
        EventsManager.InvokeAchievementUnlock(achievement);
        enabled = false;
    }
}