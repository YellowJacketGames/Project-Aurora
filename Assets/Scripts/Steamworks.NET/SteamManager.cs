using System;
using UnityEngine;
using Steamworks;

public enum SteamAchievements
{
    ACH_0, //  FirstJourney
    ACH_1, // PASSPORT
    ACH_2, //SpeakAllCharactersLvl2
    ACH_3, //FindGamingDoorLvl3
    ACH_4, //FinishOneShotLvl3
    ACH_5, //CrossTheBridge
    ACH_6, //FinishOneShotLvl4
    ACH_7, //SolveCentralParkLabyrinth
    ACH_8, //FinishConeyIslandGamesUnder3Min
    ACH_9, //SeeEntireFilmConeyIsland
    ACH_10, //reach top of harlem
    ACH_11, //reach bot of harlem
    ACH_12, //ClimbChryslerBuildingWalkingOnly
    ACH_13, //FindAllKeys
    ACH_14, //end of journey
    ACH_15 //Platinum
}

public class SteamManager : MonoBehaviour
{
    public static SteamManager Instance;
    private uint appID = 3570500;
    private bool connectedToSteam = false;
    private int totalNumberOfAchievements = 16;

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
        if (!connectedToSteam) return;
        SteamClient.RunCallbacks(); 
    }

    public void UnlockAchievement(SteamAchievements achievement)
    {
        if (!connectedToSteam) return;
        var ach = new Steamworks.Data.Achievement("ACH_" + (int)achievement);
        ach.Trigger();
        CheckForPlatinumAchievement();
    }

    private void CheckForPlatinumAchievement()
    {
        var numberOfAchievementsNeeded = totalNumberOfAchievements - 1;
        var numberOfUnlockedAchievements = 0;

        for (var i = 0; i < numberOfAchievementsNeeded; i++)
        {
            var ach = new Steamworks.Data.Achievement("ACH_" + i);
            if (ach.State)
                numberOfUnlockedAchievements++;
        }

        if (numberOfUnlockedAchievements != numberOfAchievementsNeeded) return;
        {
            var ach = new Steamworks.Data.Achievement("ACH_" + (int)SteamAchievements.ACH_15);
            ach.Trigger();
        }
    }

    [ContextMenu("RESET")]
    private void ResetAllAchievements()
    {
        for (int i = 0; i < totalNumberOfAchievements; i++)
        {
            var ach = new Steamworks.Data.Achievement("ACH_" + i);
            ach.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        DisconnectFromSteam();
    }

    [ContextMenu("DISCONNECT")]
    public void DisconnectFromSteam()
    {
        if (!connectedToSteam) return;
        SteamClient.Shutdown();
    }
}