using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type { Weapon, Item, MISC };
public enum rarity { Common, Rare, Epic, Legendary };

[CreateAssetMenu] 
public class Loot : ScriptableObject
{
    /// <summary>
    /// Each loot should know....
    ///     1. What GameObject it is dropping
    ///     2. Name of the loot
    ///     3. Sprite Image
    ///     4. Cost
    ///     5. Rarity 
    ///         Common = 1
    ///         Rare = 2
    ///         Epic = 3
    ///         Legendary = 4
    ///         Error(Default Value) = 0
    /// </summary>


    public GameObject lootObject;
    public type lootType;
    public string lootName;
    public Sprite lootSprite;
    public int lootCost;
    public rarity lootRarity;
}
