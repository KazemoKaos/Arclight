using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAbilityCooldownStackable : Item
{
    PlayerStats stats;
    public float reductionAmount;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.mainAbilityCooldownTime += reductionAmount;
        stats.UpdateAbilityTime();
    }

    public override void ApplyEffect()
    {
        stats.mainAbilityCooldownTime += reductionAmount;
        stats.UpdateAbilityTime();
    }
}
