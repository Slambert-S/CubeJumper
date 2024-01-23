using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetMasterVolume (float level)
    {
        //audioMixer.SetFloat("masterVolume", level);<
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20f);
    }
    public void SetSoundFXVolume(float level)
    {
        _audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVolume(float level)
    {
        
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
       
    }

    public float GetMasterVolumeLevel()
    {
        bool result = _audioMixer.GetFloat("masterVolume", out float value);
        float newValue = Mathf.Pow(10, (value / 20));
        //Debug.Log(" calculated value :" + newValue + "  Original value : " + level);
        return newValue;
    }

    public float GetSoundFXVolumeLevel()
    {
        bool result = _audioMixer.GetFloat("soundFXVolume", out float value);
        float newValue = GetLevelFromVolume(value);
        //Debug.Log(" calculated value :" + newValue + "  Original value : " + level);
        return newValue;
    }

    public float GetMusicVolumeLevel()
    {
        bool result = _audioMixer.GetFloat("musicVolume", out float value);
        float newValue = GetLevelFromVolume(value);
        //Debug.Log(" calculated value :" + newValue + "  Original value : " + level);
        return newValue;
    }

    private float GetLevelFromVolume(float volume)
    {
        float newValue = Mathf.Pow(10, (volume / 20));
        return newValue;
    }

}
