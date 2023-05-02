using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, SettingsMenuObj;
    bool isMenuActive = false;
    bool isSettingsMenuActive = false;

    public static Action PauseAudio;
    public static Action ResumeAudio;

    public void ResumeGame()
    {
        isMenuActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ResumeAudio?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InputManager.EnableInput();
        TogglePlayerUI.EnableUI?.Invoke();
    }

    public void OpenSettings()
    {
        isSettingsMenuActive = true;
        pauseMenu.SetActive(false);
        SettingsMenuObj.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        isSettingsMenuActive = false;
        pauseMenu.SetActive(true);
        SettingsMenuObj.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseMenu.SetActive(false);
        TogglePlayerUI.EnableUI?.Invoke();
        isMenuActive = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void OpenPauseMenu()
    {
        if (!isMenuActive)
        {
            Pause();
        }
        else if (isMenuActive && !isSettingsMenuActive)
        {
            ResumeGame();
        }
    }

    void Pause()
    {
        isMenuActive = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseAudio?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        InputManager.DisableInput();
        TogglePlayerUI.DisableUI?.Invoke();
    }

    void OnEnable()
    {
        InputManager.escape += OpenPauseMenu;
    }

    void OnDisable()
    {
        InputManager.escape -= OpenPauseMenu;
    }
}
