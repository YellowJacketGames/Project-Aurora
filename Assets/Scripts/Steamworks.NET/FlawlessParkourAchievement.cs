public class FlawlessParkourAchievement : SteamAchievementTrigger
{
    private bool _hasFailedParkour;

    public void FailedParkour()
    {
        _hasFailedParkour = true;
    }

    protected override void AddAch(SteamAchievements arg0)
    {
        if(_hasFailedParkour) return;
        base.AddAch(arg0);
    }
}