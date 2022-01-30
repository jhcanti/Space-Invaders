using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Sound[] sounds;
    
    private Dictionary<string, AudioSource> _audioSources;

    
    private void Awake()
    {
        _audioSources = new Dictionary<string, AudioSource>();
        foreach (var sound in sounds)
        {
            if (sound.IsMusic) continue;
            
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.Clip;
            source.playOnAwake = sound.PlayOnAwake;
            source.loop = sound.Loop;
            source.volume = sound.Volume;
            _audioSources[sound.SoundName] = source;
        }
    }


    public void PlayMusic(string musicName)
    {
        var s = Array.Find(sounds, sound => sound.SoundName == musicName);
        if (s == null)
        {
            throw new Exception($"Sound with name {musicName} does not exists");
        }
        
        if (music.isPlaying && music.clip.name.Equals(musicName))
            return;
        
        music.Stop();
        music.clip = s.Clip;
        music.playOnAwake = s.PlayOnAwake;
        music.loop = s.Loop;
        music.volume = s.Volume;
        music.Play();
    }
    
    
    public void Play(string soundName)
    {
        var s = Array.Find(sounds, sound => sound.SoundName == soundName);
        if (s == null)
        {
            throw new Exception($"Sound with name {soundName} does not exists");
        }
        
        if (!_audioSources.TryGetValue(soundName, out var source))
        {
            throw new Exception($"Sound with name {soundName} does not exists");
        }
        
        if (source.isPlaying)
            source.Stop();
        
        source.Play();
    }
}
