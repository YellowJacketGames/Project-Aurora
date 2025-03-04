using UnityEngine;

public class FlawlessDoorAchievementCallback : MonoBehaviour
{
    [SerializeField] private FlawlessDoorAchievement ACH_4;

    public void ACH_4_FAILED()
    {
        ACH_4.FailedDoor();
        this.enabled = false;
    }
}