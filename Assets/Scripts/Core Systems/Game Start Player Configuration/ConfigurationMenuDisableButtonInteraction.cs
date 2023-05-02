using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Turns off the interaction of buttons to avoid having multiple menus opening at the same time.
/// </summary>
public class ConfigurationMenuDisableButtonInteraction : MonoBehaviour
{
    [SerializeField] List<Button> leftPanel;

    public void deactiveLPButtons()
    {
        foreach (Button lpButton in leftPanel)
        {
            lpButton.interactable = false;
        }
    }

    public void activeLPButtons()
    {
        foreach (Button lpButton in leftPanel)
        {
            lpButton.interactable = true;
        }
    }
}
