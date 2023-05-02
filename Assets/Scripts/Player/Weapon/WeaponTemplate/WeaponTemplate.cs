using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon Templates", menuName = "ScriptableObjects/WeaponTemplates")]
public class WeaponTemplate : ScriptableObject
{
    public string weaponName;
    public GameObject projectile;                        // Weapon Projectile
    public Image weaponImage;                            // UI Icon for what weapon type is being used
    public RuntimeAnimatorController weaponAnimations;   // Animations for the weapon itself
    public RuntimeAnimatorController playerAnimations;   // Animations for the player
    public List<AudioClip> attackClips;                  // Sounds that play when attacking
    public AudioClip magOut;                             // Noise for mag out
    public AudioClip magIn;                              // Noise for mag in
    public AudioClip reloadFull;                         // Noise for the full reload. Use this is the reload doesn't have mag out / in or other reasons


    public Vector3 weaponPosition;                       // Where the weapon should be positioned in the player's hand on initialization
    public Vector3 weaponRotation;                       // The rotation for the weapon

    public float baseDamage;                    // Base Damage
    public float critDamage;                    // Crit Damage
    public float reloadSpeed;                   // Reload Speed
    public float adsSpeed;                      // Aim Down Sights Speed
    public float readySpeed;                    // Holster/Draw Speed
    public Vector2 recoilAmount;                // Recoil Amount
    public float range;                         // Range
    public float rateOfFire;                    // RoF
    public int magazineSize;                    // Magazine Size
    public bool fullAuto;

    public float zoomAmount;                    // Zoom Amount (When ADSing)
    public float projectileForce;               // The amount of force used on the projectile
    public AmmoTypes ammoType;                  // Type of ammo used
    public List<WeaponPerkInfo> weaponPerks;       // Perks applied to the weapon
}
