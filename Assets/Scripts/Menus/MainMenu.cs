using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObj, SettingsMenuObj, ConfigMenu, logbookObj;
    public GameObject soundMenuObj, videoMenuObj, controlsMenuObj, logbookItems, logbookEnemies;
    public GameObject LevelSelectionMenuObj, StartConfirmationMenuObj;

    public static Action PlayMainMenuMusic;

    public void PlayGame()
    {
        MainMenuObj.SetActive(false);
        ConfigMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void EnablePlayerInput()
    {
        InputManager.EnableInput();
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // --------------BUTTON FUNCTIONS--------------
    public void BackToMainMenu()
    {
        MainMenuObj.SetActive(true);
        soundMenuObj.SetActive(false);
        videoMenuObj.SetActive(false);
        controlsMenuObj.SetActive(false);
        logbookItems.SetActive(false);
        logbookEnemies.SetActive(false);
        ConfigMenu.SetActive(false);
    }

    public void OpenLogBook()
    {
        MainMenuObj.SetActive(false);
        logbookObj.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenuObj.SetActive(false);
        SettingsMenuObj.SetActive(true);
        soundMenuObj.SetActive(true);
        videoMenuObj.SetActive(false);
        controlsMenuObj.SetActive(false);
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

    public void OpenLevelSelection()
    {
        ConfigMenu.SetActive(false);
        LevelSelectionMenuObj.SetActive(true);
    }

    public void OpenStartConfirmationMenu()
    {
        StartConfirmationMenuObj.SetActive(true);
    }

    public void CloseStartConfirmation()
    {
        StartConfirmationMenuObj.SetActive(false);
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 1f;
        UnloadAllScenesExcept("MainMenu");
        if (DontDestroy.instance != null) { Destroy(DontDestroy.instance.gameObject); }
        PlayMainMenuMusic?.Invoke();
    }

    public void UnloadAllScenesExcept(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != sceneName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}
