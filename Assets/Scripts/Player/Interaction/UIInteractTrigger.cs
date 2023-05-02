using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractTrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Damagable"))
        {
            uiElement.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        uiElement.SetActive(false);
    }
}
