using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InteractableChest : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Chest Implementation. Each chest should know how much they cost, and should drop an item when the player buys it.
    /// </summary>
    
    LootManager lootTable;
    [SerializeField] ScalingManager difficultyScale;
    [SerializeField] Vector3 spawnOffset;               // Offset for the spawning loot object. This is to prevent it from spawning in the actual chest itself
    [SerializeField] Vector3 chestOffset;
    [SerializeField] float baseCost = 25f;              // Base cost of chest
    [SerializeField] float multiplier = 1.25f;          // Used in chest multipler calculation.
    [SerializeField] TextMeshProUGUI priceText;
    Animator chestAnimation;
    float chestMultiplier;                              // Used in chest price calculation
    float chestPrice;                                   // Cost of chest
    List<Loot> droppedLoot;

    bool purchased;

    public static Action purchasedChest;

    void Start()
    {
        difficultyScale = DontDestroy.instance.GetComponentInChildren<ScalingManager>();
        //Chest Price Calculations
        chestMultiplier = Mathf.Pow(difficultyScale.diffScale, multiplier);
        chestPrice = Mathf.RoundToInt((baseCost * chestMultiplier) / 5) * 5;    // Round chest to nearest multiple of 5
        priceText.text = "$" + chestPrice.ToString();
        //The diffScale is 0 when the game starts. This will cause the chest to be free.
        if (chestPrice < baseCost)
        {
            chestPrice = baseCost;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + chestOffset.y, transform.position.z);

        lootTable = GetComponent<LootManager>();
        chestAnimation = GetComponent<Animator>();
        droppedLoot = new List<Loot>();
    }

    public void StartInteract(PlayerInteraction interactController)
    {
        if (interactController.currency.getCurrency() >= chestPrice && !purchased)
        {
            droppedLoot = lootTable.GetLootDrop(); //Determines loot when the chest is interacted with. 
            interactController.currency.removeCurrency((int)chestPrice);
            if (chestAnimation) { chestAnimation.SetBool("Opened", true); }
            dropItem();
            purchasedChest?.Invoke();
            purchased = true;
            priceText.enabled = false;
        }
    }

    public void StopInteract()
    {
        // Do nothing
    }

    void dropItem()
    {
        foreach(Loot l in droppedLoot)
        {
            GameObject temp = Instantiate(l.lootObject, gameObject.transform.position + spawnOffset, l.lootObject.transform.rotation);
            temp.GetComponent<Rigidbody>().AddForce(Vector3.up * 3f, ForceMode.Impulse);
        }
    }
}
