using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderSetUp : MonoBehaviour
{
    public SoundMixerManager mixerLink;
    [SerializeField] private SliderType sliderType;
    private Slider mainSlider;
    // Start is called before the first frame update
    void Start()
    {
        

       
    }

    private void Awake()
    {
        mixerLink = FindObjectOfType<SoundMixerManager>();
        mainSlider = this.GetComponent<Slider>();

        LinkSliderToMixer();
        SetSliderPositonRelativeToMixer();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void LinkSliderToMixer()
    {
        switch (sliderType)
        {
            case SliderType.Master:
                mainSlider.onValueChanged.AddListener(delegate { mixerLink.SetMasterVolume(mainSlider.value); });
                break;
            case SliderType.Music:
                mainSlider.onValueChanged.AddListener(delegate { mixerLink.SetMusicVolume(mainSlider.value); });
                break;
            case SliderType.SoundFX:
                mainSlider.onValueChanged.AddListener(delegate { mixerLink.SetSoundFXVolume(mainSlider.value); });
                break;
            default:
                break;

        }      
    }

    private void SetSliderPositonRelativeToMixer()
    {
        switch (sliderType)
        {
            case SliderType.Master:
                mainSlider.value = mixerLink.GetMasterVolumeLevel();
               // mainSlider.onValueChanged.AddListener(delegate { mixerLink.SetMasterVolume(mainSlider.value); });
                break;
            case SliderType.Music:
                mainSlider.value = mixerLink.GetMusicVolumeLevel();
                
                break;
            case SliderType.SoundFX:
                
                mainSlider.value = mixerLink.GetSoundFXVolumeLevel();

                break;
            default:
                break;

        }
    }
    public enum SliderType
    {
        Master,
        Music,
        SoundFX
    }
}


