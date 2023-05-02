using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Logbook : MonoBehaviour
{
    public GameObject itemsObj, enemiesObj;

    public void OpenItems()
    {
        itemsObj.SetActive(true);
        enemiesObj.SetActive(false);
    }

    public void OpenEnemies()
    {
        itemsObj.SetActive(false);
        enemiesObj.SetActive(true);
    }
}
