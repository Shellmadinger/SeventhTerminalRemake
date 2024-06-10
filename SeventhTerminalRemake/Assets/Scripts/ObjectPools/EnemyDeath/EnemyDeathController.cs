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
    public ParticleSystem enemyHit;
    GameObject target;

    EnemyDeathEffectPool enemyEffectPool;
    

    private void Start()
    {
        //Get enemy pool component 
        enemyEffectPool = GetComponent<EnemyDeathEffectPool>();
        //Get the enemy spawner, game manager and player. Since these objects are in the scene, we use find to get them
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
            //Get the direction between enemy and player and move
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
        if(health> 0)
        {
            enemyHit.Play();
        }
        if (health <= 0)
        {
            //Get the death effect from the pool and called Kill function
            enemyEffectPool._pool.Get();
            enemyRelease.Kill(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //When colliding with the player, kill the enemy and get the death effect

            enemyEffectPool._pool.Get();
            enemyRelease.Kill(this);

            Vector3 dir = (collision.transform.position - transform.position).normalized;
            //Set isKnockedBack to true
            collision.gameObject.GetComponent<KnockBack>().isKnockedBack = true;
            collision.gameObject.GetComponent<KnockBack>().direction = dir;
        }
    }

    public void SetPool(ObjectPool<EnemyDeathController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }

    private void OnDisable()
    {
        //Reset health when killed
        health = 10f;
    }
}
