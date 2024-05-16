using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyDeathController enemySpawn;
    [SerializeField] float timer;
    EnemyDeathPool enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = GetComponent<EnemyDeathPool>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime % 60;
        if(timer>= 5f)
        {
            timer = 0f;
            enemySpawn.health = 10f;
            enemyPool._pool.Get();
        }
    }

    public void Kill(EnemyDeathController enemySpawn)
    {
        enemyPool._pool.Release(enemySpawn);
    }
}
