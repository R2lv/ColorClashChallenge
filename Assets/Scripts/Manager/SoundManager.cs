using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("ButtonClickSound")]
    public AudioClip buttonClip;
    public AudioSource buttonAudioSource;


    [Space(10)]
    [Header("GameOverSound")]
    public AudioClip winClip;
    public AudioSource winAudioSource;


    public void ButtonClickSound()
    {
        buttonAudioSource.clip = buttonClip;
        buttonAudioSource.Play();
    }

    public void GameOverSound()
    { 
        winAudioSource.clip = winClip;
        winAudioSource.Play();
    }
}
