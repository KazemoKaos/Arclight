using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPerkMagSize : WeaponPerk
{
    public int magazineIncrease;

    public override void ApplyEffect()
    {
        weapon.magazineSize += magazineIncrease;
    }
}
