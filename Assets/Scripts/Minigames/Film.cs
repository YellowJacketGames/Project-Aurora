using UnityEngine;
using UnityEngine.Video;

public class Film : MonoBehaviour
{
    public string videoFileName = "videoplayback.mp4";
    public string audioFileName = "videoplayback.mp4";
    [SerializeField] private VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    public float minDistance = 5.0f;
    public float maxDistance = 25.0f;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

        videoPlayer.playOnAwake = false;
        videoPlayer.Stop();
        audioSource.clip = audioClip;
        // videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.isLooping = true;
        // videoPlayer.controlledAudioTrackCount = 1;
        // videoPlayer.SetDirectAudioVolume(0, 1.0f);

        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.volume = .2f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.loop = true;

        videoPlayer.SetTargetAudioSource(0, audioSource);

        renderTexture = new RenderTexture(1920, 1080, 0);
        videoPlayer.targetTexture = renderTexture;

        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = renderTexture;
        }

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        videoPlayer.Play();
        audioSource.Play();
    }
}