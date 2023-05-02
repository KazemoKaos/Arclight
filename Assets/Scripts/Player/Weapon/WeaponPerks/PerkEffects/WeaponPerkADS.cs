using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPerkADS : WeaponPerk
{
    public float speed;

    public override void ApplyEffect()
    {
        weapon.adsSpeed += speed;
    }
}
