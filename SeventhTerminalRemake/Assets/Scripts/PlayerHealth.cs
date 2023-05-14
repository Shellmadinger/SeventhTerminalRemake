using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public string attackerTag;
    public float damageAmount;
    [SerializeField]
    float health;

    private void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
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
            health -= damageAmount;
        }

    }
}