using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseEnemyUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider shieldBar;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] TextMeshProUGUI enemyLevelText;

    BaseEnemyAI controller;

    void Start()
    {
        controller = GetComponent<BaseEnemyAI>();
        enemyNameText.text = controller.stats.enemyName;
        enemyLevelText.text = controller.enemyLevel.ToString();

        healthBar.maxValue = controller.maxHealth;
        healthBar.value = controller.maxHealth;
        shieldBar.maxValue = controller.maxShield;
        shieldBar.value = controller.maxShield;
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
