using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoFStackable : Item
{
    PlayerStats stats;
    public float speedIncrease;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.rateFireMult = stats.rateFireMult + (stats.rateFireMult * (speedIncrease * count));
        stats.UpdateRateOfFire();
    }

    public override void ApplyEffect()
    {
        stats.rateFireMult = stats.rateFireMult + (stats.rateFireMult * (speedIncrease * count));
        stats.UpdateRateOfFire();
    }
}
