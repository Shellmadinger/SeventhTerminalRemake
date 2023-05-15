using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathPool : MonoBehaviour
{
    public ObjectPool<EnemyDeathController> _pool;

    private EnemyHealth enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyHealth>();
        _pool = new ObjectPool<EnemyDeathController>(CreateHitEffect, OnTakeHitEffectFromPool, OnReturnHitEffectToPool, OnDestroyHitEffect, true, 1000, 1000);
    }

    //Creating the hit effect
    private EnemyDeathController CreateHitEffect()
    {
        EnemyDeathController enemyControl = Instantiate(enemy.enemyKill, enemy.gameObject.transform.position, Quaternion.identity);

        enemyControl.SetPool(_pool);

        return enemyControl;
    }
    //What to do after taking the hit effect from the pool
    private void OnTakeHitEffectFromPool(EnemyDeathController enemyControl)
    {
        enemyControl.transform.position = enemy.gameObject.transform.position;
        enemyControl.transform.rotation = Quaternion.identity;

        enemyControl.gameObject.SetActive(true);
    }
    //What to do when returning the hit effect to the pool
    private void OnReturnHitEffectToPool(EnemyDeathController enemyControl)
    {
        enemyControl.gameObject.SetActive(false);
    }
    //What to do when destroying hit effect
    private void OnDestroyHitEffect(EnemyDeathController enemyControl)
    {
        Destroy(enemyControl.gameObject);
    }
}
