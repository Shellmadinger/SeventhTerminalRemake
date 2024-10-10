using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] soundsArray;
    // Start is called before the first frame update
    void Awake()
    {
        //We need this audio manager for certain scenes, so we don't destroy the manager on a scene transition
        DontDestroyOnLoad(this);
        //for each sound in the sound array, we add an audio source and the associate clip provided by me
        foreach (Sounds s in soundsArray)
        {
            s.soundSource = gameObject.AddComponent<AudioSource>();
            s.soundSource.clip = s.soundClip;
        }
        
    }

    public void Play(string soundName)
    {
        //when playing a sound, we just find it in the sound array and play it from the source
        Sounds s = Array.Find(soundsArray, sounds => sounds.name == soundName);
        s.soundSource.Play();
    }
}
