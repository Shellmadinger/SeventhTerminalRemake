using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public float health;
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
        horiMove = Input.GetAxis("Horizontal");
        vertMove = Input.GetAxis("Vertical");
        fullMovement = new Vector3(horiMove, 0f, vertMove);

        Vector3 move = transform.TransformDirection(fullMovement) * speed;
        body.velocity = new Vector3(move.x, body.velocity.y, move.z);
    }

    void GameOver()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
    }
}
