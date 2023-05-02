using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Drops the money loot object. Gets automatically deleted from the particle effect once it is done playing.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class MoneyDropLoot : MonoBehaviour
{
    public int moneyAmount;
    public static event Action<int> moneyDrop;

    void Start()
    {
        moneyDrop?.Invoke(moneyAmount);
    }
}
