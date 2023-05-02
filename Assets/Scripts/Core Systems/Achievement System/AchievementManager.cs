using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public static int _achieve01;
    private static string _achieve01Count = "Advancement";
    public static int _achieve02;
    private static string _achieve02Count = "Learning Process";
    public static int _achieve03;
    private static string _achieve03Count = "Money Bags";
    public static int _achieve04;
    private static string _achieve04Count = "Big Spender";
    public static float _achieve05;
    private static string _achieve05Count = "Survivor";
    public static int _achieve06;
    private static string _achieve06Count = "Macho";

    private void Awake()
    {
        singleton();
        loadData();
    }

    /// <summary>
    /// Amount of chests opened. Achievement is earned at 1 chest opened.
    /// </summary>
    public static void achievement01()
    {
        _achieve01++;
    }

    /// <summary>
    /// Amount of deaths. Achievement is earned at 5 total deaths.
    /// </summary>
    public static void achievement02()
    {
        _achieve02++;
    }

    /// <summary>
    /// Total amount of currency held at one time. Achievement is earned when player has 5000
    /// </summary>
    /// <param name="x"></param>
    public static void achievement03(int x)
    {
        int max = _achieve03;
        if (x > max) _achieve03 = x;
    }

    /// <summary>
    /// Total amount of currency spent. Achievement is earned when player spent 10000.
    /// </summary>
    /// <param name="x"></param>
    public static void achievement04(int x)
    {
        _achieve04 += x;
    }

    /// <summary>
    /// Longest time the player has stayed alive. Achievement is earned when player survives for 30 mins.
    /// </summary>
    /// <param name="x"></param>
    public static void achievement05(Timer x)
    {
        float max = _achieve05 * 60f; //get seconds
        float time = x.timer;
        if (time > max) _achieve05 = time / 60f; //Convert back to minutes
    }

    /// <summary>
    /// Highest level the player has gotten to. Achievement is earned when the player reaches level 30.
    /// </summary>
    /// <param name="x"></param>
    public static void achievement06(int x)
    {
        int max = _achieve06; 
        if (x > max) _achieve06 = x; 
    }

    public static void save()
    {
        //PlayerPrefs.SetInt(); for all achievements
        PlayerPrefs.SetInt(_achieve01Count, _achieve01);
        PlayerPrefs.SetInt(_achieve02Count, _achieve02);
        PlayerPrefs.SetInt(_achieve03Count, _achieve03);
        PlayerPrefs.SetInt(_achieve04Count, _achieve04);
        PlayerPrefs.SetFloat(_achieve05Count, _achieve05);
        PlayerPrefs.SetInt(_achieve06Count, _achieve06);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        save();
    }

    void loadData()
    {
        _achieve01 = PlayerPrefs.GetInt(_achieve01Count, 0);
        _achieve02 = PlayerPrefs.GetInt(_achieve02Count, 0);
        _achieve03 = PlayerPrefs.GetInt(_achieve03Count, 0);
        _achieve04 = PlayerPrefs.GetInt(_achieve04Count, 0);
        _achieve05 = PlayerPrefs.GetFloat(_achieve05Count, 0);
        _achieve06 = PlayerPrefs.GetInt(_achieve06Count, 0);
    }

    void singleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        InteractableChest.purchasedChest += achievement01;
        LoadEndGameData.endGame += achievement02;
        PlayerCurrency.UpdateCurrencyUI += achievement03;
        PlayerCurrency.MoneySpent += achievement04;
        LoadEndGameData.sendTimer += achievement05;
        PlayerLevel.levelChange += achievement06;
        LoadEndGameData.endGame += save;
    }

    private void OnDisable()
    {
        InteractableChest.purchasedChest -= achievement01;
        LoadEndGameData.endGame -= achievement02;
        PlayerCurrency.UpdateCurrencyUI -= achievement03;
        PlayerCurrency.MoneySpent -= achievement04;
        LoadEndGameData.sendTimer -= achievement05;
        PlayerLevel.levelChange -= achievement06;
        LoadEndGameData.endGame -= save;
    }
}
