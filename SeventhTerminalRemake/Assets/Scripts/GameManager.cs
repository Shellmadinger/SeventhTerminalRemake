using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gameState = 0;
    [SerializeField] TMP_Text timerText;
    [SerializeField] float timer = 3;
    [SerializeField] float gameTimerMin;
    [SerializeField] float gameTimerSec;
    [SerializeField] PlayerHealth playerIsDead;
    float gameTimerDisplay;

    private void Start()
    {
        //Set timer on start
        timerText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        GamePlayTimer();
    }

    void CountDown()
    {
        if(gameState == 0)
        {
            //If gamestate is 0, start the timer
            Debug.Log(timer -= Time.deltaTime % 60);
            if(timer <= 0)
            {
                //When timer reaches 0, advance state and reset timer
                gameState = 1;
                timer = 3;
               
            }
        }
    }

    void GamePlayTimer()
    {
        if(gameState == 1)
        {
            //Run game timer when the countdown is doned
            gameTimerDisplay += Time.deltaTime;
            //Make values for display timer
            gameTimerMin = Mathf.FloorToInt(gameTimerDisplay/ 60);
            gameTimerSec = Mathf.FloorToInt(gameTimerDisplay % 60);

            //Send values into text
            timerText.text = gameTimerMin.ToString("0#") + ":" + gameTimerSec.ToString("0#");
            
            if(playerIsDead.isDead == true)
            {
                gameState = 2;
            }
        }
        
    }

   
}
