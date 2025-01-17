using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!SteamManager.Initialized)
            return;
        var name = SteamFriends.GetPersonaName();
        Debug.LogWarning(name);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
