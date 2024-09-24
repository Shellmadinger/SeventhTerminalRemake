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
        DontDestroyOnLoad(this);
        foreach (Sounds s in soundsArray)
        {
            s.soundSource = gameObject.AddComponent<AudioSource>();
            s.soundSource.clip = s.soundClip;
        }
        
    }

    // Update is called once per frame
    public void Play(string soundName)
    {
        Sounds s = Array.Find(soundsArray, sounds => sounds.name == soundName);
        s.soundSource.Play();
    }
}
