using System.Collections;
using System;
using UnityEngine;

public class InteractablePortal : MonoBehaviour, IInteractable
{
    //PlayerInteraction currentInteractor; //Use when you need to reference the object/player containing the interface

    public static Action ConfirmMenu;

    //Open Level Selection Menu
    public void StartInteract(PlayerInteraction interactController)
    {
        // Invokes event to open the menu. (Moved functionality to ConfirmationMenu)
        ConfirmMenu?.Invoke();

        //Pause(); //Causes loading issues unless you call UnPause() in every Start function for every Scene
    }

    public void StopInteract()
    {

    }

    void Pause()
    {
        Time.timeScale = 0f;
    }
}