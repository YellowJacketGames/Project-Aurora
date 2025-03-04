public class SpeakToAllAchievement : SteamAchievementTrigger
{
    private int _currentPeopleTalked;
    private const int DesiredPeopleTalked = 5;


    public void NewPeopleTalked()
    {
        _currentPeopleTalked++;
        if (_currentPeopleTalked >= DesiredPeopleTalked)
            EventsManager.InvokeAchievementUnlock(achievement);
    }
}