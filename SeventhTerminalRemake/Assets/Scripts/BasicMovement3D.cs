using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public GameManager currentGameState;
    public float gravity;
    public float jumpForce;
    public Rigidbody body;
    [SerializeField] KnockBack knockBackBool;
    [SerializeField] LayerMask ground;
    float horiMove;
    float vertMove;
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
        if (currentGameState != null)
        {
            //Check if the current game state is 1, which is when actual gameplay should start
            if (currentGameState.gameState == 1)
            {
                //Check if the player is grounded and apply our gravity value the physics engine
                CheckGrounded();
                Physics.gravity = new Vector3(0, (gravity*-1), 0);
                //Get x and Y axises
                horiMove = Input.GetAxis("Horizontal");
                vertMove = Input.GetAxis("Vertical");
                //Set fullMovement to the vector of horiMove and vertMove, and normalize the vector if it goes over 1
                fullMovement = new Vector3(horiMove, 0f, vertMove);
                if(fullMovement.magnitude > 1f)
                {
                    fullMovement.Normalize();
                }
                //Move Gameobject
                Vector3 move = transform.TransformDirection(fullMovement) *speed * Time.deltaTime;
                body.velocity = new Vector3(move.x, body.velocity.y, move.z);

                if (Input.GetButton("Jump") && isGrounded == true)
                {
                    isJumping = true;
                    if (isJumping == true)
                    {
                        //When jumping, add this equation to body.velocity
                        body.velocity += (Vector3.up * Physics.gravity.y * jumpForce * Time.deltaTime)*-1;
                    }

                }

            }
        }
    }
    void CheckGrounded()
    {
        //Send a raycast downwards, and if it hits, set isGrounded to true. Otherwise, set it to false.
        //Fairly simple isGrounded Check
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 5f, ground))
        {
            isGrounded = true;
            isJumping = false;
        }

        else
        {
            isGrounded = false;
        }
    }

}
