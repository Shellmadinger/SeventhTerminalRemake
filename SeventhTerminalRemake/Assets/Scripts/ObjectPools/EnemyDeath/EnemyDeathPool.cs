using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathPool : MonoBehaviour
{
    public ObjectPool<EnemyDeathController> _pool;

    private EnemySpawner enemy;
    Vector3 randVec;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemySpawner>();
        _pool = new ObjectPool<EnemyDeathController>(CreateHitEffect, OnTakeHitEffectFromPool, OnReturnHitEffectToPool, OnDestroyHitEffect, true, 1000, 1000);
    }

    //Creating the enemy
    private EnemyDeathController CreateHitEffect()
    {
        randVec = new Vector3(Random.Range(-80, 80), Random.Range(-80, 80));
        Debug.Log(randVec);
        //EnemyDeathController enemyControl = Instantiate(enemy.enemySpawn, enemy.gameObject.transform.position, Quaternion.identity);
        EnemyDeathController enemyControl = Instantiate(enemy.enemySpawn, randVec, Quaternion.identity);

        enemyControl.SetPool(_pool);

        return enemyControl;
    }
    //What to do after taking the enemy from the pool
    private void OnTakeHitEffectFromPool(EnemyDeathController enemyControl)
    {
        //enemyControl.transform.position = enemy.gameObject.transform.position;
        randVec = new Vector3(Random.Range(-5, -180), Random.Range(-3,2), Random.Range(27,170));
        enemyControl.transform.position = randVec;
        enemyControl.transform.rotation = Quaternion.identity;

        enemyControl.gameObject.SetActive(true);
    }
    //What to do when returning the enemy to the pool
    private void OnReturnHitEffectToPool(EnemyDeathController enemyControl)
    {
        enemyControl.gameObject.SetActive(false);
    }
    //What to do when destroying the enemy
    private void OnDestroyHitEffect(EnemyDeathController enemyControl)
    {
        Destroy(enemyControl.gameObject);
    }
}
