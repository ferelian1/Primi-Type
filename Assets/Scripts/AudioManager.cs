using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource typingSound;



    public void typing()
    {
        typingSound.Play();
    }
}
