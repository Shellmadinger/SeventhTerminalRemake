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
    [SerializeField] LayerMask ground;
    float horiMove;
    float vertMove;
    float rayDistance;
    float gravity;
    Vector3 fullMovement;
    bool isGrounded;
    bool isJumping;
    bool gameManagerOverride;
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (currentGameState != null)
        {
            //Debug.Log(isGrounded);
            //Check if the current game state is 1, which is when actual gameplay should start
            if (currentGameState.gameState == 1 || gameManagerOverride == true)
            {
                CheckGrounded();
                //Debug.Log(Physics.gravity);
                //if (isGrounded == true) { Physics.gravity = new Vector3(0, (gravityOnGround*-1), 0); }
                Physics.gravity = new Vector3(0, (gravityOnGround*-1), 0);
                //if (isJumping == true && isGrounded == false) { Physics.gravity = new Vector3(0, -50f, 0); }
                //Get x and Y axises
                horiMove = Input.GetAxis("Horizontal");
                vertMove = Input.GetAxis("Vertical");
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
                        //Vector3 jumpForce = Vector3.up * 100f;
                        body.velocity += (Vector3.up * Physics.gravity.y * (gravityOnJump) * Time.deltaTime)*-1;
   
                       
                    }

                }

            }
        }

        else
        {
            gameManagerOverride = true;
        }
       

    }
    void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 5f, ground))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }
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
