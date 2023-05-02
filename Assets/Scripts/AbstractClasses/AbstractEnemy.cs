using UnityEngine;
using System;

public abstract class AbstractEnemy : MonoBehaviour
{
    // Scriptable object for its stats
    public EnemyStats stats;

    protected EnemyLevel levelGrab;
    // Needed to determine the starting stats.
    //public ScalingManager difficultyScale;
    public float coeff;
    public float rewardMult = 0.2f;     // Default value

    [HideInInspector] public string enemyName;
    [HideInInspector] public Sprite enemyImage;
    [HideInInspector] public GameObject projectile;

    public int enemyLevel;
    [HideInInspector] public int enemyValue;

    // Max health
    [HideInInspector] public int maxHealth;
    // Max shield
    [HideInInspector] public int maxShield;
    // Damage
    [HideInInspector] public float baseDamage; //Used for normal AiEnemies
    [HideInInspector] public float damageModifier; //Used for bosses; Example: skilldmg = (base dmg of skill)(damageModifier)
    // How much currency/EXP it drops
    [HideInInspector] public float EXPAmount;
     public int currencyAmount;
    // What the enemy does drop (Loot Manager)
    [HideInInspector] public LootManager lootDrops;

    public static event Action pushUpdatedStats;

    private void Awake()
    {
        levelGrab = EnemyLevel.instance;
        coeff = levelGrab.coeff;
        initializeEnemy();
    }

    private void Start()
    {
        levelGrab = EnemyLevel.instance;
        coeff = levelGrab.coeff;
        initializeEnemy();
    }

    protected void updateStats()
    {
        coeff = levelGrab.coeff;
        enemyLevel = Mathf.FloorToInt(1 + coeff / 0.33f); //Formula used by ROR
        maxHealth += Mathf.RoundToInt(stats.maxHealth * .3f);
        baseDamage += Mathf.RoundToInt(stats.baseDamage * .2f);
        damageModifier += .2f;
        
        EXPAmount = coeff * enemyValue * rewardMult; //Formula used by ROR 
        currencyAmount = Mathf.CeilToInt(2 * EXPAmount); //Formula used by ROR 
        pushUpdatedStats?.Invoke();
        UpdateStats();
    }

    void initializeEnemy()
    {
        enemyName = stats.enemyName;
        enemyImage = stats.enemyImage;
        projectile = stats.projectile;

        enemyLevel = Mathf.FloorToInt(1 + coeff / 0.33f); //Formula used by ROR
        enemyValue = stats.enemyValue;
        maxShield = stats.maxShield;

        maxHealth = Mathf.RoundToInt(stats.maxHealth + (stats.maxHealth * (enemyLevel * .3f)));
        baseDamage = Mathf.RoundToInt(stats.baseDamage + (stats.baseDamage * (enemyLevel * .2f)));
        damageModifier = enemyLevel * .2f;
        EXPAmount = coeff * enemyValue * rewardMult; //Formula used by ROR 
        currencyAmount = Mathf.CeilToInt(2 * EXPAmount); //Formula used by ROR 
    }

    protected virtual void UpdateStats() { }

    // ------------------EVENTS------------------
    // Event for any enemy getting hit
    public static Action EnemyHit;

    // Event for enemy being defeated
    public static Action EnemyDefeat;

    // Event for enemy being defeated, but passes in the gameobject of the enemy
    public static Action<GameObject> EnemyDefeatDrop;

    // Event for boss being defeated
    public static Action BossDefeated;

    private void OnEnable()
    {
        EnemyLevel.enemyLevelUp += updateStats;
    }

    private void OnDisable()
    {
        EnemyLevel.enemyLevelUp -= updateStats;
    }
}
