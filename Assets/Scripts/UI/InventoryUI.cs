using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] PlayerStats stats;

    [Tooltip("Stat Texts")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI shieldRegenText;
    [SerializeField] TextMeshProUGUI movementText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI rofText;
    [SerializeField] TextMeshProUGUI reloadText;
    [SerializeField] TextMeshProUGUI recoilText;
    [SerializeField] TextMeshProUGUI magText;
    [SerializeField] TextMeshProUGUI dashText;
    [SerializeField] TextMeshProUGUI abilityText;
    [SerializeField] TextMeshProUGUI grenadeText;
    [SerializeField] TextMeshProUGUI meleeText;

    [Tooltip("Weapon 1 Texts")]
    [SerializeField] TextMeshProUGUI weapon1Name;
    [SerializeField] TextMeshProUGUI weapon1Dam;
    [SerializeField] TextMeshProUGUI weapon1Rel;
    [SerializeField] TextMeshProUGUI weapon1ADS;
    [SerializeField] TextMeshProUGUI weapon1Rea;
    [SerializeField] TextMeshProUGUI weapon1Rec;
    [SerializeField] TextMeshProUGUI weapon1ROF;
    [SerializeField] TextMeshProUGUI weapon1Mag;
    [SerializeField] TextMeshProUGUI weapon1Ran;
    [SerializeField] TextMeshProUGUI weapon1Typ;

    [Tooltip("Weapon 2 Texts")]
    [SerializeField] TextMeshProUGUI weapon2Name;
    [SerializeField] TextMeshProUGUI weapon2Dam;
    [SerializeField] TextMeshProUGUI weapon2Rel;
    [SerializeField] TextMeshProUGUI weapon2ADS;
    [SerializeField] TextMeshProUGUI weapon2Rea;
    [SerializeField] TextMeshProUGUI weapon2Rec;
    [SerializeField] TextMeshProUGUI weapon2ROF;
    [SerializeField] TextMeshProUGUI weapon2Mag;
    [SerializeField] TextMeshProUGUI weapon2Ran;
    [SerializeField] TextMeshProUGUI weapon2Typ;


    [Tooltip("Weapon 3 Texts")]
    [SerializeField] TextMeshProUGUI weapon3Name;
    [SerializeField] TextMeshProUGUI weapon3Dam;
    [SerializeField] TextMeshProUGUI weapon3Rel;
    [SerializeField] TextMeshProUGUI weapon3ADS;
    [SerializeField] TextMeshProUGUI weapon3Rea;
    [SerializeField] TextMeshProUGUI weapon3Rec;
    [SerializeField] TextMeshProUGUI weapon3ROF;
    [SerializeField] TextMeshProUGUI weapon3Mag;
    [SerializeField] TextMeshProUGUI weapon3Ran;
    [SerializeField] TextMeshProUGUI weapon3Typ;

    void UpdateHealthText(float amt) { healthText.text = amt.ToString(); }
    void UpdateShieldText(float amt) { shieldText.text = amt.ToString(); }
    void UpdateShieldRegenText(float amt) { shieldRegenText.text = amt.ToString() + "%"; }
    void UpdateMovementText(float amt) { movementText.text = "+" + amt.ToString() + "%"; }
    void UpdateDamageText(float amt) { damageText.text = "+" + amt.ToString(); }
    void UpdateROFText(float amt) { rofText.text = "+" + amt.ToString() + "%"; }
    void UpdateReloadText(float amt) { reloadText.text = "+" + (amt * 10).ToString() + "%"; }
    void UpdateRecoilText(float amt) { recoilText.text = "+" + amt.ToString(); }
    void UpdateMagText(int amt) { magText.text = "+" + amt.ToString(); }
    void UpdateDashText(float amt) { dashText.text = "+" + (amt * 100).ToString() + "%"; }
    void UpdateAbilityText(float amt) { abilityText.text = "+" + (amt * 100).ToString() + "%"; }
    void UpdateGrenadeText(float amt) { grenadeText.text = "+" + (amt * 100).ToString() + "%"; }
    void UpdateMeleeText(float amt) { meleeText.text = "+" + (amt * 100).ToString() + "%"; }

    private void Start()
    {
        UpdateHealthText(stats.maxHealth);
        UpdateShieldText(stats.maxShield);
        UpdateShieldRegenText(stats.shieldRegen);
    }

    void WeaponUI(int index, AbstractWeapon weapon)
    {
        switch (index)
        {
            case 0:
                weapon1Name.text = weapon.GetName();
                weapon1Dam.text = weapon.baseDamage.ToString() + " | " + weapon.critDamage.ToString();
                weapon1Rel.text = weapon.reloadSpeed.ToString();
                weapon1ADS.text = ((int)Mathf.Abs(weapon.adsSpeed - 100)).ToString();
                weapon1Rea.text = weapon.readySpeed.ToString();
                weapon1Rec.text = ((int)weapon.recoilAmount.x).ToString() + " | " + ((int)weapon.recoilAmount.y).ToString();
                weapon1ROF.text = weapon.rateOfFire.ToString();
                weapon1Mag.text = weapon.magazineSize.ToString();
                weapon1Ran.text = weapon.range.ToString();
                weapon1Typ.text = weapon.GetAmmoTypeName().ToString();
                break;
            case 1:
                weapon2Name.text = weapon.GetName();
                weapon2Dam.text = weapon.baseDamage.ToString() + " | " + weapon.critDamage.ToString();
                weapon2Rel.text = weapon.reloadSpeed.ToString();
                weapon2ADS.text = ((int)Mathf.Abs(weapon.adsSpeed - 100)).ToString();
                weapon2Rea.text = weapon.readySpeed.ToString();
                weapon2Rec.text = ((int)weapon.recoilAmount.x).ToString() + " | " + ((int)weapon.recoilAmount.y).ToString();
                weapon2ROF.text = weapon.rateOfFire.ToString();
                weapon2Mag.text = weapon.magazineSize.ToString();
                weapon2Ran.text = weapon.range.ToString();
                weapon2Typ.text = weapon.GetAmmoTypeName().ToString();
                break;
            case 2:
                weapon3Name.text = weapon.GetName();
                weapon3Dam.text = weapon.baseDamage.ToString() + " | " + weapon.critDamage.ToString();
                weapon3Rel.text = weapon.reloadSpeed.ToString();
                weapon3ADS.text = ((int)Mathf.Abs(weapon.adsSpeed - 100)).ToString();
                weapon3Rea.text = weapon.readySpeed.ToString();
                weapon3Rec.text = ((int)weapon.recoilAmount.x).ToString() + " | " + ((int)weapon.recoilAmount.y).ToString();
                weapon3ROF.text = weapon.rateOfFire.ToString();
                weapon3Mag.text = weapon.magazineSize.ToString();
                weapon3Ran.text = weapon.range.ToString();
                weapon3Typ.text = weapon.GetAmmoTypeName().ToString();
                break;
        }
    }

    void ActiveInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InputManager.EnterInventory();
            inventoryUI.SetActive(true);
            inGameUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(ctx.canceled)
        {
            inGameUI.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryUI.SetActive(false);
            InputManager.ExitInventory();
        }
        else
        {
            inGameUI.SetActive(true);
            inventoryUI.SetActive(false);
        }
    }

    private void OnEnable()
    {
        InputManager.inventory += ActiveInventory;
        PlayerWeaponInventory.AddWeaponUI += WeaponUI;

        PlayerStats.UpdateMaxHealth += UpdateHealthText;
        PlayerStats.UpdateMaxShield += UpdateShieldText;
        PlayerStats.UpdateShieldRegen += UpdateShieldRegenText;
        PlayerStats.UpdateMoveSpeed += UpdateMovementText;
        PlayerStats.UpdateDamage += UpdateDamageText;
        PlayerStats.UpdateROF += UpdateROFText;
        PlayerStats.UpdateReload += UpdateReloadText;
        PlayerStats.UpdateRecoil += UpdateRecoilText;
        PlayerStats.UpdateMag += UpdateMagText;
        PlayerStats.UpdateDashCooldown += UpdateDashText;
        PlayerStats.UpdateAbilityCooldown += UpdateAbilityText;
        PlayerStats.UpdateMeleeCooldown += UpdateMeleeText;
        PlayerStats.UpdateGrenadeCooldown += UpdateGrenadeText;
    }

    private void OnDisable()
    {
        InputManager.inventory -= ActiveInventory;
        PlayerWeaponInventory.AddWeaponUI -= WeaponUI;

        PlayerStats.UpdateMaxHealth -= UpdateHealthText;
        PlayerStats.UpdateMaxShield -= UpdateShieldText;
        PlayerStats.UpdateShieldRegen -= UpdateShieldRegenText;
        PlayerStats.UpdateMoveSpeed -= UpdateMovementText;
        PlayerStats.UpdateDamage -= UpdateDamageText;
        PlayerStats.UpdateROF -= UpdateROFText;
        PlayerStats.UpdateReload -= UpdateReloadText;
        PlayerStats.UpdateRecoil -= UpdateRecoilText;
        PlayerStats.UpdateMag -= UpdateMagText;
        PlayerStats.UpdateDashCooldown -= UpdateDashText;
        PlayerStats.UpdateAbilityCooldown -= UpdateAbilityText;
        PlayerStats.UpdateMeleeCooldown -= UpdateMeleeText;
        PlayerStats.UpdateGrenadeCooldown -= UpdateGrenadeText;
    }
}
