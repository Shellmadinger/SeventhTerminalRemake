using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement3D : MonoBehaviour
{
    public float speed;
    public GameManager currentGameState;
    public float gravityFactor;
    public Rigidbody body;
    //public CharacterController playerCon;
    float horiMove;
    float vertMove;
    //float gravity;
    Vector3 fullMovement;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Check if the current game state is 1, which is when actual gameplay should start
        if(currentGameState.gameState == 1)
        {
            //gravity -= gravityFactor * Time.deltaTime;
            //Get x and Y axises
            horiMove = Input.GetAxis("Horizontal");
            vertMove = Input.GetAxis("Vertical");
            fullMovement = new Vector3(horiMove, 0f, vertMove);
            //Move Gameobject
            Vector3 move = transform.TransformDirection(fullMovement) * speed;
            body.velocity = new Vector3(move.x, body.velocity.y, move.z);
           
        }
       
    }

}
