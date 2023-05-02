using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStackable : Item
{
    PlayerStats stats;
    public int chargeAmount = 1;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.meleeCharges += chargeAmount;
        stats.UpdateMeleeCharge();
    }

    public override void ApplyEffect()
    {
        stats.meleeCharges += chargeAmount;
        stats.UpdateMeleeCharge();
    }
}
