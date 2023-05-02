using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainAbility : ScriptableObject
{
    public string abilityName;                  // Name of the ability
    public float mainAbilityCooldown;           // The cooldown time of the ability
    public int mainAbilityDamage;               // The damage of the ability
    public int mainAbilityCharges;
    public bool abilityOnCooldown { get; set; }  // If the ability is on cooldown or not
    public GameObject abilityGameObj;            // What objects spawns when the ability is used
    public float mainAbilityRange;               // The range of the Main Ability

    public abstract void UseMain(Animator anim);
    public abstract void MainAttack(Transform playerTransform);
}
