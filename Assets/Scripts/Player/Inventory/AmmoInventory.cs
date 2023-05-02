using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AmmoTypes { PRIMARY, SECONDARY, HEAVY, INF};
public class AmmoInventory : MonoBehaviour
{
    int maxPrimaryAmmo;
    int currentPrimaryAmmo;         // Current primary ammo in inventory
    int maxSecondaryAmmo;
    int currentSecondaryAmmo;       // Current secondary ammo in inventory
    int maxHeavyAmmo;
    int currentHeavyAmmo;           // Current heavy ammo in inventory

    [SerializeField] PlayerStats stats;                // Temp reference to player stats until properly implemented

    // Updates the reserve ammo counter
    public static Action<int, AmmoTypes> UpdateAmmoReserves;

    private void Awake()
    {
        maxPrimaryAmmo = stats.maxPrimaryAmmo;
        maxSecondaryAmmo = stats.maxSecondaryAmmo;
        maxHeavyAmmo = stats.maxHeavyAmmo;

        currentPrimaryAmmo = maxPrimaryAmmo;
        currentSecondaryAmmo = maxSecondaryAmmo / 2;
        currentHeavyAmmo = maxHeavyAmmo / 2;
    }

    public int GetMaxAmmoAmount(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.PRIMARY:
                return maxPrimaryAmmo;
            case AmmoTypes.SECONDARY:
                return maxSecondaryAmmo;
            case AmmoTypes.HEAVY:
                return maxHeavyAmmo;
        }

        // Should be impossible to get this
        return -1;
    }

    /// <summary>
    /// Returns how much of that ammo type is left
    /// </summary>
    /// <param name="ammoType">The type of ammo to check how much is left of</param>
    /// <returns></returns>
    public int CheckAmmo(AmmoTypes ammoType)
    {
        return (ammoType == AmmoTypes.PRIMARY ? currentPrimaryAmmo : ammoType == AmmoTypes.SECONDARY ? currentSecondaryAmmo : ammoType == AmmoTypes.HEAVY ? currentHeavyAmmo : 0);
    }

    /// <summary>
    /// Adds the ammo total of the parameter ammotype
    /// </summary>
    /// <param name="ammoType">The type of ammo the weapon uses</param>
    /// <param name="amount">How much to add to the total</param>
    public void AddAmmo(AmmoTypes ammoType, int amount)
    {
        switch (ammoType)
        {
            case AmmoTypes.PRIMARY:
                if (currentPrimaryAmmo + amount > maxPrimaryAmmo) { currentPrimaryAmmo = maxPrimaryAmmo; }
                else { currentPrimaryAmmo += amount; }
                UpdateAmmoReserves?.Invoke(currentPrimaryAmmo, AmmoTypes.PRIMARY);
                break;
            case AmmoTypes.SECONDARY:
                if (currentSecondaryAmmo + amount > maxSecondaryAmmo) { currentSecondaryAmmo = maxSecondaryAmmo; }
                else { currentSecondaryAmmo += amount; }
                UpdateAmmoReserves?.Invoke(currentSecondaryAmmo, AmmoTypes.SECONDARY);
                break;
            case (AmmoTypes.HEAVY):
                if (currentHeavyAmmo + amount > maxHeavyAmmo) { currentHeavyAmmo = maxHeavyAmmo; }
                else { currentHeavyAmmo += amount; }
                UpdateAmmoReserves?.Invoke(currentHeavyAmmo, AmmoTypes.HEAVY);
                break;
        }
    }

    /// <summary>
    /// Subtracts the ammo total of the parameter ammotype
    /// </summary>
    /// <param name="ammoType">The type of ammo the weapon uses</param>
    /// <param name="amount">How much to subtract the total from</param>
    public void SubtractAmmo(AmmoTypes ammoType, int amount)
    {
        switch (ammoType)
        {
            case AmmoTypes.PRIMARY:
                if (currentPrimaryAmmo - amount < 0) { currentPrimaryAmmo = 0; }
                else { currentPrimaryAmmo -= amount; }
                UpdateAmmoReserves?.Invoke(currentPrimaryAmmo, AmmoTypes.PRIMARY);
                break;
            case AmmoTypes.SECONDARY:
                if (currentSecondaryAmmo - amount < 0) { currentSecondaryAmmo = 0; }
                else { currentSecondaryAmmo -= amount; }
                UpdateAmmoReserves?.Invoke(currentSecondaryAmmo, AmmoTypes.SECONDARY);
                break;
            case (AmmoTypes.HEAVY):
                if (currentHeavyAmmo - amount < 0) { currentHeavyAmmo = 0; }
                else { currentHeavyAmmo -= amount; }
                UpdateAmmoReserves?.Invoke(currentHeavyAmmo, AmmoTypes.HEAVY);
                break;
        }
    }

    /// <summary>
    /// Returns the remaining ammo based on the ammo type given
    /// </summary>
    /// <param name="ammoType"></param>
    /// <returns></returns>
    public int GetAmmoAmount(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.PRIMARY:
                return currentPrimaryAmmo;
            case AmmoTypes.SECONDARY:
                return currentSecondaryAmmo;
            case AmmoTypes.HEAVY:
                return currentHeavyAmmo;
        }

        // Should be impossible to get this
        return -1;
    }


    void PrimaryAmmoMod(int amt) { maxPrimaryAmmo = amt; }
    void SecondaryAmmoMod(int amt) { maxSecondaryAmmo = amt; }
    void HeavyAmmoMod(int amt) { maxHeavyAmmo = amt; }

    private void OnEnable()
    {
        PlayerStats.UpdateMaxPrimaryAmmo += PrimaryAmmoMod;
        PlayerStats.UpdateMaxSecondaryAmmo += SecondaryAmmoMod;
        PlayerStats.UpdateMaxHeavyAmmo += HeavyAmmoMod;

        AmmoPickup.AddAmmo += AddAmmo;
    }

    private void OnDisable()
    {
        PlayerStats.UpdateMaxPrimaryAmmo -= PrimaryAmmoMod;
        PlayerStats.UpdateMaxSecondaryAmmo -= SecondaryAmmoMod;
        PlayerStats.UpdateMaxHeavyAmmo -= HeavyAmmoMod;

        AmmoPickup.AddAmmo -= AddAmmo;
    }
}
