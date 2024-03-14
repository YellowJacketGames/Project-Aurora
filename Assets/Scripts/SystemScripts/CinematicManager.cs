using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CinematicManager : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    float videoLength;

    public bool skip;
    void Start()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
        videoLength = (float)player.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        videoLength -= Time.deltaTime;

        if (videoLength <= Time.deltaTime || skip)
        {
            GameManager.instance.SetLevelToLoad("Level1_VisualNovel");
            GameManager.instance.currentTransitionManager.NextLevel();
            player.Stop();
        }
    }

    public void Load()
    {
        SceneManager.LoadScene("LoadingScreen");

    }

    public void SkipCutscene()
    {
        skip = true;
    }
}
