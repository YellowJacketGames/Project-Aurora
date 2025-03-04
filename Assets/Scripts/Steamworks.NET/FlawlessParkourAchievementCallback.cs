using UnityEngine;

public class FlawlessParkourAchievementCallback : MonoBehaviour
{
    [SerializeField] private FlawlessParkourAchievement ACH_5;

    public void ACH_5_FAILED()
    {
        ACH_5.FailedParkour();
        this.enabled = false;
    }
}
