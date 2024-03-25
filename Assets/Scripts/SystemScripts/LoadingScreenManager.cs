using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] bool test = false;
    [SerializeField] bool test2 = false;

    private void Start()
    {
        GameManager.instance.currentTransitionManager.SetFadeOut();
        
        if(GameManager.instance.GetLoadingScreenClip() != null)
        {

        }
        player.clip = GameManager.instance.GetLoadingScreenClip();
        Invoke("ActivateTimer", 1f);
    }
    
    public void ActivateTimer()
    {
        BeginLoad();
        test2 = true;
    }
    public void Update()
    {
        if (test2)
        {
            if (!player.isPlaying)
            {
                test = true;
            }
        }
    }
    public void BeginLoad()
    {
        StartCoroutine(LoadLevel(GameManager.instance.GetCurrentLevelName()));
    }
    IEnumerator LoadLevel(string name)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation load = new AsyncOperation();
        load = SceneManager.LoadSceneAsync(name);
        load.allowSceneActivation = false;

        while (!load.isDone)
        {
            if(load.progress >= 0.9f && test)
            {
                Debug.Log("Finished Loading");

                load.allowSceneActivation = true;
            }
            Debug.Log("Loading");
            yield return null;
        }
        

    }
}
