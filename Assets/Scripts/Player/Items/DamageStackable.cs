using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStackable : Item
{
    PlayerStats stats;
    public float damage;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.damageMult += damage;
        stats.UpdateDmg();
    }

    public override void ApplyEffect()
    {
        stats.damageMult += damage;
        stats.UpdateDmg();
    }
}
