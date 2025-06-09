using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderController : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider masterSlider;
    public Slider musicSlider;

    void Start()
    {
        masterSlider.value = MainMenuAudio.Instance.GetMasterVolume();
        musicSlider.value  = MainMenuAudio.Instance.GetMusicVolume();

        masterSlider.onValueChanged.AddListener(MainMenuAudio.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(MainMenuAudio.Instance.SetMusicVolume);
    }
}