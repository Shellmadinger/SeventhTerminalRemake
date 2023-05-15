using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathController : MonoBehaviour
{
    private ObjectPool<EnemyDeathController> _pool;
    private void OnParticleSystemStopped()
    {
        //Disable hit effect when it finishes going off
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<EnemyDeathController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }
}
