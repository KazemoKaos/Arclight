using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    public static EnemyLevel instance;
    int enemyLevelChecker; //Used to check if the enemy has leveled up.
    int enemyLevel = 1;
    public ScalingManager difficultyScale;
    public float coeff;

    public static event Action enemyLevelUp;

    private void Start()
    {
        instance = this;
        coeff = difficultyScale.diffScale;
        enemyLevel = Mathf.FloorToInt(1 + coeff / 0.33f); //Formula used by ROR
        enemyLevelChecker = enemyLevel;
    }

    void Update()
    {
        coeff = difficultyScale.diffScale;

        //Keeps updating the enemy level. Once it is reached, level up.
        enemyLevel = Mathf.FloorToInt(1 + coeff / 0.33f); //Formula used by ROR

        if (enemyLevel > enemyLevelChecker)
        {
            enemyLevelUp?.Invoke(); // Call level up function in enemy
            enemyLevelChecker++;
        }
    }
}
