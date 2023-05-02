using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;
    [SerializeField] AudioClip healNoise;
    AudioSource audioSource;
    public float currentHealth;
    public float currentShield;

    [HideInInspector] public float maxShield;
    [HideInInspector] public float maxHealth;

    [HideInInspector] public float damageReceived;
    [HideInInspector] public float finalBlowDamage;
    [HideInInspector] public string finalBlowDamageOwner;
    [HideInInspector] public int playerDeaths;

    float regenDelay;
    float shieldRegenSpeed;
    float regenedShield;
    float lastDamageTime = -1;

    int playerLives = 1;

    public float GetHealth => currentHealth;
    public float GetShield => currentShield;

    public static event Action UpdateUI;
    public static event Action PlayerHit;
    public static event Action PlayerDeath; // Event for Player Death (Placeholder) | Note that another event needs to be Invoked when the player defeats the final boss |

    private void Start()
    {
        maxHealth = stats.maxHealth;
        maxShield = stats.maxShield;
        regenDelay = stats.regenDelay;
        shieldRegenSpeed = stats.shieldRegen;

        currentHealth = maxHealth;
        currentShield = maxShield;

        UpdateUI?.Invoke();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lastDamageTime >= 0 && Time.time - lastDamageTime >= regenDelay)
        {
            if (currentShield != maxShield)
            {
                regenedShield = shieldRegenSpeed * Time.deltaTime;
                RegenShield(regenedShield);
            }
        }
    }

    public void Damage(float damage, string enemyName)
    {
        if(currentShield - damage < 0)
        {
            if(currentShield > 0) { float temp = Mathf.Abs(currentShield - damage); currentShield = 0f; currentHealth -= temp; } //player loses shield then health
            else if(currentHealth - damage <= 0) { currentHealth = 0; playerDeaths++; finalBlowDamage = damage; finalBlowDamageOwner = enemyName; } //player is dead
            else { currentHealth -= damage; } //player loses health
        }
        else
        {
            currentShield -= damage; //player loses shield
        }
        lastDamageTime = Time.time;

        damageReceived += damage;

        PlayerHit?.Invoke();

        if (currentHealth <= 0) 
        {
            playerLives--;

            if(playerLives == 0) { finalBlowDamage = damage; finalBlowDamageOwner = enemyName; gameObject.layer = 0; PlayerDeath?.Invoke(); }
            else { currentHealth = maxHealth; }
        }                           

        UpdateUI?.Invoke();
    }

    void RegenShield(float amount)
    {
        if (currentShield + amount <= maxShield) { currentShield += amount; }             // If the shield heal amount is less than the max
        else { currentShield = maxShield; regenedShield = 0; lastDamageTime = -1; }

        UpdateUI?.Invoke();
    }

    public void Heal(float amount)
    {
        if(currentHealth + amount <= maxHealth) { currentHealth += amount; }                  // If the amount being healed for is less than what the max health is
        else if(currentHealth < maxHealth)                                                    // If the amount being healed for is greater than the max health
        { 
            currentHealth = maxHealth;                                                        // Set the health back to max
        }
        UpdateUI?.Invoke();
        audioSource.PlayOneShot(healNoise);
    }

    void RestorePlayer()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        UpdateUI?.Invoke();
    }

    void HealthMod(float amt) { maxHealth = amt; UpdateUI?.Invoke(); }
    void ShieldMod(float amt) { maxShield = amt; lastDamageTime = Time.time; UpdateUI?.Invoke(); }
    void RegenDelayMod(float amt) { regenDelay = amt; }
    void ShieldRegenMod(float amt) { shieldRegenSpeed = amt; }
    void PlayerLives(int amt) { playerLives++; }

    private void OnEnable()
    {
        PlayerStats.UpdateMaxHealth += HealthMod;
        PlayerStats.UpdateMaxShield += ShieldMod;
        PlayerStats.UpdateRegenDelay += RegenDelayMod;
        PlayerStats.UpdateShieldRegen += ShieldRegenMod;
        PlayerStats.UpdatePlayerLives += PlayerLives;
        SceneLoader.LoadNextLevel += RestorePlayer;
    }

    private void OnDisable()
    {
        PlayerStats.UpdateMaxHealth -= HealthMod;
        PlayerStats.UpdateMaxShield -= ShieldMod;
        PlayerStats.UpdateRegenDelay -= RegenDelayMod;
        PlayerStats.UpdateShieldRegen -= ShieldRegenMod;
        PlayerStats.UpdatePlayerLives -= PlayerLives;
        SceneLoader.LoadNextLevel -= RestorePlayer;
    }
}
