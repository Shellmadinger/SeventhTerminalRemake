using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Camera cam;

    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        //Set ray to the camera
        ray.origin = cam.transform.position;
        ray.direction = cam.transform.forward;
        if(Physics.Raycast(ray, out hit))
        {
            //Set transform to the hit point if raycast hit something
            transform.position = hit.point;
        }

        else
        {
            //Mainly here to prevent the gun from firing backwards.
            transform.position = ray.origin + ray.direction * 50f;
        }
       
    }
}
