using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to add the methods of fading in and out simple source apart from the audioManager.
public class SoundFunctions
{

    public IEnumerator FadeInSound(AudioSource source, float targetVolume, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            float value = Mathf.Lerp(source.volume, targetVolume, time / duration);
            source.volume = value;
            time += Time.deltaTime;

            yield return null;
        }

        source.volume = targetVolume;
    }

    public IEnumerator FadeOutSound(AudioSource source, float targetVolume, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            float value = Mathf.Lerp(source.volume, 0, time / duration);
            source.volume = value;
            time += Time.deltaTime;

            yield return null;
        }

        source.volume = 0;
    }

}
