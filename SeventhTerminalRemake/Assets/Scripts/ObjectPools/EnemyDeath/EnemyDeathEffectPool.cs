using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathEffectPool : MonoBehaviour
{
    public ObjectPool<EnemyDeathEffectController> _pool;

    private EnemyDeathController enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyDeathController>();
        _pool = new ObjectPool<EnemyDeathEffectController>(CreateHitEffect, OnTakeHitEffectFromPool, OnReturnHitEffectToPool, OnDestroyHitEffect, true, 1000, 1000);
    }

    //Creating the enemy
    private EnemyDeathEffectController CreateHitEffect()
    {
        EnemyDeathEffectController enemyControl = Instantiate(enemy.enemyKill, enemy.gameObject.transform.position, Quaternion.identity);

        enemyControl.SetPool(_pool);

        return enemyControl;
    }
    //What to do after taking the enemy from the pool
    private void OnTakeHitEffectFromPool(EnemyDeathEffectController enemyControl)
    {
        enemyControl.transform.position = enemy.gameObject.transform.position;
        enemyControl.transform.rotation = Quaternion.identity;

        enemyControl.gameObject.SetActive(true);
    }
    //What to do when returning the enemy to the pool
    private void OnReturnHitEffectToPool(EnemyDeathEffectController enemyControl)
    {
        enemyControl.gameObject.SetActive(false);
    }
    //What to do when destroying the enemy
    private void OnDestroyHitEffect(EnemyDeathEffectController enemyControl)
    {
        Destroy(enemyControl.gameObject);
    }
}
