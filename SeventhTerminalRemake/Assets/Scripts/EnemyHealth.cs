using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10f;
    public ParticleSystem enemyKill;
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
        var enemyEffect = Instantiate(enemyKill, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
