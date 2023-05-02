using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject menuCam;
    GameObject spawnedPlayer;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public ConfigurationManager config;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePlayer(int levelIndex)
    {
        menuCam.SetActive(false);

        spawnedPlayer = Instantiate(player);

        // Set the 3 inital stats
        spawnedPlayer.GetComponent<PlayerStats>().StartStats(config.startingHP, config.startingShield, config.CDCoffecient);

        // Set the difficulty
        spawnedPlayer.GetComponentInChildren<ScalingManager>().SetDifficulty(config.gameDifficulty);

        // Set the weapon
        GameObject tempWep = Instantiate(config.startingWeapon);
        tempWep.GetComponent<InteractableWeapon>().StartInteract(spawnedPlayer.GetComponent<PlayerInteraction>());

        // Set the abilities
        spawnedPlayer.GetComponentInChildren<PlayerAbilities>().SetAbilities(config.startingAbility, config.startingMelee, config.startingGrenade);

        //spawnedPlayer.SetActive(true);

        // Load the next scene
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        InputManager.DisableInput();
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }

        //SceneLoader.LoadNextLevel?.Invoke();
        RestoreControl();
    }

    void RestoreControl()
    {
        InputManager.EnableInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
}
