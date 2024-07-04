using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool isKnockedBack = false;
    public Vector3 direction;
    [SerializeField] Rigidbody playerBody;
    [SerializeField] float timer = 0.3f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        OnKnockBack(direction);
    }


    public void OnKnockBack(Vector3 dir)
    {
        //I've mentioned having really stupid solutions to thing in the past, but this one takes the cake
        //Since the knock back kept being really short no matter what I do, I surmised that the knock back needed be in update
        //...while still being activated on collision. this is the result of this weird conondrum.
        if (isKnockedBack == true)
        {
            //When isKnockback is true, apply force, then start timer
            playerBody.AddForce(dir*1f, ForceMode.Impulse);
            timer -= Time.deltaTime;
            //Debug.Log(timer);
            if (timer < 0)
            {
                //When timer hits 0, reset everything
                isKnockedBack = false;
                playerBody.velocity = Vector3.zero;
                timer = 0.3f;
            }
        }
    }
}
