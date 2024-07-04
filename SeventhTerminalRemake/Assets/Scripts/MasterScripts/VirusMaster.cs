using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class VirusMaster : MonoBehaviour
{
    public VirusInstance virus;
    public GameManager currentState;
    public ParticleSystem enemyHit;
    GameObject target;

    public void Initalize()
    {
        virus.virusHealth = virus.virusMaxHealth;
        currentState = GameObject.Find("Game Manager").GetComponent<GameManager>();
        target = GameObject.Find("Player");
    }


}
