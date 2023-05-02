using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The lower the magazine size is the more damage you deal
/// </summary>
public class WeaponPerkGlass : WeaponPerk
{
    public float damageIncrease;
    bool active;

    private void Update()
    {
        if (!active)
        {
            if (weapon.GetCurrentMag() <= weapon.magazineSize / 2)
            {
                weapon.damageMod += damageIncrease;
                active = true;
            }
        }
        else if(active && weapon.GetCurrentMag() >= weapon.magazineSize / 2) { weapon.damageMod -= damageIncrease; active = false; }
    }
}
