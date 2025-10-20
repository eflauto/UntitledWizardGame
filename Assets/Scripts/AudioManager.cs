using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public string[] audioTags;
    public AudioClip[] audioClips;
    private Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();
    
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
        _audioSource.volume = MainManager.Instance.sfxVolume / 100f;
        _audioSource.PlayOneShot(_audioDictionary[audioTag]);
    }

    public void PlaySound(string audioTag, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_audioDictionary[audioTag], position, MainManager.Instance.sfxVolume / 100f);
    }
}
