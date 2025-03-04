public class SteamAchievementUnderTime : SteamAchievementTrigger
{
    protected override void Awake()
    {
        if (achType == SteamAchievementTriggerType.REVERSETIMER || achType == SteamAchievementTriggerType.ONEVENT)
            StartCoroutine(StartReverseTimer());
    }

    protected override void OnEnable()
    {
        if (achType == SteamAchievementTriggerType.REVERSETIMER || achType == SteamAchievementTriggerType.ONEVENT)
            EventsManager.onAchievementCalled.AddListener(AddAch);
    }

    protected override void OnDisable()
    {
        if (achType == SteamAchievementTriggerType.REVERSETIMER || achType == SteamAchievementTriggerType.ONEVENT)
            EventsManager.onAchievementCalled.RemoveListener(AddAch);
    }
}