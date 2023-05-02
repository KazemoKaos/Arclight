using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeaponInventory : MonoBehaviour
{
    [Header("List of all weapons in inventory")]
    [SerializeField] public List<AbstractWeapon> weapons;

    [Header("Currently equipped weapon")]
    public AbstractWeapon equippedWeapon;

    [Header("Where the equipped weapon gets parented to")]
    public Transform weaponEquipPoint;

    [SerializeField] PlayerStates state;

    [Header("Weapon indexes")]
    public int equippedIndex = -1;
    int weaponIndex;

    [Header("Audio")]
    public AudioSource audioSource;
    [SerializeField] AudioClip holsterSound;
    [SerializeField] AudioClip drawSound;
    [SerializeField] AudioClip weaponPickup;

    const int maxWeapons = 3;

    public static event Action<float> Holster;
    public static event Action<float> Draw;
    public static event Action<int> UpdateWeaponUI;
    public static event Action<int, AbstractWeapon> AddWeaponUI;

    void Start()
    {
        StartCoroutine(nameof(LateStart));

        //Equip the first weapon
        Equip(0);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.02f);

        int count = 0;
        foreach (AbstractWeapon weapon in weapons)
        {
            AddWeaponUI?.Invoke(count, weapons[count]);
            count++;
        }
    }

    /// <summary>
    /// Equips the desired Weapon
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public AbstractWeapon Equip(int index)
    {
        // If we have no weapons, can't equip anything
        if (weapons == null) { return equippedWeapon; }

        // The index needs to be within the array's bounds.
        if (index > weapons.Count - 1) { return equippedWeapon; }

        // Can't equip already equipped weapon
        // if (equippedIndex == index) { return equippedWeapon; }

        // Disable the currently equipped weapon
        if (equippedWeapon != null) { equippedWeapon.gameObject.SetActive(false); }

        equippedIndex = index;
        equippedWeapon = weapons[equippedIndex];
        equippedWeapon.gameObject.SetActive(true);
        equippedWeapon.AnimatorChangeEvent();

        // Enable the functionality of the weapon. This is need for when new weapons are picked up
        equippedWeapon.enabled = true;

        // Play weapon draw animation of the new equipped weapon
        Draw?.Invoke(equippedWeapon.GetScaledReady());

        audioSource.PlayOneShot(drawSound);

        //Return.
        return equippedWeapon;
    }

    /// <summary>
    /// Switches to Weapon 1
    /// </summary>
    void SwitchWeapon1()
    {
        if (weapons.Count > 0 && equippedIndex != 0 && !state.useAbility)
        {
            weaponIndex = 0;
            HolsterAnimation();
        }
    }

    /// <summary>
    /// Switches to Weapon 2
    /// </summary>
    void SwitchWeapon2()
    {
        if (weapons.Count > 1 && equippedIndex != 1 && !state.useAbility)
        {
            weaponIndex = 1;
            HolsterAnimation();
        }
    }

    /// <summary>
    /// Switches to Weapon 3
    /// </summary>
    void SwitchWeapon3()
    {
        if (weapons.Count > 2 && equippedIndex != 2 && !state.useAbility)
        {
            weaponIndex = 2;
            HolsterAnimation();
        }
    }

    /// <summary>
    /// Switches weapon based on direction of the scroll wheel.
    /// </summary>
    /// <param name="value"></param>
    void SwitchWeapon(float value)
    {
        if (!state.useAbility)
        {
            if (value > 1)
            {
                if (weaponIndex + 1 >= weapons.Count) { SwitchWeapon1(); }
                else
                {
                    switch (weaponIndex + 1)
                    {
                        case 1:
                            SwitchWeapon2();
                            break;
                        case 2:
                            SwitchWeapon3();
                            break;
                    }
                }
            }
            else if (value < 1)
            {
                if (weaponIndex - 1 < 0)
                {
                    switch (weapons.Count)
                    {
                        case 3:
                            SwitchWeapon3();
                            break;
                        case 2:
                            SwitchWeapon2();
                            break;
                    }
                }
                else
                {
                    switch (weaponIndex - 1)
                    {
                        case 0:
                            SwitchWeapon1();
                            break;
                        case 1:
                            SwitchWeapon2();
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Swaps the weapon to the new one. Gets called by an animation event in the holster animation.
    /// </summary>
    void HolsterWeapon() { Equip(weaponIndex); }

    /// <summary>
    /// Plays the holster animation. Calls HolsterWeapon() from event in animation
    /// </summary>
    void HolsterAnimation()
    {
        Holster?.Invoke(equippedWeapon.GetScaledReady());
        UpdateWeaponUI?.Invoke(weaponIndex);
        audioSource.PlayOneShot(holsterSound);
    }

    /// <summary>
    /// Adds the inputted weapon to the player's inventory
    /// </summary>
    /// <param name="weapon"></param>
    public void AddWeapon(GameObject weapon)
    {
        // This will check if a weapon was dropped to pick up this weapon
        bool weaponOverflow = false;

        // Set the weapon to be parented to the bone that will animate it
        weapon.transform.parent = weaponEquipPoint;

        // If at max amount of weapons that can be held, drop the weapon currently held
        if (weapons.Count == maxWeapons)
        {
            RemoveWeapon(equippedWeapon.transform.gameObject);
            weaponOverflow = true;
            weapons[equippedIndex] = weapon.GetComponent<AbstractWeapon>();
            weapons[equippedIndex].ApplyPerks();
            AddWeaponUI?.Invoke(equippedIndex, weapons[equippedIndex]); // Update the Weapon Inventory UI
        }
        else
        {
            // Add the weapon to the list of weapons in the inventory
            weapons.Add(weapon.GetComponent<AbstractWeapon>());
            weapons[weapons.Count - 1].ApplyPerks();
            AddWeaponUI?.Invoke(weapons.Count - 1, weapons[weapons.Count - 1]); // Update the Weapon Inventory UI
        }

        // If another weapon was dropped to pick this one up, immediately equip this weapon
        if (weaponOverflow) { Equip(equippedIndex); }
        else if(weapons.Count == 1) { Equip(0); }

        audioSource.PlayOneShot(weaponPickup);
    }

    /// <summary>
    /// Drops the currently equipped weapon. Can only do this if another weapon will immediately replace it
    /// </summary>
    /// <param name="weapon"></param>
    public void RemoveWeapon(GameObject weapon)
    {
        // Clear the parent of the weapon
        weapon.transform.parent = null;

        // Clear the perk objects
        weapons[equippedIndex].DeletePerkObjects();

        // Remove the weapon from the list
        weapons[equippedIndex] = null;

        // Clear the equipped weapon
        equippedWeapon = null;

        // Make the weapon interactable again
        weapon.GetComponent<InteractableWeapon>().MakeInteractable();
    }

    private void OnEnable()
    {
        InputManager.weaponOne += SwitchWeapon1;
        InputManager.weaponTwo += SwitchWeapon2;
        InputManager.weaponThree += SwitchWeapon3;
        InputManager.switchSelection += SwitchWeapon;
    }

    private void OnDisable()
    {
        InputManager.weaponOne -= SwitchWeapon1;
        InputManager.weaponTwo -= SwitchWeapon2;
        InputManager.weaponThree -= SwitchWeapon3;
        InputManager.switchSelection -= SwitchWeapon;
    }

}
