using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Virus")]
public class VirusInstance : ScriptableObject
{
    public float virusHealth;
    public float virusMaxHealth;
    public float virusSpeed;
    public float virusScoring;
}
