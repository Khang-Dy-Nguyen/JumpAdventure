using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(.1f, 3f)] public float pitch = 1f;
    [HideInInspector] public AudioSource source;
    public bool loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] soundEffects;  // SFX: shoot, hit, invaderDie
    public Sound[] musicTracks;   // Nhạc nền

    private int currentMusic = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Init audio sources
        foreach (var s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (var m in musicTracks)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }
    }

    public void PlaySFX(string name)
    {
        var s = Array.Find(soundEffects, x => x.name == name);
        if (s != null) s.source.Play();
        else Debug.LogWarning("Sound effect not found: " + name);
    }

    public void PlayMusic(string name)
    {
        if (currentMusic >= 0) musicTracks[currentMusic].source.Stop();
        var m = Array.Find(musicTracks, x => x.name == name);
        if (m != null)
        {
            m.source.Play();
            currentMusic = Array.IndexOf(musicTracks, m);
        }
    }
}