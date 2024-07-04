using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class OPEffectController : MonoBehaviour
{
    private ObjectPool<OPEffectController> _pool;
    private void OnParticleSystemStopped()
    {
        //Disable hit effect when it finishes going off
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<OPEffectController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }
}
