using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShopKeeper : MonoBehaviour, IInteractable
{
    public GameObject shopMenu, itemSpawnLocation;
    bool interacted; //Has the shop been opened before?
    public static Action openShop;

    public void StartInteract(PlayerInteraction interactController)
    {
        TogglePlayerUI.DisableUI?.Invoke();
        InputManager.DisableInput();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //openShop?.Invoke();
        shopMenu.SetActive(true);
        itemSpawnLocation.SetActive(true);
        if (!interacted)
        {
            interacted = true;
            gameObject.GetComponent<Shop>().initializeShop();
        }
        Time.timeScale = 0f;
    }

    public void StopInteract()
    {

    }
}
