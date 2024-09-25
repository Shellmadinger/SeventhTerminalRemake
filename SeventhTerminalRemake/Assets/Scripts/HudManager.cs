using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    //Fields for Ui Elements
    [SerializeField] GameObject gamePlayUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameManager currentState;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioSource gameOverAudio;
    bool soundHasPlayed = false;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set the necessary elements at start
        gamePlayUI.SetActive(true);
        gameOverScreen.SetActive(false);
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState.gameState == 1 && Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused == false)
            {
                gamePlayUI.SetActive(false);
                pauseUI.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }

            else
            {
                gamePlayUI.SetActive(true);
                pauseUI.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }

        }
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
