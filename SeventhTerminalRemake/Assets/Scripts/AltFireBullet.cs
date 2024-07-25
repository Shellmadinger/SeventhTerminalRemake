using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltFireBullet : MonoBehaviour
{
    public float altGunDamage;
    [SerializeField] float speed;
  
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward*speed);
        //Debug.Log(this.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit something");
        IDamageable isDamageable = collision.transform.GetComponent<IDamageable>();
        if(isDamageable != null)
        {
            isDamageable.Damage(altGunDamage);
        }
    }
}
