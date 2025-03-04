using UnityEngine;

public class SpeakToAllCallback : MonoBehaviour
{
    [SerializeField] private SpeakToAllAchievement speakToAllAchievement;

    public void OnInteracted()
    {
        speakToAllAchievement.NewPeopleTalked();
        this.enabled = false;
    }
}