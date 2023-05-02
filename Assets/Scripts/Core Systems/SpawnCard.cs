using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnCard")]
public class SpawnCard : ScriptableObject
{
    public GameObject spawnObject;
    public float weight;
    public float cost;
}
