using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public GameManager currentGameState;
    public float gravityOnGround;
    public float gravityOnJump;
    public Rigidbody body;
    [SerializeField] KnockBack knockBackBool;
    float horiMove;
    float vertMove;
    float jump;
    float gravity;
    Vector3 fullMovement;
    bool isGrounded;
    bool isJumping;
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
            if(isGrounded == true) { Physics.gravity = new Vector3(0,(gravityOnGround * -1),0); }
            if(isJumping == true && isGrounded == false) { Physics.gravity = new Vector3(0, -9.81f, 0); }
            //Get x and Y axises
            horiMove = Input.GetAxis("Horizontal");
            vertMove = Input.GetAxis("Vertical");
            fullMovement = new Vector3(horiMove, 0f, vertMove);
            //Move Gameobject
            Vector3 move = transform.TransformDirection(fullMovement) * speed;
            body.velocity = new Vector3(move.x, body.velocity.y, move.z);

            if (Input.GetButton("Jump") && isGrounded == true)
            {
                isJumping = true;
                if (isJumping == true)
                {
                    //Vector3 jumpForce = Vector3.up * 100f;
                    body.velocity += (Vector3.up * Physics.gravity.y * (gravityOnJump - 1) * Time.deltaTime) * -1;
                    Debug.Log(body.velocity);
                }
                
            }

            Debug.Log(Physics.gravity);
        }

    }

    //Collision checks for isGrounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { isGrounded = true; isJumping = false; }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        isJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") { isGrounded = false; }
    }

}
