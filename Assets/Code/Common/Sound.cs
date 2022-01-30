using UnityEngine;

[System.Serializable]
public class Sound
{
    public string SoundName;
    public AudioClip Clip;
    public bool PlayOnAwake;
    public bool Loop;
    public bool IsMusic;
    
    [Range(0f, 1f)]
    public float Volume;
}
