using System.Collections;
using UnityEngine;

public class SteamAchievementTrigger : MonoBehaviour
{
    [SerializeField] protected SteamAchievements achievement;
    [SerializeField] protected SteamAchievementTriggerType achType;
    [SerializeField] protected int waitTimeSeconds = 0;
    protected bool CanUnlockAchievement = true;

    protected virtual void Awake()
    {
        if (achType == SteamAchievementTriggerType.AWAKE)
            AddAch(achievement);
        else if (achType == SteamAchievementTriggerType.TIMER)
            StartCoroutine(StartTimer());
        else if (achType == SteamAchievementTriggerType.REVERSETIMER)
            StartCoroutine(StartReverseTimer());
    }

    protected virtual void OnEnable()
    {
        if (achType == SteamAchievementTriggerType.ONEVENT)
            EventsManager.onAchievementCalled.AddListener(AddAch);
    }

    protected virtual void OnDisable()
    {
        if (achType == SteamAchievementTriggerType.ONEVENT)
            EventsManager.onAchievementCalled.RemoveListener(AddAch);
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTimeSeconds);
        if (CanUnlockAchievement)
            AddAch(achievement);
        CanUnlockAchievement = false;
        yield return null;
    }

    protected  IEnumerator StartReverseTimer()
    {
        yield return new WaitForSeconds(waitTimeSeconds);
        CanUnlockAchievement = false;
        yield return null;
    }

    protected virtual void AddAch(SteamAchievements arg0)
    {
        if (achievement != arg0) return;
        SteamManager.Instance.UnlockAchievement(arg0);
    }
}

public enum SteamAchievementTriggerType
{
    NULL,
    AWAKE,
    ONEVENT,
    TIMER,
    REVERSETIMER
}