using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    private ObjectPool<EnemyDeathEffectController> _pool;
    private void OnParticleSystemStopped()
    {
        //Disable hit effect when it finishes going off
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<EnemyDeathEffectController> pool)
    {
        //Setting up the hit effect pool
        _pool = pool;
    }
}
