using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    bool status;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject hitmarker;

    [SerializeField] float hitTime;
    float timer;

    void ActiveCrosshair(float aimSpeed)
    {
        status = !status;               // Set the crosshair to opposite of what it is
        crosshair.SetActive(!status);   // Since status is default (false) we want to treat it as the opposite so false is true and vice versa
    }

    void ActiveHitMarker()
    {
        if (!hitmarker.activeSelf) { hitmarker.SetActive(true); }
        timer = 0f;
    }

    private void Update()
    {
        if (hitmarker.activeSelf)
        {
            if (timer >= hitTime) { hitmarker.SetActive(false); timer = 0f; }
            else { timer += Time.deltaTime; }
        }
    }

    private void OnEnable()
    {
        AbstractWeapon.aim += ActiveCrosshair;
        IDamageable.Hit += ActiveHitMarker;
    }

    private void OnDisable()
    {
        AbstractWeapon.aim -= ActiveCrosshair;
        IDamageable.Hit -= ActiveHitMarker;
    }
}
