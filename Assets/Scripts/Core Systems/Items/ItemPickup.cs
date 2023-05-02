using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] public StackableItems item;
    [SerializeField] TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    private void Start()
    {
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.ItemDescription;
    }

    public void StartInteract(PlayerInteraction interactController)
    {
        interactController.itemInventory.AddToInventory(item);
        Destroy(gameObject);
    }

    public void StopInteract()
    {
        // Do nothing
    }
}
