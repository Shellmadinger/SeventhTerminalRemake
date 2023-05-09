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
        ray.origin = cam.transform.position;
        ray.direction = cam.transform.forward;
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }

        else
        {
            transform.position = ray.origin + ray.direction * 50f;
        }
       
    }
}
