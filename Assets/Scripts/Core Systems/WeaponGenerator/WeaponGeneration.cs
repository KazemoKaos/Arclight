using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneration : MonoBehaviour
{
    [SerializeField] AbstractWeapon weapon;
    [SerializeField] List<WeaponPerkInfo> weaponPerks;

    float rDam = 0, rCrit = 0, rReload = 0, rAds = 0, rReady = 0, rRange = 0, rRof = 0;
    Vector2 rRecoil = Vector2.zero;
    int rMag = 0;

    /// <summary>
    /// Randomly generates weapon values and sets them for the weapon
    /// </summary>
    public void GenerateWeapon()
    {
        weapon = GetComponent<AbstractWeapon>();

        if(weapon.IsRandomizeOnStart())
        {
            switch (weapon.GetRarity())
            {
                case AbstractWeapon.WeaponRarity.Common:       // Common (More negative variation)
                    rDam = Random.Range(-10, 5);
                    rCrit = Random.Range(-10, 5);
                    rReload = Random.Range(-10, 5);
                    rAds = Random.Range(-5, 5);
                    rReady = Random.Range(-10, 5);
                    rRange = Random.Range(-10, 5);
                    rRof = Random.Range(-10, 5);
                    rRecoil = new Vector2(Random.Range(-5, 10), Random.Range(-5, 10));
                    rMag = Random.Range(-10, 5);
                    break;
                case AbstractWeapon.WeaponRarity.Rare:         // Rare (More equal variation)
                    rDam = Random.Range(-10, 10);
                    rCrit = Random.Range(-10, 10);
                    rReload = Random.Range(-10, 10);
                    rAds = Random.Range(-5, 10);
                    rReady = Random.Range(-10, 10);
                    rRange = Random.Range(-10, 10);
                    rRof = Random.Range(-10, 10);
                    rRecoil = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                    rMag = Random.Range(-10, 10);

                    weapon.AddPerk(weaponPerks[Random.Range(0, weaponPerks.Count)]);
                    break;
                case AbstractWeapon.WeaponRarity.Epic:         // Epic (More positive variation)
                    rDam = Random.Range(-5, 15);
                    rCrit = Random.Range(-5, 15);
                    rReload = Random.Range(-5, 15);
                    rAds = Random.Range(-2, 10);
                    rReady = Random.Range(-5, 15);
                    rRange = Random.Range(-5, 15);
                    rRof = Random.Range(-5, 15);
                    rRecoil = new Vector2(Random.Range(-15, 5), Random.Range(-15, 5));
                    rMag = Random.Range(-5, 15);

                    for(int i = 0; i < 2; i++)
                    {
                        weapon.AddPerk(weaponPerks[Random.Range(0, weaponPerks.Count)]);
                    }
                    break;
                case AbstractWeapon.WeaponRarity.Legendary:    // Legendary (Almost completely positive variation)
                    rDam = Random.Range(-2, 20);
                    rCrit = Random.Range(-2, 20);
                    rReload = Random.Range(-2, 20);
                    rAds = Random.Range(-2, 20);
                    rReady = Random.Range(-2, 20);
                    rRange = Random.Range(-2, 20);
                    rRof = Random.Range(-2, 20);
                    rRecoil = new Vector2(Random.Range(-25, 5), Random.Range(-25, 5));
                    rMag = Random.Range(-5, 20);

                    for (int i = 0; i < 3; i++)
                    {
                        weapon.AddPerk(weaponPerks[Random.Range(0, weaponPerks.Count)]);
                    }
                    break;
                default:
                    Debug.LogError("Weapon Rarity Issue");
                    break;
            }

            // Call CreateWeapon(...) with the appropriate values
            weapon.CreateWeapon(rDam, rCrit, rReload, rAds, rReady, rRange, rRof, rRecoil, rMag);

            // Delete this script since weapon generation should only happen once
            Destroy(this);
        }
        else { Destroy(this); }
    }
}
