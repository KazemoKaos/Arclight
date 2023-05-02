using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stackable Objects")]
public class StackableItems : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public GameObject itemObj;
    public int itemID;
    public Sprite itemImage;
}
