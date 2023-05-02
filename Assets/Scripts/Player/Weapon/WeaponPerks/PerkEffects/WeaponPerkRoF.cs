using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPerkRoF : WeaponPerk
{
    public float RoF;

    public override void ApplyEffect()
    {
        weapon.rateOfFire += RoF;
    }
}
