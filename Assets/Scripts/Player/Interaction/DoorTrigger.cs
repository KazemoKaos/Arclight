using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] public GameObject portal;
    [SerializeField] private GameObject uiElement;
    Animator doorAnimator;

    private void Start()
    {
        doorAnimator = portal.GetComponent<Animator>();    
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Damagable"))
        {
            doorAnimator.SetBool("isDoorOpen", true);
            uiElement.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("isDoorOpen", false);
        uiElement.SetActive(false);
    }
}
