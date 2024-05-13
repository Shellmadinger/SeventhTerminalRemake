using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    float timer;
    public EnemyDeathPool enemyPool;
    public EnemyDeathController enemies;
    public EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyPool.GetComponent<EnemyDeathPool>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime % 60;
        if (timer >= 5)
        {
            enemyPool._pool.Get();
            enemyHealth.health = enemyHealth.maxHealth;
            timer = 0;
        }
    }
}
