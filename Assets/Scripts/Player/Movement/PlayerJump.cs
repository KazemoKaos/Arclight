using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;               // Replace this later with Stats Script
    public float baseJumpHeight;    // Replace this later with Stats Script
    public float jumpHeight;
    public float maxJumpCharges;
    float jumpCharges;
    float jumpCooldown = .2f;
    bool jump;
    PlayerStates playerState;

    AudioSource audioSource;
    [SerializeField] AudioClip jumpNoise;

    public static event Action jumpAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Gets the player's rigidbody
        playerState = GetComponent<PlayerStates>();
        audioSource = GetComponent<AudioSource>();

        jumpCharges = maxJumpCharges;
    }

    private void FixedUpdate()
    {
        if(jump && jumpCharges < maxJumpCharges && playerState.isGrounded) { jumpCharges = maxJumpCharges; }
    }

    /// <summary>
    /// Allows the player to jump
    /// </summary>
    void JumpAction()
    {
        // Check with stats script to see if the player is able to jump
        if (playerState.isGrounded && jumpCharges != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce((baseJumpHeight + jumpHeight) * Vector3.up, ForceMode.Impulse);    // Allows the player to jump up
            jumpCharges--;
            jumpAction?.Invoke();
            jump = false;
            StartCoroutine(nameof(JumpCooldown));
            audioSource.PlayOneShot(jumpNoise);
        }
        else if(!playerState.isGrounded && jumpCharges != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce((baseJumpHeight + jumpHeight) * Vector3.up, ForceMode.Impulse);    // Allows the player to jump up
            jumpCharges--;
            jumpAction?.Invoke();
            audioSource.PlayOneShot(jumpNoise);
        }
    }

    /// <summary>
    /// Small internal cooldown because player is registered back on ground faster than they can leave it
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        jump = true;
    }

    void JumpChargeMod(int amt) { maxJumpCharges = amt; }

    /// <summary>
    /// Enables the button mapping for Jump
    /// </summary>
    private void OnEnable()
    {
        InputManager.jump += JumpAction;
        PlayerStats.UpdateJumpCharges += JumpChargeMod;
    }

    /// <summary>
    /// Disables the button mapping for Jump
    /// </summary>
    private void OnDisable()
    {
        InputManager.jump -= JumpAction;
        PlayerStats.UpdateJumpCharges -= JumpChargeMod;
    }
}
