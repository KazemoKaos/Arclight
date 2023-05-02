using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class MeleeAbilityTemplate : MonoBehaviour
{
    public float cooldownTime;          // How long the cooldown is
    protected float currentCooldownTime;
    public float damage;                // How much damage the ability does
    public float range;                 // How much range does the ability have
    public int charges;                 // How many times can the ability be used in succession
    public Image abilityIcon;           // The UI Icon for the ability
    public GameObject meleeObj;         // The object that could spawn when using the ability. DOES NOT REQUIRE ONE.
    public AudioClip soundEffect;

    public bool useMelee;

    public static Action<int> UpdateMeleeChargeUI;
    public static Action<float> UpdateMeleeCooldownUI;
    public static Action<float> UpdateMeleeCooldownMax;

    private void Awake()
    {
        currentCooldownTime = cooldownTime;
    }

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

    public void UpdateCooldownTime(float amt) { currentCooldownTime = Mathf.Clamp(cooldownTime - (cooldownTime * amt), 0, Mathf.Infinity); UpdateMeleeCooldownMax?.Invoke(currentCooldownTime); }

    protected IEnumerator ResetAbility(float time) { yield return new WaitForSeconds(time); useMelee = false; }
}
