using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string infoText;
    public string itemName;
    [SerializeField] TextMeshProUGUI itemCountText;
    int count = 1;

    ItemInventoryUI invUI;

    private void Start()
    {
        invUI = GetComponentInParent<ItemInventoryUI>();
    }

    public void IncrementItemCount()
    {
        count++;
        itemCountText.text = count.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        invUI.UpdateInfoText(infoText, itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        invUI.UpdateInfoText(string.Empty, string.Empty);
    }
}
