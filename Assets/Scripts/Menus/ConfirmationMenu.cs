using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationMenu : MonoBehaviour
{
    public GameObject confirmationMenu;
    public GameObject levelSelectionMenu;

    public void Confirm()
    {
        confirmationMenu.SetActive(false);
        //levelSelectionMenu.SetActive(true);
    }

    public void Deny()
    {
        confirmationMenu.SetActive(false);
        InputManager.EnableInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OpenConfirmMenu()
    {
        confirmationMenu.SetActive(true);
        InputManager.DisableInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OpenLevelSelectionMenu()
    {
        TogglePlayerUI.DisableUI?.Invoke();
        levelSelectionMenu.SetActive(true);
        InputManager.DisableInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseLevelSelectionMenu()
    {
        levelSelectionMenu.SetActive(false);
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
    
    public void DestroyPlayer()
    {
        Destroy(transform.root.gameObject);
    }

    private void OnEnable()
    {
        InteractablePortal.ConfirmMenu += OpenLevelSelectionMenu;
    }

    private void OnDisable()
    {
        InteractablePortal.ConfirmMenu -= OpenLevelSelectionMenu;
    }
}
