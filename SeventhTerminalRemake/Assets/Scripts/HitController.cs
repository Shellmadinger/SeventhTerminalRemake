using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitController : MonoBehaviour
{

    private ObjectPool<HitController> _pool;
    private void OnParticleSystemStopped()
    {
        //Disable hit effect when it finishes going off
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<HitController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }
}
