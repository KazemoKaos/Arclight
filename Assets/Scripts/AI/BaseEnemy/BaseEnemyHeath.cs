using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHeath : MonoBehaviour, IDamageable
{
    public float currentHealth;
    public float currentShield;
    public float enemyDamage;

    public float maxHealth;
    public float maxShield;

    protected BaseEnemyAI controller;
    BaseEnemyUI enemyUI;

    private void Start()
    {
        enemyUI = GetComponent<BaseEnemyUI>();
        controller = GetComponent<BaseEnemyAI>();

        maxHealth = controller.stats.maxHealth;
        maxShield = controller.stats.maxShield;

        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    public float GetHealth => currentHealth;

    public float GetShield => currentShield;

    public void Damage(float damage, string ownerName)
    {
        if (currentShield - damage < 0)
        {
            if (currentShield > 0) { float temp = Mathf.Abs(currentShield - damage); currentShield = 0; currentHealth -= temp; enemyDamage += damage; }
            else { currentHealth -= damage; enemyDamage += damage; }
        }
        else
        {
            currentShield -= damage;
        }

        if (currentHealth <= 0) { Death(); }

        // Invoke the event for taking damage
        enemyUI.UpdateHealthUI(currentHealth, currentShield);

        // Invoke the enemy hit event
        AbstractEnemy.EnemyHit?.Invoke();

        // Invokes the event for showing the damage number of the object that just took damage
        IDamageable.DamageNumber?.Invoke(transform, damage);
        IDamageable.Hit?.Invoke();
    }

    public void Heal(float amount)
    {
        if (currentHealth + amount <= maxHealth) { currentHealth += amount; }      // If the amount being healed for is less than what the max health is
        else if (currentHealth < maxHealth)                                        // If the amount being healed for is greater than the max health
        {
            float temp = maxHealth - currentHealth;                                 // Subtract the remaing health from the max
            currentHealth = maxHealth;                                            // Set the health back to max
            amount -= temp;                                                             // Subtract the healing amount from the subtraction above
            if (currentShield + amount <= maxShield) { currentShield += amount; }  // If the shield heal amount is less than the max
            else { currentShield = maxShield; }                                   // Else if the current shield heal amount is greater, reset it to max
        }
        else                                                                            // If the health is maxed but shield is not
        {
            if (currentShield + amount <= maxShield) { currentShield += amount; } // If the shield heal amount is less than the max
            else { currentShield = maxShield; }                                   // Else if the current shield heal amount is greater, reset it to max
        }
    }

    /// <summary>
    /// Death function of the AI.
    /// </summary>
    void Death()
    {
        List<Loot> droppedLoot;
        Loot moneyDrop;
        Loot EXPDrop;

        moneyDrop = controller.lootDrops.GetMoneyDrop();
        EXPDrop = controller.lootDrops.GetEXPDrop();

        MoneyDropLoot moneyTemp = Instantiate(moneyDrop.lootObject.GetComponent<MoneyDropLoot>(), transform.position, moneyDrop.lootObject.transform.rotation);
        EXPDropLoot EXPTemp = Instantiate(EXPDrop.lootObject.GetComponent<EXPDropLoot>(), transform.position, EXPDrop.lootObject.transform.rotation);
        moneyTemp.moneyAmount = controller.currencyAmount;
        EXPTemp.expAmount = (int)controller.EXPAmount;

        // Spawn the droppable items
        droppedLoot = controller.lootDrops.GetLootDrop();

        foreach (Loot l in droppedLoot)
        {
            if (l.lootObject != null) { Instantiate(l.lootObject, transform.position, l.lootObject.transform.rotation); }
        }

        // Invoke the enemy defeat event
        AbstractEnemy.EnemyDefeat?.Invoke();

        // Invoke the enemy defeat event with the gameobject
        AbstractEnemy.EnemyDefeatDrop?.Invoke(gameObject);

        // Despawn the enemy
        controller.anims.Play("Die", 0, 0);
        controller.Die();
        Destroy(this);
    }

    void updateStats()
    {
        maxHealth = controller.stats.maxHealth;
        enemyUI.UpdateHealthUI(currentHealth, currentShield);
    }

    private void OnEnable()
    {
        AbstractEnemy.pushUpdatedStats += updateStats;
    }
    private void OnDisable()
    {
        AbstractEnemy.pushUpdatedStats -= updateStats;
    }
}
