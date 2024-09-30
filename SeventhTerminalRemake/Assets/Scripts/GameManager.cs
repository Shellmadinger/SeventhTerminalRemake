using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gameState = 0;
    public bool powerUp = false;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float gameTimerMin;
    [SerializeField] float gameTimerSec;
    [SerializeField] PlayerHealth playerIsDead;
    [SerializeField] AudioClip countDownBeeps;
    [SerializeField] VirusInstance malwareStats;
    [SerializeField] VirusInstance trojanStats;
    [SerializeField] VirusInstance bSVStats;
    AudioSource countDownAudio;
    float gameTimerDisplay;
    float totalScore;

    private void Start()
    {
        Time.timeScale = 1;
        //Set timer and score on start
        timerText.text = "Timer: 00:00";
        totalScore = 0;
        countDownText.text = " ";
        countDownAudio = GetComponent<AudioSource>();
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        GamePlayTimer();
        Score();
    }

    IEnumerator CountDown()
    {
        countDownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countDownAudio.PlayOneShot(countDownBeeps);
            countDownText.text = i.ToString("#");
            yield return new WaitForSeconds(1f);
           
        }

        countDownAudio.pitch = 1.5f;
        countDownAudio.PlayOneShot(countDownBeeps);
        countDownText.gameObject.SetActive(false);
        gameState = 1;

    }


    void GamePlayTimer()
    {
        if (gameState == 1)
        {
            //Run game timer when the countdown is doned
            gameTimerDisplay += Time.deltaTime;
            //Make values for display timer
            gameTimerMin = Mathf.FloorToInt(gameTimerDisplay / 60);
            gameTimerSec = Mathf.FloorToInt(gameTimerDisplay % 60);

            //Send values into text
            timerText.text = "Timer: " + gameTimerMin.ToString("0#") + ":" + gameTimerSec.ToString("0#");

            if (playerIsDead.isDead == true)
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
