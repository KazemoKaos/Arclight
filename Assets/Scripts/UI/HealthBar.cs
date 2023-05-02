using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Slider shieldBar;
    [SerializeField] PlayerHealth health;

    [SerializeField] TextMeshProUGUI healthNum;
    [SerializeField] TextMeshProUGUI shieldNum;


    void UpdateUI()
    {
        healthBar.value = (int)health.currentHealth;
        healthBar.maxValue = health.maxHealth;
        healthNum.text = healthBar.value.ToString();

        shieldBar.value = (int)health.currentShield;
        shieldBar.maxValue = health.maxShield;
        shieldNum.text = shieldBar.value.ToString();
    }

    private void OnEnable()
    {
        PlayerHealth.UpdateUI += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerHealth.UpdateUI -= UpdateUI;
    }
}
