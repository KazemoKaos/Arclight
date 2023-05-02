using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] PlayerWeaponInventory inventory;

    public float returnSpeed;
    public float snappiness;

    private Vector3 targetRotation;
    private Vector3 currentRotation;

    void GenerateRecoil(Vector2 recoil)
    {
        targetRotation += new Vector3(-recoil.x/10, Random.Range(-recoil.y/10, recoil.y/10), 0f);
    }

    private void Update()
    {
        if(targetRotation != Vector3.zero)
        {
            //The target rotation is the recoil after firing which we always lerp to zero
            if(!inventory.equippedWeapon.IsFiring() && inventory.equippedWeapon.IsFullAuto())
            {
                targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            }
            else if(!inventory.equippedWeapon.IsFullAuto()) { targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime); }

            //The current rotation is itself slerped to the target rotation
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

            //Set the rotation of the camera and whatever is held to that recoil direction
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    private void OnEnable()
    {
        AbstractWeapon.attack += GenerateRecoil;
    }

    private void OnDisable()
    {
        AbstractWeapon.attack -= GenerateRecoil;
    }
}
