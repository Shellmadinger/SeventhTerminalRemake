using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyDeathController enemySpawn;
    public float timeToSpawn;
    [SerializeField] GameManager currentState;
    [SerializeField] float timer;
    float dynamicEnemyHealth = 5f;
    float objectPullCount = 1;
    EnemyDeathPool enemyPool;
    float timeElapsedMin;
    float internalTimer;
    // Start is called before the first frame update
    void Start()
    {
        //Get Enemeypool
        enemyPool = GetComponent<EnemyDeathPool>();
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

                //a for loop meant for pull multiple objects at once
                for (int i = 0; i < objectPullCount; i++)
                {
                    enemySpawn.health = dynamicEnemyHealth;
                    enemyPool._pool.Get();
                }

                if (timeElapsedMin == 1)
                {
                    //at one minute, we set objectPullCount to 2 and start spawning 2 objects at the same time.
                    objectPullCount = 2;
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
        enemyPool._pool.Release(enemySpawn);
    }
}
