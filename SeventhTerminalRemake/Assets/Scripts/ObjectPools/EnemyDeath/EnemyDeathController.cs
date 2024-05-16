using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathController : MonoBehaviour
{
    private ObjectPool<EnemyDeathController> _pool;
    public float health = 10f;
    public float speed = 10f;
    public GameManager currentState;
    public EnemyDeathEffectController enemyKill;
    public EnemySpawner enemyRelease;
    GameObject target;


    EnemyDeathEffectPool enemyEffectPool;

    private void Start()
    {
        //Get enemy pool component 
        enemyEffectPool = GetComponent<EnemyDeathEffectPool>();
        enemyRelease = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        currentState = GameObject.Find("Game Manager").GetComponent<GameManager>();
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (currentState.gameState == 1)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            transform.position += transform.forward * speed * Time.deltaTime;
        }
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
