using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRegenStackable : Item
{
    PlayerStats stats;
    public float speedIncrease;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.shieldRegen += speedIncrease;
        stats.UpdateRegen();
    }

    public override void ApplyEffect()
    {
        stats.shieldRegen += speedIncrease;
        stats.UpdateRegen();
    }
}
