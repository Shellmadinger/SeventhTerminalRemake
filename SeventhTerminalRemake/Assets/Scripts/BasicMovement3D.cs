using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public Rigidbody body;
    float horiMove;
    float vertMove;
    Vector3 fullMovement;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //Get x and Y axises
        horiMove = Input.GetAxis("Horizontal");
        vertMove = Input.GetAxis("Vertical");
        fullMovement = new Vector3(horiMove, 0f, vertMove);
        //Move Gameobject
        Vector3 move = transform.TransformDirection(fullMovement) * speed;
        body.velocity = new Vector3(move.x, body.velocity.y, move.z);
    }

}
