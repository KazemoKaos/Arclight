using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reloads the weapon on an enemy defeat
/// </summary>
public class WeaponPerkScav : WeaponPerk
{
    void Effect() { weapon.Reload(); }

    private void OnEnable()
    {
        AbstractEnemy.EnemyDefeat += Effect;
    }

    private void OnDisable()
    {
        AbstractEnemy.EnemyDefeat -= Effect;
    }
}
