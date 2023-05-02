using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStatAllocationTexts : MonoBehaviour
{
    //Used to display texts in the Allocation Menu

    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI CDRText;
    [SerializeField] TextMeshProUGUI pointsRemainingText;
    [SerializeField] StatAllocation stats;

    void Start()
    {
        displayAll();
    }

    void Update()
    {
        displayAll();
    }

    void displayAll()
    {
        displayHPText();
        displayArmorText();
        displayCDRText();
        displayPointsRemaining();
    }
    void displayPointsRemaining()
    {
        pointsRemainingText.text = "Points Left: " + stats.pointsLeft.ToString();
    }
    void displayHPText()
    {
        HPText.text = "HP: " + stats.HPStat.ToString();
    }
    void displayArmorText()
    {
        shieldText.text = "Shield: " + stats.shieldStat.ToString();
    }
    void displayCDRText()
    {
        CDRText.text = "CDR: " + stats.CDRStat.ToString();
    }
}
