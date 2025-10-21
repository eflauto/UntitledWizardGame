using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public string[] audioTags;
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> _audioDictionary = new();
    
    private AudioSource _audioSource;
    
    protected void Start()
    {
        for (var i = 0; i < audioTags.Length; i++)
        {
            _audioDictionary.Add(audioTags[i], audioClips[i]);
        }
        
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string audioTag)
    {
        _audioSource.volume = MainManager.Instance.sfxVolume / 100f;
        _audioSource.PlayOneShot(_audioDictionary[audioTag]);
    }

    public void PlaySound(string audioTag, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_audioDictionary[audioTag], position, MainManager.Instance.sfxVolume / 100f);
    }

    public void PlayLoop(string audioTag)
    {
        _audioSource.loop = true;
        PlaySound(audioTag);
    }

    public void PlayLoop(string audioTag, Vector3 position)
    {
        _audioSource.loop = true;
        PlaySound(audioTag, position);
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }

    public bool SoundPlaying()
    {
        return _audioSource.isPlaying;
    }

    public void SetSpatializeSettings(float spatialize, int distance)
    {
        _audioSource.spatialBlend = spatialize;
        _audioSource.maxDistance = distance;
    }
    
    public void SetVolume()
    {
        _audioSource.volume = MainManager.Instance.musicVolume / 100f;
    }
}
