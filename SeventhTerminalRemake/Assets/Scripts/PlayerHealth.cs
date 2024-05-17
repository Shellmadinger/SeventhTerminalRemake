using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public string attackerTag;
    public float damageAmount;
    public HealthBar healthBar;
    [SerializeField]
    float health;
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
        GameOver();
    }

    void GameOver()
    {
        if (health <= 0)
        {
            //Kill player if they are below 0 health
            Debug.Log("You died idiot");
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