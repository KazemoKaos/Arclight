using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeStackable : Item
{
    PlayerStats stats;
    public int chargeAmount = 1;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.grenadeCharges += chargeAmount;
        stats.UpdateGrenadeCharge();
    }

    public override void ApplyEffect()
    {
        stats.grenadeCharges += chargeAmount;
        stats.UpdateGrenadeCharge();
    }
}
