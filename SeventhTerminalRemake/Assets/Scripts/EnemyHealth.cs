using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10f;
    public EnemyDeathController enemyKill;

    EnemyDeathPool enemyPool;

    private void Start()
    {
        //Get enemy pool component 
        enemyPool = GetComponent<EnemyDeathPool>();
    }
    public void TakeDamage(float amount)
    {
        //Reduce health by amount, then kill the enemy when health is 0
        health -= amount;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //Get the enemy pool and destroy the gameObject
        enemyPool._pool.Get();
        Destroy(gameObject);
    }
}
