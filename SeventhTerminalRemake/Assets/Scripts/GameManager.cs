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
            Debug.Log(timer -= Time.deltaTime % 60);
            if(timer <= 0)
            {
                gameState = 1;
                timer = 3;
               
            }
        }
    }
}
