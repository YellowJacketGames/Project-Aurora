using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamAchievements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
        SteamUserStats.StoreStats();
        // SteamUserStats.ResetAllStats(true); resets all stats from users
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
