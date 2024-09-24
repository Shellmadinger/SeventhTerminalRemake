using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{

    public string name;

    public AudioClip soundClip;

    [HideInInspector]
    public AudioSource soundSource;
}
