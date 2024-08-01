using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BSVPool : MonoBehaviour
{
    public ObjectPool<BSVController> _pool;
    public BSVController bSV;
    [SerializeField] Transform bSVParent;
    private EnemySpawner bSVSpawning;

    Vector3 randVec;
    // Start is called before the first frame update
    void Start()
    {
        bSVSpawning = GetComponent<EnemySpawner>();

        _pool = new ObjectPool<BSVController>(CreateObject, OnGet, OnRelease, OnObjectDestroy, true, 50, 50);

    }

    private BSVController CreateObject()
    {
        BSVController bSVHolder = Instantiate(bSV, bSVSpawning.gameObject.transform.position, Quaternion.identity, bSVParent);
        bSVHolder.SetPool(_pool);

        return bSVHolder;
    }

    private void OnGet(BSVController bSVHolder)
    {
        randVec = new Vector3(Random.Range(-5, -180), Random.Range(-3, 2), Random.Range(27, 170));

        bSVHolder.transform.position = randVec;

        bSVHolder.gameObject.SetActive(true);
    }

    private void OnRelease(BSVController bSVHolder)
    {
        bSVHolder.gameObject.SetActive(false);

    }

    private void OnObjectDestroy(BSVController bSVHolder)
    {
        Destroy(bSVHolder.gameObject);
    }
}
