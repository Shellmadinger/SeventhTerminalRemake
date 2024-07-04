using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanHorseBehaviour : MonoBehaviour
{
    public EnemySpawner malwareSpawns;
    public EnemyDeathController trojanHorse;
    // Start is called before the first frame update
    void Start()
    {
        malwareSpawns = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    public void SpawnMalware()
    {
        Debug.Log("TrojanHorse died");
    }
}
