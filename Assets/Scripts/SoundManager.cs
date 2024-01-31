using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Serializable]
    public class Sound
    {
        public string key;
        public AudioClip[] audio;
        public float volume = 1f;
    }

    [SerializeField] public Sound[] sounds;

    private AudioSource[] _sources;

    private void Awake()
    {
        instance = this;

        _sources = new AudioSource[sounds.Length];
    }

    private AudioSource GetSoundSource(string key)
    {
        AudioSource s = null;

        for (var i = 0; i < sounds.Length; i++)
            if (key == sounds[i].key)
            {
                if (_sources[i] == null)
                {
                    if (sounds[i].audio.Length == 0) break;

                    var a = new GameObject().AddComponent<AudioSource>();
                    a.transform.parent = transform;
                    a.playOnAwake = false;

                    var n = UnityEngine.Random.Range(0, sounds[i].audio.Length);
                    a.clip = sounds[i].audio[n];

                    a.volume = sounds[i].volume;

                    _sources[i] = a;
                }

                s = _sources[i];
                break;
            }

        return s;
    }

    public void Play(string key)
    {
        var s = GetSoundSource(key);
        if (s != null) s.Play();
    }

    public void Stop(string key)
    {
        var s = GetSoundSource(key);
        if (s != null) s.Stop();
    }

    public void SetVolume(string key, float value)
    {
        var s = GetSoundSource(key);
        if (s != null) s.volume = value;
    }

    public void SetLoop(string key, bool value)
    {
        var s = GetSoundSource(key);
        if (s != null) s.loop = value;
    }

    public void SetPitch(string key, float value)
    {
        var s = GetSoundSource(key);
        if (s != null) s.pitch = value;
    }

    public static void PlaySound(string key)
    {
        if (instance != null) instance.Play(key);
    }

    public static void StopSound(string key)
    {
        if (instance != null) instance.Stop(key);
    }

    public static void SetSoundVolume(string key, float value)
    {
        if (instance != null) instance.SetVolume(key, value);
    }

    public static void SetSoundLoop(string key, bool value)
    {
        if (instance != null) instance.SetLoop(key, value);
    }

    public static void SetSoundPitch(string key, float value)
    {
        if (instance != null) instance.SetPitch(key, value);
    }
}
