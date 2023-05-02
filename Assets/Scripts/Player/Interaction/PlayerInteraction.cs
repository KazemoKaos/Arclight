using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayRange = 3;
    [SerializeField] public PlayerCurrency currency;            // Used to take money from player when purchasing
    [SerializeField] public PlayerWeaponInventory weaponInventory;    // Used to add weapon to player's inventory
    [SerializeField] public ItemInventory itemInventory;              // Used to add items to player's inventory
    [SerializeField] public Camera _mainCam;
    IInteractable currentInteractable;

    //StopInteract may be buggy due to lack of testing
    //Please report any bugs
    void Interact()
    {
        currentInteractable?.StopInteract();
        //Debug.Log("Interaction Stopped");

        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);
        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<IInteractable>() != null)
            {
                currentInteractable = hitObject.GetComponent<IInteractable>();
                currentInteractable.StartInteract(this);
                //Debug.Log("Interacting");
            }
        }
    }

    private void OnEnable()
    {
        InputManager.interact += Interact;
    }

    private void OnDisable()
    {
        InputManager.interact -= Interact;
    }
}