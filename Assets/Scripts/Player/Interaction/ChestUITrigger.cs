using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUITrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;

    private bool isTriggered = false;
    //private bool isPurchased = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damagable"))
        {
            if (!isTriggered /*&& !isPurchased*/)
            {
                uiElement.SetActive(true);
                isTriggered = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        uiElement.SetActive(false);
        isTriggered = false;
    }

    public void DisableUI()
    {
        uiElement.SetActive(false);
        //isPurchased = true;
        isTriggered = false;
    }

    private void OnEnable()
    {
        InteractableChest.purchasedChest += DisableUI;
        InteractableShopKeeper.openShop += DisableUI;
    }

    private void OnDisable()
    {
        InteractableChest.purchasedChest -= DisableUI;
        InteractableShopKeeper.openShop += DisableUI;
    }
}
