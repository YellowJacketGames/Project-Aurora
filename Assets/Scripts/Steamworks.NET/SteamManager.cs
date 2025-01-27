using System;
using UnityEngine;
using Steamworks;

public enum SteamAchievements
{
    Intro, 
    PassportCheck, 
    FindAllKeys, 
    FindAllPortraitist, 
    CrossTheBridge,
    Platinum
}

public class SteamManager : MonoBehaviour
{
    public static SteamManager Instance;
    private uint appID = 000000;
    private bool connectedToSteam = false;
    private int totalNumberOfAchievements = 5;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        try
        {
            SteamClient.Init(appID);
            connectedToSteam = true; 
        }
        catch (Exception e)
        {
            connectedToSteam = false;   
        }
    }

    private void Update()
    {
        if(!connectedToSteam) return;
        SteamClient.RunCallbacks();
    }

    public void UnlockAchievement(SteamAchievements achievement)
    {
        if(!connectedToSteam)return;
        var ach = new Steamworks.Data.Achievement("Achievement_" + (int)achievement);
        ach.Trigger();
        CheckForPlatinumAchievement();
    }

    private void CheckForPlatinumAchievement()
    {
        var numberOfAchievementsNeeded = totalNumberOfAchievements - 1;
        var numberOfUnlockedAchievements = 0;
        
        for (var i = 0; i < numberOfAchievementsNeeded; i++)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + i);
            if (ach.State)
                numberOfUnlockedAchievements++;
        }

        if (numberOfUnlockedAchievements != numberOfAchievementsNeeded) return;
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + (int)SteamAchievements.Platinum);
            ach.Trigger();
        }

    }

    private void ResetAllAchievements()
    {
        for (int i = 0; i < totalNumberOfAchievements; i++)
        {
            var ach = new Steamworks.Data.Achievement("Achievement_" + i);
            ach.Clear();
        }
    }
    public void DisconnectFromSteam()
    {
        if(!connectedToSteam) return;
        SteamClient.Shutdown();
            
    }
}