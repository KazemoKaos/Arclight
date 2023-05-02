using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthStackable : Item
{
    PlayerStats stats;
    public float healthIncrease;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.maxHealth += healthIncrease;
        stats.UpdateHealth();
    }

    public override void ApplyEffect()
    {
        stats.maxHealth += healthIncrease;
        stats.UpdateHealth();
    }
}
