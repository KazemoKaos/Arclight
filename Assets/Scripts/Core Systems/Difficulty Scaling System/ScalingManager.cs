using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalingManager : MonoBehaviour
{
    public static ScalingManager instance;
    //Scaling
    [SerializeField] public float diffScale;        // Difficulty scale of the game (Main Variable)
    float stageScale;                               // Stage Scaling on difficulty
    float timeInMinutes;                            // The time, in minutes, passed

    //Factors
    [SerializeField] public float intDiff = 3;          // Initial Difficulty (from GameConfig)
    float timeFactor;       // Time factor
    float stageFactor;      // Stage Factor
    [SerializeField] StageTracker stageCompleted;     //Number of stages completed
    [SerializeField] Timer timer;


    void Start()
    {
        instance = this;

        // Time Factor Initialization
        timeFactor = 0.0506f * intDiff;

        // Stage Factor Initialization
        stageFactor = 1.15f;
    }

    void Update()
    {
        updateDiffScale(); // Difficulty Scale Calculation
    }

    public void SetDifficulty(int diff) { intDiff = diff; }

    /// <summary>
    /// Update the overall scaling of all difficulty based values in the game.
    /// 
    /// This is the main variable that all scaled objects will use.
    /// </summary>
    void updateDiffScale()
    { 
        timeInMinutes = Mathf.FloorToInt(timer.timer / 60);

        updateStageScale();                 

        diffScale = (1 + timeInMinutes * timeFactor) * stageScale;  //difficulty calculation
    }

    /// <summary>
    /// Updates the current stage factor based on the number of stages completed
    /// </summary>
    void updateStageScale()
    {
        stageScale = Mathf.Pow(stageFactor, stageCompleted.GetCurrentStages());
    }
}
