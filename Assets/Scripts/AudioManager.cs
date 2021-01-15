using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance = null;
    public static AudioManager Instance => instance;

    public bool SoundOn = true;

    private Dictionary<Sounds, Sound> sounds;

    public void Awake()
    {
        DontDestroyOnLoad(instance ??= this);
    }
    private void Start()
    {
        sounds = new Dictionary<Sounds, Sound> {
            { Sounds.Click, new Sound { Clip =  Resources.Load<AudioClip>("Sounds/Click_Clip")} },
            { Sounds.Die, new Sound { Clip = Resources.Load<AudioClip>("Sounds/Die_Clip")} },
            { Sounds.Move, new Sound { Clip = Resources.Load<AudioClip>("Sounds/Move_Clip")} },
            { Sounds.Switch, new Sound { Clip = Resources.Load<AudioClip>("Sounds/Switch_Clip")} },
            { Sounds.Win, new Sound { Clip = Resources.Load<AudioClip>("Sounds/Win_Clip")} },
        };

        foreach (var sound in sounds.Values)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    public void Play(Sounds sound)
    {
        if (SoundOn)
            sounds[sound]?.Source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip Clip { get; set; }

    public float Volume { get; set; } = 1;

    public float Pitch { get; set; } = 1;

    public bool Loop { get; set; } = false;

    public AudioSource Source { get; set; }
}
public enum Sounds
{
    Move,
    Die,
    Win,
    Switch,
    Click,
}

