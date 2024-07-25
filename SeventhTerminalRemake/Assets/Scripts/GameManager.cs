using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gameState = 0;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float timer = 3;
    [SerializeField] float gameTimerMin;
    [SerializeField] float gameTimerSec;
    [SerializeField] PlayerHealth playerIsDead;
    float gameTimerDisplay;
    float totalScore;

    private void Start()
    {
        //Set timer and score on start
        timerText.text = "Timer: 00:00";
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        GamePlayTimer();
        Score();
    }

    void CountDown()
    {
        if(gameState == 0)
        {
            countDownText.gameObject.SetActive(true);
            //If gamestate is 0, start the timer
            timer -= Time.deltaTime % 60;
            countDownText.text = timer.ToString("#");
           
            if(timer <= 0)
            {
                //When timer reaches 0, advance state and reset timer
                countDownText.gameObject.SetActive(false);
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
            timerText.text = "Timer: " + gameTimerMin.ToString("0#") + ":" + gameTimerSec.ToString("0#");
            
            if(playerIsDead.isDead == true)
            {
                gameState = 2;
            }
        }
        
    }

    void Score()
    {
        //update score text
        scoreText.text = "Score: " + totalScore.ToString();
    }

    public void AddToScore(float virusScoring)
    {
        //Just a scoring function
        totalScore += virusScoring;
    }

   
}
