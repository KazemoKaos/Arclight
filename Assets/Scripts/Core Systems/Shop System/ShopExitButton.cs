using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopExitButton : MonoBehaviour
{
    public GameObject shopMenu, itemSpawnLocation;
    Shop store;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => closeShop());
        store = shopMenu.GetComponentInParent<Shop>();
    }
    void closeShop()
    {
        TogglePlayerUI.EnableUI?.Invoke();
        Time.timeScale = 1f;
        InputManager.EnableInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        shopMenu.SetActive(false);
        itemSpawnLocation.SetActive(false);
        store.clearDescription();
        store.resetSelector();
    }
}
