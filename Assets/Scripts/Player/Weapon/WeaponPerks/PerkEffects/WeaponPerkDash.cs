using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reloads weapon after player dashes
/// </summary>
public class WeaponPerkDash : WeaponPerk
{
    void Effect()
    {
        weapon.Reload();
    }

    private void OnEnable()
    {
        PlayerDash.dashAction += Effect;
    }

    private void OnDisable()
    {
        PlayerDash.dashAction -= Effect;
    }
}
