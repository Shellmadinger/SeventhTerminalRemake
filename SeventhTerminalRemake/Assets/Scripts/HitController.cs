using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitController : MonoBehaviour
{

    private ObjectPool<HitController> _pool;
    private void OnParticleSystemStopped()
    {
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<HitController> pool)
    {
        _pool = pool;
    }
}
