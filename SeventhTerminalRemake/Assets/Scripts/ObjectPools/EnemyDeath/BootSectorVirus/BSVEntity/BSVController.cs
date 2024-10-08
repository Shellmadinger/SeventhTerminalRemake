using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BSVController : MonoBehaviour, IDamageable
{
    private ObjectPool<BSVController> _pool;
    public VirusInstance virus;
    public int score = 20;
    public float maxHealth = 5;
    public float speed = 30f;
    public float health;
    public GameManager currentState;
    public ParticleSystem enemyHit;
    [SerializeField] BSVEffectPool effectPool;
    [SerializeField] float bSVTimer;
    [SerializeField] int timeUntilExplosion;
    [SerializeField] AudioClip virusSoundClip;
    [SerializeField] AudioClip virusDeathClip;
    [SerializeField] AudioClip virusSpawnClip;
    [SerializeField] AudioSource virusDeathAudio;
    [SerializeField] AudioSource virusSource;
    EnemySpawner enemySpawner;
    GameObject target;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get the enemy spawner, game manager and player. Since these objects are in the scene, we use find to get them
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        currentState = GameObject.Find("Game Manager").GetComponent<GameManager>();
        target = GameObject.Find("Player");
        virusSource = GetComponent<AudioSource>();
        virusSource.clip = virusSoundClip;
        virusDeathAudio.clip = virusDeathClip;
        if (enemySpawner.powerUp == true)
        {
            maxHealth = 10;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
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
        if (health > 0)
        {
            enemyHit.Play();
            virusSource.pitch = Random.Range(1.2f, 1.6f);
            virusSource.spatialBlend = 0;
            virusSource.Play();
        }
        //For the BSV, they have a timer of it runs out, they explode and die.
        if (health <= 0 || bSVTimer >= timeUntilExplosion)
        {
            //Get the death effect from the pool and called Kill function
            StartCoroutine(OnDeath());
        }
    }

    IEnumerator OnDeath()
    {
        virusDeathAudio.pitch = Random.Range(1.6f, 2f);
        virusDeathAudio.Play();
        yield return new WaitForSeconds(0.1f);
        effectPool._pool.Get();
        currentState.AddToScore(score);
        enemySpawner.KillBSV(this);
    }

    public void Damage(float damageAmount)
    {
        TakeDamage(damageAmount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //When colliding with the player, kill the enemy and get the death effect

            effectPool._pool.Get();
            enemySpawner.KillBSV(this);

            Vector3 dir = (collision.transform.position - transform.position).normalized;
            //Set isKnockedBack to true
            collision.gameObject.GetComponent<KnockBack>().isKnockedBack = true;
            collision.gameObject.GetComponent<KnockBack>().direction = dir;
        }
    }

    public void SetPool(ObjectPool<BSVController> pool)
    {
        _pool = pool;
    }


    private void OnDisable()
    {
        //bSV's are fairly powerful enemies already, so their powerup only doubles their health
        if (enemySpawner.powerUp == true)
        {
            maxHealth = 10;
        }
        //Reset health when killed
        health = maxHealth;
        bSVTimer = 0;
    }

    private void OnEnable()
    {
       //On Enable, set the bSVTimer and set the spawn sound to be affected by 3D space
        bSVTimer += Time.deltaTime % 60;
        virusSource.spatialBlend = 1;
        virusSource.pitch = 0.5f;
        virusSource.PlayOneShot(virusSpawnClip);
    }
}
