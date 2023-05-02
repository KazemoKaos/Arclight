using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadEndGameData : MonoBehaviour
{
    //Get References
    public Timer timer;
    public PlayerCurrency currency;
    public ItemInventory inventory;
    public PlayerHealth health;
    public StageTracker stage;

    int killCount = 0;
    int bossKillCount = 0;
    float damageDealt = 0f;

    //Text Fields
    [SerializeField] TextMeshProUGUI totalTimeText;
    [SerializeField] TextMeshProUGUI killsText;
    [SerializeField] TextMeshProUGUI deathsText;
    [SerializeField] TextMeshProUGUI damageDealtText;
    [SerializeField] TextMeshProUGUI damageTakenText;
    [SerializeField] TextMeshProUGUI stagesCompletedText;
    [SerializeField] TextMeshProUGUI totalCreditsText;
    [SerializeField] TextMeshProUGUI itemsCollectedText;
    [SerializeField] TextMeshProUGUI bossesDefeatedText;
    [SerializeField] TextMeshProUGUI killedByText;
    [SerializeField] TextMeshProUGUI damageDoneText;

    public static Action<Timer> sendTimer; 
    public static Action endGame; 

    public void DisplayAndLoadInfo()
    {
        //Timer
        totalTimeText.text = timer.GetTimer;

        //Kills
        killsText.text = killCount.ToString();

        //Deaths
        deathsText.text = health.playerDeaths.ToString();

        //Damage Dealt
        damageDealtText.text = damageDealt.ToString();

        //Damage Taken
        damageTakenText.text = health.damageReceived.ToString();

        //Stages Completed
        stagesCompletedText.text = stage.GetCurrentStages().ToString();

        //Total Credits
        totalCreditsText.text = currency.GetTotalCurrency().ToString();

        //Items Collected
        itemsCollectedText.text = inventory.GetTotalItems().ToString();

        //Bosses Defeated
        bossesDefeatedText.text = bossKillCount.ToString();

        //Killed By
        killedByText.text = health.finalBlowDamageOwner;
        
        //Damage Done (By Enemy)
        damageDoneText.text = health.finalBlowDamage.ToString();

        //Pushes data to achievementManager
        sendTimer?.Invoke(timer);
        endGame?.Invoke();
    }

    void IncreaseKillCount()
    {
        killCount++;
    }

    void DamageDone(Transform temp, float damage)
    {
        damageDealt += damage;
    }

    void IncreaseBossKillCount()
    {
        bossKillCount++;
    }

    void OnEnable()
    {
        PlayerHealth.PlayerDeath += DisplayAndLoadInfo;
        AbstractEnemy.EnemyDefeat += IncreaseKillCount;
        AbstractEnemy.BossDefeated += IncreaseBossKillCount;
        IDamageable.DamageNumber += DamageDone;
    }

    void OnDisable()
    {
        PlayerHealth.PlayerDeath -= DisplayAndLoadInfo;
        AbstractEnemy.EnemyDefeat -= IncreaseKillCount;
        AbstractEnemy.BossDefeated -= IncreaseBossKillCount;
        IDamageable.DamageNumber -= DamageDone;
    }
}
