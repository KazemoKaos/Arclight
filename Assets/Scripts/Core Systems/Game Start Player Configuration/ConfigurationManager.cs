using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    //Configuration file to configure the player when the game starts.
    public static ConfigurationManager instance;

    [SerializeField] public List<MeleeAbilityTemplate> meleeAbilities;
    [SerializeField] public List<GrenadeAbilityTemplate> grenadeAbilities;
    [SerializeField] public List<MainAbilityTemplate> mainAbilities;

    [SerializeField] public List<GameObject> weapons;

    //Configurable elements
    public GameObject startingWeapon;
    public MainAbilityTemplate startingAbility;
    public MeleeAbilityTemplate startingMelee;
    public GrenadeAbilityTemplate startingGrenade;

    //Configurable stats
    public int gameDifficulty = 1;
    public int startingHP = 10;
    public int startingShield = 10;
    public int CDCoffecient = 10;

    private void Start()
    {
        startingWeapon = weapons[0];
        startingAbility = mainAbilities[0];
        startingGrenade = grenadeAbilities[0];
        startingMelee = meleeAbilities[0];
    }

    public void updateStartingWeapon(int x)
    {
        startingWeapon = weapons[x];
    }
    public void updateStartingAbility(int x)
    {
        startingAbility = mainAbilities[x];
    }
    public void updateStartingMelee(int x)
    {
        startingMelee = meleeAbilities[x];
    }
    public void updateStartingGrenade(int x)
    {
        startingGrenade = grenadeAbilities[x];
    }
    public void updateGameDifficulty(int x)
    {
        gameDifficulty = x;
    }
    public void updateStartingHP(StatAllocation x)
    {
        startingHP = x.HPStat;
    }
    public void updateStartingShield(StatAllocation x)
    {
        startingShield = x.shieldStat;
    }
    public void updateStartingCDCoffecient(StatAllocation x)
    {
        CDCoffecient = x.CDRStat;
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
