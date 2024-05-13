using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;
    public EnemyDeathController enemyKill;

    private void Start()
    {
        health = maxHealth;
        //Get enemy pool component 
        enemyKill = GetComponent<EnemyDeathController>();
    }

    private void Update()
    {
        ResetHealth();
        Debug.Log(gameObject.activeSelf);
    }
    public void TakeDamage(float amount)
    {
        //Reduce health by amount, then kill the enemy when health is 0
        health -= amount;
        if (health <= 0)
        {
            enemyKill.EnemyHasDied();
        }
    }

    void ResetHealth()
    {
        if (this.gameObject.activeSelf == false)
        {
            health = maxHealth;
        }
    }
}
