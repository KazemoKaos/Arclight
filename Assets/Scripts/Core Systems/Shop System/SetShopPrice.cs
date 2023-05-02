using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sets the price of all rarity and returns the given values.
/// </summary>
public class SetShopPrice : MonoBehaviour
{
    ScalingManager difficultyScale;
    [SerializeField] float baseCost = 25f;              // Base cost of common item
    [SerializeField] float multiplier = 1.25f;          // Used in shop multipler calculation.
    float shopMultiplier;                              // Used in chest price calculation
    int commonPrice; //Price of common rarity loot.
    int rarePrice;
    int epicPrice;
    int legendaryPrice;

    public void run()
    {
        difficultyScale = DontDestroy.instance.GetComponentInChildren<ScalingManager>();
        shopMultiplier = Mathf.Pow(difficultyScale.diffScale, multiplier);
        commonPrice = Mathf.RoundToInt((baseCost * shopMultiplier) / 5) * 5;    // Round price to nearest multiple of 5
        //Other prices are based off of the common price. Ex: rare is 50% more than common.
        rarePrice = Mathf.RoundToInt((commonPrice * 1.5f) / 5) * 5;
        epicPrice = commonPrice * 2;
        legendaryPrice = commonPrice * 3;
    }

    public int getPrice(Loot x)
    {
        switch (x.lootRarity)
        {
            case rarity.Common: return commonPrice;
            case rarity.Rare: return rarePrice;
            case rarity.Epic: return epicPrice;
            default: return legendaryPrice;
        }
    }
}
