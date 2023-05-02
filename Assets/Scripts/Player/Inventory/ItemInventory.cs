using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] GameObject itemInv;
    [SerializeField] ItemInventoryUI inventoryUI;
    [SerializeField] AudioClip itemPickupNoise;
    AudioSource audioSource;

    public class ItemStack
    {
        public Item item;
        public int count;
    }

    public List<ItemStack> items;

    private void Awake() { items = new List<ItemStack>(); audioSource = GetComponent<AudioSource>(); }

    /// <summary>
    /// Adds item to the player inventory
    /// </summary>
    /// <param name="itm"></param>
    public void AddToInventory(StackableItems itm)
    {
        audioSource.PlayOneShot(itemPickupNoise);

        // Check if that item is already in the list
        foreach (ItemStack item in items)
        {
            // If it is, apply its effect again and exit
            if(item.item.itemID == itm.itemID) { item.item.count++; item.item.ApplyEffect(); inventoryUI.IncrementItemCount(itm.itemID); return; }
        }

        // If not in the list, add it to the list and apply its effect
        items.Add(new ItemStack { item = Instantiate(itm.itemObj, itemInv.transform).GetComponent<Item>(), count = 1 });
        inventoryUI.AddItem(itm.itemID, itm);
        items[items.Count - 1].item.count = 1;
    }

    public int GetTotalItems()
    {
        int temp = 0;

        for(int a = 0; a < items.Count; a++)
        {
            for(int b = 0; b < items[a].count; b++)
            {
                temp++;
            }
        }

        return temp;
    }
}

