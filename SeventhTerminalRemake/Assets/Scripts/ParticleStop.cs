using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStop : MonoBehaviour
{
    private void Start()
    {
        //For some godforsaken reason, OnParticeSystemStopped won't be called unless all of this extra stuff is here
        //This is not the case all other particle effects that use that function, so idk whats different here
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    private void OnParticleSystemStopped()
    {
        this.gameObject.SetActive(false);
    }
}
