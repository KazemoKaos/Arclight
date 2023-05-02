using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerLevelText;
    int levelBaseline = 1; 
    int level;
    int totalEXP = 0;
    [SerializeField] float hpIncrease = 33f; //HP INCREASE OF PLAYER DURING LEVEL UP
    [SerializeField] float dmgIncrease = 2.4f; //DMG INCREASE OF PLAYER DURING LEVEL UP

    public static event Action<float,float> levelUp;
    public static event Action<int> levelChange;

    void AddEXP(int amt)
    {
        totalEXP += amt;
        level = Mathf.FloorToInt(
           Mathf.Log(1 + 0.0275f * totalEXP, 1.55f) + 1); //Formula used to calculate the player level. Definitely not from ROR2

        if( level > levelBaseline)
        {
            playerLevelText.text = level.ToString(); //Updates player level UI
            levelUp?.Invoke(hpIncrease,dmgIncrease);
            levelChange?.Invoke(level);
            levelBaseline++;
        }
    }

    private void OnEnable()
    {
        EXPDropLoot.expDrop += AddEXP;
    }

    private void OnDisable()
    {
        EXPDropLoot.expDrop -= AddEXP;
    }
}
