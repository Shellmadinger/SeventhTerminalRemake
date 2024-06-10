using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public GameManager currentGameState;
    public float gravityFactor;
    public Rigidbody body;
    [SerializeField] KnockBack knockBackBool;
    float horiMove;
    float vertMove;
    float gravity;
    Vector3 fullMovement;
    bool isGrounded;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Check if the current game state is 1, which is when actual gameplay should start
        if (currentGameState.gameState == 1)
        {
            //reduce gravity by gravity factor
            gravity -= gravityFactor * Time.deltaTime;
            //Make gravity zero is grounded
            if (isGrounded == true) { gravity = 0; }
            //Get x and Y axises
            horiMove = Input.GetAxis("Horizontal");
            vertMove = Input.GetAxis("Vertical");
            fullMovement = new Vector3(horiMove, 0f, vertMove);
            //Move Gameobject
            Vector3 move = transform.TransformDirection(fullMovement) * speed;
            body.velocity = new Vector3(move.x, gravity, move.z);
        }

    }

    //Collision checks for isGrounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { isGrounded = true; }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { isGrounded = false; }
    }

}
