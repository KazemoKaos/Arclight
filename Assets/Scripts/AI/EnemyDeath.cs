using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDeath : MonoBehaviour
{
    EnemyAI controller;

    private void Awake()
    {
        controller = GetComponent<EnemyAI>();
    }


    public void OnDeath()
    {
        List<Loot> droppedLoot;
        Loot moneyDrop;
        Loot EXPDrop;

        moneyDrop = controller.lootDrops.GetMoneyDrop();
        EXPDrop = controller.lootDrops.GetEXPDrop();

        MoneyDropLoot moneyTemp = Instantiate(moneyDrop.lootObject.GetComponent<MoneyDropLoot>(), transform.position, moneyDrop.lootObject.transform.rotation);
        EXPDropLoot EXPTemp = Instantiate(EXPDrop.lootObject.GetComponent<EXPDropLoot>(), transform.position, EXPDrop.lootObject.transform.rotation);
        moneyTemp.moneyAmount = controller.currencyAmount;
        EXPTemp.expAmount = (int)controller.EXPAmount;

        // Spawn the droppable items
        droppedLoot = controller.lootDrops.GetLootDrop();

        foreach (Loot l in droppedLoot)
        {
            if (l.lootObject != null) { Instantiate(l.lootObject, transform.position, l.lootObject.transform.rotation); }
        }

        // Invoke the enemy defeat event
        AbstractEnemy.EnemyDefeat?.Invoke();

        // Invoke the enemy defeat event with the gameobject
        AbstractEnemy.EnemyDefeatDrop?.Invoke(gameObject);

        // Despawn the enemy
        Destroy(gameObject);
    }
}
