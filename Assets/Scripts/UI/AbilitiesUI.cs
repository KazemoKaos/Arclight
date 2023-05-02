using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesUI : MonoBehaviour
{
    [SerializeField] Image meleeAbilityImage;
    [SerializeField] Image grenadeAbilityImage;
    [SerializeField] Image mainAbilityImage;

    [SerializeField] Slider meleeSlider;
    [SerializeField] Slider grenadeSlider;
    [SerializeField] Slider mainAbilitySlider;
    [SerializeField] Slider dashSlider;

    [SerializeField] TextMeshProUGUI meleeChargeText;
    [SerializeField] TextMeshProUGUI grenadeChargeText;
    [SerializeField] TextMeshProUGUI mainAbilityChargeText;
    [SerializeField] TextMeshProUGUI dashChargeText;

    void UpdateMeleeCharge(int count) { meleeChargeText.text = count.ToString(); }
    void UpdateGrenadeCharge(int count) { grenadeChargeText.text = count.ToString(); }
    void UpdateMainAbilityCharge(int count) { mainAbilityChargeText.text = count.ToString(); }
    void UpdateDashCharge(int count) { dashChargeText.text = count.ToString(); }

    void UpdateMeleeSlider(float val) { meleeSlider.value = val; }
    void UpdateGrenadeSlider(float val) { grenadeSlider.value = val; }
    void UpdateMainAbilitySlider(float val) { mainAbilitySlider.value = val; }
    void UpdateDashSlider(float val) { dashSlider.value = val; }

    void UpdateMeleeCooldownMax(float val) { meleeSlider.maxValue = val; }
    void UpdateGrenadeCooldownMax(float val) { grenadeSlider.maxValue = val; }
    void UpdateMainAbilityCooldownMax(float val) { mainAbilitySlider.maxValue = val; }
    void UpdateDashCooldownMax(float val) { dashSlider.maxValue = val; }


    private void OnEnable()
    {
        MeleeAbilityTemplate.UpdateMeleeChargeUI += UpdateMeleeCharge;
        MeleeAbilityTemplate.UpdateMeleeCooldownUI += UpdateMeleeSlider;
        MeleeAbilityTemplate.UpdateMeleeCooldownMax += UpdateMeleeCooldownMax;

        GrenadeAbilityTemplate.UpdateGrenadeChargeUI += UpdateGrenadeCharge;
        GrenadeAbilityTemplate.UpdateGrenadeCooldownUI += UpdateGrenadeSlider;
        GrenadeAbilityTemplate.UpdateGrenadeCooldownMax += UpdateGrenadeCooldownMax;

        MainAbilityTemplate.UpdateMainAbilityChargeUI += UpdateMainAbilityCharge;
        MainAbilityTemplate.UpdateMainAbilityCooldownUI += UpdateMainAbilitySlider;
        MainAbilityTemplate.UpdateMainAbilityCooldownMax += UpdateMainAbilityCooldownMax;

        PlayerDash.UpdateDashMaxCooldown += UpdateDashCooldownMax;
        PlayerDash.UpdateDashCooldownUI += UpdateDashSlider;
        PlayerDash.UpdateDashChargeUI += UpdateDashCharge;
    }

    private void OnDisable()
    {
        MeleeAbilityTemplate.UpdateMeleeChargeUI -= UpdateMeleeCharge;
        MeleeAbilityTemplate.UpdateMeleeCooldownUI -= UpdateMeleeSlider;
        MeleeAbilityTemplate.UpdateMeleeCooldownMax -= UpdateMeleeCooldownMax;

        GrenadeAbilityTemplate.UpdateGrenadeChargeUI -= UpdateGrenadeCharge;
        GrenadeAbilityTemplate.UpdateGrenadeCooldownUI -= UpdateGrenadeSlider;
        GrenadeAbilityTemplate.UpdateGrenadeCooldownMax -= UpdateGrenadeCooldownMax;

        MainAbilityTemplate.UpdateMainAbilityChargeUI -= UpdateMainAbilityCharge;
        MainAbilityTemplate.UpdateMainAbilityCooldownUI -= UpdateMainAbilitySlider;
        MainAbilityTemplate.UpdateMainAbilityCooldownMax -= UpdateMainAbilityCooldownMax;

        PlayerDash.UpdateDashMaxCooldown -= UpdateDashCooldownMax;
        PlayerDash.UpdateDashCooldownUI -= UpdateDashSlider;
        PlayerDash.UpdateDashChargeUI -= UpdateDashCharge;
    }
}
