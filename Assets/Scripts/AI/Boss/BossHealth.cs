using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] MechBoss bossAI;
    GameObject bossHealthBar;
    [SerializeField] GameObject bossHealthBarPrefab;
    [SerializeField] Transform lootDropPoint;
    [SerializeField] AudioClip deathNoise;          // Noise made when defeated
    AudioSource audioSource;
    public float lootDropForce;
    LootManager loot;
    List<Loot> droppedItems;

    Slider healthBar;
    TextMeshProUGUI bossName;

    float currentHealth;
    float currentShield;

    public float GetHealth => currentHealth;

    public float GetShield => currentShield;

    private void Start()
    {
        bossHealthBar = Instantiate(bossHealthBarPrefab, transform);

        healthBar = bossHealthBar.GetComponentInChildren<Slider>();
        healthBar.maxValue = bossAI.maxHealth;
        currentHealth = bossAI.maxHealth;
        healthBar.value = bossAI.maxHealth;

        bossName = bossHealthBar.GetComponentInChildren<TextMeshProUGUI>();
        bossName.text = bossAI.enemyName;

        loot = GetComponent<LootManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Damage(float damage, string ownerName)
    {
        if (currentHealth > 0)
        {
            if (currentShield - damage < 0)
            {
                if (currentShield > 0) { float temp = Mathf.Abs(currentShield - damage); currentShield = 0; currentHealth -= temp; }
                else { currentHealth -= damage; }

                if (currentHealth < 0) { currentHealth = 0; }
            }
            else
            {
                currentShield -= damage;
            }

            if (currentHealth <= 0) { Death(); }

            // Invoke the event for taking damage
            healthBar.value = currentHealth;

            // Invoke the enemy hit event
            AbstractEnemy.EnemyHit?.Invoke();

            // Invokes the event for showing the damage number of the object that just took damage
            IDamageable.DamageNumber?.Invoke(transform, damage);
            IDamageable.Hit?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth + amount <= bossAI.maxHealth) { currentHealth += amount; }      // If the amount being healed for is less than what the max health is
        else if (currentHealth < bossAI.maxHealth)                                        // If the amount being healed for is greater than the max health
        {
            float temp = bossAI.maxHealth - currentHealth;                                 // Subtract the remaing health from the max
            currentHealth = bossAI.maxHealth;                                            // Set the health back to max
            amount -= temp;                                                             // Subtract the healing amount from the subtraction above
            if (currentShield + amount <= bossAI.maxShield) { currentShield += amount; }  // If the shield heal amount is less than the max
            else { currentShield = bossAI.maxShield; }                                   // Else if the current shield heal amount is greater, reset it to max
        }
        else                                                                            // If the health is maxed but shield is not
        {
            if (currentShield + amount <= bossAI.maxShield) { currentShield += amount; } // If the shield heal amount is less than the max
            else { currentShield = bossAI.maxShield; }                                   // Else if the current shield heal amount is greater, reset it to max
        }
        healthBar.value = currentHealth;
    }

    IEnumerator DestoryHealthBar()
    {
        yield return new WaitForSeconds(7f);
        Destroy(bossHealthBar);
    }

    /// <summary>
    /// Death function of the AI.
    /// </summary>
    void Death()
    {
        // Disable AI
        GetComponent<NavMeshAgent>().enabled = false;

        // Play death animation
        GetComponent<Animator>().Play("Death");

        // Play death noise
        audioSource.PlayOneShot(deathNoise);

        // Drop items
        droppedItems = loot.GetLootDrop();
        foreach(Loot l in droppedItems)
        {
            GameObject temp;
            temp = Instantiate(l.lootObject, lootDropPoint.position, l.lootObject.transform.rotation);
            temp.GetComponent<Rigidbody>().AddForce(temp.transform.up * Random.Range(5f, lootDropForce), ForceMode.Impulse);
        }

        // Destroy healthbar
        StartCoroutine(DestoryHealthBar());

        // Spawn portal to go to next level
        AbstractEnemy.BossDefeated?.Invoke();
    }

    void enemyLevelUp()
    {
        healthBar.maxValue = bossAI.maxHealth;
    }

    private void OnEnable()
    {
        AbstractEnemy.pushUpdatedStats += enemyLevelUp;
    }
    private void OnDisable()
    {
        AbstractEnemy.pushUpdatedStats -= enemyLevelUp;
    }
}
