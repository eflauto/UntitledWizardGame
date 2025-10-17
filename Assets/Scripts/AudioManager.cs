using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public string[] audioTags;
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> _audioDictionary;
    
    private AudioSource _audioSource;
    
    private void Start()
    {
        for (var i = 0; i < audioTags.Length; i++)
        {
            _audioDictionary.Add(audioTags[i], audioClips[i]);
        }
        
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string audioTag)
    {
        _audioSource.volume = MainManager.Instance.sfxVolume;
        _audioSource.clip = _audioDictionary[audioTag];
        _audioSource.Play();
    }
}
