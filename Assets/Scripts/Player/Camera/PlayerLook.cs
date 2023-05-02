using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform camRot;
    [HideInInspector] public Vector2 aimDir;
    Rigidbody rb;
    float mouseX, mouseY;
    float xRotation;

    public float sensitivity;

    private void Awake()
    {
        // Hide the cursor and don't allow it to move off-screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sensitivity = PlayerPrefs.GetFloat("currentSensitivity", 5);
    }

    private void Update()
    {
        CamMovement();
    }

    void CamMovement()
    {
        // Gets the input values and multiplies them by a sensitvity modifer
        mouseX += aimDir.x * sensitivity * .05f;    // 1 = .05 | 100 = 5
        mouseY = aimDir.y * sensitivity * .05f;

        xRotation -= mouseY;                            // Get the xRotational value
        xRotation = Mathf.Clamp(xRotation, -70f, 70);   // Clamp the xRotation so the player can't look backwards

        camRot.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate the camera in the direction of the input
       
        rb.MoveRotation(Quaternion.Euler(Vector3.up * mouseX));     // Rotate the player in the direction of the input
    }

    /// <summary>
    /// Gets the input values for the mouse/looking
    /// </summary>
    /// <param name="ctx"></param>
    void OnLook(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { aimDir = ctx.ReadValue<Vector2>(); }
        else if(ctx.canceled) { aimDir = Vector2.zero; }
    }

    void UpdateSensitivity(float sens)
    {
        sensitivity = sens;
    }

    private void OnEnable()
    {
        InputManager.look += OnLook;
        SettingsMenu.UpdateSensitivity += UpdateSensitivity;
    }

    private void OnDisable()
    {
        InputManager.look -= OnLook;
        SettingsMenu.UpdateSensitivity -= UpdateSensitivity;
    }
}
