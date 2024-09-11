using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    //Fields for Ui Elements
    [SerializeField] GameObject gamePlayUI;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameManager currentState;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioSource gameOverAudio;
    bool soundHasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set the necessary elements at start
        gamePlayUI.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState.gameState == 2)
        {
            if (soundHasPlayed == false)
            {
                gameOverAudio.PlayOneShot(gameOverClip);
                soundHasPlayed = true;
            }
            //Pull up the game over screen when the state changes
            gamePlayUI.SetActive(false);
            gameOverScreen.SetActive(true);
            
        }
    }
}
