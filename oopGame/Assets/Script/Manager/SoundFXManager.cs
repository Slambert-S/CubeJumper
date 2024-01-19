using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn game object 
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position,Quaternion.identity);
        //assigne the audio clip

        audioSource.clip = audioClip;
        //assigne volume

        audioSource.volume = volume;
        // play sound
        audioSource.Play();
        // get lenght of FX volume
        float clipLenght = audioSource.clip.length;
        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }
}
