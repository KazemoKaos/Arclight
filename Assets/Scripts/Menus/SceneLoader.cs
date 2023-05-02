using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public GameObject player;

    public static Action LoadNextLevel;

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    public void LoadGameStart(int levelIndex, GameObject player)
    {
        this.player = player;
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
        player.SetActive(true);
    }

    void RestoreControl()
    {
        InputManager.EnableInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        loadingScreen.SetActive(false);
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
        LoadNextLevel?.Invoke();

        RestoreControl();
    }
}
