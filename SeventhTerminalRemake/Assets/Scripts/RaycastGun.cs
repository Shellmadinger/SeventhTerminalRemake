using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public float range = 100;
    public float fireRate = 15f;
    public float gunDamage = 1f;
    public float overHeatMax;
    public TrailRenderer bulletTrail;
    public Camera fpcamera;
    public Transform trailOrigin;
    public Transform trailTarget;
    public ParticleSystem muzzleFlash;
    public HitController hitEffect;
    public RaycastHit hit;
    public GameManager currentState;
    [SerializeField] float overHeat = 0f;
    float fireTime = 0f;
    bool isFiring;
    bool isOverHeating;
    HitPool hitPool;

    private void Start()
    {
        //Get Hitpool Script
        hitPool = GetComponent<HitPool>();
    }
    // Update is called once per frame
    void Update()
    {
        //Like with other scripts, check if the game state is 1 before activating the fire button.
        //God, there has to be a better way to do this...
        if(currentState.gameState == 1)
        {
            //If the conditions here are met, the gun will fire. Also note the overheating bool
            if (Input.GetButton("Fire1") && Time.time >= fireTime && isOverHeating == false)
            {
                //Call Fire method when time is greater than the fire rate
                fireTime = Time.time + 1f / fireRate;
                Fire();
            }

            //Checks if the gun is firing, based on if mouse button is being pressed and overheating is false;
            if (Input.GetMouseButton(0) && isOverHeating == false)
            {
                isFiring = true;
                
            }

            else if (Input.GetMouseButton(0)!=true)
            {
                isFiring = false;
            }
        }

        OverHeating();
       
    }

    void Fire()
    {
        muzzleFlash.Play();
        //Instantiate trail from gun when firing raycast
        var trail = Instantiate(bulletTrail, trailOrigin.transform.position, Quaternion.identity);
        trail.AddPosition(trailOrigin.transform.position);
        if (Physics.Raycast(fpcamera.transform.position, fpcamera.transform.forward, out hit, range))
        {
            //If raycast hits something, set trail to raycast. Also pull from the hitEffect pool
            trail.transform.position = hit.point;
            hitPool._pool.Get();

            EnemyDeathController enemy= hit.transform.GetComponent<EnemyDeathController>();
            if(enemy != null)
            {
                enemy.TakeDamage(gunDamage);
            }
        }

        else
        {
            //Set trail to the tailTarget game object if the raycast hit nothing
            trail.transform.position = trailTarget.transform.position;
        }

    }

    void OverHeating()
    {
        //A lot of ifs here, but they are necessary for this mechanic to work.
        //If the gun is NOT firing and NOT overheating currently, decrease the overheat gauge to 0;
        if (isFiring == false && isOverHeating == false)
        {
            overHeat -= 2f;
            if (overHeat <= 0)
            {
                overHeat = 0;
            }
        }
        
        //if the gun is firing, increase the overheat gauge
        else if (isFiring == true)
        {
            overHeat += 0.5f;
            //if the overheat gauge goes over the maxium, start overheating
            if (overHeat >= overHeatMax)
            {
                overHeat = overHeatMax;
                isFiring = false;
                isOverHeating = true;
            }
        }

        //When overheating, you will not be able to fire your gun, and need to wait for the gun to cool down
        if(isOverHeating == true)
        {
            //In other words, when overheating, decrease the heat gauge by a lower amount until it back to 0
            overHeat -= 0.25f;
            if(overHeat<= 0)
            {
                //At this point, the gun will stop overheating and you can use it again.
                overHeat = 0;
                isOverHeating = false;
            }
        }
       
    }
}
