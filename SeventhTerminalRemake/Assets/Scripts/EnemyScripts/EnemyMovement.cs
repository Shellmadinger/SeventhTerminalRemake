using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameManager currentState;
    public GameObject target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //Since enemies need to be instantiated, we want to find the game manager and player via code like this
        //So that the enemies have the necessary information for when they spawn
        currentState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure to check the state to prevent movement at the start
        if (currentState.gameState == 1)
        {
            //Enemy movement code
            Vector3 targetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            transform.position += transform.forward * speed * Time.deltaTime;
        }
      
    }
}
