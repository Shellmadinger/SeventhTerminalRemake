using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public HealthBar healthBar;
    public bool isDead = false;
    [SerializeField] float health;
    [SerializeField] GameObject gunModel;
    [SerializeField] AudioClip damageClip;
    AudioSource damageAudio;
    //Rigidbody body;

    private void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
        //Set max health for health bar.
        healthBar.SetMaxHealth(((int)maxHealth));
        damageAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHasDied();
    }

    void PlayerHasDied()
    {
        if (health <= 0)
        {
            //Kill player if they git 0 health
            isDead = true;
            //Disable gun model when health is 0. This is mainly a workaround to destroying the player object
            //as doing that would also destroy the camera.
            gunModel.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var objTag = collision.gameObject.tag;
        switch (objTag)
        {
            case ("BootSectorVirus"):
                health -= 5f;
                healthBar.SetHealth(((int)health));
                damageAudio.PlayOneShot(damageClip);
                break;
            case ("TrojanHorse"):
                health -= 3f;
                healthBar.SetHealth(((int)health));
                damageAudio.PlayOneShot(damageClip);
                break;
            case ("Malware"):
                health -= 1f;
                healthBar.SetHealth(((int)health));
                damageAudio.PlayOneShot(damageClip);
                break;
            default:
                break;
        }
    }
}