using System;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoTypes ammoType;
    public int ammoAmount;

    public static Action<AmmoTypes, int> AddAmmo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AddAmmo?.Invoke(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
