using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
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
        if (!winMusic.isPlaying)  // Pastikan hanya diputar jika belum ada musik yang sedang diputar
        {
            winMusic.Play();
        }
        else
        {
            Debug.Log("Musik WIN sudah diputar.");
        }
    }
    public void Losing()
    {
        if (!loseMusic.isPlaying)  // Pastikan hanya diputar jika belum ada musik yang sedang diputar
        {
            loseMusic.Play();
        }
        else
        {
            Debug.Log("Musik LOSE sudah diputar.");
        }
    }

}
