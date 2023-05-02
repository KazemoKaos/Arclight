using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPerkDamage : WeaponPerk
{
    public int damage;

    public override void ApplyEffect()
    {
        weapon.baseDamage += damage;
        weapon.critDamage += damage;
    }
}
