using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A small scriptable object script to store all the sounds in the game with the Audio Manager

[CreateAssetMenu(fileName = "Game Sounds", menuName = "ScriptableObjects/Create new game sounds", order = 1)]

public class GameSounds : ScriptableObject
{
    [SerializeField] private Sound[] soundsInGame; //Array to store all game sounds

    public Sound[] ReturnSoundsInGame()
    {
        if(soundsInGame != null) //If the sounds array is null, we return null
        {
            return soundsInGame;
        }

        return null;
    }
}
