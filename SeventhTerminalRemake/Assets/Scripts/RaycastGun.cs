using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public float range = 100;
    public Camera fpcamera;
    public TrailRenderer bulletTrail;
    public Transform trailOrigin;

 
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
        RaycastHit hit;
        var trail = Instantiate(bulletTrail, trailOrigin.transform.position, Quaternion.identity);
        trail.AddPosition(trailOrigin.transform.position);
        Debug.DrawRay(fpcamera.transform.position, fpcamera.transform.forward, Color.green, 10f);
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            trail.transform.position = hit.point;
        }

    }
}
