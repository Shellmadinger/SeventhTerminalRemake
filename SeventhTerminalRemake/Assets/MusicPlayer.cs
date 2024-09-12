using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public GameManager currentState;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioSource musicPlayer;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState.gameState);
        if (currentState.gameState == 1)
        {
            if (!musicPlayer.isPlaying)
            {
                musicPlayer.Play();
            }
           
        }

        if(currentState.gameState == 2)
        {
            musicPlayer.Stop();
        }
    }
}
