using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider shieldBar;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] TextMeshProUGUI enemyLevelText;

    EnemyAI controller;

    void Start()
    {
        controller = GetComponent<EnemyAI>();
        enemyNameText.text = controller.enemyName;
        enemyLevelText.text = controller.enemyLevel.ToString();

        healthBar.maxValue = controller.stats.maxHealth;
        shieldBar.maxValue = controller.stats.maxShield;
    }

    // This gets called from EnemyHealth whenever the enemy takes damage.
    public void UpdateHealthUI(float currentHealth, float currentShield)
    {
        healthBar.value = currentHealth;
        shieldBar.value = currentShield;
    }

    public void UpdateLevelUI()
    {
        enemyLevelText.text = controller.enemyLevel.ToString();
    }
}
