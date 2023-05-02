using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TogglePlayerUI : MonoBehaviour
{
    [SerializeField] GameObject playerUIObj;

    public static Action DisableUI, EnableUI;


    public void DisablePlayerUI()
    {
        playerUIObj.SetActive(false);
    }

    public void EnablePlayerUI()
    {
        playerUIObj.SetActive(true);
    }


    private void OnEnable()
    {
        TogglePlayerUI.EnableUI += EnablePlayerUI;
        TogglePlayerUI.DisableUI += DisablePlayerUI;
    }

    private void OnDisable()
    {
        TogglePlayerUI.EnableUI -= EnablePlayerUI;
        TogglePlayerUI.DisableUI -= DisablePlayerUI;
    }
}
