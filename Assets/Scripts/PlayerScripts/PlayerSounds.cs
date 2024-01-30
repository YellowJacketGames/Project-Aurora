using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle player sounds in the Player Animations
public class PlayerSounds : MonoBehaviour
{
    public void PlayPlayerSound(string soundName)
    {
        AudioManager.instance.PlayWithRandomPitch(0.8f, 1.2f, soundName);
    }
}
