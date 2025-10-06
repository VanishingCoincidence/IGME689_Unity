using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        Debug.Log(name + "playing");
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        temp.source.Play();
    }

    public void Stop(string name)
    {
        Debug.Log(name + "playing");
        Sound temp = Array.Find(sounds, sound => sound.name == name);
        temp.source.Stop();
    }
}
