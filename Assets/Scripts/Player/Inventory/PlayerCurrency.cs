using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCurrency : MonoBehaviour
{
    int currency = 0; // The player currency
    int totalCurrency = 0; // For tracking all money collected

    public static event Action<int> UpdateCurrencyUI;
    public static event Action<int> MoneySpent; //Use to track how much was spent
    public void addCurrency(int x) 
    {
        //Player obtaining currency
        currency = currency + x;
        totalCurrency += x;
        UpdateCurrencyUI?.Invoke(currency);
    }

    public void removeCurrency(int x)  
    {
        // Player losing currency. No such thing as debt.
        MoneySpent?.Invoke(x);
        if (currency > x)
        {
            currency = currency - x;
        }
        else
        {
            currency = 0;
        }
        UpdateCurrencyUI?.Invoke(currency);
    }

    void clearCurrency()
    {
        currency = 0;
        UpdateCurrencyUI?.Invoke(currency);
    }

    public int getCurrency()
    {
        return currency;
    }

    public int GetTotalCurrency() { return totalCurrency; }

    private void OnEnable()
    {
        MoneyDropLoot.moneyDrop += addCurrency;
        ShopTransaction.transaction += removeCurrency;
        SceneLoader.LoadNextLevel += clearCurrency;
    }

    private void OnDisable()
    {
        MoneyDropLoot.moneyDrop -= addCurrency;
        ShopTransaction.transaction -= removeCurrency;
        SceneLoader.LoadNextLevel -= clearCurrency;
    }
}
