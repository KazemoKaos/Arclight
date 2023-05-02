using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityMelee : ScriptableObject
{
    public string meleeName;
    public float meleeCooldown;
    public int meleeDamage;
    public int damageModifier;
    public float meleeRange;
    public int meleeCharges;
    public MeleeAbilityTemplate meleeAbility;
    public bool meleeOnCooldown { get; set; }

    public GameObject meleeObj;

    /// <summary>
    /// Starts the animation for the melee
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="playerTransform"></param>
    public abstract void UseMelee(Animator anim);

    /// <summary>
    /// Does the melee damage/attack for the animation
    /// </summary>
    public abstract void MeleeAttack(Transform playerTransform);
}
