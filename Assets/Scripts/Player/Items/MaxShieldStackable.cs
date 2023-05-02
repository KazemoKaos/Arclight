using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxShieldStackable : Item
{
    PlayerStats stats;
    public int shieldBoost;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.maxShield += shieldBoost;
        stats.UpdateShield();
    }

    public override void ApplyEffect()
    {
        stats.maxShield += shieldBoost;
    }
}
