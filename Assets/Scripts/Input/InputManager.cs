using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    static PlayerInputActions actions;

    //Player Controls
    public static Action<InputAction.CallbackContext> movement;
    public static Action<InputAction.CallbackContext> look;
    public static Action<InputAction.CallbackContext> primaryAction;
    public static Action<InputAction.CallbackContext> secondaryAction;
    public static Action<InputAction.CallbackContext> inventory;
    public static Action<float> switchSelection;
    public static Action jump;
    public static Action melee;
    public static Action grenade;
    public static Action ability;
    public static Action dodge;
    public static Action reload;
    public static Action interact;
    public static Action weaponOne;
    public static Action weaponTwo;
    public static Action weaponThree;

    //Functional Controls
    public static Action escape;

    // Testing Controls
    public static Action onSpawnEnemy;

    // Check if controls are disabled
    static bool controlsActive;

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    /// <summary>
    /// Disables the player's in-game functions (walk, jump, attack, etc.)
    /// 
    /// We would want to do this if the player opens up a menu or in-game event/cutscene.
    /// </summary>
    public static void DisableInput()
    {
        controlsActive = false;
        actions.KeyboardAndMouse.Disable();
    }

    /// <summary>
    /// Enables the player's in-game functions (walk, jump, attack, etc.)
    /// </summary>
    public static void EnableInput()
    {
        actions.KeyboardAndMouse.Enable();
        controlsActive = true;
    }

    public static void EnterInventory()
    {
        actions.KeyboardAndMouse.Look.Disable();
        actions.KeyboardAndMouse.PrimaryAction.Disable();
        actions.KeyboardAndMouse.SecondaryAction.Disable();
        actions.KeyboardAndMouse.Interact.Disable();
    }

    public static void ExitInventory()
    {
        if (controlsActive)
        {
            actions.KeyboardAndMouse.Look.Enable();
            actions.KeyboardAndMouse.PrimaryAction.Enable();
            actions.KeyboardAndMouse.SecondaryAction.Enable();
            actions.KeyboardAndMouse.Interact.Enable();
        }
    }

    public void OnEnable()
    {
        //Player Controls
        actions.KeyboardAndMouse.Movement.performed += InvokeMovement;
        actions.KeyboardAndMouse.Movement.canceled += InvokeMovement;
        actions.KeyboardAndMouse.Look.performed += InvokeLook;
        actions.KeyboardAndMouse.Look.canceled += InvokeLook;
        actions.KeyboardAndMouse.PrimaryAction.performed += InvokePrimaryAction;
        actions.KeyboardAndMouse.PrimaryAction.canceled += InvokePrimaryAction;
        actions.KeyboardAndMouse.SecondaryAction.performed += InvokeSecondaryAction;
        actions.KeyboardAndMouse.SecondaryAction.canceled += InvokeSecondaryAction;
        actions.KeyboardAndMouse.Inventory.performed += InvokeInventory;
        actions.KeyboardAndMouse.Inventory.canceled += InvokeInventory;
        actions.KeyboardAndMouse.SwitchSelection.performed += InvokeSwitchSelection;
        actions.KeyboardAndMouse.Jump.performed += InvokeJump;
        actions.KeyboardAndMouse.Melee.performed += InvokeMelee;
        actions.KeyboardAndMouse.Grenade.performed += InvokeGrenade;
        actions.KeyboardAndMouse.Ability.performed += InvokeAbility;
        actions.KeyboardAndMouse.Dodge.performed += InvokeDodge;
        actions.KeyboardAndMouse.Reload.performed += InvokeReload;
        actions.KeyboardAndMouse.Interact.performed += InvokeInteract;
        actions.KeyboardAndMouse.WeaponOne.performed += InvokeWeaponOne;
        actions.KeyboardAndMouse.WeaponTwo.performed += InvokeWeaponTwo;
        actions.KeyboardAndMouse.WeaponThree.performed += InvokeWeaponThree;

        actions.KeyboardAndMouse.SpawnEnemy.performed += InvokeRespawnAgent;

        //Functional Controls
        actions.FunctionalControls.Escape.performed += InvokeEscape;

        //Enable
        actions.KeyboardAndMouse.Enable();
        actions.FunctionalControls.Enable();

        controlsActive = true;
    }

    public void OnDisable()
    {
        //Player Controls
        actions.KeyboardAndMouse.Movement.performed -= InvokeMovement;
        actions.KeyboardAndMouse.Movement.canceled -= InvokeMovement;
        actions.KeyboardAndMouse.Look.performed -= InvokeLook;
        actions.KeyboardAndMouse.Look.canceled -= InvokeLook;
        actions.KeyboardAndMouse.PrimaryAction.performed -= InvokePrimaryAction;
        actions.KeyboardAndMouse.PrimaryAction.canceled -= InvokePrimaryAction;
        actions.KeyboardAndMouse.SecondaryAction.performed -= InvokeSecondaryAction;
        actions.KeyboardAndMouse.SecondaryAction.canceled -= InvokeSecondaryAction;
        actions.KeyboardAndMouse.Inventory.performed -= InvokeInventory;
        actions.KeyboardAndMouse.Inventory.canceled -= InvokeInventory;
        actions.KeyboardAndMouse.SwitchSelection.performed -= InvokeSwitchSelection;
        actions.KeyboardAndMouse.Jump.performed -= InvokeJump;
        actions.KeyboardAndMouse.Melee.performed -= InvokeMelee;
        actions.KeyboardAndMouse.Grenade.performed -= InvokeGrenade;
        actions.KeyboardAndMouse.Ability.performed -= InvokeAbility;
        actions.KeyboardAndMouse.Dodge.performed -= InvokeDodge;
        actions.KeyboardAndMouse.Reload.performed -= InvokeReload;
        actions.KeyboardAndMouse.Interact.performed -= InvokeInteract;
        actions.KeyboardAndMouse.WeaponOne.performed -= InvokeWeaponOne;
        actions.KeyboardAndMouse.WeaponTwo.performed -= InvokeWeaponTwo;
        actions.KeyboardAndMouse.WeaponThree.performed -= InvokeWeaponThree;

        actions.KeyboardAndMouse.SpawnEnemy.performed -= InvokeRespawnAgent;

        //Functional Controls
        actions.FunctionalControls.Escape.performed -= InvokeEscape;

        //Disable
        actions.KeyboardAndMouse.Disable();
        actions.FunctionalControls.Disable();

        controlsActive = false;
    }

    //========================================================
    //ACTIONS=================================================
    //========================================================

    void InvokeMovement(InputAction.CallbackContext ctx)
    {
        movement?.Invoke(ctx);
    }

    void InvokeLook(InputAction.CallbackContext ctx)
    {
        look?.Invoke(ctx);
    }

    void InvokePrimaryAction(InputAction.CallbackContext ctx)
    {
        primaryAction?.Invoke(ctx);
    }

    void InvokeSecondaryAction(InputAction.CallbackContext ctx)
    {
        secondaryAction?.Invoke(ctx);
    }

    void InvokeSwitchSelection(InputAction.CallbackContext ctx)
    {
        switchSelection?.Invoke(ctx.ReadValue<float>());
    }

    void InvokeInventory(InputAction.CallbackContext ctx)
    {
        inventory?.Invoke(ctx);
    }

    void InvokeJump(InputAction.CallbackContext ctx)
    {
        jump?.Invoke();
    }

    void InvokeMelee(InputAction.CallbackContext ctx)
    {
        melee?.Invoke();
    }

    void InvokeGrenade(InputAction.CallbackContext ctx)
    {
        grenade?.Invoke();
    }

    void InvokeAbility(InputAction.CallbackContext ctx)
    {
        ability?.Invoke();
    }

    void InvokeDodge(InputAction.CallbackContext ctx)
    {
        dodge?.Invoke();
    }

    void InvokeReload(InputAction.CallbackContext ctx)
    {
        reload?.Invoke();
    }

    void InvokeInteract(InputAction.CallbackContext ctx)
    {
        interact?.Invoke();
    }

    void InvokeWeaponOne(InputAction.CallbackContext ctx)
    {
        weaponOne?.Invoke();
    }

    void InvokeWeaponTwo(InputAction.CallbackContext ctx)
    {
        weaponTwo?.Invoke();
    }

    void InvokeWeaponThree(InputAction.CallbackContext ctx)
    {
        weaponThree?.Invoke();
    }

    void InvokeEscape(InputAction.CallbackContext ctx)
    {
        escape?.Invoke();
    }

    void InvokeRespawnAgent(InputAction.CallbackContext ctx)
    {
        onSpawnEnemy?.Invoke();
    }
}
