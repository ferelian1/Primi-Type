using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAudioSettings : MonoBehaviour
{
    [Header("Assign Slider UI di Pause Panel")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;

    private bool listenersAdded = false;

    void OnEnable()
    {
        if (MainMenuAudio.Instance == null)
        {
            Debug.LogWarning("[PauseAudioSettings] MainMenuAudio.Instance is null");
            return;
        }
        // Remove listener lama jika ada
        if (listenersAdded)
        {
            masterSlider.onValueChanged.RemoveListener(OnMasterSliderChanged);
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        }
        // Baca saved pref
        masterSlider.value = MainMenuAudio.Instance.GetMasterVolume();
        musicSlider.value  = MainMenuAudio.Instance.GetMusicVolume();

        // Tambah listener
        masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        listenersAdded = true;

        Debug.Log("[PauseAudioSettings] OnEnable: init sliders to saved values");
    }

    void OnDisable()
    {
        if (!listenersAdded) return;
        masterSlider.onValueChanged.RemoveListener(OnMasterSliderChanged);
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        listenersAdded = false;
    }

    private void OnMasterSliderChanged(float val)
    {
        MainMenuAudio.Instance.SetMasterVolume(val);
    }
    private void OnMusicSliderChanged(float val)
    {
        MainMenuAudio.Instance.SetMusicVolume(val);
    }
}
