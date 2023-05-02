using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Warp script for the throwable object that spawns from the Main Ability Warp-Strike.
/// 
/// WARNING: This does NOT work well with mesh colliders for some reason. If you use this to hit a mesh collider object
/// there's a good chance the object will go right through it.
/// </summary>
public class WarpKnife : MonoBehaviour
{
    [SerializeField] float travelTime;
    Transform player;
    Vector3 prevPos;
    Vector3 currentPos;
    Vector3 teleportPos;
    private float damage;

    private void Start()
    {
        Invoke(nameof(SelfDestruct), travelTime); // Destroys the object if it hasn't hit anything.
    }

    private void Update()
    {
        // The previous position is now the position of where it was last frame
        prevPos = transform.position;
    }

    public void SetValues(float abilityDamage, Transform pl) { damage = abilityDamage; player = pl; }

    /// <summary>
    /// Destroys the object
    /// </summary>
    /// <returns></returns>
    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void Hit(Transform hitObj)
    {
        // Deal damage if the colliding object is damagable
        if (hitObj.gameObject.CompareTag("Damagable")) 
        {
            if (hitObj.transform.GetComponent<IDamageable>() != null) { hitObj.transform.GetComponent<IDamageable>().Damage(damage); }
            else if (hitObj.transform.GetComponentInParent<IDamageable>() != null) { hitObj.transform.GetComponentInParent<IDamageable>().Damage(damage); }
        }
        else if (hitObj.gameObject.CompareTag("CritDamagable"))
        {
            if (hitObj.transform.GetComponent<IDamageable>() != null) { hitObj.transform.GetComponent<IDamageable>().Damage(damage); }
            else if (hitObj.transform.GetComponentInParent<IDamageable>() != null) { hitObj.transform.GetComponentInParent<IDamageable>().Damage(damage); }
        }
        player.transform.position = teleportPos;    // Set the player's position to the teleport position
        SelfDestruct();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))  // If the object collides with something other than the player
        {
            teleportPos = prevPos;
            Hit(other.transform);
        }
    }
}
