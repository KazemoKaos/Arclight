using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityGrenade : ScriptableObject
{
    public string grenadeName;
    public float grenadeCooldown;
    public int grenadeDamage;
    public bool grenadeOnCooldown { get; set; }
    public float throwSpeed;
    public float detonationTime;
    public float blastRadius;
    public int grenadeCharges;

    public GameObject grenadeObj;

    /// <summary>
    /// Starts the animation for the grenade throw
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="playerTransform"></param>
    public abstract void UseGrenade(Animator anim);

    /// <summary>
    /// Spawns in the grenade and applys force to throw it
    /// </summary>
    public abstract void GrenadeAttack(Transform playerTransform);
}
