using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Script to manage stuff exclusive to the door level such as music and other stuff
public class DoorLevelManager : LevelManager
{
    [Header("Door Level Component")]
    [SerializeField] private AudioSource layerSongBase;
    [SerializeField] private AudioSource[] layeredSong;
    [SerializeField] private float layerTransitionDuration;
    [SerializeField] private float layerDefaultVolume;

    int test;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(layeredSong[test] != null)
            {
                FadeInMusicLayer(test);
                test++;
            }
        }
    }
    public void FadeInMusicLayer(int layer)
    {
        StartCoroutine(FadeInMusic(layeredSong[layer]));
    }


    IEnumerator FadeInMusic (AudioSource source)
    {
        float time = 0;

        while(time < layerTransitionDuration)
        {
            float value = Mathf.Lerp(source.volume, layerDefaultVolume, time / layerTransitionDuration);
            source.volume = value;
            time += Time.deltaTime;

            yield return null;
        }

        source.volume = layerDefaultVolume;
    }
}
