//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""id"": ""0a98cdd5-a4d6-4357-b213-89ceeea2211b"",
            ""actions"": [
                {
                    ""name"": ""SpawnEnemy"",
                    ""type"": ""Button"",
                    ""id"": ""2b379730-5acc-44a8-9fdf-d13ad4081d66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""8d6bba9a-4550-4bf8-bd49-4b69bd415927"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9eb7396b-53ff-4afc-a395-c84c28240ee8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryAction"",
                    ""type"": ""Button"",
                    ""id"": ""07c51028-0629-4516-90f5-1c960591c9a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryAction"",
                    ""type"": ""Button"",
                    ""id"": ""557b009e-02de-42b0-988f-28cfcd5b9adf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""2dbd3fe6-7728-4264-8cf5-0756b1cb1691"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grenade"",
                    ""type"": ""Button"",
                    ""id"": ""d36a4f4f-07f1-4e0e-8ae2-ba5f97584a43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability"",
                    ""type"": ""Button"",
                    ""id"": ""e99bf043-6778-4a8b-99c7-cd28159d4628"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d4eb3e33-5e21-4faf-8643-c6d8f05aa9d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""d3e870f7-5170-43bb-b44a-e7fa11696922"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchSelection"",
                    ""type"": ""Value"",
                    ""id"": ""6010f5f2-d239-4190-b16d-70f183f703f7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e8c669d6-dd17-49e9-ba30-519446baacc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""71d5877f-1fba-418a-9f0a-51b73cd36478"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponOne"",
                    ""type"": ""Button"",
                    ""id"": ""716f9271-553e-46f8-ab2e-25bdb263831a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponTwo"",
                    ""type"": ""Button"",
                    ""id"": ""739c530f-5021-4510-99c2-9dba861d0eba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponThree"",
                    ""type"": ""Button"",
                    ""id"": ""f8bb7f6d-550f-4b38-8b9e-9bc4689d5221"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""189430a1-f022-496b-a65f-d39289128fa0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1ea55bf8-7f87-4b72-a296-7853a9f971f8"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d8907c9e-77f1-4c20-b343-0be0bcb430b8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0de81104-dc90-4b78-8beb-a45102a69027"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b493907e-673b-44fa-a538-9551eb70427d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0e8f75b-23ab-484c-b06a-d0d98b069381"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8024c521-6e97-4f18-a094-2a5710f73a4d"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8a7c5b5-f248-4828-b68c-4fd28a61cd56"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""PrimaryAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7f51f81-3ce6-48b0-9942-aadc8075f9e7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""SecondaryAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13dd88e7-eea9-44b8-96ee-1597efbfd35c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""532408c0-b49c-4681-a1a7-123038f5f040"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Grenade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7bf4e44-448a-4a8a-a283-36e52c90c8d4"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e94f2a07-1138-433d-ab92-bad9e897a15b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d2e22a2-c4c6-439a-9a54-1237d760c4a7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7391556-93ed-44dc-8f48-6aae6ad9383b"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""SwitchSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a88f127-175e-45d5-9e95-7b64102bdf76"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f96c1c58-fa08-47cd-be54-30fded4713de"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba3dff1e-729d-4f89-8e35-0e16bcdd7134"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""SpawnEnemy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1846135-5ed1-40be-9c25-13ea6decbdc1"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""WeaponOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26d83a95-ef74-489c-8615-19485e33d4b8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""WeaponTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53b8672c-e50d-40f9-a345-f4266da116f9"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""WeaponThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cacda20c-09f2-4c68-9009-f775ed62c572"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""FunctionalControls"",
            ""id"": ""cdd5eea9-4915-41c1-9ada-b1178bd96ee6"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""4a367fa3-a191-42d0-9ee3-a9aa94462357"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""29fb2d5a-6495-48ff-9e98-0f1bdac4ede8"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse&Keyboard"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse&Keyboard"",
            ""bindingGroup"": ""Mouse&Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // KeyboardAndMouse
        m_KeyboardAndMouse = asset.FindActionMap("KeyboardAndMouse", throwIfNotFound: true);
        m_KeyboardAndMouse_SpawnEnemy = m_KeyboardAndMouse.FindAction("SpawnEnemy", throwIfNotFound: true);
        m_KeyboardAndMouse_Movement = m_KeyboardAndMouse.FindAction("Movement", throwIfNotFound: true);
        m_KeyboardAndMouse_Look = m_KeyboardAndMouse.FindAction("Look", throwIfNotFound: true);
        m_KeyboardAndMouse_PrimaryAction = m_KeyboardAndMouse.FindAction("PrimaryAction", throwIfNotFound: true);
        m_KeyboardAndMouse_SecondaryAction = m_KeyboardAndMouse.FindAction("SecondaryAction", throwIfNotFound: true);
        m_KeyboardAndMouse_Melee = m_KeyboardAndMouse.FindAction("Melee", throwIfNotFound: true);
        m_KeyboardAndMouse_Grenade = m_KeyboardAndMouse.FindAction("Grenade", throwIfNotFound: true);
        m_KeyboardAndMouse_Ability = m_KeyboardAndMouse.FindAction("Ability", throwIfNotFound: true);
        m_KeyboardAndMouse_Jump = m_KeyboardAndMouse.FindAction("Jump", throwIfNotFound: true);
        m_KeyboardAndMouse_Dodge = m_KeyboardAndMouse.FindAction("Dodge", throwIfNotFound: true);
        m_KeyboardAndMouse_SwitchSelection = m_KeyboardAndMouse.FindAction("SwitchSelection", throwIfNotFound: true);
        m_KeyboardAndMouse_Interact = m_KeyboardAndMouse.FindAction("Interact", throwIfNotFound: true);
        m_KeyboardAndMouse_Reload = m_KeyboardAndMouse.FindAction("Reload", throwIfNotFound: true);
        m_KeyboardAndMouse_WeaponOne = m_KeyboardAndMouse.FindAction("WeaponOne", throwIfNotFound: true);
        m_KeyboardAndMouse_WeaponTwo = m_KeyboardAndMouse.FindAction("WeaponTwo", throwIfNotFound: true);
        m_KeyboardAndMouse_WeaponThree = m_KeyboardAndMouse.FindAction("WeaponThree", throwIfNotFound: true);
        m_KeyboardAndMouse_Inventory = m_KeyboardAndMouse.FindAction("Inventory", throwIfNotFound: true);
        // FunctionalControls
        m_FunctionalControls = asset.FindActionMap("FunctionalControls", throwIfNotFound: true);
        m_FunctionalControls_Escape = m_FunctionalControls.FindAction("Escape", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // KeyboardAndMouse
    private readonly InputActionMap m_KeyboardAndMouse;
    private IKeyboardAndMouseActions m_KeyboardAndMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardAndMouse_SpawnEnemy;
    private readonly InputAction m_KeyboardAndMouse_Movement;
    private readonly InputAction m_KeyboardAndMouse_Look;
    private readonly InputAction m_KeyboardAndMouse_PrimaryAction;
    private readonly InputAction m_KeyboardAndMouse_SecondaryAction;
    private readonly InputAction m_KeyboardAndMouse_Melee;
    private readonly InputAction m_KeyboardAndMouse_Grenade;
    private readonly InputAction m_KeyboardAndMouse_Ability;
    private readonly InputAction m_KeyboardAndMouse_Jump;
    private readonly InputAction m_KeyboardAndMouse_Dodge;
    private readonly InputAction m_KeyboardAndMouse_SwitchSelection;
    private readonly InputAction m_KeyboardAndMouse_Interact;
    private readonly InputAction m_KeyboardAndMouse_Reload;
    private readonly InputAction m_KeyboardAndMouse_WeaponOne;
    private readonly InputAction m_KeyboardAndMouse_WeaponTwo;
    private readonly InputAction m_KeyboardAndMouse_WeaponThree;
    private readonly InputAction m_KeyboardAndMouse_Inventory;
    public struct KeyboardAndMouseActions
    {
        private @PlayerInputActions m_Wrapper;
        public KeyboardAndMouseActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpawnEnemy => m_Wrapper.m_KeyboardAndMouse_SpawnEnemy;
        public InputAction @Movement => m_Wrapper.m_KeyboardAndMouse_Movement;
        public InputAction @Look => m_Wrapper.m_KeyboardAndMouse_Look;
        public InputAction @PrimaryAction => m_Wrapper.m_KeyboardAndMouse_PrimaryAction;
        public InputAction @SecondaryAction => m_Wrapper.m_KeyboardAndMouse_SecondaryAction;
        public InputAction @Melee => m_Wrapper.m_KeyboardAndMouse_Melee;
        public InputAction @Grenade => m_Wrapper.m_KeyboardAndMouse_Grenade;
        public InputAction @Ability => m_Wrapper.m_KeyboardAndMouse_Ability;
        public InputAction @Jump => m_Wrapper.m_KeyboardAndMouse_Jump;
        public InputAction @Dodge => m_Wrapper.m_KeyboardAndMouse_Dodge;
        public InputAction @SwitchSelection => m_Wrapper.m_KeyboardAndMouse_SwitchSelection;
        public InputAction @Interact => m_Wrapper.m_KeyboardAndMouse_Interact;
        public InputAction @Reload => m_Wrapper.m_KeyboardAndMouse_Reload;
        public InputAction @WeaponOne => m_Wrapper.m_KeyboardAndMouse_WeaponOne;
        public InputAction @WeaponTwo => m_Wrapper.m_KeyboardAndMouse_WeaponTwo;
        public InputAction @WeaponThree => m_Wrapper.m_KeyboardAndMouse_WeaponThree;
        public InputAction @Inventory => m_Wrapper.m_KeyboardAndMouse_Inventory;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardAndMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardAndMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardAndMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface != null)
            {
                @SpawnEnemy.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSpawnEnemy;
                @SpawnEnemy.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSpawnEnemy;
                @SpawnEnemy.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSpawnEnemy;
                @Movement.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnLook;
                @PrimaryAction.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPrimaryAction;
                @PrimaryAction.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPrimaryAction;
                @PrimaryAction.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnPrimaryAction;
                @SecondaryAction.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSecondaryAction;
                @SecondaryAction.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSecondaryAction;
                @SecondaryAction.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSecondaryAction;
                @Melee.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnMelee;
                @Grenade.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnGrenade;
                @Grenade.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnGrenade;
                @Grenade.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnGrenade;
                @Ability.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnAbility;
                @Ability.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnAbility;
                @Ability.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnAbility;
                @Jump.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnJump;
                @Dodge.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnDodge;
                @SwitchSelection.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSwitchSelection;
                @SwitchSelection.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSwitchSelection;
                @SwitchSelection.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnSwitchSelection;
                @Interact.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInteract;
                @Reload.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnReload;
                @WeaponOne.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponOne;
                @WeaponOne.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponOne;
                @WeaponOne.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponOne;
                @WeaponTwo.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponTwo;
                @WeaponTwo.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponTwo;
                @WeaponTwo.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponTwo;
                @WeaponThree.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponThree;
                @WeaponThree.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponThree;
                @WeaponThree.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnWeaponThree;
                @Inventory.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnInventory;
            }
            m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpawnEnemy.started += instance.OnSpawnEnemy;
                @SpawnEnemy.performed += instance.OnSpawnEnemy;
                @SpawnEnemy.canceled += instance.OnSpawnEnemy;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @PrimaryAction.started += instance.OnPrimaryAction;
                @PrimaryAction.performed += instance.OnPrimaryAction;
                @PrimaryAction.canceled += instance.OnPrimaryAction;
                @SecondaryAction.started += instance.OnSecondaryAction;
                @SecondaryAction.performed += instance.OnSecondaryAction;
                @SecondaryAction.canceled += instance.OnSecondaryAction;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Grenade.started += instance.OnGrenade;
                @Grenade.performed += instance.OnGrenade;
                @Grenade.canceled += instance.OnGrenade;
                @Ability.started += instance.OnAbility;
                @Ability.performed += instance.OnAbility;
                @Ability.canceled += instance.OnAbility;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @SwitchSelection.started += instance.OnSwitchSelection;
                @SwitchSelection.performed += instance.OnSwitchSelection;
                @SwitchSelection.canceled += instance.OnSwitchSelection;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @WeaponOne.started += instance.OnWeaponOne;
                @WeaponOne.performed += instance.OnWeaponOne;
                @WeaponOne.canceled += instance.OnWeaponOne;
                @WeaponTwo.started += instance.OnWeaponTwo;
                @WeaponTwo.performed += instance.OnWeaponTwo;
                @WeaponTwo.canceled += instance.OnWeaponTwo;
                @WeaponThree.started += instance.OnWeaponThree;
                @WeaponThree.performed += instance.OnWeaponThree;
                @WeaponThree.canceled += instance.OnWeaponThree;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
            }
        }
    }
    public KeyboardAndMouseActions @KeyboardAndMouse => new KeyboardAndMouseActions(this);

    // FunctionalControls
    private readonly InputActionMap m_FunctionalControls;
    private IFunctionalControlsActions m_FunctionalControlsActionsCallbackInterface;
    private readonly InputAction m_FunctionalControls_Escape;
    public struct FunctionalControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public FunctionalControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_FunctionalControls_Escape;
        public InputActionMap Get() { return m_Wrapper.m_FunctionalControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FunctionalControlsActions set) { return set.Get(); }
        public void SetCallbacks(IFunctionalControlsActions instance)
        {
            if (m_Wrapper.m_FunctionalControlsActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_FunctionalControlsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_FunctionalControlsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_FunctionalControlsActionsCallbackInterface.OnEscape;
            }
            m_Wrapper.m_FunctionalControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
            }
        }
    }
    public FunctionalControlsActions @FunctionalControls => new FunctionalControlsActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse&Keyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    public interface IKeyboardAndMouseActions
    {
        void OnSpawnEnemy(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnPrimaryAction(InputAction.CallbackContext context);
        void OnSecondaryAction(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnGrenade(InputAction.CallbackContext context);
        void OnAbility(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnSwitchSelection(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnWeaponOne(InputAction.CallbackContext context);
        void OnWeaponTwo(InputAction.CallbackContext context);
        void OnWeaponThree(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
    }
    public interface IFunctionalControlsActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
}
