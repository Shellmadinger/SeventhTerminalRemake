using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public string attackerTag;
    public float damageAmount;
    public HealthBar healthBar;
    public bool isDead = false;
    [SerializeField] float health;
    [SerializeField] GameObject gunModel;
    //Rigidbody body;

    private void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
        //Set max health for health bar.
        healthBar.SetMaxHealth(((int)maxHealth));
        //body = GetComponent<Rigidbody>();
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
        //Inflict damage when colliding with attacker
        if (collision.gameObject.tag == attackerTag)
        {
            //Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            //body.AddForce(direction * 1000, ForceMode.Impulse);
            health -= damageAmount;
            //Change health bar
            healthBar.SetHealth(((int)health));
        }

    }
}