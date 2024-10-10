using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltFireBullet : MonoBehaviour
{
    public float altGunDamage;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem bulletTrail;
    [SerializeField] float speed;
    [SerializeField] AudioClip altFireEndingExplosionSound;
    private float seconds = 0f;
    AudioSource altFireEndingExplosionAudio;
    private GameObject newParent;

    void Update()
    {
        DestroyOnTimer();
        //Move bullet forward
        transform.position += transform.forward * speed * Time.deltaTime;
        newParent = GameObject.Find("AltFireParent");
        altFireEndingExplosionAudio = GetComponent<AudioSource>();

    }

    void DestroyOnTimer()
    {
        //I have a weird issue where sometimes, the altfire bullet wouldn't trigger collision and just move through walls.
        //In this case, this fucntion kills the object if it persists for too long
        seconds += Time.deltaTime % 60;
        if (seconds >= 3f)
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
        if (other.gameObject.layer == 3)
        {
            //Get isDamageable and apply damage if it's there
            IDamageable isDamageable = other.transform.GetComponent<IDamageable>();
            if (isDamageable != null)
            {
                isDamageable.Damage(altGunDamage);
            }
        }

        else if (other.gameObject.layer == 6 || other.gameObject.tag == "Ground")
        {
            //If the bullet hits something thats environment or ground, start the coroutine
            StartCoroutine(DestroyObject());
        }

    }

    IEnumerator DestroyObject()
    {
        //This coroutine is so that the altfire ending explosion sound doesn't trigger multiple times.
        //We do this by setting a WaitForSeconds to a very small value after playing the sound, then doing everything else
        altFireEndingExplosionAudio.PlayOneShot(altFireEndingExplosionSound);
        yield return new WaitForSeconds(0.1f);
        bulletTrail.gameObject.transform.parent = null;
        bulletTrail.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Destroy(this.gameObject);
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

}
