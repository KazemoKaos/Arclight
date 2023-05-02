using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStates : MonoBehaviour
{
    // This needs to communicate with the weapon to get the proper states as well as determine states on it's own.
    // However, when weapons are swapped, the states need to be reset. For example reloading would be false if swapping weapons since the newly swapped weapon is not reloading.


    [Header("Ground Check")]
    [SerializeField] LayerMask groundMask;  // What is "ground"
    CapsuleCollider capsuleCollider;
    public bool isGrounded;                 // If the player is on the ground or not
    public float groundDistance;            // How far is the check distance between the player and the ground

    public bool useAbility;

    bool inAir;

    public static event Action Landed;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, capsuleCollider.bounds.extents.y + groundDistance, groundMask);

        if (!isGrounded) { inAir = true; }
        else if(inAir && isGrounded) { inAir = false; Landed?.Invoke(); }
    }
}
