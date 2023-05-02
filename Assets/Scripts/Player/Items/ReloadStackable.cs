using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadStackable : Item
{
    PlayerStats stats;
    public float reloadAmount;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.reloadSpeedMult += reloadAmount;
        stats.UpdateReloadSpeed();
    }

    public override void ApplyEffect()
    {
        stats.reloadSpeedMult += reloadAmount;
        stats.UpdateReloadSpeed();
    }
}
