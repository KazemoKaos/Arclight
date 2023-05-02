using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtons : MonoBehaviour
{
    /// <summary>
    /// This script controls the color of the button.
    /// When a difficullty is selected, change all difficulty button color to white then change the button pressed to red.
    /// The easy button is defaulted.
    /// </summary>
    public Button easy;
    public Button normal;
    public Button hard;
    void Start()
    {//Setting default
        easy.onClick.Invoke();
    }

    void resetButtons()
    {//This is used to make all of the buttons white to avoid having multiple red buttons.
        easy.image.color = Color.white;
        normal.image.color = Color.white;
        hard.image.color = Color.white;
    }
    public void setGameToEasy()
    {
        resetButtons(); //Makes all of the button white
        easy.image.color = Color.red; //Makes this button red to show it has been selected.
    }

    public void setGameToNormal()
    {
        resetButtons();
        normal.image.color = Color.red;
    }

    public void setGameToHard()
    {
        resetButtons();
        hard.image.color = Color.red;
    }
}
