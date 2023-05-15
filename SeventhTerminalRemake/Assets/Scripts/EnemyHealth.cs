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
        enemyPool = GetComponent<EnemyDeathPool>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        enemyPool._pool.Get();
        Destroy(gameObject);
    }
}
