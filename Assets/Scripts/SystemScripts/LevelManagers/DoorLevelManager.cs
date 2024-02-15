using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ink.Runtime;

//Script to manage stuff exclusive to the door level such as music and other stuff
public class DoorLevelManager : LevelManager
{
    [Header("Door Level Song Components")]
    [SerializeField] private AudioSource layerSongBase;
    [SerializeField] private AudioSource[] layeredSong;
    [SerializeField] private float layerTransitionDuration;
    [SerializeField] private float layerDefaultVolume;
    [SerializeField] public Speaker shadowSpeaker;
    [Space]
    [Header("Door order")]
    [SerializeField] private DoorElementLevel3[] doorOrder;
    [SerializeField] private Transform[] orderedPositions;
    [Space]
    [Header("Door correct texts")]
    [SerializeField] private TextAsset[] correctText;
    [Space]
    [SerializeField] public Transform initialPosition;
    private int doorIndex;

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

    IEnumerator FadeOutMusic(AudioSource source)
    {
        float time = 0;

        while (time < layerTransitionDuration)
        {
            float value = Mathf.Lerp(source.volume, 0, time / layerTransitionDuration);
            source.volume = value;
            time += Time.deltaTime;

            yield return null;
        }

        source.volume = 0;
    }
    public bool CheckIfCorrectDoor(DoorElementLevel3 door)
    {
        if(door == doorOrder[doorIndex])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AdvanceDoorConversation()
    {
        GameManager.instance.currentController.playerConversationComponent.SetCurrentDialogue(new Story(correctText[doorIndex].text));
        GameManager.instance.currentController.playerConversationComponent.SetNewSpeaker(shadowSpeaker);
        GameManager.instance.currentController.ChangeState(PlayerState.Conversation);
    }

    public void AdvanceMusic()
    {
        FadeInMusicLayer(doorIndex);
    }
    public void AdvancePlayerPosition()
    {
        GameManager.instance.currentController.transform.position = orderedPositions[doorIndex].position;
    }

    public void AdvanceDoorIndex()
    {
        doorIndex++;
    }

    public void ProgressLevel()
    {
        AdvanceMusic();
        Invoke("AdvancePlayerPosition", 1f);
        Invoke("AdvanceDoorIndex", 1.5f);
    }
    public void ResetDoorLevel()
    {
        doorIndex = 0;

        for (int i = 0; i < layeredSong.Length; i++)
        {
            StartCoroutine(FadeOutMusic(layeredSong[i]));
        }

        Invoke("ResetPlayer", 1f);
    }

    public void ResetPlayer()
    {
        GameManager.instance.currentController.transform.position = initialPosition.position;

    }
}
