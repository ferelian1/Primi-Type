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
        loseMusic.Play();
    }
}
