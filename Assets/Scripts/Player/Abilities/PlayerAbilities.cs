using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the player to use their abilities. Any changes to abilities can be done through this script
/// </summary>
public class PlayerAbilities : MonoBehaviour
{
    [SerializeField]
    MainAbilityTemplate mainAbility;
    [SerializeField]
    MeleeAbilityTemplate melee;
    [SerializeField]
    GrenadeAbilityTemplate grenade;

    [SerializeField]
    MainAbilityTemplate mainAbilityEquip;
    [SerializeField]
    MeleeAbilityTemplate meleeEquip;
    [SerializeField]
    GrenadeAbilityTemplate grenadeEquip;

    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject abilitySpawnPoint;

    PlayerStates state;

    float meleeTimeMod;
    int meleeChargeMod;
    float meleeDamMod;

    float grenadeTimeMod;
    int grenadeChargeMod;
    float grenadeDamMod;

    float mainTimeMod;
    int mainChargeMod;
    float mainDamMod;

    Animator armsAnim;

    private void Awake()
    {
        armsAnim = GetComponent<Animator>();
        state = GetComponentInParent<PlayerStates>();
        attackPoint = Camera.main.transform;

        if (melee != null) { meleeEquip = Instantiate(melee, abilitySpawnPoint.transform); }
        if(mainAbility != null) { mainAbilityEquip = Instantiate(mainAbility, abilitySpawnPoint.transform); }
        if (grenade != null) { grenadeEquip = Instantiate(grenade, abilitySpawnPoint.transform); }
    }

    public void SetAbilities(MainAbilityTemplate ma, MeleeAbilityTemplate me, GrenadeAbilityTemplate ge)
    {
        if (melee != null) { Destroy(meleeEquip.gameObject); }
        if (mainAbility != null) { Destroy(mainAbilityEquip.gameObject); }
        if (grenade != null) { Destroy(grenadeEquip.gameObject); }

        meleeEquip = Instantiate(me, abilitySpawnPoint.transform);
        meleeEquip.UpdateCooldownTime(meleeTimeMod);
        meleeEquip.damage += meleeDamMod;
        meleeEquip.charges += meleeChargeMod;


        mainAbilityEquip = Instantiate(ma, abilitySpawnPoint.transform);
        mainAbilityEquip.UpdateCooldownTime(mainTimeMod);
        mainAbilityEquip.damage += mainDamMod;
        mainAbilityEquip.charges += mainChargeMod;

        grenadeEquip = Instantiate(ge, abilitySpawnPoint.transform);
        grenadeEquip.UpdateCooldownTime(grenadeTimeMod);
        grenadeEquip.damage += grenadeDamMod;
        grenadeEquip.charges += grenadeChargeMod;
    }

    /// <summary>
    /// Starts the Main Ability Animation
    /// </summary>
    void UseMain() 
    { 
        if (!mainAbilityEquip.useMain && !meleeEquip.useMelee & !grenadeEquip.useGrenade) 
        { 
            mainAbilityEquip.UseAbility(armsAnim);
            state.useAbility = mainAbilityEquip.useMain; 
        } 
    }

    /// <summary>
    /// Starts the Melee Ability Animation
    /// </summary>
    void UseMelee() 
    { 
        if (!mainAbilityEquip.useMain && !meleeEquip.useMelee & !grenadeEquip.useGrenade) 
        { 
            meleeEquip.UseMelee(armsAnim);
            state.useAbility = meleeEquip.useMelee; 
        } 
    }

    /// <summary>
    /// Starts the Grenade Ability Animation
    /// </summary>
    void UseGrenade() 
    {
        if (!mainAbilityEquip.useMain && !meleeEquip.useMelee & !grenadeEquip.useGrenade) 
        { 
            grenadeEquip.UseGrenade(armsAnim);
            state.useAbility = grenadeEquip.useGrenade; 
        }
    }


    // These get called by animation events to do their action
    //--------------------------------------------------------
    void MainAttack() { mainAbilityEquip.MainAttack(attackPoint); }

    void MeleeAttack() { meleeEquip.MeleeAttack(attackPoint); }

    void GrenadeAttack() { grenadeEquip.GrenadeAttack(attackPoint); }

    void MainDone() { mainAbilityEquip.useMain = false; state.useAbility = false; }
    void MeleeDone() { meleeEquip.useMelee = false; state.useAbility = false; }
    void GrenadeDone() { grenadeEquip.useGrenade = false; state.useAbility = false; }

    // These get called by events to do their action when picking up an item
    //--------------------------------------------------------
    void AdjustMainAbilityCooldown(float time) { mainTimeMod += time; mainAbilityEquip.UpdateCooldownTime(time); }
    void AdjustGrenadeCooldown(float time) { grenadeTimeMod += time; grenadeEquip.UpdateCooldownTime(time); }
    void AdjustMeleeCooldown(float time) { meleeTimeMod += time; meleeEquip.UpdateCooldownTime(time); }
    void AdjustMainAbilityDamage(float dmg) { mainDamMod += dmg; mainAbilityEquip.damage += dmg; }
    void AdjustGrenadeDamage(float dmg) { grenadeDamMod += dmg; grenadeEquip.damage += dmg; }
    void AdjustMeleeDamage(float dmg) { meleeDamMod += dmg; meleeEquip.damage += dmg; }
    void AdjustMainAbilityCharges(int charge) { mainChargeMod += charge; mainAbilityEquip.charges += charge; }
    void AdjustGrenadeCharges(int charge) { grenadeChargeMod += charge; grenadeEquip.charges += charge; }
    void AdjustMeleeCharges(int charge) { meleeChargeMod += charge; meleeEquip.charges += charge; }

    private void OnEnable()
    {
        InputManager.ability += UseMain;
        InputManager.grenade += UseGrenade;
        InputManager.melee += UseMelee;

        PlayerStats.UpdateAbilityCooldown += AdjustMainAbilityCooldown;
        PlayerStats.UpdateAbilityDamage += AdjustMainAbilityDamage;
        PlayerStats.UpdateAbilityCharges += AdjustMainAbilityCharges;
        PlayerStats.UpdateGrenadeCooldown += AdjustGrenadeCooldown;
        PlayerStats.UpdateGrenadeDamage += AdjustGrenadeDamage;
        PlayerStats.UpdateGrenadeCharges += AdjustGrenadeCharges;
        PlayerStats.UpdateMeleeCooldown += AdjustMeleeCooldown;
        PlayerStats.UpdateMeleeDamage += AdjustMeleeDamage;
        PlayerStats.UpdateMeleeCharges += AdjustMeleeCharges;
    }

    private void OnDisable()
    {
        InputManager.ability -= UseMain;
        InputManager.grenade -= UseGrenade;
        InputManager.melee -= UseMelee;

        PlayerStats.UpdateAbilityCooldown -= AdjustMainAbilityCooldown;
        PlayerStats.UpdateAbilityDamage -= AdjustMainAbilityDamage;
        PlayerStats.UpdateAbilityCharges -= AdjustMainAbilityCharges;
        PlayerStats.UpdateGrenadeCooldown -= AdjustGrenadeCooldown;
        PlayerStats.UpdateGrenadeDamage -= AdjustGrenadeDamage;
        PlayerStats.UpdateGrenadeCharges -= AdjustGrenadeCharges;
        PlayerStats.UpdateMeleeCooldown -= AdjustMeleeCooldown;
        PlayerStats.UpdateMeleeDamage -= AdjustMeleeDamage;
        PlayerStats.UpdateMeleeCharges -= AdjustMeleeCharges;
    }
}
