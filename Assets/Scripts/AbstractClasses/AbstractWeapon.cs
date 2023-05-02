using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// What every weapon will contain. This allows other objects to access the weapon, no matter the type.
/// </summary>
public abstract class AbstractWeapon : MonoBehaviour
{
    // ---------------------------- STATIC VARIABLES -------------------------
    // --ESSENTIALS--
    [SerializeField] protected WeaponTemplate weaponStats;          // This will load in the randomized weapon stats and apply it to the weapon. This only needs to be done once per weapon generation.
    protected PlayerStates state;
    protected GameObject projectile;                                // Weapon Projectile
    public ParticleSystem muzzleFlash;                              // Weapon Muzzle Flash Particle Effect
    protected GameObject weaponBarrel;                              // Weapon Barrel Position
    [SerializeField] protected GameObject weaponSight;              // The sight that will become active, if one exists
    protected Animator animator;                                    // Animator component to play the animations (for the weapon only)

    // Other Scripts
    protected AmmoInventory ammoInventory;      // The totals for the current ammo held by the player
    protected WeaponStatManager weaponStat;

    // --STATIC VALUES--
    protected float zoomAmount;                 // Zoom Amount (When ADSing)
    protected float projectileForce;            // The amount of force used on the projectile
    protected AmmoTypes ammoType;               // Type of ammo used
    public enum WeaponRarity { Common, Rare, Epic, Legendary };

    // 0 = common, 1 = rare, 2 = epic, 3 = legendary
    [SerializeField] WeaponRarity rarity;

    // ---------------------------- ADJUSTABLE VARIABLES -------------------------
    [Tooltip("The base amount of damage a body shot does")]
    public float baseDamage;                // Base Damage
    [Tooltip("The base amount of damage a critical shot does")]
    public float critDamage;                // Crit Damage
    [Tooltip("The base reload speed of the weapon")]
    public float reloadSpeed;               // Reload Speed
    [Tooltip("The base aim down sights speed of the weapon")]
    public float adsSpeed;                  // Aim Down Sights Speed
    [Tooltip("How fast the weapon can be stored and drawn")]
    public float readySpeed;                // Holster/Draw Speed
    [Tooltip("The vertical and horizontal recoil of the weapon")]
    public Vector2 recoilAmount;            // Recoil Amount (0 - 100 values ONLY)
    [Tooltip("How far the weapon can shoot")]
    public float range;                     // Range
    [Tooltip("How many rounds per minute can be shot")]
    public float rateOfFire;                // RoF
    [Tooltip("The max size of the magazine")]
    public int magazineSize;                // Magazine Size
    [Tooltip("The list of perks on the weapon")]
    public List<WeaponPerkInfo> weaponPerks;   // The list of weapon perks
    [SerializeField]protected List<WeaponPerk> perks;          // The perk objects

    protected int currentMagazine = 0;

    [SerializeField] private bool randomizeOnStart;
    bool perksApplied;
    protected bool firing = false;
    protected bool fullAuto;          // If the weapon can be fired full auto

    // ---------------------------- MODIFIERS -------------------------
    public float damageMod = 0f;

    // ---------------------------- FUNCTIONS -------------------------
    // If the weapon persists throughout scenes (doesn't get destroyed and then reloaded) this will only get called once!
    private void Awake() { InitializeWeapon(); if (!randomizeOnStart) { ApplyPerks(); } else { GetComponent<WeaponGeneration>().GenerateWeapon(); } }

    /// <summary>
    /// Initializes the base adjustable stats of the weapon
    /// </summary>
    private void InitializeWeapon()
    {
        animator = GetComponent<Animator>();                                // The animator for the weapon itself
        animator.runtimeAnimatorController = weaponStats.weaponAnimations;  // The runtimeAnimatorController for the weapon. If the weapon is special, then it has a different one

        ammoType = weaponStats.ammoType;                    // The type of ammo the weapon uses
        baseDamage = weaponStats.baseDamage;
        critDamage = weaponStats.critDamage;
        reloadSpeed = weaponStats.reloadSpeed;
        adsSpeed = weaponStats.adsSpeed;
        recoilAmount = weaponStats.recoilAmount;
        readySpeed = weaponStats.readySpeed;
        range = weaponStats.range;
        magazineSize = weaponStats.magazineSize;
        rateOfFire = weaponStats.rateOfFire;

        zoomAmount = weaponStats.zoomAmount;                // The zoomAmount
        projectileForce = weaponStats.projectileForce;      // The speed at which the projectile is fired
        projectile = weaponStats.projectile;                // The projectile that will be fired

        weaponPerks.AddRange(weaponStats.weaponPerks);      // The perks the weapon has
        perks = new List<WeaponPerk>();
    }

    /// <summary>
    /// Changes the base stats of the weapon during generation of the weapon
    /// </summary>
    public void CreateWeapon(float rDam, float rCrit, float rReload, float rADS, float rReady, float rRange, float rROF, Vector2 rRecoil, int rMag)
    {
        baseDamage = Mathf.Clamp(Mathf.Abs(baseDamage += rDam), 1, Mathf.Infinity);
        critDamage = Mathf.Clamp(Mathf.Abs(critDamage += rCrit), 1, Mathf.Infinity);

        reloadSpeed = Mathf.Clamp(Mathf.Abs(reloadSpeed += rReload), 1, 100);
        adsSpeed = Mathf.Clamp(Mathf.Abs(adsSpeed += rADS), 1, 100);
        readySpeed = Mathf.Clamp(Mathf.Abs(readySpeed += rReady), 1, 100);

        recoilAmount += rRecoil;
        range = Mathf.Clamp(Mathf.Abs(range += rRange), 1, Mathf.Infinity);
        rateOfFire = Mathf.Clamp(Mathf.Abs(rateOfFire += rROF), 80, 1000);
        magazineSize = (int)Mathf.Clamp(Mathf.Abs(magazineSize += rMag), 1f, Mathf.Infinity);

        ApplyPerks();
    }

    public void AddPerks(List<WeaponPerkInfo> perk) { weaponPerks.AddRange(perk); }
    public void AddPerk(WeaponPerkInfo perk) { weaponPerks.Add(perk); }

    public void DeletePerkObjects()
    {
        foreach(WeaponPerk obj in perks)
        {
            Destroy(obj.gameObject);
        }

        perks.Clear();

        perksApplied = false;
    }

    /// <summary>
    /// Applys the perks to the weapon
    /// </summary>
    public void ApplyPerks()
    {
        if (!perksApplied)
        {
            // Apply each perk if it exists
            foreach (WeaponPerkInfo perk in weaponPerks)
            {
                perks.Add(Instantiate(perk.perkPrefab, transform).GetComponent<WeaponPerk>());
                perks[perks.Count - 1].SetWeapon(this);
                perks[perks.Count - 1].ApplyEffect();
            }
            perksApplied = true;
        }
    }

    protected void ActivatePerks(bool status)
    {
        foreach (WeaponPerk perk in perks)
        {
            perk.enabled = status;
        }
    }

    // ---------------------------- GETTERS ----------------------------
    public AmmoTypes GetAmmoType() { return ammoType; }
    public int GetCurrentMag() { return currentMagazine; }
    public bool IsRandomizeOnStart() { return randomizeOnStart; }
    public bool IsFiring() { return firing; }
    public bool IsFullAuto() { return fullAuto;}
    public WeaponRarity GetRarity() { return rarity; }
    public string GetName() { return weaponStats.weaponName; }
    public string GetAmmoTypeName()
    {
        switch (ammoType)
        {
            case AmmoTypes.PRIMARY:
                return "P";
            case AmmoTypes.SECONDARY:
                return "S";
            case AmmoTypes.HEAVY:
                return "H";
            case AmmoTypes.INF:
                return "INF";
        }
        return "INF";
    }
    public float GetScaledReload() { return Scale(1, 100, 0.4f, 3f, (reloadSpeed + (reloadSpeed * weaponStat.reloadMod))); }
    public float GetScaledADS() { return Scale(1, 100, 0.05f, 0.25f, adsSpeed); }
    public float GetScaledReady() { return Scale(1, 100, 0.5f, 2.5f, readySpeed); }

    float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        OldValue = Math.Max(OldMin, Math.Min(OldValue, OldMax));
        return (OldValue - OldMin) * (NewMax - NewMin) / (OldMax - OldMin) + NewMin;
    }

    // ---------------------------- ABSTRACT METHODS ----------------------------
    public virtual void Reload() { }

    // ---------------------------- EVENTS ----------------------------
    public static event Action<float> aim;                                  // Tells the animator to play the aim animation                         (passes the adsSpeed)
    public static event Action<float> reload;                               // Tells the animator to play the reload animation                      (passes the reloadSpeed)
    public static event Action<RuntimeAnimatorController> animatorChange;   // Tells the animator to switch the player animator to the new one      (passes the new runtimeAnimatorController component)
    public static event Action<Vector2> attack;                             // Tells the animator to play the attack animator                       (passes the recoil amount in x and y)
    public static event Action<int> UpdateAmmoUI;                           // Tells the UI to update the current ammo text                         (passes the current mag)

    protected void AimEvent() { aim?.Invoke(GetScaledADS()); }
    protected void ReloadEvent() { reload?.Invoke(GetScaledReload()); }
    public void AnimatorChangeEvent() { animatorChange?.Invoke(weaponStats.playerAnimations); }
    protected void AttackEvent(Vector2 actualRecoilAmount) { attack?.Invoke(new Vector2(UnityEngine.Random.Range(actualRecoilAmount.x, actualRecoilAmount.x), UnityEngine.Random.Range(-actualRecoilAmount.y, actualRecoilAmount.y))); }
    protected void UpdateAmmoUIEvent(int mag) { UpdateAmmoUI?.Invoke(mag); }
}
