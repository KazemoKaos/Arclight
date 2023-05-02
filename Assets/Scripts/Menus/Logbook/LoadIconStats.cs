using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadIconStats : MonoBehaviour
{
    public Sprite lockedImg;
    public Sprite iconImgL;
    public string iconNameL;
    public string iconDescL;
    public bool isUnlockedL = false;
    [SerializeField]
    TextMeshProUGUI iconText;
    [SerializeField]
    Image itemImage;

    public GameObject textObj;

    public void Start()
    {
        //Do a bool check to see if it is unlocked or use an event when the item is interacted with for the first time, then save to a PlayerPref
        itemImage.sprite = iconImgL;
    }

    public void DisplayAndLoadData()
    {
        //itemImage.sprite = iconImgL;
        textObj.SetActive(true);
        iconText.text = iconNameL + ": " + iconDescL;
    }
}
