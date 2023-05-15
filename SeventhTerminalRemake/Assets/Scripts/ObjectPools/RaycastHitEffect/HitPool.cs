using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitPool : MonoBehaviour
{
    public ObjectPool<HitController> _pool;

    private RaycastGun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<RaycastGun>();
        _pool = new ObjectPool<HitController>(CreateHitEffect, OnTakeHitEffectFromPool, OnReturnHitEffectToPool, OnDestroyHitEffect, true, 100, 100);
    }

    //Creating the hit effect
    private HitController CreateHitEffect()
    {
        HitController hitControl = Instantiate(gun.hitEffect, gun.hit.point, Quaternion.LookRotation(gun.hit.normal));

        hitControl.SetPool(_pool);

        return hitControl;
    }
    //What to do after taking the hit effect from the pool
    private void OnTakeHitEffectFromPool(HitController hitControl)
    {
        hitControl.transform.position = gun.hit.point;
        hitControl.transform.rotation = Quaternion.LookRotation(gun.hit.normal);

        hitControl.gameObject.SetActive(true);
    }
    //What to do when returning the hit effect to the pool
    private void OnReturnHitEffectToPool(HitController hitControl)
    {
        hitControl.gameObject.SetActive(false);
    }
    //What to do when destroying hit effect
    private void OnDestroyHitEffect(HitController hitControl)
    {
        Destroy(hitControl.gameObject);
    }
}
