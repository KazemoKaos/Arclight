using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Essentials")]
    Camera mCam;    // The FPS camera
    Rigidbody rb;   // The player's physics
    PlayerStates playerState;
    AudioSource audioSource;
    [SerializeField] List<AudioClip> walkNoise;

    [Header("Movement")]
    public float walkSpeed;             // BASE player walk speed
    public float inAirSpeed;            // BASE player in air speed
    public float movementSpeed;         // TOTAL player speed
    Vector2 inputDirection;             // The movement input vector
    public Vector3 moveDir;             // The player's current movement direction

    [Space(10)]
    public float groundDrag;    // Drag for when player is on the ground
    public float airDrag;       // Drag for when the player is in the air

    bool groundCheck;

    public static event Action<Vector3> movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mCam = Camera.main;
        playerState = GetComponent<PlayerStates>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Movement(); // Move the player based off the player input
        PlayFootstepSounds();

        groundCheck = playerState.isGrounded;
        if (groundCheck && rb.drag != groundDrag) { rb.drag = groundDrag; }
        else if(!groundCheck && rb.drag != airDrag) { rb.drag = airDrag; }
    }

    /// <summary>
    /// Moves the player based on input
    /// </summary>
    void Movement()
    {
        // The player moves based off the input value and direction of the camera
        Vector3 moveX = mCam.transform.right.normalized * inputDirection.x;     // Gets the X direction of movement
        Vector3 moveZ = transform.forward.normalized * inputDirection.y;        // Gets the Z direction of movement
        moveDir = moveX + moveZ;                                                // Create a new Vector3 for the direction of movement

        // This is what moves the player
        if (groundCheck) { rb.AddForce(moveDir * (walkSpeed + movementSpeed) * Time.deltaTime, ForceMode.Impulse); }           // Apply force in the movement direction
        else if (!groundCheck) { rb.AddForce(moveDir * (inAirSpeed + movementSpeed / 2) * Time.deltaTime, ForceMode.Impulse); }

        movement?.Invoke(moveDir);
    }

    /// <summary>
    /// Gets the user input from the input manager
    /// </summary>
    /// <param name="ctx"></param>
    void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            inputDirection = ctx.ReadValue<Vector2>();  // Get values based off the inputs being pressed
        }
        else if (ctx.canceled)
        {
            inputDirection = Vector2.zero;              // The input is now zero, stop moving
            moveDir = Vector3.zero;                     // Zero the player's movement direction since they're not moving
            movement?.Invoke(moveDir);
        }
    }

    /// <summary>
    /// Plays Footstep Sounds. This code is slightly old, so may not be great, but it functions alright-y!
    /// </summary>
    private void PlayFootstepSounds()
    {
        //Check if we're moving on the ground. We don't need footsteps in the air.
        if (groundCheck && rb.velocity.sqrMagnitude > 0.1f)
        {
            //Play it!
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkNoise[UnityEngine.Random.Range(0, walkNoise.Count)]);
            }
        }
    }

    void MovementSpeedMod(float amt) { movementSpeed = amt; }

    private void OnEnable()
    {
        InputManager.movement += OnMove;    // Subscribe the movement functionality to the method "OnMove()"
        PlayerStats.UpdateMoveSpeed += MovementSpeedMod;
    }

    private void OnDisable()
    {
        InputManager.movement -= OnMove; 
        PlayerStats.UpdateMoveSpeed -= MovementSpeedMod;
    }
}
