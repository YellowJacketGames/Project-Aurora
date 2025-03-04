public class FlawlessDoorAchievement : SteamAchievementTrigger
{
    private bool _hasFailedDoor;

    public void FailedDoor()
    {
        _hasFailedDoor = true;
    }

    protected override void AddAch(SteamAchievements arg0)
    {
        if(_hasFailedDoor) return;
        base.AddAch(arg0);
    }
}