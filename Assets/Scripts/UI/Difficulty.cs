using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public Text DiffText;
    public bool isPlaying;
    public string diff;

    private void Start()
    {
        if (isPlaying == true)
        {
            DiffText.text = "Level: " + diff;
        }
    }
}
