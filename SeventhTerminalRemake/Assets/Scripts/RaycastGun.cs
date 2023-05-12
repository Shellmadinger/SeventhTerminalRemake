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
        hitPool = GetComponent<HitPool>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= fireTime)
        {
            fireTime = Time.time + 1f / fireRate;
            Fire();
        }
    }

    void Fire()
    {
        muzzleFlash.Play();

        var trail = Instantiate(bulletTrail, trailOrigin.transform.position, Quaternion.identity);
        trail.AddPosition(trailOrigin.transform.position);
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            trail.transform.position = hit.point;
            hitPool._pool.Get();
        }

        else
        {
            trail.transform.position = trailTarget.transform.position;
        }

    }
}
