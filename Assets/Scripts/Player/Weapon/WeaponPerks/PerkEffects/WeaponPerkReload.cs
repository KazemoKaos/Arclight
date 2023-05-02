using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPerkReload : WeaponPerk
{
    public float reloadBoost;

    public override void ApplyEffect()
    {
        weapon.reloadSpeed = weapon.reloadSpeed + (weapon.reloadSpeed * reloadBoost);
    }
}
