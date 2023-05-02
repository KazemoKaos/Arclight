using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class GrenadeAbilityTemplate : MonoBehaviour
{
    public float cooldownTime;          // How long the cooldown is
    protected float currentCooldownTime;
    public float damage;                // How much damage the ability does
    public float range;                 // How much range does the ability have
    public int charges;                 // How many times can the ability be used in succession
    public float throwSpeed;
    public float detonationTime;
    public float blastRadius;
    public Image abilityIcon;           // The UI Icon for the ability
    public GameObject grenadeObj;       // The object that could spawn when using the ability.
    public AudioClip soundEffect;

    public bool useGrenade;

    public static Action<int> UpdateGrenadeChargeUI;
    public static Action<float> UpdateGrenadeCooldownUI;
    public static Action<float> UpdateGrenadeCooldownMax;

    private void Awake()
    {
        currentCooldownTime = cooldownTime;
    }


    /// <summary>
    /// Starts the animation for the melee
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="playerTransform"></param>
    public abstract void UseGrenade(Animator anim);

    /// <summary>
    /// Does the melee damage/attack for the animation
    /// </summary>
    public abstract void GrenadeAttack(Transform playerTransform);

    public void UpdateCooldownTime(float amt) { currentCooldownTime = Mathf.Clamp(cooldownTime - (cooldownTime * amt), 0, Mathf.Infinity); UpdateGrenadeCooldownMax?.Invoke(currentCooldownTime); }

    protected IEnumerator ResetAbility(float time) { yield return new WaitForSeconds(time); useGrenade = false; }
}
