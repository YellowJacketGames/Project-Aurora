using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle player sounds in the Player Animations
public class PlayerSounds : MonoBehaviour
{
    public void PlayPlayerSound(string soundName) //TODO: Why the player has 2 instances of this script assigned? 
    {
        AudioManager.instance.PlayWithRandomPitch(0.8f, 1.2f, soundName);
    }
}
