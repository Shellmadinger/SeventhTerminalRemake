using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gameState = 0;
    [SerializeField]
    float timer = 3;

    // Update is called once per frame
    void Update()
    {
        CountDown();
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

   
}
