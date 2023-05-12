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


    private HitController CreateHitEffect()
    {
        HitController hitControl = Instantiate(gun.hitEffect, gun.hit.point, Quaternion.LookRotation(gun.hit.normal));

        hitControl.SetPool(_pool);

        return hitControl;
    }

    private void OnTakeHitEffectFromPool(HitController hitControl)
    {
        hitControl.transform.position = gun.hit.point;
        hitControl.transform.rotation = Quaternion.LookRotation(gun.hit.normal);

        hitControl.gameObject.SetActive(true);
    }

    private void OnReturnHitEffectToPool(HitController hitControl)
    {
        hitControl.gameObject.SetActive(false);
    }

    private void OnDestroyHitEffect(HitController hitControl)
    {
        Destroy(hitControl.gameObject);
    }
}
