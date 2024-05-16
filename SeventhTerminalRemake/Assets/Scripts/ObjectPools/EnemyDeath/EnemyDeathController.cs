using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathController : MonoBehaviour
{
    private ObjectPool<EnemyDeathController> _pool;
    public float health = 10f;
    public EnemyDeathEffectController enemyKill;
    public EnemySpawner enemyRelease;

    EnemyDeathEffectPool enemyEffectPool;

    private void Start()
    {
        //Get enemy pool component 
        enemyEffectPool = GetComponent<EnemyDeathEffectPool>();
        enemyRelease = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }
    public void TakeDamage(float amount)
    {
        //Reduce health by amount, then kill the enemy when health is 0
        health -= amount;
        if (health <= 0)
        {
            enemyEffectPool._pool.Get();
            enemyRelease.Kill(this);
        }
    }


    public void SetPool(ObjectPool<EnemyDeathController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }

    private void OnDisable()
    {
        health = 10f;
    }
}
