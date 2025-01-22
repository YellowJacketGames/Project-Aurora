using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource audioPlayer;

    [Space(10)] [SerializeField] private VideoClip logoVideo;
    [SerializeField] private bool triggerLogo = false;
    private bool onlyLogo = false;


    [Space(10)] bool videoFinishedPlaying = false;
    [SerializeField] bool videoStartedPlaying = false;

    private void Start()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
        videoPlayer.clip = GameManager.instance.GetLoadingScreenVideoClip();
        if (videoPlayer.clip == null) //if null, only trigger logo
        {
            triggerLogo = false;
            onlyLogo = true;
            videoPlayer.clip = logoVideo;
        }
        else
            triggerLogo = true;

        audioPlayer.clip = GameManager.instance.GetLoadingScreeAudioClip();

        if (videoPlayer.clip)
            videoPlayer.Play();
        if (audioPlayer.clip)
            audioPlayer.Play();
        StartCoroutine(LoadLevel(GameManager.instance.GetCurrentLevelName()));
    }


    public void Update()
    {
        if (!videoPlayer.isPlaying && videoPlayer.time >= (videoPlayer.clip.length * 0.9f))
        {
            if (onlyLogo)
            {
                videoFinishedPlaying = true;
            }
            else
            {
                if (triggerLogo && logoVideo != null)
                {
                    videoPlayer.clip = logoVideo;
                    videoPlayer.Play();
                    videoFinishedPlaying = false;
                    triggerLogo = false;
                    onlyLogo = true;
                }
            }

        }
    }

    IEnumerator LoadLevel(string name)
    {
        yield return new WaitForSeconds(1.1f);
        AsyncOperation load = SceneManager.LoadSceneAsync(name);
        load.allowSceneActivation = false;

        while (!load.isDone)
        {
            if (load.progress >= 0.90f && videoFinishedPlaying)
            {
                Debug.Log("Finished Loading");
                load.allowSceneActivation = true;
            }

            Debug.Log("Loading");
            yield return null;
        }
    }
}