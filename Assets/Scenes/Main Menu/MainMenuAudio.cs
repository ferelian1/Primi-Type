using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuAudio : MonoBehaviour
{
    public static MainMenuAudio Instance { get; private set; }

    [Header("Mixer & Parameter Names")]
    public AudioMixer mixer;
    public string masterParam = "MasterVolume";
    public string musicParam  = "MusicVolume";

    // Default value 1.0 (linear)
    private float masterVolume = 1f;
    private float musicVolume  = 1f;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            

            // Load saved values (jika ada), default 1
            masterVolume = PlayerPrefs.GetFloat("masterVolume", 1f);
            musicVolume  = PlayerPrefs.GetFloat("musicVolume",  1f);

            ApplyVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // convert linear [0–1] ke decibel dan set ke mixer
    private void ApplyVolumes()
    {
        mixer.SetFloat(masterParam, Mathf.Log10(Mathf.Clamp(masterVolume, 0.0001f, 1f)) * 20f);
        mixer.SetFloat(musicParam,  Mathf.Log10(Mathf.Clamp(musicVolume,  0.0001f, 1f)) * 20f);
    }

    // dipanggil slider Master
    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        ApplyVolumes();
    }

    // dipanggil slider Music
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        ApplyVolumes();
    }

    // bagi UI untuk mendapatkan current value
    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume()  => musicVolume;
}
