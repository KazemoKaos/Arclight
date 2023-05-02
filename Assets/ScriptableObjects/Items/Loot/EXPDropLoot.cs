using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drops the EXP loot object. 
/// </summary>

public class EXPDropLoot : MonoBehaviour
{
    // Start is called before the first frame update
    public int expAmount;
    public static event Action<int> expDrop;

    void Awake()
    {
       //Whenever the AI system is implemented, It should grab the exp amount from there
    }
    void Start()
    {
        expDrop?.Invoke(expAmount);
        Destroy(gameObject); //Delete itself
    }
}
