using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltFireBullet : MonoBehaviour
{
    public float altGunDamage;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem bulletTrail;
    [SerializeField] float speed;
    private float seconds = 0f;
    private GameObject newParent;

    void Update()
    {
        DestroyOnTimer();
        //Move bullet forward
        transform.position += transform.forward * speed * Time.deltaTime;
        newParent = GameObject.Find("AltFireParent");
        
    }

    void DestroyOnTimer()
    {
        seconds += Time.deltaTime % 60;
        if(seconds >= 3f)
        {
            DestroyObject();
        }
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
                DestroyObject();
        }
        
    }

    void DestroyObject()
    {
        bulletTrail.gameObject.transform.parent = null;
        bulletTrail.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Destroy(this.gameObject);
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }
}
