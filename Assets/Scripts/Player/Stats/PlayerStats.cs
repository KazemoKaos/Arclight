using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Stores all the adjustable stats of the player affected by items, upgrades, or abilities
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // HEALTH
        // -- Needs to go to PlayerHealth
    public float maxHealth;          // Max amount of health the player has
    public static event Action<float> UpdateMaxHealth;
    public float maxShield;          // Max amount of shield the player has
    public static event Action<float> UpdateMaxShield;
    public float shieldRegen;      // How fast their shield regens
    public static event Action<float> UpdateShieldRegen;
    public float regenDelay;       // How long before regen starts
    public static event Action<float> UpdateRegenDelay;
    public int playerLives;
    public static event Action<int> UpdatePlayerLives;

    public void UpdateHealth() { UpdateMaxHealth?.Invoke(maxHealth); }
    public void UpdateShield() { UpdateMaxShield?.Invoke(maxShield); }
    public void UpdateRegen() { UpdateShieldRegen?.Invoke(shieldRegen); }
    public void UpdateRegenTime() { UpdateRegenDelay?.Invoke(regenDelay); }
    public void UpdatePlayerLife() { UpdatePlayerLives?.Invoke(playerLives); }

    // MOVEMENT
    // -- Needs to go to PlayerMovement
    public float movementMult;     // Multipler to player's movement speed
    public static event Action<float> UpdateMoveSpeed;

    public void UpdateMovementSpeed() { UpdateMoveSpeed?.Invoke(movementMult); }

    // JUMP
    // -- Needs to go to PlayerJump
    public int jumpCharges;
    public static event Action<int> UpdateJumpCharges;

    public void UpdateJumpCharge() { UpdateJumpCharges?.Invoke(jumpCharges); }

    // DASH
    // -- Needs to go to PlayerDash
    public int dashCharges;        // How many times can the player dash
    public static event Action<int> UpdateDashCharges;
    // These need to be +/- modifiers
    public float dashCooldown;     // How long the dash cooldown is
    public static event Action<float> UpdateDashCooldown;

    public void UpdateDashTime() { UpdateDashCooldown?.Invoke(dashCooldown); }
    public void UpdateDashCharge() { UpdateDashCharges?.Invoke(dashCharges); }

    // WEAPON
    // -- Needs to go to AbstractWeapon
    public float damageMult;       // Weapon damage multiplier
    public static event Action<float> UpdateDamage;
    public float rateFireMult;     // Rate of Fire multiplier
    public static event Action<float> UpdateROF;
    public float reloadSpeedMult;  // Reload speed multiplier
    public static event Action<float> UpdateReload;
    public float recoilMult;       // Recoil multiplier
    public static event Action<float> UpdateRecoil;
    public int magazineSizeMult; // Magazine size multiplier
    public static event Action<int> UpdateMag;

    public void UpdateDmg() { UpdateDamage?.Invoke(damageMult); UpdateAbilityDamage?.Invoke(damageMult); UpdateGrenadeDamage?.Invoke(damageMult); UpdateMeleeDamage?.Invoke(damageMult); }
    public void UpdateRateOfFire() { UpdateROF?.Invoke(rateFireMult); }
    public void UpdateReloadSpeed() { UpdateReload?.Invoke(reloadSpeedMult); }
    public void UpdateRecoilAmount() { UpdateRecoil?.Invoke(recoilMult); }
    public void UpdateMagazineSize() { UpdateMag?.Invoke(magazineSizeMult); }

    // ABILITY
    // -- Needs to go to PlayerAbilities
    public float mainAbilityDamageMult;    // Main ability damage multiplier
    public static event Action<float> UpdateAbilityDamage;
    public float grenadeDamageMult;        // Grenade damage multiplier
    public static event Action<float> UpdateGrenadeDamage;
    public float meleeDamageMult;          // Melee damage multiplier
    public static event Action<float> UpdateMeleeDamage;

    public void UpdateAbilityDmg() { UpdateAbilityDamage?.Invoke(mainAbilityDamageMult); }
    public void UpdateGrenadeDmg() { UpdateGrenadeDamage?.Invoke(grenadeDamageMult); }
    public void UpdateMeleeDmg() { UpdateMeleeDamage?.Invoke(meleeDamageMult); }

    // These need to be +/- modifiers
    public float mainAbilityCooldownTime;      // How long for the main ability to recharge
    public static event Action<float> UpdateAbilityCooldown;
    public float grenadeCooldownTime;          // How long for the grenade to recharge
    public static event Action<float> UpdateGrenadeCooldown;
    public float meleeCooldownTime;            // How long for the melee to recharge
    public static event Action<float> UpdateMeleeCooldown;

    public void UpdateAbilityTime() { UpdateAbilityCooldown?.Invoke(mainAbilityCooldownTime); }
    public void UpdateGrenadeTime() { UpdateGrenadeCooldown?.Invoke(grenadeCooldownTime); }
    public void UpdateMeleeTime() { UpdateMeleeCooldown?.Invoke(meleeCooldownTime); }

    public float overallCDR;               // Overall cooldown multiplier for abilities
    public void UpdateCDR() { UpdateAbilityCooldown?.Invoke(mainAbilityCooldownTime); UpdateGrenadeCooldown?.Invoke(grenadeCooldownTime); UpdateMeleeCooldown?.Invoke(meleeCooldownTime); }

    // These need to be +/- modifiers
    public int mainAbilityCharges;         // How many times player can use main ability
    public static event Action<int> UpdateAbilityCharges;
    public int grenadeCharges;             // How many times player can use grenade
    public static event Action<int> UpdateGrenadeCharges;
    public int meleeCharges;               // How many times player can use melee
    public static event Action<int> UpdateMeleeCharges;

    public void UpdateAbilityCharge() { UpdateAbilityCharges?.Invoke(mainAbilityCharges); }
    public void UpdateGrenadeCharge() { UpdateGrenadeCharges?.Invoke(grenadeCharges); }
    public void UpdateMeleeCharge() { UpdateMeleeCharges?.Invoke(meleeCharges); }

    // AMMO
        // -- Needs to go to AmmoInventory
    public int maxPrimaryAmmo = 300;             // Maximum amount of primary ammo player can hold
    public static event Action<int> UpdateMaxPrimaryAmmo;
    public int maxSecondaryAmmo = 200;           // Maximum amount of secondary ammo player can hold
    public static event Action<int> UpdateMaxSecondaryAmmo;
    public int maxHeavyAmmo = 100;               // Maximum amount of heavy ammo player can hold
    public static event Action<int> UpdateMaxHeavyAmmo;

    public void UpdateMaxPrimAmmo() { UpdateMaxPrimaryAmmo?.Invoke(maxPrimaryAmmo); }
    public void UpdateMaxSecAmmo() { UpdateMaxSecondaryAmmo?.Invoke(maxSecondaryAmmo); }
    public void UpdateMaxHevAmmo() { UpdateMaxHeavyAmmo?.Invoke(maxHeavyAmmo); }

    // --------------------------------FUNCTIONS--------------------------------

    // Increases the stats for when player levels up
    void IncreaseLevel(float hpIncrease, float dmgIncrease)
    {
        maxHealth += hpIncrease;
        UpdateHealth();
        damageMult += dmgIncrease;
        UpdateDmg();
        // ...
    }

    public void StartStats(float hp, float shield, float cdr)
    {
        maxHealth = hp*10;
        maxShield = shield*10;
        overallCDR = (cdr*10)/100;

        mainAbilityCooldownTime = overallCDR - 1;
        grenadeCooldownTime = overallCDR - 1;
        meleeCooldownTime = overallCDR - 1;

        GetComponent<PlayerHealth>().maxHealth = maxHealth;
        GetComponent<PlayerHealth>().maxShield = maxShield;

        UpdateHealth();
        UpdateShield();
        UpdateCDR();
    }

    private void OnEnable()
    {
        PlayerLevel.levelUp += IncreaseLevel;
    }

    private void OnDisable()
    {
        PlayerLevel.levelUp -= IncreaseLevel;
    }
}
