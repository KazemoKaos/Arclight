using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement movement;            // Reference to Movement script to get players direction
    Rigidbody rb;                       // Player's rigidbody
    PlayerStates states;

    public float baseDashDistance;      // The base amount of dash distance
    public float baseDashDistanceAir;   // The base amount of dash distance in the air
    public float dashCooldownTime;      // Cooldown time of the dash
    float timer;                        // Current amount of time left before another dash can be used
    [SerializeField] int maxCharges;    // How many times the player can dodge in succession
    int charges;

    [SerializeField] AudioClip dashSound;
    AudioSource audioSource;

    public static event Action dashAction;

    public static Action<int> UpdateDashChargeUI;
    public static Action<float> UpdateDashCooldownUI;
    public static Action<float> UpdateDashMaxCooldown;
         
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
        states = GetComponent<PlayerStates>();

        audioSource = GetComponent<AudioSource>();

        charges = maxCharges;

        UpdateDashChargeUI?.Invoke(charges);
        UpdateDashMaxCooldown?.Invoke(dashCooldownTime);
    }

    private void Update()
    {
        if(charges < maxCharges)
        {
            if (timer >= dashCooldownTime)
            {
                charges++;
                timer = 0f;
                UpdateDashCooldownUI?.Invoke(timer);
                UpdateDashChargeUI?.Invoke(charges);
            }
            else { timer += Time.deltaTime; UpdateDashCooldownUI?.Invoke(timer); }
        }
    }

    void Dodge()
    {
        if (charges != 0)
        {
            audioSource.PlayOneShot(dashSound);

            if (states.isGrounded)
            {
                if (movement.moveDir == Vector3.zero) { rb.AddForce(transform.forward * baseDashDistance, ForceMode.Impulse); }
                else { rb.AddForce(movement.moveDir * baseDashDistance, ForceMode.Impulse); }
            }
            else
            {
                if (movement.moveDir == Vector3.zero) { rb.AddForce(transform.forward * baseDashDistanceAir, ForceMode.Impulse); }
                else { rb.AddForce(movement.moveDir * baseDashDistanceAir, ForceMode.Impulse); }
            }
            dashAction?.Invoke();
            charges--;
            UpdateDashChargeUI?.Invoke(charges);
        }
    }

    void DodgeChargeMod(int amt) { maxCharges = amt; }
    void DodgeCooldownMod(float amt) { dashCooldownTime += amt; }

    private void OnEnable()
    {
        InputManager.dodge += Dodge;
        PlayerStats.UpdateDashCharges += DodgeChargeMod;
        PlayerStats.UpdateDashCooldown += DodgeCooldownMod;
    }

    private void OnDisable()
    {
        InputManager.dodge -= Dodge;
        PlayerStats.UpdateDashCharges -= DodgeChargeMod;
        PlayerStats.UpdateDashCooldown -= DodgeCooldownMod;
    }
}
