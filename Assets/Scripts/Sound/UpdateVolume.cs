using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UpdateVolume : MonoBehaviour
{
    public bool isMusic;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] private TMP_Text amount;

    public void ChangeVolume()
    {
        if (slider != null)
        {
            if (slider.value > 0)
                mixer.SetFloat("Volume", Mathf.Log10(slider.value) * 20);
            else
                mixer.SetFloat("Volume", -80f);
            if (isMusic)
                GameManager.instance.Data.SetMusicVolume(slider.value);
            else
                GameManager.instance.Data.SetMusicVolume(slider.value);
            UpdatePercentage();
        }
    }

    private void Start()
    {
        slider.value = Mathf.Pow(10, GetVolumeSettings() / 20);
        UpdatePercentage();
        Debug.Log(slider.value);
    }

    private void UpdatePercentage()
    {
        var rounded = Mathf.RoundToInt(slider.value * 100);
        amount.text = rounded.ToString();
        PlaySoundOnStep(rounded);
    }
    private int lastNotifiedValue = -1;
    public void PlaySoundOnStep(int roundedValue)
    {
        if (roundedValue % 10 != 0 || roundedValue == lastNotifiedValue) return;
        AudioManager.instance.PlayWithRandomPitch("Click"); 
        lastNotifiedValue = roundedValue;
    }
    private float GetVolumeSettings()
    {
        return isMusic ? GameManager.instance.Data.musicVolume : GameManager.instance.Data.soundVolume;
    }
    /*float volume; 
        if (mixer.GetFloat("MasterVolume", out volume))
        {
            return volume; 
        }
        else
        {
            Debug.LogWarning("cannot adquire volume value.");
            return -80f; 
        }*/
}