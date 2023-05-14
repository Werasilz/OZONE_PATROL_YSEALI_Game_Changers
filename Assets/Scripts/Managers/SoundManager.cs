using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioSource> soundEffectSources;

    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> m_soundEffectClips;
    public List<AudioClip> soundEffectClips => m_soundEffectClips;
    [SerializeField] private List<AudioClip> m_musicClips;
    public List<AudioClip> musicClips => m_musicClips;

    public override void Awake()
    {
        base.Awake();
    }
    
    private void Start() 
    {
      PlayMusic(0);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        // Check if any sound effect sources are available
        bool foundAvailableSource = false;
        foreach (AudioSource source in soundEffectSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                foundAvailableSource = true;
                break;
            }
        }

        // If no available source was found, create a new one and play the sound effect on it
        if (!foundAvailableSource)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            newSource.Play();
            soundEffectSources.Add(newSource);
        }
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < m_musicClips.Count)
        {
            musicSource.clip = m_musicClips[index];
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
