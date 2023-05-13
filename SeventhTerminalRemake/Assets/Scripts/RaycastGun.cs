using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public float range = 100;
    public float fireRate = 15f;
    public TrailRenderer bulletTrail;
    public Camera fpcamera;
    public Transform trailOrigin;
    public Transform trailTarget;
    public ParticleSystem muzzleFlash;
    public HitController hitEffect;
    public RaycastHit hit;

    float fireTime = 0f;
    HitPool hitPool;

    private void Start()
    {
        //Get Hitpool Script
        hitPool = GetComponent<HitPool>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= fireTime)
        {
            //Call Fire method when time is greater than the fire rate
            fireTime = Time.time + 1f / fireRate;
            Fire();
        }
    }

    void Fire()
    {
        muzzleFlash.Play();

        //Instantiate trail wfrom gun when firing raycast
        var trail = Instantiate(bulletTrail, trailOrigin.transform.position, Quaternion.identity);
        trail.AddPosition(trailOrigin.transform.position);
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            //If raycast hits something, set trail to raycast. Also pull from the hitEffect pool
            trail.transform.position = hit.point;
            hitPool._pool.Get();
        }

        else
        {
            //Set trail to the tailTarget game object if the raycast hit nothing
            trail.transform.position = trailTarget.transform.position;
        }

    }
}
