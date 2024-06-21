using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private List<AudioClipEntry> audioClipEntries; // List of AudioClipEntry to populate the dictionary
    private Dictionary<string, AudioClip> audioEffects = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want it to persist across scenes

            InitializeAudioEffects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioEffects()
    {
        // Populate the dictionary with audio clips
        foreach (var entry in audioClipEntries)
        {
            if (entry.clip != null && !string.IsNullOrEmpty(entry.name))
            {
                audioEffects[entry.name] = entry.clip;
            }
        }
    }

    // Play a music clip
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // Play a sound effect by name
    public void PlaySFX(string effectName)
    {
        if (audioEffects.TryGetValue(effectName, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{effectName}' not found!");
        }
    }

    // Stop the current music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Stop all sound effects
    public void StopAllSFX()
    {
        sfxSource.Stop();
    }

    // Set music volume
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Set SFX volume
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


[System.Serializable]
public class AudioClipEntry
{
    public string name;
    public AudioClip clip;
}