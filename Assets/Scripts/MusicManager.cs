using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip song;
    
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();

        PlayLoop();
    }

    private void PlayLoop()
    {
        _audioSource.loop = true;
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.volume = MainManager.Instance.musicVolume / 100f;
        _audioSource.clip = song;
        _audioSource.Play();
    }

    public void SetVolume()
    {
        _audioSource.volume = MainManager.Instance.musicVolume / 100f;
    }
}
