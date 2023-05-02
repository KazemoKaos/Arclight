using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponPerks")]
public class WeaponPerkInfo : ScriptableObject
{
    public string perkName;
    public string perkDescription;
    public GameObject perkPrefab;
}
