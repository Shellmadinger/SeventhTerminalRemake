using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltFireBullet : MonoBehaviour
{
    public float altGunDamage;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float speed;
  
    // Update is called once per frame
    void Update()
    {
        //Move bullet forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //This requires some context
        //Tl'DR; due to how unity registers collisons, the bullet has to be a trigger in order for it to work
        //Otherwise, it would need a rigidbody WITH physics in order to work, which doesn't not feel good at all.
        //Also for this reason, enemies now have kinimatic rigidbodies

        //Check layer for enemy layer
        if(other.gameObject.layer == 3)
        {
            IDamageable isDamageable = other.transform.GetComponent<IDamageable>();
            if (isDamageable != null)
            {
                isDamageable.Damage(altGunDamage);
            }
        }

        else if(other.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
            Instantiate(explosion, this.transform.position, Quaternion.identity);
        }
        
    }
}
