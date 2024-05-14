using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathController : MonoBehaviour
{
    private ObjectPool<EnemyDeathController> _pool;
    public float health = 10f;
    public EnemyDeathEffectController enemyKill;

    EnemyDeathEffectPool enemyPool;

    private void Start()
    {
        //Get enemy pool component 
        enemyPool = GetComponent<EnemyDeathEffectPool>();
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

    public void SetPool(ObjectPool<EnemyDeathController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }
}
