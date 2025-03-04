public class FlawlessNoRunAchievement : SteamAchievementTrigger
{
    private bool _hasFailedNoRun;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.currentController.playerMovementComponent.OnTargetSpeedChanged += FailedNoRun;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.instance.currentController.playerMovementComponent.OnTargetSpeedChanged -= FailedNoRun;
    }

    private void FailedNoRun(float arg0)
    {
        if (arg0 > 9)
            _hasFailedNoRun = true;
    }

    protected override void AddAch(SteamAchievements arg0)
    {
        if (_hasFailedNoRun) return;
        base.AddAch(arg0);
    }
}