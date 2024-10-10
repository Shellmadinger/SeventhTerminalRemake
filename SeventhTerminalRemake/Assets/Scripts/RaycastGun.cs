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
    public HealthBar overHeatMeter;
    [SerializeField] float overHeat = 0f;
    [SerializeField] GameObject altFireBullet;
    [SerializeField] Transform altFirePoint;
    [SerializeField] Transform playerRotation;
    [SerializeField] AudioClip rayGunSound;
    [SerializeField] AudioClip altFireSound;
    float fireTime = 0f;
    bool isFiring;
    bool isOverHeating;
    bool gameManagerOverride;
    bool usingRegularFire;
    HitPool hitPool;
    AudioSource rayGunSource;

    private void Start()
    {
        //Get Hitpool Script
        hitPool = GetComponent<HitPool>();
        rayGunSource = GetComponent<AudioSource>();
        rayGunSource.clip = rayGunSound;
    }
    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            //Like with other scripts, check if the game state is 1 before activating the fire button.
            //God, there has to be a better way to do this...
            if (currentState.gameState == 1 || gameManagerOverride == true)
            {
                //If the conditions here are met, the gun will fire. Also note the overheating bool
                if (Input.GetButton("Fire1") && Time.time >= fireTime && isOverHeating == false)
                {
                    //Call Fire method when time is greater than the fire rate
                    fireTime = Time.time + 1f / fireRate;
                    Fire();
                    rayGunSource.pitch = Random.Range(0.8f, 1.2f);
                    rayGunSource.Play();
                }

                //Checks if the gun is firing, based on if mouse button is being pressed and overheating is false;
                if (Input.GetMouseButton(0) && isOverHeating == false)
                {
                    
                    isFiring = true;
                    usingRegularFire = true;

                }

                //If we aren't shooting, set these booleans to false
                else if (Input.GetMouseButton(0) != true)
                {
                    isFiring = false;
                    usingRegularFire = false;
                }

                //When pressing left mouse button, use the altfire and increase the overheat gauge by a set amount
                if (Input.GetMouseButtonDown(1) && isOverHeating == false && usingRegularFire == false)
                {
                    isFiring = true;
                    Instantiate(altFireBullet, altFirePoint.position, Quaternion.Euler(altFirePoint.rotation.eulerAngles.x, altFirePoint.rotation.eulerAngles.y,
                        altFirePoint.rotation.eulerAngles.z));
                    rayGunSource.pitch = Random.Range(0.8f, 1f);
                    rayGunSource.PlayOneShot(altFireSound);
                    
                    overHeat += overHeatMax * 0.65f;
                }

                OverHeating();
            }

            
        }

        else
        {
            Debug.Log("Game Manager not set. Initiating override");
            gameManagerOverride = true;
        }
        
       
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

            IDamageable dealDamage= hit.transform.GetComponent<IDamageable>();
            if(dealDamage != null)
            {
                dealDamage.Damage(gunDamage);
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
        overHeatMeter.SetHealth((int)overHeat);
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
            overHeat += 1f;
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
            overHeat -= 0.5f;
            if(overHeat<= 0)
            {
                //At this point, the gun will stop overheating and you can use it again.
                overHeat = 0;
                isOverHeating = false;
            }
        }
       
    }
}
