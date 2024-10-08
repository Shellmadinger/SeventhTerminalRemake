using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BSVPool bSVPool;
    public EnemyDeathPool malwarePool;
    public TrojanHorsePool trojanHorsePool;
    public float timeToSpawn;
    public bool powerUp = false;
    [SerializeField] GameManager currentState;
    [SerializeField] float timer;
    [SerializeField] bool canSpawn;
    [SerializeField] AudioClip virusSpawnClip;
    AudioSource virusSpawnAudio;
    int malwareSpawnChance;
    int trojanSpawnChance;
    int bSVSpawnChance;
    
   
    float timeElapsedMin;
    float internalTimer;
    // Update is called once per frame

    private void Start()
    {
        virusSpawnAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        
        if(currentState.gameState == 1 && canSpawn == true)
        {
            //When state is 1, run the internal timer
            //Internal Timer ONLY EXISTS so that I didn't have to pull the time values from Gamemanager, and use the currentState variable
            //for something other than checking the game state. No, using Time.deltaTime on it's own doesn't work properly
            //It HAS to be done by adding (Keyword here: ADDING) Time.delaTime to a variable and doing the math from there.
            //This is also why the timer even works to begin with. This is the stupidest solution to a problem I have ever used during my time with Unity
            //Why?
            internalTimer += Time.deltaTime;
            timeElapsedMin = Mathf.FloorToInt(internalTimer / 60)+3 ;// convert internalTimer to minutes
            timeToSpawn = 4 / (1 + timeElapsedMin)+1; //Formula that increases spawn frequency after every minute
            timer += Time.deltaTime % 60; //Also run another timer that's converted into second
           
            if (timer >= timeToSpawn)
            {
                //When timer is above timeToSpawn, set timer back to 0 and roll a random number between 1 and 100
                timer = 0f;
                int percentageSpawning = Random.Range(1, 100);
                //Depending on random number rolled, spawn the corrolating enemy
                if(percentageSpawning >= 0f && percentageSpawning <= bSVSpawnChance)
                {
                    virusSpawnAudio.pitch = 1.5f;
                    //virusSpawnAudio.PlayOneShot(virusSpawnClip);
                    bSVPool._pool.Get();
                }

                if (percentageSpawning >= bSVSpawnChance && percentageSpawning <= trojanSpawnChance)
                {
                    virusSpawnAudio.pitch = 0.5f;
                   // virusSpawnAudio.PlayOneShot(virusSpawnClip);
                    trojanHorsePool._pool.Get();
                }

                if (percentageSpawning >= trojanSpawnChance && percentageSpawning <= malwareSpawnChance)
                {
                    virusSpawnAudio.pitch = 1f;
                    //virusSpawnAudio.PlayOneShot(virusSpawnClip);
                    malwarePool._pool.Get();
                }   

            }

            if(timeElapsedMin <= 0)
            {
                //Inital spawn chances for enemies
                bSVSpawnChance = 1;
                trojanSpawnChance = 4;
                malwareSpawnChance = 95;
            }

            if (timeElapsedMin >= 1)
            {
                //at one minute, update the spawn chances to this
                malwareSpawnChance = 65;
                trojanSpawnChance = 25;
                bSVSpawnChance = 10;
            }

            if (timeElapsedMin >= 2)
            {
                //at two minutes, update the spawn chances again to this
                malwareSpawnChance = 50;
                trojanSpawnChance = 40;
                bSVSpawnChance = 10;
            }

            if (timeElapsedMin >= 3)
            {
                powerUp = true;
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
        //Kill method for TrojanHorse
        trojanHorsePool._pool.Release(trojanHorseReleased);

    }

    public void KillBSV (BSVController bSVReleased)
    {
        //Kill method for BSV
        bSVPool._pool.Release(bSVReleased);
    }
}
