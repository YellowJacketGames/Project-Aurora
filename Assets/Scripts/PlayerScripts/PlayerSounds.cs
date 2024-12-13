using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle player sounds in the Player Animations
public class PlayerSounds : MonoBehaviour
{
    public void PlayPlayerSound(string soundName) //TODO: Why the player has 2 instances of this script assigned? 
    {
        //return;
        AudioManager.instance.PlayWithRandomPitch(0.75f, 1.6f, soundName);
    }

    public void PlayPlayerSoundRunning(string soundName) //TODO: Why the player has 2 instances of this script assigned? 
    {
        //return; //turns out if using a blend tree its not possible to invoke sound like this, instead a 1 only model with every animation in it (not as we have it now) should be used and for each animation a curve applied, then read the curve dots and play sound based on it
        AudioManager.instance.PlayWithRandomPitch(0.7f, 1.1f, soundName);
    }
}