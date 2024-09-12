using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrojanController : MonoBehaviour, IDamageable
{
    private ObjectPool<TrojanController> _pool;
    public VirusInstance virus;
    //public float health = 10f;
    //public float speed = 10f;
    public GameManager currentState;
    ///public EnemySpawner enemyRelease;
    public ParticleSystem enemyHit;
    [SerializeField] TrojanEffectPool effectPool;
    [SerializeField] GameObject malwareSpawns;
    [SerializeField] AudioClip virusSoundClip;
    [SerializeField] AudioClip virusDeathClip;
    [SerializeField] AudioClip virusSpawnClip;
    [SerializeField] AudioSource virusDeathAudio;
    [SerializeField] AudioSource virusSource;
    EnemySpawner enemySpawner;
    GameObject target;
   


    private void Start()
    {
        virus.virusHealth = virus.virusMaxHealth;
        //Get the enemy spawner, game manager and player. Since these objects are in the scene, we use find to get them
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        currentState = GameObject.Find("Game Manager").GetComponent<GameManager>();
        target = GameObject.Find("Player");
        virusSource = GetComponent<AudioSource>();
        virusSource.clip = virusSoundClip;
        virusDeathAudio.clip = virusDeathClip;
        virusSource.PlayOneShot(virusSpawnClip);
      
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
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, virus.virusSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            transform.position += transform.forward * virus.virusSpeed * Time.deltaTime;
        }
    }
    public void TakeDamage(float amount)
    {
        //Reduce health by amount, then kill the enemy when health is 0
        virus.virusHealth -= amount;
        if (virus.virusHealth > 0)
        {
            Debug.Log(virus.virusHealth);
            enemyHit.Play();
            virusSource.pitch = Random.Range(0.4f, 0.7f);
            virusSource.spatialBlend = 0;
            virusSource.Play();
        }
        if (virus.virusHealth <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                //Instantiate malware when the Trojan Horse dies
                //It's honestly easier to instantiate the malware spawns then getting them from pool
                Vector3 adjustSpawnRange = new Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + Random.Range(-10, 10),
                    this.transform.position.z + Random.Range(-10, 10));
                Instantiate(malwareSpawns, adjustSpawnRange, Quaternion.identity);
            }

            //Get the death effect from the pool and called Kill function
            StartCoroutine(OnDeath());
          
        }
    }

    IEnumerator OnDeath()
    {
        virusDeathAudio.pitch = Random.Range(0.4f, 0.6f);
        virusDeathAudio.Play();
        yield return new WaitForSeconds(0.1f);
        effectPool._pool.Get();
        currentState.AddToScore(virus.virusScoring);
        enemySpawner.KillTrojan(this);
    }

    public void Damage(float damageAmount)
    {
        TakeDamage(damageAmount);
    }

    public void SetPool(ObjectPool<TrojanController> pool)
    {
        _pool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //For the Trojan Horse, because he's so big, he doesn't explode on collision
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            //Set isKnockedBack to true
            collision.gameObject.GetComponent<KnockBack>().isKnockedBack = true;
            collision.gameObject.GetComponent<KnockBack>().direction = dir;
        }
    }

    private void OnEnable()
    {
        virusSource.spatialBlend = 1;
        virusSource.pitch = 0.5f;
        virusSource.PlayOneShot(virusSpawnClip);
       
    }

    private void OnDisable()
    {
        //Reset health when killed
        virus.virusHealth = virus.virusMaxHealth;
    }
}
