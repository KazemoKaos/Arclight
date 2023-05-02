using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ShopTransaction : MonoBehaviour
{
    Shop store;
    int item = -1; //Selected item from shop menu
    int itemCost; //Cost of selected item

    public static event Action<int> transaction;
    private void Start()
    {
        store = GetComponent<Shop>();
    }

    public void purchase()
    {
        item = store.selectedItem;

        if (item >= 0) //Item is selected
        {
            itemCost = store.ssp.getPrice(store.shopItems[item]);
            if (store.playerCurrency >= itemCost) { doBusiness(); }
            else { store.uPoorDescription(); }
        }
        else {store.nothingSelectedDescription();}
    }

    void doBusiness()
    {
        transaction?.Invoke(itemCost); //Buy the item.
        store.successfulTransaction();
    }
}
