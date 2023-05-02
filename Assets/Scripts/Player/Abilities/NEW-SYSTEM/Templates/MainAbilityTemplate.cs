using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class MainAbilityTemplate : MonoBehaviour
{
    public float cooldownTime;          // How long the cooldown is
    protected float currentCooldownTime;
    public float damage;                  // How much damage the ability does
    public float range;                 // How much range does the ability have
    public int charges;                 // How many times can the ability be used in succession
    public Image abilityIcon;           // The UI Icon for the ability
    public GameObject abilityObj;       // The object that could spawn when using the ability. DOES NOT REQUIRE ONE.
    public AudioClip soundEffect;

    public bool useMain;

    public static Action<int> UpdateMainAbilityChargeUI;
    public static Action<float> UpdateMainAbilityCooldownUI;
    public static Action<float> UpdateMainAbilityCooldownMax;

    private void Awake()
    {
        currentCooldownTime = cooldownTime;
    }

    /// <summary>
    /// Starts the animation for the melee
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="playerTransform"></param>
    public abstract void UseAbility(Animator anim);

    /// <summary>
    /// Does the melee damage/attack for the animation
    /// </summary>
    public abstract void MainAttack(Transform playerTransform);

    public void UpdateCooldownTime(float amt) { currentCooldownTime = Mathf.Clamp(cooldownTime - (cooldownTime * amt), 0, Mathf.Infinity); UpdateMainAbilityCooldownMax?.Invoke(currentCooldownTime); }

    protected IEnumerator ResetAbility(float time) { yield return new WaitForSeconds(time); useMain = false; }
}
