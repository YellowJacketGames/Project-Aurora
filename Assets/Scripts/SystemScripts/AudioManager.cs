using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    // To reproduce a sound: Play("The name of the sound");

    //Singleton of the audio manager
    public static AudioManager instance;

    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] Sound[] sounds;
    [SerializeField] private GameSounds allSounds;


    [Header("Filters")]
    [SerializeField] AudioLowPassFilter filter;
    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        #endregion


        // Set-up
        foreach (Sound sound in sounds)
        {
            // Create a GameObject with an AudioSource component dor each Sound asset
            GameObject audioSource = new GameObject("Audio source" + sound.name);
            audioSource.transform.SetParent(transform);
            sound.sourceObject = audioSource;
            sound.source = audioSource.AddComponent<AudioSource>();
            
            // Sound properties
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;


            sound.originalVolume = sound.source.volume;


            switch (sound.soundType)
            {
                case SoundType.SFX:
                    if (sfxMixer != null)
                    sound.source.outputAudioMixerGroup = sfxMixer;
                    break;
                case SoundType.Music:
                    if (musicMixer != null)
                    sound.source.outputAudioMixerGroup = musicMixer;
                    break;
                default:
                    break;
            }

            if (sound.playOnAwake) //Since we're spawning this sounds on start, they do not execute the awake method, so we play them anyway.
                sound.source.Play();

        }
    }

    //This region holds the methods related to playing and stopping sounds
    #region Using sounds

    // Play method, if the sound isn't found, the console debugs a warning.

    #region Play
    public void Play (string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        if(newSound != null)
        {
            newSound.source.PlayOneShot(newSound.clip, newSound.volume);
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    public void PlayLoop(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        if (newSound != null)
        {
            newSound.source.Play();
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    public void PlayLoop(Sound newSound)
    {
        if (newSound != null)
        {
            newSound.source.Play();
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    public void Play(Sound newSound)
    {
        if (newSound != null)
        {
            newSound.source.PlayOneShot(newSound.clip);
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    #endregion

    #region PlayWithRandomPitch

    public void PlayWithRandomPitch(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);
        float randomPitch = Random.Range(0.8f, 1.5f);

        if (newSound != null)
        {
            newSound.source.pitch = randomPitch;
            Play(newSound);
            ResetSound(newSound);
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    public void PlayWithRandomPitch(float min, float max, string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);
        float randomPitch = Random.Range(min, max);

        if (newSound != null)
        {
            newSound.source.pitch = randomPitch;
            Play(newSound);
            ResetSound(newSound);
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }

    public void PlayWithRandomPitch(float min, float max, Sound newSound)
    {
        float randomPitch = Random.Range(min, max);
        if (newSound != null)
        {
            newSound.source.pitch = randomPitch;
            Play(newSound);
            ResetSound(newSound);
        }
        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }

    #endregion

    #region Stop
    // Stop method, if the sound isn't found, the console debugs a warning.
    public void Stop(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        if(newSound != null)
        {
            newSound.source.Stop();
        }

        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }

    public void Stop(Sound newSound)
    {
        if (newSound != null)
        {
            newSound.source.Stop();
        }

        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }
    #endregion

    #region ResetSound
    // Reset method, resets de volume of input sound, if the sound isn't found, the console debugs a warning
    public void ResetSound(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        newSound.source.volume = newSound.volume;
        newSound.source.pitch = newSound.pitch;
    }

    public void ResetSound(Sound newSound)
    {
        newSound.source.volume = newSound.volume;
        newSound.source.pitch = newSound.pitch;
    }


    #endregion

    #endregion

    //This region holds the methods related to fading sounds
    #region Fade In and Fade Out Sounds

    //Fade out method, Progressively lowers the volume of the sound, until it stops
    IEnumerator FadeOutSound(Sound sound)
    {
        while(sound.source.volume > 0)
        {
            sound.source.volume -= Time.deltaTime;
            yield return null;
        }

        sound.source.volume = 0;
        Stop(sound.name);
        ResetSound(sound.name);
    }


    //Fade in method, Progressively increases the volume of the sound, until it stops

    IEnumerator FadeInSound(Sound sound, bool loop)
    {
        Debug.Log("FadeIn");
        sound.source.volume = 0;

        if (loop)
        {
            PlayLoop(sound.name);
        }
        else
        {
            Play(sound.name);
        }

        while (sound.source.volume <= sound.originalVolume)
        {
            sound.source.volume += Time.deltaTime;
            yield return null;
        }

        sound.source.volume = sound.originalVolume;
    }

    //Calls the fade out corroutine
    public void FadeOut(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        if (newSound != null)
        {
            StartCoroutine(FadeOutSound(newSound));
        }

        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }

    //Calls the fade in corroutine

    public void FadeIn(string name, bool loop)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);

        if (newSound != null)
        {
            StartCoroutine(FadeInSound(newSound, loop));
        }

        else
        {
            Debug.Log("Sound: " + name + " not found");
        }
    }

    
    #endregion


    public void ActivateFilter()
    {
        filter.enabled = true;
    }

    public void DeactivateFilter()
    {
        filter.enabled = false;
    }
}
