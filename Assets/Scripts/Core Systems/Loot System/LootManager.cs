using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    /// <summary>
    /// This manager contains all of the loot tables. And can determine what gets dropped.
    /// </summary>

    [SerializeField] List<Loot> guaranteeDrops;
    [SerializeField] Loot monyDrop;
    [SerializeField] Loot EXPDrop;
    // List of all common loot
    [SerializeField] List<Loot> commonLoot;
    // List of all rare loot
    [SerializeField] List<Loot> rareLoot;
    // List of all epic loot
    [SerializeField] List<Loot> epicLoot;
    // List of all legendary loot
    [SerializeField] List<Loot> legendaryLoot;

    // The chance for a common to drop
    [SerializeField] float commonChance;
    // The chance for a rare to drop
    [SerializeField] float rareChance;
    // The chance for a epic to drop
    [SerializeField] float epicChance;
    // The chance for a legendary to drop
    [SerializeField] float legendaryChance;

    // How many items to drop
    [SerializeField] int numberOfDrops;

    List<Loot> rewards;

    private void Start() { 
        rewards = new List<Loot>(); 
        foreach(Loot l in guaranteeDrops) { rewards.Add(l); }
    }

    /// <summary>
    /// Gets a loot drop
    /// </summary>
    /// <returns></returns>
    public List<Loot> GetLootDrop() 
    { 
        for (int i = 0; i < numberOfDrops; i++) { rewards.Add(RollReward(RollRarity())); }
        return rewards; 
    }

    public Loot GetMoneyDrop() { return monyDrop; }
    public Loot GetEXPDrop() { return EXPDrop; }

    /// <summary>
    /// Generates the rarity value and returns the rarity tier list of loot
    /// </summary>
    /// <returns></returns>
    List<Loot> RollRarity() 
    { 
        int temp = Random.Range(1, 101);

        if(temp <= legendaryChance && legendaryLoot.Count > 0) { return legendaryLoot; }
        else if(temp <= epicChance && epicLoot.Count > 0) { return epicLoot; }
        else if(temp <= rareChance && rareLoot.Count > 0) { return rareLoot; }
        else { return commonLoot; }
    }

    /// <summary>
    /// Rolls the reward for the selected rarity tier
    /// </summary>
    /// <param name="lootList"></param>
    /// <returns></returns>
    Loot RollReward(List<Loot> lootList) { return lootList[Random.Range(0, lootList.Count)]; }

    /* The original code that was written for loot drops
 
   //public List<Loot> allLootList = new List<Loot>();

   /// <summary>
   /// Returns a loot from the "allLootList" to be dropped from chests.
   /// </summary>
   public Loot getLootFromAllLootList()
   {
       List<Loot> possibleItems = getPossibleItems(allLootList);

       if (possibleItems.Count > 0)
       {
           return possibleItems[Random.Range(0, possibleItems.Count)];
       }
       else
       {
           Debug.Log("There was no loot dropped");
           return null;
       }
   }

   /// <summary>
   /// Returns a list of loot the player is entitled to.
   /// This function can generate a list of possible items from any list of items.
   /// </summary>
   List<Loot> getPossibleItems(List<Loot> lootList)
   {
       int award = rollAward();
       List<Loot> possibleItems = new List<Loot>();

       foreach (Loot item in lootList)
       {
           if (award <= item.dropChance)
           {
               possibleItems.Add(item);
           }
       }
       return possibleItems;
   }

   /// <summary>
   /// Generates a random number.
   /// Example: if player rolls a 20, the player should have a chance to obtain all items with a greater dropChance of 20.
   /// </summary>
   int rollAward()
   {
       int randomNumber = Random.Range(1, 101);
       return randomNumber;
   }
   */
}
