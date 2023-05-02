using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public Text objText;
    public bool isPlaying;
    public string lvlObjective;

    private void Start()
    {
        if (isPlaying == true)
        {
            objText.text = "Objective: " + lvlObjective;
        }
    }
}
