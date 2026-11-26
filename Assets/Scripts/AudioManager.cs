using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume",
            Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume",
            Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
    }
}
