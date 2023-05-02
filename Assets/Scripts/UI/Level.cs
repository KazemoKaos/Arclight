using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the level of the enemies
/// </summary>
public class Level : MonoBehaviour
{
    public Text LevelText;
    public bool isPlaying;
    public string currentLevel;

    private void Start()
    {
        if(isPlaying == true)
        {
            LevelText.text = "Level: " + currentLevel;
        }
    }

}
