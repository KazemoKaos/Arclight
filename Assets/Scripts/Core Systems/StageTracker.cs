using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageNumText;
    int currentStages = 1;

    public int GetCurrentStages() { return currentStages; }

    void UpdateStage()
    {
        currentStages++;
        stageNumText.text = (currentStages).ToString();
    }

    private void OnEnable()
    {
        SceneLoader.LoadNextLevel += UpdateStage;
    }

    private void OnDisable()
    {
        SceneLoader.LoadNextLevel -= UpdateStage;
    }
}
