using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount;

    /// <summary>
    /// Calls the player's heal function when walked into
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            other.gameObject.GetComponent<IDamageable>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
