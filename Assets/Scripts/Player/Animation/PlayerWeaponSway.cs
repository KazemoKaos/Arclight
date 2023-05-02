using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSway : MonoBehaviour
{
    [Header("Movement and Look Direction")]
    [SerializeField] PlayerLook aimControls;
    //Vector2 aimControls;

    [Header("Arms Object")]
    [SerializeField] Transform playerTransform;

    [Space(5)]
    [Header("For Weapon Sway")]
    [Tooltip("Aim Sway")]
    [SerializeField] private float smooth;

    [Header("Tilt Sway")]
    public float rotationAmount = 4f;
    private Quaternion InitialRotation;
    public float maxRotationAmount = 5f;

    [Tooltip("Move Sway")]
    public float smoothRotation;
    public bool rotation_X = true;
    public bool rotation_Y = true;
    public bool rotation_Z = true;

    void Start()
    {
        InitialRotation = playerTransform.localRotation;
        aimControls = GetComponentInParent<PlayerLook>();
    }


    void Update()
    {
        Sway();
    }

    /// <summary>
    /// sway for looking around
    /// </summary>
    public void Sway()
    {
        Vector2 moveControls = aimControls.aimDir.normalized; //mouse input

        float tiltY = Mathf.Clamp(-moveControls.x * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(-moveControls.y * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(
        rotation_X ? -tiltX / 2 : 0f,
        rotation_Y ? tiltY : 0f,
        rotation_Z ? tiltY : 0
        ));

        playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation, finalRotation * InitialRotation, Time.deltaTime * smooth);
    }
}
