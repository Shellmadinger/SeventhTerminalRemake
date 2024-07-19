using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyDeathPool malwarePool;
    public TrojanHorsePool trojanHorsePool;
    public float timeToSpawn;
    [SerializeField] GameManager currentState;
    [SerializeField] float timer;
   
    float timeElapsedMin;
    float internalTimer;
    // Start is called before the first frame update
    void Start()
    {
        //Get Enemeypool
        malwarePool = GetComponent<EnemyDeathPool>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        
        if(currentState.gameState == 1)
        {
            //When state is 1, run the internal timer
            //Internal Timer ONLY EXISTS so that I didn't have to pull the time values from Gamemanager, and use the currentState variable
            //for something other than checking the game state. No, using Time.deltaTime on it's own doesn't work properly
            //It HAS to be done by adding (Keyword here: ADDING) Time.delaTime to a variable and doing the math from there.
            //This is also why the timer even works to begin with. This is the stupidest solution to a problem I have ever used during my time with Unity
            //Why?
            internalTimer += Time.deltaTime;
            timeElapsedMin = Mathf.FloorToInt(internalTimer / 60)+2;// convert internalTimer to minutes
            timeToSpawn = 4 / (1 + timeElapsedMin)+1; //Formula that increases spawn frequency after every minute
            timer += Time.deltaTime % 60; //Also run another timer that's converted into seconds
            if (timer >= timeToSpawn)
            {
                timer = 0f;
                int percentageSpawning = Random.Range(1, 100);
                Debug.Log(percentageSpawning);
                if (percentageSpawning >= 5f)
                {
                    malwarePool._pool.Get();
                }

                if (percentageSpawning >= 95f)
                {
                    trojanHorsePool._pool.Get();
                }

            }

            if (timeToSpawn < 2)
            {
                timeToSpawn = 2; //Cap timeToSpawn so that we aren't machine gunning enemies.
            }
            
        }
    }

    public void Kill(EnemyDeathController enemySpawn)
    {
        //Release enemies from the pool when they die
        //Realistically, this should be handled by Gamemanager, but it only works when it's in EnemySpawner and I'm not wasting time messing with that
        //Even then, it makes some sense for this to be here, right?
        malwarePool._pool.Release(enemySpawn);
    }

    public void KillTrojan(TrojanController trojanHorseReleased)
    {
        //Kill method fot TrojanHorse
        trojanHorsePool._pool.Release(trojanHorseReleased);

    }
}
