using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Cinemachine;

/// <summary>
/// Base weapon for the guns
/// </summary>
public class BaseWeapon : AbstractWeapon
{
    // --VARIABLES FOR THE WEAPON ONLY--
    Vector2 actualRecoilAmount;   // The recoil amount being used
    CinemachineVirtualCamera cam;
    AudioSource audioSource;

    // --LOCAL WEAPON BOOLS--
    bool reloading;         // If the weapon is reloading                       
    bool aiming;            // If aiming with the weapon                        
    bool empty;             // If the weapon is currently empty
    bool attackCooldown;    // If the weapon rate of fire is done or not
    bool attackHeld;        // If the attack button is being held
    bool holstering;
    
    private void Start()
    {
        fullAuto = weaponStats.fullAuto;                                    // If the weapon can be fired in full auto or not

        weaponBarrel = transform.Find("Barrel").gameObject;                 // Where the muzzle flash plays from and the projectile shoots from
        animator = GetComponent<Animator>();                                // The animator for the weapon itself
        animator.runtimeAnimatorController = weaponStats.weaponAnimations;  // The runtimeAnimatorController for the weapon. If the weapon is special, then it has a different one
        weaponStat = GetComponentInParent<WeaponStatManager>();
        state = GetComponentInParent<PlayerStates>();
        ammoInventory = GetComponentInParent<AmmoInventory>();              // The current amount of each ammo the player has
        cam = Camera.main.GetComponentInParent<CinemachineVirtualCamera>(); // The camera for the FoV
        audioSource = GetComponent<AudioSource>();

        actualRecoilAmount = recoilAmount;                  // How much recoil the weapon has

        // Intialize these last because perks could affect them
        if(ammoInventory.CheckAmmo(ammoType) < magazineSize + weaponStat.magMod) { currentMagazine = 0; empty = true; }
        else { currentMagazine = magazineSize + weaponStat.magMod; }
        UpdateAmmoUIEvent(currentMagazine);
    }

    private void Update()
    {
        // Check if magazine is 0, if so attempt to reload if there is reserve ammo
        if(empty && ammoInventory.CheckAmmo(ammoType) > 0 && !reloading && !holstering && !state.useAbility) { StartReload(); }

        if (aiming)
        {
            float angle = Mathf.Abs((72 / zoomAmount) - 72);
            cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, zoomAmount, angle/GetScaledADS() * Time.deltaTime);
        }
        else
        {
            float angle = Mathf.Abs((72 / zoomAmount) - 72);
            cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, 72, angle/GetScaledADS() * Time.deltaTime);
        }

        if (attackHeld && fullAuto && !empty) { Attack(); }
    }


    // ----------------------- ATTACK FUNCTIONS -----------------------

    /// <summary>
    /// The primary function of the weapon. Attacks with the weapon
    /// </summary>
    /// <param name="ctx"></param>
    void PrimaryAction(InputAction.CallbackContext ctx)
    {
        if(!empty && !reloading && !attackCooldown && ctx.performed)    // Check if able to shoot
        {
            attackHeld = true;
            Attack();
        }
        else if(ctx.performed && empty) { StartReload(); }
        else { attackHeld = false; firing = false; }
    }

    /// <summary>
    /// Does the attack action
    /// </summary>
    void Attack()
    {
        if (!empty && !reloading && !attackCooldown && !state.useAbility)
        {
            firing = true;

            // Play weapon firing animation for player arms and Apply recoil to camera
            AttackEvent(actualRecoilAmount);

            // Play the fire animation for the weapon
            animator.Play("Fire", 0 ,0);

            // Play the particle effect
            if(muzzleFlash != null) muzzleFlash.Emit(5);

            // Play the sound
            if(audioSource) audioSource.PlayOneShot(weaponStats.attackClips[UnityEngine.Random.Range(0, weaponStats.attackClips.Count)]);

            // Spawn the projectile
            GameObject newProjectile = Instantiate(projectile);

            // Apply damage to bullet
            newProjectile.GetComponent<Projectile>().SetValues(baseDamage + weaponStat.damageMod + damageMod, critDamage + weaponStat.damageMod + damageMod, range);

            // Place the projectile at the proper position
            newProjectile.transform.position = weaponBarrel.transform.position;

            // Shoots the projectile towards the center of the screen
            Ray screenCenter = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            // Get a slight spread so bullets aren't completely center
            Vector3 spread = new Vector3(UnityEngine.Random.Range(-.01f, .01f), UnityEngine.Random.Range(-.01f, .01f));
            Vector3 shootDirection = (screenCenter.direction + spread);

            newProjectile.GetComponent<Rigidbody>().velocity = shootDirection.normalized * projectileForce;

            // Start the cooldown based on the Rate of Fire
            StartCoroutine(nameof(FireCooldown));

            // Subtract the current magazine by one
            currentMagazine--;
            if (currentMagazine == 0) { empty = true; }

            UpdateAmmoUIEvent(currentMagazine);

            if (attackHeld && !fullAuto) { firing = false; }
        }
    }

    /// <summary>
    /// Cooldown for firing. The rate of fire controller essentially.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(1/((rateOfFire + (rateOfFire * weaponStat.rofMod))/60));     // Converts RPS to RPM
        attackCooldown = false;
    }

    /// <summary>
    /// The secondary function of the weapon, Aim Down Sights (ADS)
    /// </summary>
    /// <param name="ctx"></param>
    void SecondaryAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !reloading && !state.useAbility) { StartAim(); }
        else if(aiming) { StopAim(); }
    }

    // ----------------------- AIM FUNCTIONS -----------------------
    /// <summary>
    /// Starts aiming with the weapon
    /// </summary>
    void StartAim()
    {
        if (weaponSight) { weaponSight.SetActive(true); }
        AimEvent();
        aiming = true;
        actualRecoilAmount /= 2;
    }

    /// <summary>
    /// Stops aiming with the weapon
    /// </summary>
    void StopAim()
    {
        if (weaponSight) { weaponSight.SetActive(false); }
        AimEvent();
        aiming = false;
        actualRecoilAmount = recoilAmount;
    }

    // ----------------------- RELOAD FUNCTIONS -----------------------

    /// <summary>
    /// Reloads the weapon. This is called from an animation event on the weapon
    /// </summary>
    public override void Reload()
    {
        if (ammoInventory.CheckAmmo(ammoType) - (magazineSize + weaponStat.magMod - currentMagazine) <= 0) { int temp = currentMagazine; currentMagazine += ammoInventory.CheckAmmo(ammoType); ammoInventory.SubtractAmmo(ammoType, magazineSize - temp); }
        else { ammoInventory.SubtractAmmo(ammoType, magazineSize + weaponStat.magMod - currentMagazine); currentMagazine = magazineSize + weaponStat.magMod; }

        empty = false;
        UpdateAmmoUIEvent(currentMagazine);
    }

    /// <summary>
    /// Animation event for when weapon reload animation is done and the player is able to attack again
    /// </summary>
    void ReloadFinish() { reloading = false; }

    /// <summary>
    /// Starts the reload animation
    /// </summary>
    void StartReload()
    {
        if (currentMagazine < magazineSize + weaponStat.magMod && ammoInventory.CheckAmmo(ammoType) > 0 && !reloading && !state.useAbility)
        {
            firing = false;
            if (aiming) { StopAim(); }
            reloading = true;
            ReloadEvent();
            animator.SetFloat("ReloadMult", GetScaledReload());
            animator.Play("Reload", 0);
        }
    }

    void PlayMagOut() { audioSource.PlayOneShot(weaponStats.magOut); }

    void PlayMagIn() { audioSource.PlayOneShot(weaponStats.magIn); }

    void PlayReloadFull() { audioSource.PlayOneShot(weaponStats.reloadFull); }

    // ----------------------- OTHER FUNCTIONS -----------------------

    /// <summary>
    /// Resets the state of the weapon when swapping weapons
    /// </summary>
    void ResetWeapon(float t)
    {
        holstering = true;
        if (aiming) { StopAim(); }
        animator.Play("Static");    // Stop reload animation
        reloading = false;
        attackHeld = false;
        firing = false;
    }

    private void OnEnable()
    {
        InputManager.primaryAction += PrimaryAction;
        InputManager.secondaryAction += SecondaryAction;
        InputManager.reload += StartReload;
        PlayerWeaponInventory.Holster += ResetWeapon;
        //AnimatorChangeEvent(weaponStats.playerAnimations);
        if (animator) { animator.SetFloat("ReadyMult", GetScaledReady()); animator.Play("Draw"); }

        holstering = false;

        // Make sure that the weapon is in the right positions
        transform.localPosition = weaponStats.weaponPosition;
        transform.localRotation = Quaternion.Euler(weaponStats.weaponRotation);
        UpdateAmmoUIEvent(currentMagazine);

        ActivatePerks(true);
    }

    private void OnDisable()
    {
        InputManager.primaryAction -= PrimaryAction;
        InputManager.secondaryAction -= SecondaryAction;
        InputManager.reload -= StartReload;
        PlayerWeaponInventory.Holster -= ResetWeapon;
        attackCooldown = false;

        ActivatePerks(false);
    }
}
