using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public float range = 100;
    public TrailRenderer bulletTrail;
    public Camera fpcamera;
    public Transform trailOrigin;
    public Transform trailTarget;

    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
       

        var trail = Instantiate(bulletTrail, trailOrigin.transform.position, Quaternion.identity);
        trail.AddPosition(trailOrigin.transform.position);
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            trail.transform.position = hit.point;
        }

        else
        {
            trail.transform.position = trailTarget.transform.position;
        }

    }
}
