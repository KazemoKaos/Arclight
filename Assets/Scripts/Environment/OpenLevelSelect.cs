using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenLevelSelect : MonoBehaviour
{
    public static Action OpenLevelSelection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OpenLevelSelection?.Invoke();
        }
    }
}
