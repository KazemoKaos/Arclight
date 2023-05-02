using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyAmount : MonoBehaviour
{
    public TextMeshProUGUI currText;
    private static int coinCount;

    void updateCurrency(int x) { coinCount = x; currText.text = coinCount.ToString();  }

    private void OnEnable()
    {
        PlayerCurrency.UpdateCurrencyUI += updateCurrency;
    }

    private void OnDisable()
    {
        PlayerCurrency.UpdateCurrencyUI -= updateCurrency;
    }
}
