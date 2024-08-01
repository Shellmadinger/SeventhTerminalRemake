using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class TrojanHorsePool : MonoBehaviour
{
    public ObjectPool<TrojanController> _pool;
    public TrojanController trojanHorse;
    [SerializeField] Transform trojanParent;
    private EnemySpawner trojanSpawning;

    Vector3 randVec;
    // Start is called before the first frame update
    void Start()
    {
        trojanSpawning = GetComponent<EnemySpawner>();

        _pool = new ObjectPool<TrojanController>(CreateObject, OnGet, OnRelease, OnObjectDestroy, true, 50, 50);

    }

   private TrojanController CreateObject()
   {
        TrojanController trojanHolder = Instantiate(trojanHorse,trojanSpawning.gameObject.transform.position,Quaternion.identity, trojanParent);
        trojanHolder.SetPool(_pool);

        return trojanHolder;
   }
    
   private void OnGet(TrojanController trojanHolder)
   {
        randVec = new Vector3(Random.Range(-5, -180), Random.Range(-3, 2), Random.Range(27, 170));

        trojanHolder.transform.position = randVec;

        trojanHolder.gameObject.SetActive(true);
   }

   private void OnRelease(TrojanController trojanHolder)
   {
        trojanHolder.gameObject.SetActive(false);

   }

    private void OnObjectDestroy(TrojanController trojanHolder)
    {
        Destroy(trojanHolder.gameObject);
    }


}
