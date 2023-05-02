using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] GameObject LevelSelectionMenuObj;

    public void OpenLevelSelectionMenu()
    {
        LevelSelectionMenuObj.SetActive(true);
        TogglePlayerUI.DisableUI?.Invoke();
        DisablePlayerInput();
    }

    public void CloseLevelSelectionMenu()
    {
        LevelSelectionMenuObj.SetActive(false);
        TogglePlayerUI.EnableUI?.Invoke();
        EnablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        InputManager.EnableInput();
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisablePlayerInput()
    {
        InputManager.DisableInput();
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnEnable()
    {
        OpenLevelSelect.OpenLevelSelection += OpenLevelSelectionMenu;
    }

    private void OnDisable()
    {
        OpenLevelSelect.OpenLevelSelection -= OpenLevelSelectionMenu;
    }
}
