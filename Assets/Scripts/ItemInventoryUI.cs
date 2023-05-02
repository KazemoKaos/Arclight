using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInventoryUI : MonoBehaviour
{
    class ItemSlots
    {
        public int id = 0;
        public GameObject uiObj;
        public ItemSlotUI slot;

        public ItemSlots(int id, GameObject uiObj)
        {
            this.id = id;
            this.uiObj = uiObj;
        }
    }

    [SerializeField] GameObject itemTemplate;

    List<ItemSlots> items = new List<ItemSlots>();

    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI itemNameText;

    public void AddItem(int id, StackableItems itm)
    {
        items.Add(new ItemSlots(id, Instantiate(itemTemplate, transform)));
        items[items.Count - 1].uiObj.GetComponent<Image>().sprite = itm.itemImage;
        items[items.Count - 1].slot = items[items.Count - 1].uiObj.GetComponent<ItemSlotUI>();
        items[items.Count - 1].slot.infoText = itm.ItemDescription;
        items[items.Count - 1].slot.itemName = itm.ItemName;
    }

    public void IncrementItemCount(int id)
    {
        foreach(ItemSlots item in items)
        {
            if(item.id == id) { item.slot.IncrementItemCount(); }
        }
    }

    public void UpdateInfoText(string text, string itemName)
    {
        infoText.text = text;
        itemNameText.text = itemName;
    }
}
