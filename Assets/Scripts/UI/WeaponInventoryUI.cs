using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInventoryUI : MonoBehaviour
{
    class UIWeaponSlot
    {
        public int slot = 0;
        public AmmoTypes ammoType = AmmoTypes.INF;
    };

    int currentWeaponIndex;

    [SerializeField] AmmoInventory ammoInventory;
    [SerializeField] RectTransform currentWeaponIndicator;  // The UI element to show what weapon is selected
    [SerializeField] TextMeshProUGUI[] ammoCounts;          // The texts of all the 3 current ammo counts
    [SerializeField] TextMeshProUGUI[] reserveAmmoCounts;   // The texts of all the 3 reserve ammo counts
    [SerializeField] Image[] weaponImages;                  // The weapon images
    List<UIWeaponSlot> weaponSlots;                         // The list of UIWeaponSlots
    float[] pos = {130f , 90f, 50f};                        // The positions for the currentWeaponIndicator

    private void Awake()
    {
        weaponSlots = new List<UIWeaponSlot>();

        for(int i = 0; i < 3; i++) { weaponSlots.Add( new UIWeaponSlot { slot = i } ); }
    }

    /// <summary>
    /// Called when the player switches weapons.
    /// </summary>
    void UpdateCurrentWeapon(int index)
    {
        currentWeaponIndex = index;
        currentWeaponIndicator.localPosition = new Vector3(0f, pos[index], 0f);
    }

    /// <summary>
    /// Updates the ammo count of the current equipped weapon UI
    /// </summary>
    void UpdateCurrentAmmo(int count) { ammoCounts[currentWeaponIndex].text = count.ToString(); }

    void UpdateAmmoReserve(int count, AmmoTypes ammoType)
    {
        foreach(UIWeaponSlot weapon in weaponSlots)
        {
            if(weapon.ammoType == ammoType) { reserveAmmoCounts[weapon.slot].text = count.ToString(); }
        }
    }

    void AddWeapon(int index, AbstractWeapon weapon)
    {
        // Add the weapon to that slot of the index
        weaponSlots[index] = new UIWeaponSlot { slot = index, ammoType = weapon.GetAmmoType() };
        ammoCounts[index].text = weapon.magazineSize.ToString();
        reserveAmmoCounts[index].text = ammoInventory.GetAmmoAmount(weaponSlots[index].ammoType).ToString();

        // Update the UI image to be that of the new weapon
    }

    private void OnEnable()
    {
        PlayerWeaponInventory.UpdateWeaponUI += UpdateCurrentWeapon;
        AbstractWeapon.UpdateAmmoUI += UpdateCurrentAmmo;
        AmmoInventory.UpdateAmmoReserves += UpdateAmmoReserve;
        PlayerWeaponInventory.AddWeaponUI += AddWeapon;
    }

    private void OnDisable()
    {
        PlayerWeaponInventory.UpdateWeaponUI -= UpdateCurrentWeapon;
        AbstractWeapon.UpdateAmmoUI -= UpdateCurrentAmmo;
        AmmoInventory.UpdateAmmoReserves -= UpdateAmmoReserve; 
        PlayerWeaponInventory.AddWeaponUI -= AddWeapon;
    }
}
