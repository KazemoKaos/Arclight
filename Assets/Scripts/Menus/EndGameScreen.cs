using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    public GameObject EndGameScreenObj, screenObj;

    public void DisplayEndScreen()
    {
        TogglePlayerUI.DisableUI?.Invoke();
        InputManager.DisableInput();
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EndGameScreenObj.SetActive(true);
    }
    
    public void UnpauseAfterContinuing()
    {
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        TogglePlayerUI.EnableUI?.Invoke();
        screenObj.SetActive(!screenObj.activeSelf);
    }

    void OnEnable()
    {
        PlayerHealth.PlayerDeath += DisplayEndScreen;
    }

    void OnDisable()
    {
        PlayerHealth.PlayerDeath -= DisplayEndScreen;
    }
}
