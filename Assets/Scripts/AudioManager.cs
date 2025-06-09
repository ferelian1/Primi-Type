using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.Search;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource typingSound;
    [SerializeField] private AudioSource playerHurtSound;
    [SerializeField] private AudioSource winMusic;
    [SerializeField] private AudioSource loseMusic;



    public void Typing()
    {
        typingSound.Play();
    }

    public void PlayerHurting()
    {
        playerHurtSound.Play();
    }
    public void Winning()
    {
        winMusic.Play();
    }
    public void Losing()
    {
        if (!loseMusic.isPlaying)  // Pastikan hanya diputar jika belum ada musik yang sedang diputar
    {
        Debug.Log("WOI IDUP");
        loseMusic.Play();
    }
    else
    {
        Debug.Log("Musik sudah diputar.");
    }
    }
}
