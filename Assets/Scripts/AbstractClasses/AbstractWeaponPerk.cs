using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeaponPerk : MonoBehaviour
{
    public abstract void ApplyEffect(AbstractWeapon weapon);
}
