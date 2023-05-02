using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays the current stage that the player is on
/// </summary>
public class StageNum : MonoBehaviour
{
    public TextMeshProUGUI stageNum;
    public int stage;

    private void Start()
    {
        stageNum = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Updates the UI for the current stage. Call this from an event that controls what stage is active
    /// </summary>
    void UpdateLevelText()
    {
        stageNum.text = stage.ToString();
    }
}
