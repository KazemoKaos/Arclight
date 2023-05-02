using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableWeapon : MonoBehaviour, IInteractable
{
    public bool interactable = true;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] GameObject rarityEffect;
    [SerializeField] GameObject weaponStatsUI;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void StartInteract(PlayerInteraction interactController)
    {
        if (interactable)
        {
            gameObject.SetActive(false);

            // Add the weapon to the player's inventory
            interactController.weaponInventory.AddWeapon(gameObject);

            // Disable the collisions of the weapon
            boxCollider.enabled = false;

            // Disable gravity on the weapon (rigidbody)
            Destroy(gameObject.GetComponent<Rigidbody>());

            // Disable the weapon rarity particle effect
            rarityEffect.SetActive(false);

            // Disable the weapon stats UI
            weaponStatsUI.SetActive(false);

            // Don't let the weapon be interactable while held
            interactable = false;

            // Set the layer of the weapon to be the "Player" layer so it gets rendered properly
            foreach (Transform obj in transform.GetComponentInChildren<Transform>())
            {
                obj.gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

    public void StopInteract()
    {
        // Do nothing
    }

    public void MakeInteractable()
    {
        // Disable the weapon
        gameObject.GetComponent<AbstractWeapon>().enabled = false;

        // Put the weapon back in the Item layer
        foreach (Transform obj in transform.GetComponentInChildren<Transform>())
        {
            obj.gameObject.layer = LayerMask.NameToLayer("Item");
        }

        // Re-enable the collider
        boxCollider.enabled = true;

        // Re-add the rigidbody and initialize it
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();    // Create a rigidbody
        rb.rotation = Quaternion.Euler(Vector3.zero);           // Set the weapon rotation to zero
        rb.freezeRotation = true;                               // Freeze the rotation so it can't fall sideways
        rb.AddForce(transform.forward * 4f, ForceMode.Impulse); // Throw weapon forward a little bit

        // Re-enable rarity particle effect
        rarityEffect.SetActive(true);

        // Re-enable weapon stats UI
        weaponStatsUI.SetActive(true);

        interactable = true;

        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}
