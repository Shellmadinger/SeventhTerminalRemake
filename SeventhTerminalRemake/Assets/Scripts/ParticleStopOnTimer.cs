using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopOnTimer : MonoBehaviour
{
    [SerializeField] float timerSeconds;
    [SerializeField] Transform currentParent;
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent != currentParent)
        {
            StartCoroutine(DisableAfterSeconds(timerSeconds));
        }
    }

    IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (this.gameObject.activeInHierarchy)
        {
            particle.gameObject.SetActive(false);
        }
    }
}
