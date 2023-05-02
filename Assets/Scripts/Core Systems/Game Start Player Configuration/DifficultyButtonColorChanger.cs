using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonColorChanger : MonoBehaviour
{
    /// <summary>
    /// This script controls the color of the button.
    /// When a difficullty is selected, change all difficulty button color to white then change the button pressed to red.
    /// The easy button is defaulted.
    /// </summary>
    public Button easy;
    public Button normal;
    public Button hard;

    ColorBlock normalColorBlock;
    ColorBlock selectedColorBlock;

    Color selectedColor;


    void Awake()
    {
        // Create a new color block
        normalColorBlock = easy.colors;
        selectedColorBlock = easy.colors;

        // Make the selected color transparent
        selectedColor = easy.colors.selectedColor;
        selectedColor.a = 0.35f;

        selectedColorBlock.normalColor = selectedColor;

        easy.onClick.Invoke();
    }

    /// <summary>
    /// This is used to make all of the buttons white to avoid having multiple red buttons.
    /// </summary>
    void resetButtons()
    {
        easy.colors = normalColorBlock;
        normal.colors = normalColorBlock;
        hard.colors = normalColorBlock;
    }

    public void setGameToEasy()
    {
        resetButtons(); //Makes all of the button white
        easy.colors = selectedColorBlock;
    }

    public void setGameToNormal()
    {
        resetButtons();
        normal.colors = selectedColorBlock;
    }

    public void setGameToHard()
    {
        resetButtons();
        hard.colors = selectedColorBlock;
    }
}
