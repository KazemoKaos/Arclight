using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPerk : MonoBehaviour
{
    protected AbstractWeapon weapon;
    public void SetWeapon(AbstractWeapon wep) { weapon = wep; }
    public virtual void ApplyEffect() { }
}
